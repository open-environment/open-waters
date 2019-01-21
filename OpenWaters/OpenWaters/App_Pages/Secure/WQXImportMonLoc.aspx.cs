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
    public partial class WQXImportMonLoc : System.Web.UI.Page
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

                grdImport.DataSource = db_WQX.GetWQX_IMPORT_TEMP_MONLOC(User.Identity.Name);
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
                int TempID = row.Cells[1].Text.ConvertOrDefault<int>();

                if (check.Checked)
                {
                    T_WQX_IMPORT_TEMP_MONLOC m = db_WQX.GetWQX_IMPORT_TEMP_MONLOC_ByID(TempID);
                    if (m != null)
                    {
                        OrgID = m.ORG_ID;

                        int SuccID = db_WQX.InsertOrUpdateWQX_MONLOC(m.MONLOC_IDX, m.ORG_ID, m.MONLOC_ID, m.MONLOC_NAME, m.MONLOC_TYPE, m.MONLOC_DESC, m.HUC_EIGHT, m.HUC_TWELVE, m.TRIBAL_LAND_IND,
                            m.TRIBAL_LAND_NAME, m.LATITUDE_MSR, m.LONGITUDE_MSR, m.SOURCE_MAP_SCALE, m.HORIZ_ACCURACY, m.HORIZ_ACCURACY_UNIT, m.HORIZ_COLL_METHOD, m.HORIZ_REF_DATUM, m.VERT_MEASURE,
                            m.VERT_MEASURE_UNIT, m.VERT_COLL_METHOD, m.VERT_REF_DATUM, m.COUNTRY_CODE, m.STATE_CODE, m.COUNTY_CODE, m.WELL_TYPE, m.AQUIFER_NAME, m.FORMATION_TYPE, m.WELLHOLE_DEPTH_MSR,
                            m.WELLHOLE_DEPTH_MSR_UNIT, wqxSubmitStatus, null, true, wqxImport, User.Identity.Name);

                    }

                }
            }

            grdImport.Visible = false;
            btnMonLoc.Visible = true;
            btnImport.Visible = false;
            btnCancel.Visible = false;
            pnlFilter.Visible = false;

            db_WQX.DeleteT_WQX_IMPORT_TEMP_MONLOC(User.Identity.Name);

            //add to import log
            db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, "MonitoringLocations", "MonitoringLocations", 0, "Success", "100", "", null, User.Identity.Name);

            lblMsg.Text = "All selected data has been imported.";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //delete first
            int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMP_MONLOC(User.Identity.Name);

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

        protected void btnMonLoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXMonLoc.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}