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
                HyperLink hl = (HyperLink)cp.FindControl("lnkWQXHistory");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //display current task status
                T_OE_APP_TASKS t = db_Ref.GetT_OE_APP_TASKS_ByName("WQXSubmit");
                if (t != null)
                    lblStatus.Text = t.TASK_STATUS;

                //hide task options if not admin
                pnlResubmit.Visible = HttpContext.Current.User.IsInRole("ADMINS");
                chkAllOrgs.Visible = HttpContext.Current.User.IsInRole("ADMINS");

                //display grid
                DisplayGrid();
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

        protected void grdWQXLog_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    Response.Clear();

                    Response.ContentType = "application/x-unknown";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + (aa.RESPONSE_FILE != null ? aa.RESPONSE_TXT : "download.xml") + "\"");
                    Response.Buffer = true;
                    Response.OutputStream.Write(b, 0, b.Length);
//                    Response.BinaryWrite(b);
                    Response.End();
                    Response.Close();
                }
            }
        }

        protected void DisplayGrid()
        {
            string OrgID = Session["OrgID"] != null ? Session["OrgID"].ToString() : "xxx";
            if (chkAllOrgs.Checked && HttpContext.Current.User.IsInRole("ADMINS"))
                OrgID = null;

            if (rbFilter.SelectedValue == "SUB")
            {
                grdWQXLog.Visible = true;
                grdWQXLog.DataSource = db_Ref.GetV_WQX_TRANSACTION_LOG(null, txtStartDate.Text.ConvertOrDefault<DateTime?>(), txtEndDate.Text.ConvertOrDefault<DateTime?>(), OrgID);
                grdWQXLog.DataBind();
                grdWQXPending.Visible = false;
                
            }
            else
            {
                grdWQXPending.Visible = true;
                grdWQXPending.DataSource = db_WQX.GetV_WQX_PENDING_RECORDS(OrgID, txtStartDate.Text.ConvertOrDefault<DateTime?>(), txtEndDate.Text.ConvertOrDefault<DateTime?>());
                grdWQXPending.DataBind();
                grdWQXLog.Visible = false;
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            db_Ref.UpdateT_OE_APP_TASKS("WQXSubmit", "STOPPED", null, User.Identity.Name);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            DisplayGrid();
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("WQXRecordLog.xls", grdWQXLog.Visible ? grdWQXLog : grdWQXPending);
        }
    }
}