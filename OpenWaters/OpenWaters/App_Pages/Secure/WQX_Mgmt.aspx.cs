using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class WQX_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkWQXList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

            
                T_OE_APP_TASKS t = db_Ref.GetT_OE_APP_TASKS_ByName("WQXSubmit");
                if (t != null)
                    lblStatus.Text = t.TASK_STATUS;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select an Organization first.";
                return;
            }

            //submit each record individually
            if (rbSubmit.Items[0].Selected)
                WQXSubmit.WQX_Master(Session["OrgID"].ToString());

            //submit all pending data in one large batch
            if (rbSubmit.Items[1].Selected){
                //get CDX username, password, and CDX destination URL
                CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(Session["OrgID"].ToString());

                WQXSubmit.WQX_Submit_OneByOne("", 0, cred.userID, cred.credential, cred.NodeURL, Session["OrgID"].ToString(), true);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GetFile")
            {
                T_WQX_TRANSACTION_LOG aa = db_Ref.GetWQX_TRANSACTION_LOG_ByLogID(e.CommandArgument.ConvertOrDefault<int>());

                if (aa != null)
                {
                    if (aa.RESPONSE_FILE == null && aa.RESPONSE_TXT == null)
                    {
                        lblMsg.Text = "No validation details because submission succeeded.";
                        return;
                    }

                    byte[] b;
                    if (aa.RESPONSE_FILE != null)
                        b = aa.RESPONSE_FILE;
                    else                        
                        b = Utils.StrToByteArray(aa.RESPONSE_TXT);
                 
                    lblMsg.Text = "";
                    Response.ContentType = "application/x-unknown";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + aa.RESPONSE_TXT + "\"");
                    Response.BinaryWrite(b);
                    Response.End();
                    Response.Close();
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "STOPPED", null, User.Identity.Name);
        }
    }
}