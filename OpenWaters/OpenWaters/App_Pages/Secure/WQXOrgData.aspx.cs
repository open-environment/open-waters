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
    public partial class WQXOrgData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkOrgList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate dropdown on translation modal
                List<string> ds = Utils.GetAllColumnBasic("S");
                foreach (string ds1 in ds)
                    ddlField.Items.Add(new ListItem(ds1, ds1));

                //populate drop-downs                
                Utils.BindList(ddlTimeZone, dsTimeZone, "TIME_ZONE_NAME", "TIME_ZONE_NAME");
                Utils.BindList(ddlChar, dsChar, "CHAR_NAME", "CHAR_NAME");
                Utils.BindList(ddlAnalMethod, dsAnalMethod, "ANALYTIC_METHOD_IDX", "AnalMethodDisplayName");

                dsTaxa.SelectParameters["tABLE"].DefaultValue = "MeasureUnit";
                Utils.BindList(ddlUnit, dsTaxa, "VALUE", "VALUE");

                dsTaxa.SelectParameters["tABLE"].DefaultValue = "ResultSampleFraction";
                Utils.BindList(ddlFraction, dsTaxa, "VALUE", "VALUE");

                dsTaxa.SelectParameters["tABLE"].DefaultValue = "ResultStatus";
                Utils.BindList(ddlStatus, dsTaxa, "VALUE", "VALUE");

                dsTaxa.SelectParameters["tABLE"].DefaultValue = "ResultValueType";
                Utils.BindList(ddlValueType, dsTaxa, "VALUE", "VALUE");

                hdnOrgID.Value = Session["OrgID"].ToString();

                PopulateTabsData();
            }
        }

        private void PopulateTabsData()
        {
            string OrgID = hdnOrgID.Value.ToString();

            //tab 1
            T_WQX_ORGANIZATION o = db_WQX.GetWQX_ORGANIZATION_ByID(OrgID);
            if (o != null)
                ddlTimeZone.SelectedValue = o.DEFAULT_TIMEZONE;

            //tab 2
            PopulateCharTab(OrgID);

            //tab 3
            PopulateTaxaGrid(OrgID);

            //tab 4
            grdTranslate.DataSource = db_WQX.GetWQX_IMPORT_TRANSLATE_byOrg(OrgID);
            grdTranslate.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }


        //***************************TAB 1**********************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(Session["OrgID"].ToString(), null, null, null, null, null, null, null, null, null, null, null, ddlTimeZone.SelectedValue, User.Identity.Name) == 1)
                lblMsg.Text = "Data saved";
            else
                lblMsg.Text = "Error encountered";

            hdnSelectedTab.Value = "1";

        }


        //***************************TAB 2: org characteristics**********************************
        private void PopulateCharTab(string OrgID)
        {
            grdChar.DataSource = db_Ref.GetT_WQX_REF_CHAR_ORG(OrgID);
            grdChar.DataBind();
        }

        protected void btnAddChar_Click(object sender, EventArgs e)
        {
            ddlChar.SelectedValue = "";
            txtDetectLimit.Text = "";
            txtQuantLower.Text = "";
            txtQuantUpper.Text = "";
            ddlUnit.SelectedValue = "";
            ddlAnalMethod.SelectedValue = "";
            ddlFraction.SelectedValue = "";
            ddlStatus.SelectedValue = "";
            ddlValueType.SelectedValue = "";

            MPE_NewChar.Show();
        }

        protected void grdChar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string OrgID = hdnOrgID.Value.ToString();
            string CharName = e.CommandArgument.ToString();

            if (e.CommandName == "Deletes")
            {
                db_Ref.DeleteT_WQX_REF_CHAR_ORG(OrgID, CharName);
                PopulateCharTab(OrgID);
            }

            if (e.CommandName == "Select")
            {
                T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(OrgID, CharName);
                if (rco != null)
                {
                    ddlChar.SelectedValue = rco.CHAR_NAME;
                    txtDetectLimit.Text = rco.DEFAULT_DETECT_LIMIT;
                    txtQuantLower.Text = rco.DEFAULT_LOWER_QUANT_LIMIT;
                    txtQuantUpper.Text = rco.DEFAULT_UPPER_QUANT_LIMIT;
                    ddlUnit.SelectedValue = rco.DEFAULT_UNIT;
                    ddlAnalMethod.SelectedValue = rco.DEFAULT_ANAL_METHOD_IDX.ToString();
                    ddlFraction.SelectedValue = rco.DEFAULT_SAMP_FRACTION;
                    ddlStatus.SelectedValue = rco.DEFAULT_RESULT_STATUS;
                    ddlValueType.SelectedValue = rco.DEFAULT_RESULT_VALUE_TYPE;

                    MPE_NewChar.Show();
                }
                else
                    lblMsg.Text = "Unable to edit record.";
            }


            hdnSelectedTab.Value = "2";

        }

        protected void btnAddChar2_Click(object sender, EventArgs e)
        {
            db_Ref.InsertOrUpdateT_WQX_REF_CHAR_ORG(ddlChar.SelectedValue, hdnOrgID.Value.ToString(), User.Identity.Name, txtDetectLimit.Text, ddlUnit.SelectedValue,
                ddlAnalMethod.SelectedValue.ConvertOrDefault<int?>(), ddlFraction.SelectedValue, ddlStatus.SelectedValue, ddlValueType.SelectedValue, txtQuantLower.Text, txtQuantUpper.Text);

            lblMsg.Text = "Updated Successfully";

            PopulateCharTab(hdnOrgID.Value.ToString());

            hdnSelectedTab.Value = "2";
        }


        //****************************TAB 3: TAXA ********************************
        private void PopulateTaxaGrid(string OrgID)
        {
            grdTaxa.DataSource = db_Ref.GetT_WQX_REF_TAXA_ORG(OrgID);
            grdTaxa.DataBind();
        }

        protected void btnAddTaxa_Click(object sender, EventArgs e)
        {
            if (ddlTaxa.Visible == false)
            {
                ddlTaxa.Visible = true;
                dsTaxa.SelectParameters["tABLE"].DefaultValue = "Taxon";
                Utils.BindList(ddlTaxa, dsTaxa, "VALUE", "VALUE");
            }
            else
            {
                if (ddlTaxa.SelectedValue.Length > 0)
                {
                    db_Ref.InsertOrUpdateT_WQX_REF_TAXA_ORG(ddlTaxa.SelectedValue, hdnOrgID.Value.ToString(), User.Identity.Name);
                    PopulateTaxaGrid(hdnOrgID.Value.ToString());
                }
            }

            hdnSelectedTab.Value = "3";
        }

        protected void grdTaxa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                db_Ref.DeleteT_WQX_REF_TAXA_ORG(hdnOrgID.Value.ToString(), e.CommandArgument.ToString());
                PopulateTaxaGrid(hdnOrgID.Value.ToString());
            }

            hdnSelectedTab.Value = "3";
        }




        //****************************TAB 4: TRANSLATIONS ********************************
        protected void btnAddTranslate_Click(object sender, EventArgs e)
        {
            hdnSelectedTab.Value = "4";

            txtFrom.Text = "";
            txtTo.Text = "";
            MPE_Translate.Show();
        }

        protected void grdTranslate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int TransID = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "Deletes")
            {
                int SuccID = db_WQX.DeleteT_WQX_IMPORT_TRANSLATE(TransID);
                lblMsg.Text = (SuccID == 1) ? "Record successfully deleted" : "Unable to delete translation";
                PopulateTabsData();
            }

            hdnSelectedTab.Value = "4";
        }

        protected void btnAddTranslate2_Click(object sender, EventArgs e)
        {
            if (ddlField.SelectedValue != "")
            {
                int SuccID = db_WQX.InsertOrUpdateWQX_IMPORT_TRANSLATE(null, Session["OrgID"].ToString(), ddlField.SelectedValue, txtFrom.Text, txtTo.Text, User.Identity.Name);
                lblMsg.Text = (SuccID > 0) ? "Record successfully added" : "Unable to add translation";
                PopulateTabsData();
            }

            hdnSelectedTab.Value = "4";
        }

    }
}