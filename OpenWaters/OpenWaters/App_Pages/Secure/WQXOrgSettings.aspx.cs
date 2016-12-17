using System;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class WQXOrgSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //return to org listing if none in session
            if (Session["OrgID"] == null)
                Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");

            if (!IsPostBack)
            {
                //display left menu as selected 
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkOrgList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate drop-downs                
                Utils.BindList(ddlTimeZone, dsTimeZone, "TIME_ZONE_NAME", "TIME_ZONE_NAME");


                //******************* Populate organization information on form
                T_WQX_ORGANIZATION o = db_WQX.GetWQX_ORGANIZATION_ByID(Session["OrgID"].ToString());
                if (o != null)
                {
                    txtOrgID.Text = o.ORG_ID;
                    txtOrgID.ReadOnly = true;
                    ddlTimeZone.SelectedValue = o.DEFAULT_TIMEZONE;
                }

                PopCharOrTaxa();
            }

        }

        protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopCharOrTaxa();
        }

        private void PopCharOrTaxa()
        {
            pnlChars.Visible = (rbType.SelectedValue == "C");
            pnlTaxa.Visible = (rbType.SelectedValue == "T");

            //populate Org Characteristics and Taxa grids
            if (rbType.SelectedValue == "C")
            {
                if (ddlChar.Items.Count == 0)
                    Utils.BindList(ddlChar, dsChar, "CHAR_NAME", "CHAR_NAME");

                PopulateCharGrid();

                //Unit
                dsTaxa.SelectParameters["tABLE"].DefaultValue = "MeasureUnit";
                Utils.BindList(ddlUnit, dsTaxa, "VALUE", "VALUE");

                Utils.BindList(ddlAnalMethod, dsAnalMethod, "ANALYTIC_METHOD_IDX", "AnalMethodDisplayName");

                dsTaxa.SelectParameters["tABLE"].DefaultValue = "ResultSampleFraction";
                Utils.BindList(ddlFraction, dsTaxa, "VALUE", "VALUE");

                dsTaxa.SelectParameters["tABLE"].DefaultValue = "ResultStatus";
                Utils.BindList(ddlStatus, dsTaxa, "VALUE", "VALUE");

                dsTaxa.SelectParameters["tABLE"].DefaultValue = "ResultValueType";
                Utils.BindList(ddlValueType, dsTaxa, "VALUE", "VALUE");

            }

            if (rbType.SelectedValue == "T")
            {
                if (ddlTaxa.Items.Count == 0)
                    Utils.BindList(ddlTaxa, dsTaxa, "VALUE", "VALUE");
                PopulateTaxaGrid();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrgEdit.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(txtOrgID.Text, null, null, null, null, null, null, null, null, null, null, null, ddlTimeZone.SelectedValue, User.Identity.Name) == 1)
                lblMsg.Text = "Data saved";
            else
                lblMsg.Text = "Error encountered";
        }


        //***************************org characteristics**********************************
        private void PopulateCharGrid()
        {
            grdChar.DataSource = db_Ref.GetT_WQX_REF_CHAR_ORG(txtOrgID.Text);
            grdChar.DataBind();
        }

        protected void grdChar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CharName = e.CommandArgument.ToString();

            if (e.CommandName == "Deletes")
            {
                db_Ref.DeleteT_WQX_REF_CHAR_ORG(txtOrgID.Text, CharName);
                PopulateCharGrid();
            }

            if (e.CommandName == "Select")
            {                
                T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(Session["OrgID"].ToString(), CharName);
                if (rco != null)
                {
                    pnlSelChar.Visible = true;

                    txtSelChar.Text = rco.CHAR_NAME;
                    txtDetectLimit.Text = rco.DEFAULT_DETECT_LIMIT;
                    txtQuantLower.Text = rco.DEFAULT_LOWER_QUANT_LIMIT;
                    txtQuantUpper.Text = rco.DEFAULT_UPPER_QUANT_LIMIT;
                    ddlUnit.SelectedValue = rco.DEFAULT_UNIT;
                    ddlAnalMethod.SelectedValue = rco.DEFAULT_ANAL_METHOD_IDX.ToString();
                    ddlFraction.SelectedValue = rco.DEFAULT_SAMP_FRACTION;
                    ddlStatus.SelectedValue = rco.DEFAULT_RESULT_STATUS;
                    ddlValueType.SelectedValue = rco.DEFAULT_RESULT_VALUE_TYPE;
                }
            }
        }

        protected void btnAddChar_Click(object sender, EventArgs e)
        {
            db_Ref.InsertOrUpdateT_WQX_REF_CHAR_ORG(ddlChar.SelectedValue, txtOrgID.Text, User.Identity.Name, null, null, null, null, null, null, null, null);
            PopulateCharGrid();
        }

        //**************************org taxa****************************************************
        private void PopulateTaxaGrid()
        {
            grdTaxa.DataSource = db_Ref.GetT_WQX_REF_TAXA_ORG(txtOrgID.Text);
            grdTaxa.DataBind();
        }

        protected void btnAddTaxa_Click(object sender, EventArgs e)
        {
            db_Ref.InsertOrUpdateT_WQX_REF_TAXA_ORG(ddlTaxa.SelectedValue, txtOrgID.Text, User.Identity.Name);
            PopulateTaxaGrid();
        }

        protected void grdTaxa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                db_Ref.DeleteT_WQX_REF_TAXA_ORG(txtOrgID.Text, e.CommandArgument.ToString());
                PopulateTaxaGrid();
            }

        }

        protected void btnSaveDtl_Click(object sender, EventArgs e)
        {
            db_Ref.InsertOrUpdateT_WQX_REF_CHAR_ORG(txtSelChar.Text, Session["OrgID"].ToString(), User.Identity.Name, txtDetectLimit.Text, ddlUnit.SelectedValue,
                ddlAnalMethod.SelectedValue.ConvertOrDefault<int?>(), ddlFraction.SelectedValue, ddlStatus.SelectedValue, ddlValueType.SelectedValue, txtQuantLower.Text, txtQuantUpper.Text);
            lblMsgDtl.Text = "Updated Successfully";

            PopulateCharGrid();
        }

    }
}