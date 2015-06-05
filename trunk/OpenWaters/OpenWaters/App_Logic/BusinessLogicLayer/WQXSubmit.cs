using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.net.epacdxnode.test;
using System.Threading;
using System.IO;
using Ionic.Zip;
using System.Xml.Linq;

namespace OpenEnvironment.App_Logic.BusinessLogicLayer
{
    public class CDXCredentials
    {
        public string userID { get; set; }
        public string credential { get; set; }
        public string NodeURL { get; set; }
    }

    public static class WQXSubmit
    {
        /// <summary>
        /// This is the service called by the Windows service. Loops through all non disabled organizations. 
        /// </summary>
        public static void WQX_MasterAllOrgs()
        {
            //loop through all registered organizations and submit pending for each
            List<T_WQX_ORGANIZATION> orgs = db_WQX.GetWQX_ORGANIZATIONCanSubmit();
            foreach (T_WQX_ORGANIZATION org in orgs)
            {
                WQX_Master(org.ORG_ID);
            }
        }

        /// <summary>
        /// Scans database for all pending data for the selected organization and submits pending data to EPA one record at a time
        /// </summary>
        public static void WQX_Master(string OrgID)
        {
            T_OE_APP_TASKS t = db_Ref.GetT_OE_APP_TASKS_ByName("WQXSubmit");
            if (t != null)
            {
                if (t.TASK_STATUS == "STOPPED")
                {
                    //set status to RUNNING so tasks won't execute simultaneously
                    db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "RUNNING", null, "SYSTEM");

                    //get CDX username, password, and CDX destination URL
                    CDXCredentials cred = GetCDXSubmitCredentials2(OrgID);

                    //make 1 authenticate attempt just to verify. if failed, then exit, send email, and cancel for org
                    if (AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL) == "")
                    {
                        DisableWQXForOrg(OrgID, "Login failed for supplied NAAS Username and Password for " + OrgID);
                        return;  
                    }

                    //Loop through all pending monitoring locations (including both active and inactive) and submit one at a time
                    List<T_WQX_MONLOC> ms = db_WQX.GetWQX_MONLOC(false, OrgID, true);
                    foreach (T_WQX_MONLOC m in ms)
                        WQX_Submit_OneByOne("MLOC", m.MONLOC_IDX, cred.userID, cred.credential, cred.NodeURL, OrgID, m.ACT_IND);

                    //Loop through all pending projects and submit one at a time
                    List<T_WQX_PROJECT> ps = db_WQX.GetWQX_PROJECT(false, OrgID, true);
                    foreach (T_WQX_PROJECT p in ps)
                        WQX_Submit_OneByOne("PROJ", p.PROJECT_IDX, cred.userID, cred.credential, cred.NodeURL, OrgID, p.ACT_IND);

                    //Loop through all pending activities and submit one at a time
                    List<T_WQX_ACTIVITY> as1 = db_WQX.GetWQX_ACTIVITY(false, OrgID, null, null, null, null, true, null);
                    foreach (T_WQX_ACTIVITY a in as1)
                        WQX_Submit_OneByOne("ACT", a.ACTIVITY_IDX, cred.userID, cred.credential, cred.NodeURL, OrgID, a.ACT_IND);

                    //when done, update status back to stopped
                    db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "STOPPED", null, "SYSTEM");
                }
            }
            else
                db_Ref.InsertT_OE_SYS_LOG("ERROR", "WQX Submission task not found");
        }

        /// <summary>
        /// Used to submit one individual record to EPA (supports Projects, Monitoring Locations, and Samples
        /// </summary>
        /// <param name="typeText">Can be MLOC, PROJ, or ACT</param>
        /// <param name="RecordIDX"></param>
        /// <param name="userID"></param>
        /// <param name="credential"></param>
        /// <param name="NodeURL"></param>
        /// <param name="orgID"></param>
        /// <param name="InsUpdIndicator">True for Insert/Updates, False for Deletes</param>
        internal static void    WQX_Submit_OneByOne(string typeText, int RecordIDX, string userID, string credential, string NodeURL, string orgID, bool? InsUpdIndicator)
        {
            try
            {
                //*******AUTHENTICATE*********************************************************************************************************
                string token = AuthHelper(userID, credential, "Password", "default", NodeURL);

                //*******SUBMIT***************************************************************************************************************
                string requestXml = InsUpdIndicator == false ? db_WQX.SP_GenWQXXML_Single_Delete(typeText, RecordIDX) : db_WQX.SP_GenWQXXML_Single(typeText, RecordIDX);   //get XML from DB stored procedure
                byte[] bytes = Utils.StrToByteArray(requestXml);
                if (bytes == null) return;

                StatusResponseType subStatus = SubmitHelper(NodeURL, token, "WQX", "default", bytes, "submit.xml", DocumentFormatType.XML, "1");
                if (subStatus != null)
                {
                    //*******GET STATUS********************************************************************************************************
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
                            db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", null, "Timed out while getting status from EPA", subStatus.transactionId, "Failed", orgID);
                            return;
                        }
                    } while (status != "Failed" && status != "Completed");

                    //update status of record
                    if (status == "Completed")
                    {
                        UpdateRecordStatus(typeText, RecordIDX, "Y");
                        db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", null, null, subStatus.transactionId, status, orgID);
                    }

                    if (status == "Failed")
                    {
                        UpdateRecordStatus(typeText, RecordIDX, "N");

                        int iCount = 0;
                        //*******DOWNLOAD ERROR REPORT ****************************************************************************
                        NodeDocumentType[] dlResp = DownloadHelper(NodeURL, token, "WQX", subStatus.transactionId);
                        foreach (NodeDocumentType ndt in dlResp)
                        {
                            if (ndt.documentName.Contains("Validation") || ndt.documentName.Contains("Processing"))
                            {
                                Byte[] resp1 = dlResp[iCount].documentContent.Value;
                                db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", resp1, ndt.documentName, subStatus.transactionId, status, orgID);
                            }
                            iCount += 1;
                        }
                    }
                }
                else
                {
                    db_Ref.InsertUpdateWQX_TRANSACTION_LOG(null, typeText, RecordIDX, "I", null, "Unable to submit", null, "Failed", orgID);
                    DisableWQXForOrg(orgID, "Submission failed for supplied for " + orgID);
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
        
        /// <summary>
        /// Calls Exchange Network authenticate method
        /// </summary>
        /// <returns>Security token if valid or empty string if failed</returns>
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
                db_Ref.InsertT_OE_SYS_LOG("ERROR", sExept.Message.SubStringPlus(0, 1999));   //logging an authentication failure
                return "";
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
            catch (SoapException sExept)
            {
                db_Ref.InsertT_OE_SYS_LOG("WQX", sExept.Message.SubStringPlus(0, 1999));
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

        internal static ResultSetType QueryHelper(string NodeURL, string secToken, string dataFlow, string request, int? rowID, int? maxRows, List<ParameterType> pars)
        {
            try
            {
                NetworkNode2 nn = new NetworkNode2();
                nn.Url = NodeURL;
                nn.SoapVersion = SoapProtocolVersion.Soap12;

                Query q1 = new Query();
                q1.securityToken = secToken;
                q1.dataflow = dataFlow;
                q1.request = request;
                q1.rowId = (rowID ?? 0).ToString();
                q1.maxRows = (maxRows ?? -1).ToString();

                ParameterType[] ps = new ParameterType[pars.Count];
                int i = 0;
                System.Xml.XmlQualifiedName parType = new System.Xml.XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
                foreach (ParameterType par in pars)
                {
                    if (par.parameterEncoding == null) par.parameterEncoding = EncodingType.None;
                    ps[i] = par;
                    i++;
                }

                q1.parameters = ps;

                return nn.Query(q1);
            }
            catch (SoapException sExept)
            {
                db_Ref.InsertT_OE_SYS_LOG("ERROR", sExept.Message.SubStringPlus(0, 1999));   //logging an authentication failure

                //special handling of an unauthorized
                if (sExept.Message.SubStringPlus(0, 9) == "ORA-20997")
                {
                    ResultSetType rs = new ResultSetType();
                    rs.rowId = "-99";
                    return rs; 
                }
                
                return null;
            }
        }

        internal static StatusResponseType SolicitHelper(string NodeURL, string secToken, string dataFlow, string request, int? rowID, int? maxRows, List<ParameterType> pars)
        {
            try
            {
                NetworkNode2 nn = new NetworkNode2();
                nn.Url = NodeURL;
                nn.SoapVersion = SoapProtocolVersion.Soap12;

                Solicit s1 = new Solicit();
                s1.securityToken = secToken;
                s1.dataflow = dataFlow;
                s1.request = request;

                ParameterType[] ps = new ParameterType[pars.Count];
                int i = 0;
                System.Xml.XmlQualifiedName parType = new System.Xml.XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
                foreach (ParameterType par in pars)
                {
                    if (par.parameterEncoding == null) par.parameterEncoding = EncodingType.None;
                    ps[i] = par;
                    i++;
                }

                s1.parameters = ps;

                return nn.Solicit(s1);
            }
            catch (SoapException sExept)
            {
                db_Ref.InsertT_OE_SYS_LOG("WQX", sExept.Message.SubStringPlus(0, 1999));
                return null;
            }
        }

        internal static void UpdateRecordStatus(string type, int RecordIDX, string status)
        {
            if (type == "MLOC")
                db_WQX.InsertOrUpdateWQX_MONLOC(RecordIDX, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, status, System.DateTime.Now, true, null, "SYSTEM");

            if (type == "PROJ")
                db_WQX.InsertOrUpdateWQX_PROJECT(RecordIDX, null, null, null, null, null, null, null, status, System.DateTime.Now, null, null, "SYSTEM");

            if (type == "ACT")
                db_WQX.UpdateWQX_ACTIVITY_WQXStatus(RecordIDX, status, null, null, "SYSTEM");
        }

        public static CDXCredentials GetCDXSubmitCredentials2(string OrgID)
        {
            //production
            //    NodeURL = "https://cdxnodengn.epa.gov/ngn-enws20/services/NetworkNode2ServiceConditionalMTOM"; //new 2.1
            //    NodeURL = "https://cdxnodengn.epa.gov/ngn-enws20/services/NetworkNode2Service"; //new 2.0
            //test
            //    NodeURL = "https://testngn.epacdxnode.net/ngn-enws20/services/NetworkNode2ServiceConditionalMTOM"; //new 2.1
            //    NodeURL = "https://testngn.epacdxnode.net/ngn-enws20/services/NetworkNode2Service";  //new 2.0
            //    NodeURL = "https://test.epacdxnode.net/cdx-enws20/services/NetworkNode2ConditionalMtom"; //old 2.1

            var cred = new CDXCredentials();
            try
            {
                cred.NodeURL = db_Ref.GetT_OE_APP_SETTING("CDX Submission URL");

                T_WQX_ORGANIZATION org = db_WQX.GetWQX_ORGANIZATION_ByID(OrgID);
                if (org != null)
                {
                    if (string.IsNullOrEmpty(org.CDX_SUBMITTER_ID) == false && string.IsNullOrEmpty(org.CDX_SUBMITTER_PWD_HASH) == false)
                    {
                        cred.userID = org.CDX_SUBMITTER_ID;
                        cred.credential = new SimpleAES().Decrypt(System.Web.HttpUtility.UrlDecode(org.CDX_SUBMITTER_PWD_HASH).Replace(" ", "+"));
                    }
                    else
                    {
                        cred.userID = db_Ref.GetT_OE_APP_SETTING("CDX Submitter");
                        cred.credential = db_Ref.GetT_OE_APP_SETTING("CDX Submitter Password");
                    }
                }
            }
            catch { }

            return cred;
        }

        private static void DisableWQXForOrg(string OrgID, string LogMsg)
        {
            //when done, update status back to stopped
            db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(OrgID, null, null, null, null, null, null, null, null, null, null, false, null, null);
            db_Ref.InsertT_OE_SYS_LOG("WQX_ORG_STOP", LogMsg);

            List<T_OE_USERS> users = db_WQX.GetWQX_USER_ORGS_AdminsByOrg(OrgID);
            foreach (T_OE_USERS user in users)
                Utils.SendEmail(null, user.EMAIL.Split(';').ToList(), null, null, "Open Waters Submit Failure", "Automated submission for " + OrgID + " has been disabled due to a submission failure. Failure details are: " + LogMsg, null);
        }


        //********************* GETTING DATA FROM WQX ********************************************************************
        public static bool ImportActivityMaster()
        {
            //don't attempt any imports if another user is currently having their's processed
            if (db_Ref.GetWQX_IMPORT_LOG_ProcessingCount() > 0)
                return true;

            //GET LISTING OF ALL "NEW" ACTIVITY IMPORT RECORDS FROM LOG
            T_WQX_IMPORT_LOG i = db_Ref.GetWQX_IMPORT_LOG_NewActivity();
            if (i != null)
            {
                ImportActivity(i.ORG_ID, i.IMPORT_ID, i.CREATE_USERID);
            }

            return true;
        }

        public static bool ImportActivity(string OrgID, int? ImportID, string UserID)
        {
            //*******UPDATE IMPORT LOG TO SIGNIFY THAT IMPORT HAS BEGUN
            db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Processing", "1", "Import started", null, "SYSTEM");

            //*******AUTHENTICATE TO EPA***********************************
            CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(OrgID);
            string token = WQXSubmit.AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL);
            db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Processing", "2", "Authenticated to EPA", null, "SYSTEM");

            //*******SOLICIT*****************************************
            if (token.Length > 0)
            {
                List<net.epacdxnode.test.ParameterType> pars = new List<net.epacdxnode.test.ParameterType>();

                net.epacdxnode.test.ParameterType p = new net.epacdxnode.test.ParameterType();
                p.parameterName = "organizationIdentifier";
                p.Value = OrgID;
                pars.Add(p);

                net.epacdxnode.test.StatusResponseType solResp = WQXSubmit.SolicitHelper(cred.NodeURL, token, "WQX", "WQX.GetResultByParameters_v2.1", null, null, pars);
                if (solResp == null)
                {
                    db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Failed", "100", "Failed - retrieving data from EPA timed out", null, "SYSTEM");
                    return false;
                }
                db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Processing", "5", "Request for data from EPA complete - awaiting response.", null, "SYSTEM");


                //*******GET STATUS********************************************************************************************************
                string status = "";
                int i = 0;
                do
                {
                    System.Threading.Thread.Sleep(15000);
                    StatusResponseType gsResp = WQXSubmit.GetStatusHelper(cred.NodeURL, token, solResp.transactionId);
                    status = gsResp.status.ToString();
                    i += 1;
                    //exit if waiting too long
                    if (i > 90)
                    {
                        db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Failed", "100", "Failed - EPA has taken too long to process your request. Operation has been cancelled.", null, "SYSTEM");
                        return false;
                    }
                } while (status != "Failed" && status != "Completed");

                //update status of record
                if (status == "Completed")
                {
                    db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Processing", "10", "Data retrieved from EPA.", null, "SYSTEM");

                    //GET PATH TO PLACE WHERE IMPORT FILE WILL BE STORED
                    string svcPath = db_Ref.GetT_OE_APP_SETTING("Task App Path");
                    if (svcPath.Length == 0)
                        db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Failed", "100", "Failed - Administrator must configure Open Waters task application path.", null, "SYSTEM");

                    //*******DOWNLOAD RESULTS.XML FROM EPA ****************************************************************************
                    NodeDocumentType[] dlResp = WQXSubmit.DownloadHelper(cred.NodeURL, token, "WQX", solResp.transactionId);
                    foreach (NodeDocumentType ndt in dlResp)
                    {
                        //DELETE PREVIOUS FILES IF EXISTING
                        if (File.Exists(svcPath + "/Results.xml"))
                            File.Delete(svcPath + "/Results.xml");

                        using (System.IO.Stream stream = new System.IO.MemoryStream(dlResp[0].documentContent.Value))
                        {
                            using (var zip = ZipFile.Read(stream))
                            {
                                foreach (var entry in zip)
                                    entry.Extract(svcPath);
                            }
                        }
                    }

                    XDocument xdoc = XDocument.Load(svcPath + "/Results.xml");

                    var activities = (from activity
                                          in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}Activity")
                                      select new
                                      {
                                          ID = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityIdentifier") ?? String.Empty,
                                          ActivityTypeVal = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityTypeCode") ?? String.Empty,
                                          ActivityMediaVal = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityMediaName") ?? String.Empty,
                                          ActivitySubMediaVal = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityMediaSubDivisionName") ?? String.Empty,
                                          StartDate = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityStartDate") ?? String.Empty,
                                          StartTime = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityStartTime").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityStartTime").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}Time") ?? String.Empty : "",
                                          StartTimeZone = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityStartTime").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityStartTime").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}TimeZoneCode") ?? String.Empty : "",
                                          EndDate = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityEndDate") ?? String.Empty,
                                          EndTime = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityEndTime").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityEndTime").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}Time") ?? String.Empty : "",
                                          RelDepthName = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityRelativeDepthName") ?? String.Empty,
                                          ActDepth = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDepthHeightMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDepthHeightMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          ActDepthUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDepthHeightMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDepthHeightMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          ActTopDepth = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityTopDepthHeightMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityTopDepthHeightMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          ActTopDepthUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityTopDepthHeightMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityTopDepthHeightMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          ActBotDepth = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityBottomDepthHeightMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityBottomDepthHeightMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          ActBotDepthUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityBottomDepthHeightMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityBottomDepthHeightMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          ActivityDepthAltitudeReferencePointText = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDepthAltitudeReferencePointText") ?? String.Empty,
                                          ProjectIdentifier = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ProjectIdentifier") ?? String.Empty,
                                          MonitoringLocationIdentifier = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentifier") ?? String.Empty,
                                          ActivityCommentText = (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ActivityCommentText") ?? String.Empty,
                                          //BIO
                                          AssemblageSampledName = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalActivityDescription").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}AssemblageSampledName") ?? String.Empty : "",
                                          CollectionDuration = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CollectionDuration").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CollectionDuration").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          CollectionDurationUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CollectionDuration").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CollectionDuration").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          SamplingComponentName = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalHabitatCollectionInformation").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalHabitatCollectionInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}SamplingComponentName") ?? String.Empty : "",
                                          SamplingComponentPlaceInSeriesNumeric = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalHabitatCollectionInformation").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalHabitatCollectionInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}SamplingComponentPlaceInSeriesNumeric") ?? String.Empty : "",
                                          ReachLengthMeasure = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachLengthMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachLengthMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          ReachLengthMeasureUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachLengthMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachLengthMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          ReachWidthMeasure = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachWidthMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachLengthMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          ReachWidthMeasureUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachWidthMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ReachLengthMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          PassCount = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalHabitatCollectionInformation").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalHabitatCollectionInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}PassCount") ?? String.Empty : "",
                                          NetTypeName = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetInformation").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}NetTypeName") ?? String.Empty : "",
                                          NetSurfaceAreaMeasure = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetSurfaceAreaMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetSurfaceAreaMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          NetSurfaceAreaMeasureUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetSurfaceAreaMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetSurfaceAreaMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          NetMeshSizeMeasure = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetMeshSizeMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetMeshSizeMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          NetMeshSizeMeasureUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetMeshSizeMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}NetMeshSizeMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          BoatSpeedMeasure = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BoatSpeedMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BoatSpeedMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          BoatSpeedMeasureUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BoatSpeedMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BoatSpeedMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          CurrentSpeedMeasure = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CurrentSpeedMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CurrentSpeedMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                          CurrentSpeedMeasureUnit = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CurrentSpeedMeasure").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}CurrentSpeedMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                          ToxicityTestType = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalActivityDescription").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalActivityDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ToxicityTestType") ?? String.Empty : "",
                                          //SAMPLING
                                          SampleCollectionMethodID = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionMethod").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifier") ?? String.Empty : "",
                                          SampleCollectionMethodContext = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionMethod").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifierContext") ?? String.Empty : "",
                                          SampleCollectionMethodName = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionMethod").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodName") ?? String.Empty : "",
                                          SampleCollectionEquipmentName = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleDescription").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionEquipmentName") ?? String.Empty : "",
                                          SampleCollectionEquipmentComment = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleDescription").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SampleDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}SampleCollectionEquipmentCommentText") ?? String.Empty : "",
                                          SamplePrepMethodID = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SamplePreparationMethod").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SamplePreparationMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifier") ?? String.Empty : "",
                                          SamplePrepMethodContext = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SamplePreparationMethod").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SamplePreparationMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifierContext") ?? String.Empty : "",
                                          SamplePrepMethodName = activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SamplePreparationMethod").FirstOrDefault() != null ? (string)activity.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}SamplePreparationMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodName") ?? String.Empty : "",

                                      });

                    //loop through retrieved data and insert into temp table
                    foreach (var activity in activities)
                    {
                        DateTime? startDate = string.IsNullOrEmpty(activity.StartDate) ? null : (activity.StartDate + " " + activity.StartTime).ConvertOrDefault<DateTime?>();
                        DateTime? endDate = string.IsNullOrEmpty(activity.EndDate) ? null : (activity.EndDate + " " + activity.EndTime).ConvertOrDefault<DateTime?>();

                        int TempImportSampID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(null, UserID, OrgID, null, activity.ProjectIdentifier, null, activity.MonitoringLocationIdentifier,
                            null, activity.ID, activity.ActivityTypeVal, activity.ActivityMediaVal, activity.ActivitySubMediaVal, startDate, endDate, activity.StartTimeZone,
                            activity.RelDepthName, activity.ActDepth, activity.ActDepthUnit, activity.ActTopDepth, activity.ActTopDepthUnit, activity.ActBotDepth, activity.ActBotDepthUnit, 
                            activity.ActivityDepthAltitudeReferencePointText, activity.ActivityCommentText, activity.AssemblageSampledName, activity.CollectionDuration, 
                            activity.CollectionDurationUnit, activity.SamplingComponentName, activity.SamplingComponentPlaceInSeriesNumeric.ConvertOrDefault<int?>(),
                            activity.ReachLengthMeasure, activity.ReachLengthMeasureUnit, activity.ReachWidthMeasure, activity.ReachWidthMeasureUnit, 
                            activity.PassCount.ConvertOrDefault<int?>(), activity.NetTypeName, activity.NetSurfaceAreaMeasure, activity.NetSurfaceAreaMeasureUnit, activity.NetMeshSizeMeasure,
                            activity.NetMeshSizeMeasureUnit, activity.BoatSpeedMeasure, activity.BoatSpeedMeasureUnit, activity.CurrentSpeedMeasure, activity.CurrentSpeedMeasureUnit,
                            activity.ToxicityTestType, null, activity.SampleCollectionMethodID,
                            activity.SampleCollectionMethodContext, activity.SampleCollectionMethodName, activity.SampleCollectionEquipmentName, activity.SampleCollectionEquipmentComment,
                            null, activity.SamplePrepMethodID, activity.SamplePrepMethodContext, activity.SamplePrepMethodName, null, null, null, null, null, "P", "", true, true);

                        if (TempImportSampID > 0)
                        {
                            //*****************************************************************************************************
                            //import results
                            //*****************************************************************************************************
                            var results = (from result in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}Result")
                                           where result.Parent.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityIdentifier").FirstOrDefault().Value == activity.ID
                                           select new
                                           {
                                               LoggerLine = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}DataLoggerLineName") ?? String.Empty,
                                               ResultDetectionConditionText = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionConditionText") ?? String.Empty,
                                               CharName = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}CharacteristicName") ?? String.Empty,
                                               MethodSpec = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodSpeciationName") ?? String.Empty,
                                               SampFrac = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultSampleFractionText") ?? String.Empty,
                                               MsrVal = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultMeasure").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultMeasureValue") ?? String.Empty : "",
                                               MsrValUnit = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultMeasure").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                               MsrQualCode = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultMeasure").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureQualifierCode") ?? String.Empty : "",
                                               ResultStatusIdentifier = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultStatusIdentifier") ?? String.Empty,
                                               StatisticalBaseCode = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}StatisticalBaseCode") ?? String.Empty,
                                               ResultValueTypeName = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultValueTypeName") ?? String.Empty,
                                               ResultWeightBasisText = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultWeightBasisText") ?? String.Empty,
                                               ResultTimeBasisText = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultTimeBasisText") ?? String.Empty,
                                               ResultTemperatureBasisText = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultTemperatureBasisText") ?? String.Empty,
                                               ResultParticleSizeBasisText = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultParticleSizeBasisText") ?? String.Empty,

                                               PrecisionVal = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}PrecisionValue") ?? String.Empty : "",
                                               BiasVal = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}BiasValue") ?? String.Empty : "",
                                               ConfidenceIntervalValue = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ConfidenceIntervalValue") ?? String.Empty : "",
                                               UpperConfidenceLimitValue = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}UpperConfidenceLimitValue") ?? String.Empty : "",
                                               LowerConfidenceLimitValue = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}DataQuality").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}LowerConfidenceLimitValue") ?? String.Empty : "",
                                               ResultCommentText = (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultCommentText") ?? String.Empty,
                                               //BIOLOGICAL
                                               BiologicalIntentName = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault() != null ?  (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalIntentName") ?? String.Empty : String.Empty,
                                               BiologicalIndividualIdentifier = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault() != null ?  (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalIndividualIdentifier") ?? String.Empty : String.Empty,
                                               SubjectTaxonomicName = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault() != null ?  (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}SubjectTaxonomicName") ?? String.Empty : String.Empty,
                                               UnidentifiedSpeciesIdentifier = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault() != null ?  (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}UnidentifiedSpeciesIdentifier") ?? String.Empty : String.Empty,
                                               SampleTissueAnatomyName = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}BiologicalResultDescription").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}SampleTissueAnatomyName") ?? String.Empty : String.Empty,

                                               FrequencyClassDescriptorCode = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassDescriptorCode") ?? String.Empty : String.Empty,
                                               FrequencyClassDescriptorUnit = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassDescriptorUnitCode") ?? String.Empty : String.Empty,
                                               FrequencyClassDescriptorLower = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}LowerClassBoundValue") ?? String.Empty : String.Empty,
                                               FrequencyClassDescriptorUpper = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}FrequencyClassInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}UpperClassBoundValue") ?? String.Empty : String.Empty,

                                               //LABORATORY ANALYSIS
                                               ResultAnalyticalMethodID = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultAnalyticalMethod").FirstOrDefault() != null ?  (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultAnalyticalMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifier") ?? String.Empty : String.Empty,
                                               ResultAnalyticalMethodCTX = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultAnalyticalMethod").FirstOrDefault() != null ?   (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultAnalyticalMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifierContext") ?? String.Empty : String.Empty,
                                               ResultAnalyticalMethodName = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultAnalyticalMethod").FirstOrDefault() != null ?  (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultAnalyticalMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodName") ?? String.Empty : String.Empty,
                                               LaboratoryName =  result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}LaboratoryName") ?? String.Empty : String.Empty,
                                               AnalysisStartDate = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}AnalysisStartDate") ?? String.Empty : String.Empty,
                                               AnalysisEndDate = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}AnalysisEndDate") ?? String.Empty : String.Empty,
                                               ResultLaboratoryCommentCode = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultLabInformation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}ResultLaboratoryCommentCode") ?? String.Empty : String.Empty,

                                               MethodDetectionLevel = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName") ?? String.Empty : String.Empty,
                                               MethodDetectionLevel2 = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID2 => ID2.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Method Detection Level") != null ?
                                                   ((string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID3 => ID3.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Method Detection Level").Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue").FirstOrDefault() ?? String.Empty) 
                                                   : String.Empty,

                                               LabReportingLevel = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID2 => ID2.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Laboratory Reporting Level") != null ?
                                                    ((string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID3 => ID3.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Laboratory Reporting Level").Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue").FirstOrDefault() ?? String.Empty)
                                                    : String.Empty,
                                               PQL = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID2 => ID2.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Practical Quantitation Limit") != null ?
                                                    ((string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID3 => ID3.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Practical Quantitation Limit").Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue").FirstOrDefault() ?? String.Empty)
                                                    : String.Empty,
                                               UpperQuantLimit = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID2 => ID2.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Upper Quantitation Limit") != null ?
                                                    ((string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID3 => ID3.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Upper Quantitation Limit").Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue").FirstOrDefault() ?? String.Empty)
                                                    : String.Empty,
                                               LowerQuantLimit = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID2 => ID2.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Lower Quantitation Limit") != null ?
                                                    ((string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ResultDetectionQuantitationLimit").FirstOrDefault(ID3 => ID3.Element("{http://www.exchangenetwork.net/schema/wqx/2}DetectionQuantitationLimitTypeName").Value == "Lower Quantitation Limit").Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue").FirstOrDefault() ?? String.Empty)
                                                    : String.Empty,

                                               SampPrepMethodID = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparationMethod").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparationMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifier") ?? String.Empty : String.Empty,
                                               SampPrepMethodCTX = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparationMethod").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparationMethod").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MethodIdentifierContext") ?? String.Empty : String.Empty,
                                               SampPrepStartDate = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}PreparationStartDate") ?? String.Empty : String.Empty,
                                               SampPrepEndDate = result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparation").FirstOrDefault() != null ? (string)result.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}LabSamplePreparation").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}PreparationEndDate") ?? String.Empty : String.Empty,

                                               ActID = (string)result.Parent.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}ActivityIdentifier").FirstOrDefault()
                                           });

                            if (results != null)
                            {
                                foreach (var result in results)
                                {
                                    db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_RESULT(null, TempImportSampID, null, result.LoggerLine, result.ResultDetectionConditionText, result.CharName, result.MethodSpec,
                                        result.SampFrac, result.MsrVal, result.MsrValUnit, result.MsrQualCode, result.ResultStatusIdentifier, result.StatisticalBaseCode,
                                        result.ResultValueTypeName, result.ResultWeightBasisText, result.ResultTimeBasisText, result.ResultTemperatureBasisText,
                                        result.ResultParticleSizeBasisText, result.PrecisionVal, result.BiasVal, result.ConfidenceIntervalValue, result.UpperConfidenceLimitValue,
                                        result.LowerConfidenceLimitValue, result.ResultCommentText, null, null, null, result.BiologicalIntentName, result.BiologicalIndividualIdentifier, 
                                        result.SubjectTaxonomicName, result.UnidentifiedSpeciesIdentifier, result.SampleTissueAnatomyName, null, null, null,
                                        null, null, null, null, null, null, null, null, null, result.FrequencyClassDescriptorCode, result.FrequencyClassDescriptorUnit, result.FrequencyClassDescriptorUpper, 
                                        result.FrequencyClassDescriptorLower, null, result.ResultAnalyticalMethodID, result.ResultAnalyticalMethodCTX, 
                                        result.ResultAnalyticalMethodName, null, result.LaboratoryName, 
                                        result.AnalysisStartDate.ConvertOrDefault<DateTime?>(), result.AnalysisEndDate.ConvertOrDefault<DateTime?>(),
                                        null, result.ResultLaboratoryCommentCode, result.MethodDetectionLevel2,
                                        result.LabReportingLevel, result.PQL, result.LowerQuantLimit, result.UpperQuantLimit, null, null, result.SampPrepMethodID, result.SampPrepMethodCTX,
                                        result.SampPrepStartDate.ConvertOrDefault<DateTime?>(), result.SampPrepEndDate.ConvertOrDefault<DateTime?>(), null,
                                        "P", "", true, OrgID, true);
                                }
                            }   //************* END RESULTS LOOPING  *********************************************************
                        }
                    }    //***************** END ACTIVITY LOOPING *****************************************************


                    db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Success", "100", "Import complete.", null, "SYSTEM");
                    return true;

                }

                //IF GOT THIS FAR, THEN IT FAILED
                db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Failed", "100", "Unable to retrieve data from EPA.", null, "SYSTEM");
                return false;

            }
            else
            {
                db_Ref.InsertUpdateWQX_IMPORT_LOG(ImportID, null, null, null, 0, "Failed", "100", "Unable to authenticate to EPA-WQX server.", null, "SYSTEM");
                return false;
            }
        }

    }
}