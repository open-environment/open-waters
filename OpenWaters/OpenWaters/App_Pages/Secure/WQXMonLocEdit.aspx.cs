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
    public partial class WQXMonLocEdit : System.Web.UI.Page
    {
        int MonLocIDX;

        protected void Page_Load(object sender, EventArgs e)
        {
            //return to mon loc listing if none in session
            if (Session["MonLocIDX"] == null || Session["OrgID"] == null)
                Response.Redirect("~/App_Pages/Secure/WQXMonLoc.aspx");

            //read session variables
            MonLocIDX = Int32.Parse(Session["MonLocIDX"].ToString());

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkMonLocList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";
                
                //populate drop-downs                
                Utils.BindList(ddlMonLocType, dsRefData, "VALUE", "TEXT");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "HorizontalCollectionMethod";
                Utils.BindList(ddlHorizMethod, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "HorizontalCoordinateReferenceSystemDatum";
                Utils.BindList(ddlHorizDatum, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "MeasureUnit";
                Utils.BindList(ddlVertUnit, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "VerticalCollectionMethod";
                Utils.BindList(ddlVertMethod, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "VerticalCoordinateReferenceSystemDatum";
                Utils.BindList(ddlVertDatum, dsRefData, "VALUE", "VALUE");              
                dsRefData.SelectParameters["tABLE"].DefaultValue = "State";
                Utils.BindList(ddlState, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "Country";
                Utils.BindList(ddlCountry, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "WellType";
                Utils.BindList(ddlWellType, dsRefData, "VALUE", "VALUE");


                //******************* Populate monitoring location information on form
                T_WQX_MONLOC m = db_WQX.GetWQX_MONLOC_ByID(MonLocIDX);
                if (m != null)
                {
                    lblMonLocIDX.Text = m.MONLOC_IDX.ToString();
                    txtMonLocID.Text = m.MONLOC_ID;
                    txtMonLocName.Text = m.MONLOC_NAME;
                    ddlMonLocType.SelectedValue = m.MONLOC_TYPE;
                    txtMonLocDesc.Text = m.MONLOC_DESC;
                    txtHUC8.Text = m.HUC_EIGHT;
                    txtHUC12.Text = m.HUC_TWELVE;
                    chkLandInd.Checked = (m.TRIBAL_LAND_IND == "Y");
                    txtLandName.Text = m.TRIBAL_LAND_NAME;
                    txtLatitude.Text = m.LATITUDE_MSR;
                    txtLongitude.Text = m.LONGITUDE_MSR;
                    txtSourceMapScale.Text = m.SOURCE_MAP_SCALE.ToString();
                    ddlHorizMethod.SelectedValue = m.HORIZ_COLL_METHOD;
                    ddlHorizDatum.SelectedValue = m.HORIZ_REF_DATUM;
                    txtVertMeasure.Text = m.VERT_MEASURE;
                    ddlVertUnit.SelectedValue = m.VERT_MEASURE_UNIT;
                    ddlVertMethod.SelectedValue = m.VERT_COLL_METHOD;
                    ddlVertDatum.SelectedValue = m.VERT_REF_DATUM;
                    ddlState.SelectedValue = m.STATE_CODE;

                    //county handling
                    dsCounty.SelectParameters["StateCode"].DefaultValue = ddlState.SelectedValue;
                    Utils.BindList(ddlCounty, dsCounty, "COUNTY_CODE", "COUNTY_NAME");
                    ddlCounty.SelectedValue = m.COUNTY_CODE;

                    ddlCountry.SelectedValue = m.COUNTRY_CODE;
                    ddlWellType.SelectedValue = m.WELL_TYPE;
                    txtAquifer.Text = m.AQUIFER_NAME;

                    if (m.WQX_IND != null)
                        chkWQXInd.Checked = (bool)m.WQX_IND;
                    if (m.ACT_IND != null)
                        chkActInd.Checked = (bool)m.ACT_IND;
                }
                else
                {
                    chkActInd.Checked = true;
                    chkWQXInd.Checked = true;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //***************** data validation start *************************
            if (string.IsNullOrEmpty(txtMonLocID.Text))
                lblMsg.Text = "Monitoring Location ID is required.";

            //***************** data validation end *************************

            //save updates to Mon Loc
            int SuccID = db_WQX.InsertOrUpdateWQX_MONLOC(lblMonLocIDX.Text.ConvertOrDefault<int?>(), Session["OrgID"].ToString(), txtMonLocID.Text, txtMonLocName.Text, 
                ddlMonLocType.SelectedValue, txtMonLocDesc.Text, txtHUC8.Text, txtHUC12.Text, null, txtLandName.Text, txtLatitude.Text, txtLongitude.Text, 
                txtSourceMapScale.Text.ConvertOrDefault<int?>(), null, null, ddlHorizMethod.SelectedValue, ddlHorizDatum.SelectedValue, txtVertMeasure.Text,
                ddlVertUnit.SelectedValue, ddlVertMethod.SelectedValue, ddlVertDatum.SelectedValue, ddlCountry.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue, 
                ddlWellType.SelectedValue, txtAquifer.Text, null, null, null, "U", null, chkActInd.Checked, chkWQXInd.Checked, User.Identity.Name);

            if (SuccID > 0)
            {
                Response.Redirect("~/App_Pages/Secure/WQXMonLoc.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
                lblMsg.Text = "Error updating record.";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXMonLoc.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue.Length > 0)
            {
                dsCounty.SelectParameters["StateCode"].DefaultValue = ddlState.SelectedValue;
                Utils.BindList(ddlCounty, dsCounty, "COUNTY_CODE", "COUNTY_NAME");
            }

        }
    }
}