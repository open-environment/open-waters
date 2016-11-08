using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;


namespace OpenEnvironment.App_Pages.Secure
{
    public partial class Assess_Assess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkAssess");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                Utils.BindList(ddlAssessUnit, dsAssessUnit, "ATTAINS_ASSESS_UNIT_IDX", "ASSESS_UNIT_ID");

                hdnReportIDX.Value = (Session["AssessRptIDX"] ?? "").ToString();
                hdnAssessIDX.Value = (Session["AssessIDX"] ?? "").ToString();
                PopulateForm();
            }

        }

        private void PopulateForm()
        {
            if (hdnReportIDX.Value.ConvertOrDefault<int>() > 0)
            {
                int? AssessID = hdnAssessIDX.Value.ConvertOrDefault<int?>();

                T_ATTAINS_ASSESS a = db_Attains.GetT_ATTAINS_ASSESS_byID(AssessID.ConvertOrDefault<int>());
                if (a != null)
                {
                    ddlAssessUnit.SelectedValue = a.ATTAINS_ASSESS_UNIT_IDX.ToString();
                    txtRptCycle.Text = a.REPORTING_CYCLE;
                    txtLastAssess.Text = a.CYCLE_LAST_ASSESSED;
                    txtLastMon.Text = a.CYCLE_LAST_MONITORED;
                    ddlAgencyCode.SelectedValue = a.AGENCY_CODE;
                    ddlReportStatus.SelectedValue = a.REPORT_STATUS;
                    ddlTrophicStatus.SelectedValue = a.TROPHIC_STATUS_CODE;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //****************** validation ******************************************
            if (ddlAssessUnit.SelectedValue == "")
            {
                lblMsg.Text = "Assessment Unit must be specified";
                return;
            }

            if (hdnReportIDX.Value.Length > 0)
            {
                int SuccID = db_Attains.InsertOrUpdateATTAINS_ASSESS(hdnAssessIDX.Value.ConvertOrDefault<int?>(), txtRptCycle.Text, ddlReportStatus.SelectedValue, ddlAssessUnit.SelectedValue.ConvertOrDefault<int>(), ddlAgencyCode.SelectedValue, txtLastAssess.Text,
                    txtLastMon.Text, ddlTrophicStatus.SelectedValue, User.Identity.Name);

                if (SuccID > 0)
                {
                    lblMsg.Text = "Saved successfully";
                    return;
                }
            }

            lblMsg.Text = "Error saving data";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int SuccID = db_Attains.DeleteT_ATTAINS_ASSESS(hdnAssessIDX.Value.ConvertOrDefault<int>());

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx");
            else
                lblMsg.Text = "Unable to delete assessment.";

        }
    }
}