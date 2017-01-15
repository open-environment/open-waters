using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class WQXImportMetric : System.Web.UI.Page
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

                grdImport.DataSource = db_WQX.GetWQX_IMPORT_TEMP_ACTIVITY_METRIC(User.Identity.Name);
                grdImport.DataBind();
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //if importing data from EPA, then save the records as already synced and passing
            bool wqxImport = ((Request.QueryString["e"] ?? "") == "1") ? true : chkWQXImport.Checked;
            string wqxSubmitStatus = ((Request.QueryString["e"] ?? "") == "1") ? "Y" : "U";

            string OrgID="";

            foreach (GridViewRow row in grdImport.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkImport");
                int TempID = grdImport.DataKeys[row.RowIndex]["TEMP_ACTIVITY_METRIC_IDX"].ConvertOrDefault<int>();

                if (check.Checked)
                {
                    T_WQX_IMPORT_TEMP_ACTIVITY_METRIC m = db_WQX.GetWQX_IMPORT_TEMP_ACTIVITY_METRIC_byID(TempID);
                    if (m != null)
                    {
                        OrgID = m.ORG_ID;

                        int SuccID = db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, m.ACTIVITY_IDX.ConvertOrDefault<int>(), m.METRIC_TYPE_ID, 
                            m.METRIC_TYPE_ID_CONTEXT, m.METRIC_TYPE_NAME, null, null, null, null, null, null, m.METRIC_SCALE, m.METRIC_FORMULA_DESC, 
                            m.METRIC_VALUE_MSR, m.METRIC_VALUE_MSR_UNIT, m.METRIC_SCORE, m.METRIC_COMMENT, wqxImport, null, null, true, User.Identity.Name);

                    }

                }
            }

            grdImport.Visible = false;
            btnImport.Visible = false;
            btnCancel.Visible = false;
            pnlFilter.Visible = false;

            db_WQX.DeleteT_WQX_IMPORT_TEMP_ACTIVITY_METRIC(User.Identity.Name);

            //add to import log
            db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, "Activity Metrics", "Activity Metrics", 0, "Success", "100", "", null, User.Identity.Name);

            lblMsg.Text = "All selected data has been imported.";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //delete first
            int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMP_ACTIVITY_METRIC(User.Identity.Name);

            //then redirect
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
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