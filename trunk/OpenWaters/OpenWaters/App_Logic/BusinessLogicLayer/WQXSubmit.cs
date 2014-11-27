using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.net.epacdxnode.test;
using System.Threading;
//using OpenEnvironment.WQXNew;

namespace OpenEnvironment.App_Logic.BusinessLogicLayer
{
    public static class WQXSubmit
    {
        /// <summary>
        /// Calls an authenticate method
        /// </summary>
        /// <returns>Security token</returns>
        internal static string AuthHelper(string userID, string credential, string authMethod, string domain, string NodeURL)
        {
            NetworkNode2 nn = new NetworkNode2();
            nn.Url = NodeURL;
            Authenticate auth1 = new Authenticate();
            auth1.userId = userID;
            auth1.credential = credential;
            auth1.authenticationMethod = authMethod;
            auth1.domain = domain;
            try
            {
                AuthenticateResponse resp = nn.Authenticate(auth1);
                return resp.securityToken;
            }
            catch (SoapException sExept)
            {
                return sExept.Message;
            }
        }

        internal static StatusResponseType SubmitHelper(string NodeURL, string secToken, string dataFlow, string flowOperation, byte[] doc, string docName, DocumentFormatType docFormat, string docID)
        {
            try
            {
                AttachmentType att1 = new AttachmentType();
                att1.Value = doc;
                NodeDocumentType doc1 = new NodeDocumentType();
                doc1.documentName = docName;
                doc1.documentFormat = docFormat;
                doc1.documentId = docID;
                doc1.documentContent = att1;
                NodeDocumentType[] docArray = new NodeDocumentType[1];
                docArray[0] = doc1;
                Submit sub1 = new Submit();
                sub1.securityToken = secToken;
                sub1.transactionId = "";
                sub1.dataflow = dataFlow;
                sub1.flowOperation = flowOperation;
                sub1.documents = docArray;
                NetworkNode2 nn = new NetworkNode2();
                nn.SoapVersion = SoapProtocolVersion.Soap12;
                nn.Url = NodeURL;
                return nn.Submit(sub1);
            }
            catch
            {
                return null;
            }
        }

        internal static StatusResponseType GetStatusHelper(string NodeURL, string secToken, string transID)
        {
            try
            {
                NetworkNode2 nn = new NetworkNode2();
                nn.Url = NodeURL;
                GetStatus gs1 = new GetStatus();
                gs1.securityToken = secToken;
                gs1.transactionId = transID;
                return nn.GetStatus(gs1);
            }
            catch
            {
                return null;
            }
        }

        internal static NodeDocumentType[] DownloadHelper(string NodeURL, string secToken, string dataFlow, string transID)
        {
            try
            {
                NetworkNode2 nn = new NetworkNode2();
                nn.Url = NodeURL;
                Download dl1 = new Download();
                dl1.securityToken = secToken;
                dl1.dataflow = dataFlow;
                dl1.transactionId = transID;
                return nn.Download(dl1);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Scans database for all pending data for the selected organization and submits pending data to EPA one record at a time
        /// </summary>
        public static void WQX_Master(string OrgID)
        {
            T_OE_APP_TASKS t = db_Ref.GetT_OE_APP_TASKS_ByName("WQXSubmit");
            if (t.TASK_STATUS == "STOPPED")
            {
                //set status to RUNNING so tasks won't execute simultaneously
                db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "RUNNING", null, "SYSTEM");

                //get CDX username, password, and CDX destination URL
                string userID;
                string credential;
                string NodeURL;
                GetCDXSubmitCredentials(OrgID, out userID, out credential, out NodeURL);

                //Loop through all pending monitoring locations and submit one at a time
                List<T_WQX_MONLOC> ms = db_WQX.GetWQX_MONLOC(true, OrgID, true);
                foreach (T_WQX_MONLOC m in ms)
                    WQX_Submit_OneByOne("MLOC", m.MONLOC_IDX, userID, credential, NodeURL);

                //Loop through all pending projects and submit one at a time
                List<T_WQX_PROJECT> ps = db_WQX.GetWQX_PROJECT(true, OrgID, true);
                foreach (T_WQX_PROJECT p in ps)
                    WQX_Submit_OneByOne("PROJ", p.PROJECT_IDX, userID, credential, NodeURL);

                //Loop through all pending activities and submit one at a time
                List<T_WQX_ACTIVITY> as1 = db_WQX.GetWQX_ACTIVITY(true, OrgID, null, null, null, null, true, null);
                foreach (T_WQX_ACTIVITY a in as1)
                    WQX_Submit_OneByOne("ACT", a.ACTIVITY_IDX, userID, credential, NodeURL);

                //when done, update status back to stopped
                db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "STOPPED", null, "SYSTEM");
            }
        }

        /// <summary>
        /// This is the service called by the Windows service 
        /// </summary>
        public static void WQX_MasterAllOrgs()
        {
            //loop through all registered organizations and submit pending for each
            List<T_WQX_ORGANIZATION> orgs = db_WQX.GetWQX_ORGANIZATION();
            foreach (T_WQX_ORGANIZATION org in orgs)
            {
                WQX_Master(org.ORG_ID);
            }
        }



