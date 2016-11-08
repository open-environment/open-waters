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
    public partial class Assess_Unit : System.Web.UI.Page
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

                Utils.BindList(ddlMonLoc, dsMonLoc, "MONLOC_IDX", "MONLOC_ID");

                hdnReportIDX.Value = (Session["AssessRptIDX"] ?? "").ToString();
                hdnAssessUnitIDX.Value = (Session["AssessUnitIDX"] ?? "").ToString();
                PopulateForm();
            }
        }

        private void PopulateForm()
        {
            ddlWaterType.DataSource = db_Attains.GetT_ATTAINS_REF_WATER_TYPE();
            ddlWaterType.DataTextField = "WATER_TYPE_CODE";
            ddlWaterType.DataValueField = "WATER_TYPE_CODE";
            ddlWaterType.DataBind();


            if (hdnReportIDX.Value.ConvertOrDefault<int>() > 0)
            {
                int? AssessUnitID = hdnAssessUnitIDX.Value.ConvertOrDefault<int?>();

                T_ATTAINS_ASSESS_UNITS unit = db_Attains.GetT_ATTAINS_ASSESS_UNITS_byID(AssessUnitID);
                if (unit != null)
                {
                    txtAssessID.Text = unit.ASSESS_UNIT_ID;
                    txtAssessName.Text = unit.ASSESS_UNIT_NAME;
                    txtLocDesc.Text = unit.LOCATION_DESC;
                    ddlAgencyCode.SelectedValue = unit.AGENCY_CODE;
                    ddlStateCode.SelectedValue = unit.STATE_CODE;
                    ddlStatusInd.SelectedValue = unit.ACT_IND;
                    ddlWaterType.SelectedValue = unit.WATER_TYPE_CODE;
                    txtWaterSize.Text = unit.WATER_SIZE.ToString();
                    ddlWaterSizeUnit.SelectedValue = unit.WATER_UNIT_CODE;
                }

                grdMonLoc.DataSource = db_Attains.GetT_ATTAINS_ASSESS_UNITS_MLOC_byAssessUnit(AssessUnitID);
                grdMonLoc.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hdnReportIDX.Value.Length > 0)
            {
                int SuccID = db_Attains.InsertOrUpdateATTAINS_ASSESS_UNITS(hdnAssessUnitIDX.Value.ConvertOrDefault<int?>(), hdnReportIDX.Value.ConvertOrDefault<int?>(), txtAssessID.Text,
                    txtAssessName.Text, txtLocDesc.Text, ddlAgencyCode.SelectedValue, ddlStateCode.SelectedValue, ddlStatusInd.SelectedValue, ddlWaterType.SelectedValue, txtWaterSize.Text.ConvertOrDefault<decimal?>(), ddlWaterSizeUnit.SelectedValue, txtUseClassCode.Text, txtUseClassName.Text,
                    User.Identity.Name);
                if (SuccID > 0)
                {
                    lblMsg.Text = "Assessment Unit saved";
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
            int SuccID = db_Attains.DeleteT_ATTAINS_ASSESS_UNITS(hdnAssessUnitIDX.Value.ConvertOrDefault<int>());

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx");
            else
                lblMsg.Text = "Unable to delete assessment unit.";

        }


        //******************** ASSOC MON LOC *************************
        protected void btnAddSave_Click(object sender, EventArgs e)
        {
            int SuccID = db_Attains.InsertOrUpdateATTAINS_ASSESS_UNITS_MLOC(hdnAssessUnitIDX.Value.ConvertOrDefault<int>(), ddlMonLoc.SelectedValue.ConvertOrDefault<int>(), User.Identity.Name);
            if (SuccID == 0)
                lblMsg.Text = "Unable to add monitoring location.";
            else
                PopulateForm();
        }

        protected void grdMonLoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                db_Attains.DeleteT_ATTAINS_ASSESS_UNITS_MLOC(hdnAssessUnitIDX.Value.ConvertOrDefault<int>(), e.CommandArgument.ToString().ConvertOrDefault<int>());
                PopulateForm();
            }

        }

    }
}