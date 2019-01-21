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
    public partial class WQXImportProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);

            if ((Request.QueryString["e"] ?? "") == "1")
                chkWQXImport.Visible = false;

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                grdImport.DataSource = db_WQX.GetWQX_IMPORT_TEMP_PROJECT(User.Identity.Name);
                grdImport.DataBind();
            }

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //if importing data from EPA, then save the records as already synced and passing
            bool wqxImport = ((Request.QueryString["e"] ?? "") == "1") ? true : chkWQXImport.Checked;
            string wqxSubmitStatus = ((Request.QueryString["e"] ?? "") == "1") ? "Y" : "U";

            string OrgID = "";

            foreach (GridViewRow row in grdImport.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkImport");
                int TempID = row.Cells[1].Text.ConvertOrDefault<int>();

                if (check.Checked)
                {
                    T_WQX_IMPORT_TEMP_PROJECT p = db_WQX.GetWQX_IMPORT_TEMP_PROJECT_ByID(TempID);
                    if (p != null)
                    {
                        OrgID = p.ORG_ID;

                        int SuccID = db_WQX.InsertOrUpdateWQX_PROJECT(p.PROJECT_IDX, p.ORG_ID, p.PROJECT_ID, p.PROJECT_NAME, p.PROJECT_DESC, p.SAMP_DESIGN_TYPE_CD, p.QAPP_APPROVAL_IND, 
                            p.QAPP_APPROVAL_AGENCY, wqxSubmitStatus, null, true, wqxImport, User.Identity.Name);

                    }

                }
            }

            grdImport.Visible = false;
            btnProject.Visible = true;
            btnImport.Visible = false;
            btnCancel.Visible = false;
            pnlFilter.Visible = false;

            db_WQX.DeleteT_WQX_IMPORT_TEMP_PROJECT(User.Identity.Name);

            //add to import log
            db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, "Projects", "Projects", 0, "Success", "100", null, null, User.Identity.Name);

            lblMsg.Text = "All selected data have been imported.";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
        }

        protected void btnProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXProject.aspx");
        }

        protected Boolean VerifyCheck(object r)
        {
            string s = r.ConvertOrDefault<string>();
            if (s == "P")
                return true;
            else
                return false;
        }

        protected void grdImport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "IMPORT_STATUS_CD") != null)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "IMPORT_STATUS_CD").ToString() == "F")
                        e.Row.BackColor = System.Drawing.Color.LightPink;
                    else
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }


    }
}