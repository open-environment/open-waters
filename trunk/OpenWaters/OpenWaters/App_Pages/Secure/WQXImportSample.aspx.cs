using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class WQXImportSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                Response.Redirect("~");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                PopulateGrid();
            }
        }

        private void PopulateGrid()
        {
            //display grid
            grdImport.DataSource = db_WQX.GetWQX_IMPORT_TEMP_SAMP_RESULT_Disp(User.Identity.Name);
            grdImport.DataBind();
            grdImport.RemoveEmptyColumns(true, "Include in Import");

            //show activity id conflict resolution panel if needed
            pnlActivityID.Visible = db_WQX.GetWQX_IMPORT_TEMP_SAMPLE_DupActivityIDs(User.Identity.Name, Session["OrgID"].ToString()) > 0;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string OrgID = "";

            db_WQX.SP_ImportActivityFromTemp(User.Identity.Name, chkWQXImport.Checked == true ? "Y" : "N", (ddlActivityReplaceType != null ? ddlActivityReplaceType.SelectedValue : "R"));

            pnlFilter.Visible = false;
            grdImport.Visible = false;
            btnCancel.Visible = false;
            btnSample.Visible = true;

            //update import log
            db_Ref.UpdateWQX_IMPORT_LOG_MarkPendingSampImportAsComplete(Session["OrgID"].ToString());

            lblMsg.Text = "All selected data has been imported.";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //delete first
            int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMP_SAMPLE(User.Identity.Name);

            //then redirect
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
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

        protected void btnSample_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");
        }

        protected void grdImport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdImport.PageIndex = e.NewPageIndex;
            PopulateGrid();
        }


    }
}