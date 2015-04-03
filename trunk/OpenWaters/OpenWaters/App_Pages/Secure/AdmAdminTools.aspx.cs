using OpenEnvironment.net.epacdxnode.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class AdmAdminTools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // forms-based authorization
                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    Response.Redirect("~/App_Pages/Public/AccessDenied.aspx");
            }

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string OrgID = Session["OrgID"].ToString();
            int iCount=0;

            //get credentials
            CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(OrgID);

            string token = WQXSubmit.AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL);

            NodeDocumentType[] dlResp = WQXSubmit.DownloadHelper(cred.NodeURL, token, "WQX", txtTransID.Text);
            foreach (NodeDocumentType ndt in dlResp)
            {
                if (ndt.documentName.Contains("Validation") || ndt.documentName.Contains("Processing"))
                {
                    Byte[] resp1 = dlResp[iCount].documentContent.Value;
                }

                iCount += 1;
            }
        }

        protected void btnGetStatus_Click(object sender, EventArgs e)
        {
            string OrgID = Session["OrgID"].ToString();
            int iCount = 0;

            //get credentials
            CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(OrgID);

            //auth
            string token = WQXSubmit.AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL);

            StatusResponseType gsResp = WQXSubmit.GetStatusHelper(cred.NodeURL, token, txtTransID.Text);
            string status = gsResp.status.ToString();


        }

        protected void btnTransHistory_Click(object sender, EventArgs e)
        {
            try
            {
                string OrgID = Session["OrgID"].ToString();

                //get CDX username, password, and CDX destination URL
                CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(OrgID);

                //*******AUTHENTICATE***********************************
                string token = WQXSubmit.AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL);

                //*******QUERY*****************************************
                if (token.Length > 0)
                {
                    List<net.epacdxnode.test.ParameterType> pars = new List<net.epacdxnode.test.ParameterType>();

                    net.epacdxnode.test.ParameterType p = new net.epacdxnode.test.ParameterType();
                    p.parameterName = "organizationIdentifier";
                    p.Value = OrgID;
                    pars.Add(p);

                    p.parameterName = "transactionDateBegin";
                    p.Value = "2015-01-04";
                    p.parameterType = new System.Xml.XmlQualifiedName("DateTime", "http://www.w3.org/2001/XMLSchema");
                    pars.Add(p);

                    net.epacdxnode.test.ResultSetType queryResp = WQXSubmit.QueryHelper(cred.NodeURL, token, "WQX", "WQX.GetTransactionHistoryByParameters_v2.1", null, null, pars);


                }
                else
                {
                    //lblMsg.Text = "Unable to authenticate to EPA-WQX server.";
                }
            }
            catch
            {
                return;
            }
        }
    }
}