        internal static void WQX_Submit_OneByOne(string typeText, int RecordIDX, string userID, string credential, string NodeURL)
        {
            try
            {
                //*******AUTHENTICATE***********************************
                string token = AuthHelper(userID, credential,  "Password", "default", NodeURL);

                //*******SUBMIT*****************************************
                string requestXml = db_WQX.SP_GenWQXXML_Single(typeText, RecordIDX);   //get XML from DB stored procedure
                byte[] bytes = Utils.StrToByteArray(requestXml);
                if (bytes == null) return;
                
                StatusResponseType subStatus = SubmitHelper(NodeURL, token, "WQX", "default", bytes, "submit.xml", DocumentFormatType.XML, "1");

                if (subStatus != null)
                {
                    //*******GET STATUS**************************************
                    string status = "";
                    int i = 0;
                    do
                    {
                        Thread.Sleep(10000);
                        StatusResponseType gsResp = GetStatusHelper(NodeURL, token, subStatus.transactionId);
                        status = gsResp.status.ToString();
                        i += 1;
                        //exit if waiting too long
                        if (i > 30)
                        {
                            UpdateRecordStatus(typeText, RecordIDX, "N");
                            db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", null, "Timed out while getting status from EPA", subStatus.transactionId, "Failed");
                            return;
                        }
                    } while (status != "Failed" && status != "Completed");

                    //*******DOWNLOAD**************************************
                    NodeDocumentType[] dlResp = DownloadHelper(NodeURL, token, "WQX", subStatus.transactionId);


                    //update status of record
                    if (status == "Completed")
                    {
                        UpdateRecordStatus(typeText, RecordIDX, "Y");
                        db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", null, null, subStatus.transactionId, status);
                    }

                    if (status == "Failed")
                    {
                        UpdateRecordStatus(typeText, RecordIDX, "N");

                        int iCount = 0;
                        foreach (NodeDocumentType ndt in dlResp)
                        {
                            if (ndt.documentName.Contains("Validation") || ndt.documentName.Contains("Processing"))
                            {
                                Byte[] resp1 = dlResp[iCount].documentContent.Value;
                                db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", resp1, ndt.documentName, subStatus.transactionId, status);
                            }
                            iCount += 1;
                        }
                    }
                }
                else
                {
                    db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", null, "Unable to submit", null, "Failed");
                }
            }
            catch (SoapException sExept)
            {
                string execption1;
                if (sExept.Detail != null)
                    execption1 = sExept.Detail.InnerText;
                else
                    execption1 = sExept.Message;
            }

        }
        
        internal static void UpdateRecordStatus(string type, int RecordIDX, string status)
        {
            if (type == "MLOC")
            {
                db_WQX.InsertOrUpdateWQX_MONLOC(RecordIDX, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, status, System.DateTime.Now, true, null, "SYSTEM");
            }

            if (type == "PROJ")
            {
                db_WQX.InsertOrUpdateWQX_PROJECT(RecordIDX, null, null, null, null, null, null, null, status, System.DateTime.Now, null, null, "SYSTEM");
            }

            if (type == "ACT")
            {
                db_WQX.InsertOrUpdateWQX_ACTIVITY(RecordIDX, null, null, null, null, null, null, null, null, null, null, null, status, null, null, "SYSTEM");
            }
        }

        public static void GetCDXSubmitCredentials(string OrgID, out string userID, out string credential, out string NodeURL)
        {
            //production
            //    NodeURL = "https://cdxnodengn.epa.gov/ngn-enws20/services/NetworkNode2ServiceConditionalMTOM"; //new 2.1
            //    NodeURL = "https://cdxnodengn.epa.gov/ngn-enws20/services/NetworkNode2Service"; //new 2.0
            //test
            //    NodeURL = "https://testngn.epacdxnode.net/ngn-enws20/services/NetworkNode2ServiceConditionalMTOM"; //new 2.1
            //    NodeURL = "https://testngn.epacdxnode.net/ngn-enws20/services/NetworkNode2Service";  //new 2.0
            //    NodeURL = "https://test.epacdxnode.net/cdx-enws20/services/NetworkNode2ConditionalMtom"; //old 2.1

            NodeURL = db_Ref.GetT_OE_APP_SETTING("CDX Submission URL");

            //get submission credentials for the given organization
            userID = "";
            credential = "";

            T_WQX_ORGANIZATION org = db_WQX.GetWQX_ORGANIZATION_ByID(OrgID);
            if (org != null)
            {
                if (org.CDX_SUBMITTER_ID != null && org.CDX_SUBMITTER_PWD_HASH != null)
                {
                    userID = org.CDX_SUBMITTER_ID;
                    credential = org.CDX_SUBMITTER_PWD_HASH;
                }
                else
                {
                    userID = db_Ref.GetT_OE_APP_SETTING("CDX Submitter");
                    credential = db_Ref.GetT_OE_APP_SETTING("CDX Submitter Password");
                }
            }
        }

    }
}