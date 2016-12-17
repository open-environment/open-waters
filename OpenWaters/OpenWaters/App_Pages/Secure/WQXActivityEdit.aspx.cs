using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class WQXActivityEdit : System.Web.UI.Page
    {
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in grdResults.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && row.RowState.HasFlag(DataControlRowState.Edit) == false)
                {
                    // enable click on row to enter edit mode 
                    row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(grdResults, "Edit$" + row.DataItemIndex, true);
                }
            }
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ActivityIDX"] == null || Session["OrgID"] == null || Session["UserIDX"] == null)
                Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkActivityList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //display warning if no reference data imported yet
                if (db_Ref.GetT_WQX_REF_DATA_Count() == 0)
                {
                    lblMsg.Text = "You must import reference data from EPA before you can enter sample data.";
                    return;
                }

                //redirect to org settings page if no org chars defined yet
                if (db_Ref.GetT_WQX_REF_CHAR_ORG_Count(Session["OrgID"].ToString()) == 0)
                {
                    lblMsg.Text = "You must define the characteristics that your organization will use (Organization screen) before you can enter sample data.";
                    return;
                }

                //ONLY ALLOW EDIT FOR AUTHORIZED USERS OF ORG
                btnSave.Visible = db_WQX.CanUserEditOrg(Session["UserIDX"].ConvertOrDefault<int>(), Session["OrgID"].ToString());

                //populate drop-downs - sample level
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ActivityType";
                Utils.BindList(ddlActivityType, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ActivityMedia";
                Utils.BindList(ddlActMedia, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ActivityMediaSubdivision";
                Utils.BindList(ddlActSubMedia, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "SampleCollectionEquipment";
                Utils.BindList(ddlEquip, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "Assemblage";
                Utils.BindList(ddlAssemblage, dsRefData, "VALUE", "VALUE");
                Utils.BindList(ddlMonLoc, dsMonLoc, "MONLOC_IDX", "MONLOC_ID");
                Utils.BindList(ddlProject, dsProject, "PROJECT_IDX", "PROJECT_ID");
                Utils.BindList(ddlSampColl, dsSampColl, "SAMP_COLL_METHOD_IDX", "SAMP_COLL_METHOD_ID");

                //populate activity data
                T_WQX_ACTIVITY a = db_WQX.GetWQX_ACTIVITY_ByID(Session["ActivityIDX"].ConvertOrDefault<int>());
                if (a != null)
                {
                    hdnActivityIDX.Value = a.ACTIVITY_IDX.ToString();
                    txtActivityID.Text = a.ACTIVITY_ID;
                    txtStartDate.Text = a.ACT_START_DT.ToString("MM/dd/yyyy hh:mm tt");
                    txtTimeZone.Text = a.ACT_TIME_ZONE;
                    txtTimeZone.Visible = (a.ACT_TIME_ZONE != null);
                    ddlMonLoc.SelectedValue = a.MONLOC_IDX.ToString();
                    ddlProject.SelectedValue = a.PROJECT_IDX.ToString();
                    if (a.ACT_IND != null) chkActInd.Checked = (bool)a.ACT_IND;
                    if (a.WQX_IND != null) chkWQXInd.Checked = (bool)a.WQX_IND;
                    if (a.CREATE_DT != null) lblModifyDate.Text = "Last modified: " + a.CREATE_DT.ToString();
                    if (a.UPDATE_DT != null) lblModifyDate.Text = "Last modified: " + a.UPDATE_DT.ToString();

                    //populate activity tab
                    ddlActivityType.SelectedValue = a.ACT_TYPE;
                    ddlActMedia.SelectedValue = a.ACT_MEDIA;
                    ddlActSubMedia.SelectedValue = a.ACT_SUBMEDIA;
                    txtEndDate.Text = (a.ACT_END_DT != null) ? ((DateTime)a.ACT_END_DT).ToString("MM/dd/yyyy hh:mm tt") : "";
                    txtDepth.Text = a.ACT_DEPTHHEIGHT_MSR;
                    ddlDepthUnit.SelectedValue = a.ACT_DEPTHHEIGHT_MSR_UNIT;
                    ddlEquip.SelectedValue = a.SAMP_COLL_EQUIP;
                    ddlSampColl.SelectedValue = a.SAMP_COLL_METHOD_IDX.ToString();
                    txtActComments.Text = a.ACT_COMMENT;

                    //populate bio tab
                    ddlAssemblage.SelectedValue = a.BIO_ASSEMBLAGE_SAMPLED;
                    txtBioDuration.Text = a.BIO_DURATION_MSR;
                    ddlBioDurUnit.SelectedValue = a.BIO_DURATION_MSR_UNIT;
                    txtSamplingComponent.Text = a.BIO_SAMP_COMPONENT;
                    txtSampComponentSeq.Text = (a.BIO_SAMP_COMPONENT_SEQ != null ? a.BIO_SAMP_COMPONENT_SEQ.ToString() : "");

                    ddlEntryType.SelectedValue = (a.ENTRY_TYPE ?? "C");

                    //populate results
                    PopulateResultsGrid();
                }
                else
                {
                    UpdatePanel1.Visible = false;
                    pnlMetrics.Visible = false;
                    pnlResultBtn.Visible = false;
                }

                txtActivityID.Focus();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? ActID = (Session["ActivityIDX"].ToString() == "-1" ? null : Session["ActivityIDX"].ConvertOrDefault<int?>());

            //update activity and set WQX Indicator to U so it might get sent to EPA again
            int SuccID = db_WQX.InsertOrUpdateWQX_ACTIVITY(ActID, Session["OrgID"].ToString(), ddlProject.SelectedValue.ConvertOrDefault<int>(), ddlMonLoc.SelectedValue.ConvertOrDefault<int>(), txtActivityID.Text,
                ddlActivityType.SelectedValue, ddlActMedia.SelectedValue, ddlActSubMedia.SelectedValue, txtStartDate.Text.ConvertOrDefault<DateTime?>(), txtEndDate.Text.ConvertOrDefault<DateTime?>(), null,
                null, txtDepth.Text, ddlDepthUnit.SelectedValue, null, null, null, null, null, txtActComments.Text,
                ddlAssemblage.SelectedValue, txtBioDuration.Text, ddlBioDurUnit.SelectedValue, txtSamplingComponent.Text, txtSampComponentSeq.Text.ConvertOrDefault<int?>(), 
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, ddlSampColl.SelectedValue.ConvertOrDefault<int?>(), ddlEquip.SelectedValue, null, null, null, null, null, null, null,
                "U", chkActInd.Checked, chkWQXInd.Checked, User.Identity.Name, ddlEntryType.SelectedValue);

            if (SuccID > 0)
            {
                lblMsg.Text = "Sample saved successfully.";
                UpdatePanel1.Visible = true;
                Session.Add("ActivityIDX", SuccID);

                //populate results
                PopulateResultsGrid();
            }
            else
                lblMsg.Text = "Error updating record.";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");
        }

        protected void ddlEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            db_WQX.UpdateWQX_ACTIVITY_EntryType(hdnActivityIDX.Value.ConvertOrDefault<int?>(), ddlEntryType.SelectedValue);
            PopulateResultsGrid();
        }

        //RESULTS HANDLING ************************************************************************************
        private void PopulateResultsGrid()
        {
            try
            {
                //display metrics grid
                pnlMetrics.Visible = grdMetrics.Rows.Count > 0 || ddlEntryType.SelectedValue == "H" || ddlEntryType.SelectedValue == "T";

                //allow edit for some (last column)
                grdResults.Columns[grdResults.Columns.Count-1].Visible = btnSave.Visible;
               
                //taxon counts display 
                grdResults.Columns[2].Visible = ddlEntryType.SelectedValue == "T";
                grdResults.Columns[14].Visible = ddlEntryType.SelectedValue == "T";
                grdResults.Columns[15].Visible = ddlEntryType.SelectedValue == "T";


                grdResults.ShowFooter = true;
                grdResults.DataSource = db_WQX.GetT_WQX_RESULT(Session["ActivityIDX"].ConvertOrDefault<int>());
                grdResults.DataBind();

                //if there is no data, then fill in an empty row and hide the footer (stupid work around)
                if (grdResults.Rows.Count == 0)
                {
                    List<T_WQX_RESULT> rows = new List<T_WQX_RESULT>();
                    T_WQX_RESULT row = new T_WQX_RESULT();
                    row.ACTIVITY_IDX = Session["ActivityIDX"].ConvertOrDefault<int>();
                    row.CHAR_NAME = "";
                    row.DETECTION_LIMIT = "";
                    row.RESULT_MSR = "";
                    row.RESULT_MSR_UNIT = "";
                    rows.Add(row);

                    grdResults.ShowFooter = false;
                    grdResults.DataSource = rows;
                    grdResults.DataBind();
                }
            }
            catch
            {
                lblMsg.Text = "Error displaying results.";
            }
        }

        protected void grdResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            db_WQX.DeleteT_WQX_RESULT(grdResults.DataKeys[e.RowIndex].Values[0].ConvertOrDefault<int>());
            PopulateResultsGrid();
        }

        protected void grdResults_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdResults.EditIndex = e.NewEditIndex;
            PopulateResultsGrid();
        }

        /// Update an existing result record.
        /// </summary>
        protected void grdResults_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMsgDtl.Text = "";

            int ActID = Session["ActivityIDX"].ConvertOrDefault<int>();

            DropDownList ddlChar = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlChar");
            DropDownList ddlTaxa = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlTaxa");
            TextBox txtResultVal = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtResultVal");
            DropDownList ddlUnit = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlUnit");
            DropDownList ddlAnalMethod = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlAnalMethod");
            TextBox txtDetectLimit = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtDetectLimit");
            TextBox txtPQL = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtPQL");
            TextBox txtLowerQuantLimit = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtLowerQuantLimit");
            TextBox txtUpperQuantLimit = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtUpperQuantLimit");
    
            DropDownList ddlSampFraction = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlSampFraction");
            DropDownList ddlResultValueType = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlResultValueType");
            DropDownList ddlResultStatus = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlResultStatus");
            TextBox txtAnalysisDate = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtAnalysisDate");

            DropDownList ddlBioIntent = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlBioIntent");
            DropDownList ddlFreqClass = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlFreqClass");

            TextBox txtComment = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtComment");

            //*********************VALIDATION****************************************
            double num;
            if (!(double.TryParse(txtResultVal.Text, out num) || txtResultVal.Text == "ND" || txtResultVal.Text =="NR" || txtResultVal.Text == "PAQL" || txtResultVal.Text == "PBQL" || txtResultVal.Text == "DNQ"))
            {
                lblMsgDtl.Text = "Result must be numeric, or one of the following values: ND, NR, PAQL, PBQL, DNQ";
                return;
            }

            //if numeric, then check if result is within valid range (if available for the char/unit pairing)
            if (double.TryParse(txtResultVal.Text, out num))
            {
                T_WQX_REF_CHAR_LIMITS cl = db_Ref.GetT_WQX_REF_CHAR_LIMITS_ByNameUnit(ddlChar.SelectedValue, ddlUnit.SelectedValue);
                if (cl != null)
                {
                    if ((decimal)num < cl.LOWER_BOUND || (decimal)num > cl.UPPER_BOUND)
                    {
                        lblMsgDtl.Text = "Result value is outside acceptable range. " + cl.LOWER_BOUND + " - " + cl.UPPER_BOUND + " Please provide another value and save again.";
                        return;
                    }
                }
            }
            //*********************END VALIDATION************************************

            int SuccID = db_WQX.InsertOrUpdateT_WQX_RESULT(grdResults.DataKeys[e.RowIndex].Values[0].ConvertOrDefault<int>(), ActID, null, ddlChar.SelectedValue,
                ddlSampFraction.SelectedValue, txtResultVal.Text, ddlUnit.SelectedValue, 
                ddlResultStatus.SelectedValue, ddlResultValueType.SelectedValue, txtComment.Text, null, null, ddlTaxa.SelectedValue, null, 
                ddlAnalMethod.SelectedValue.ConvertOrDefault<int?>(), ddlLabName.SelectedValue.ConvertOrDefault<int?>(), txtAnalysisDate.Text.ConvertOrDefault<DateTime?>(), txtDetectLimit.Text, txtPQL.Text,
                txtLowerQuantLimit.Text, txtUpperQuantLimit.Text, ddlPrepMethod.SelectedValue.ConvertOrDefault<int?>(), txtPrepStartDate.Text.ConvertOrDefault<DateTime?>(), txtDilution.Text, 
                ddlFreqClass.SelectedValue, null, User.Identity.Name);

            if (SuccID > 0)
            {
                //also update activity to set to "U" so it will be flagged for submission to EPA
                db_WQX.UpdateWQX_ACTIVITY_WQXStatus(ActID, "U", null, null, User.Identity.Name);

                grdResults.EditIndex = -1;
                PopulateResultsGrid();
            }
            else
                lblMsgDtl.Text = "Error saving update.";
        }

        /// Add a new result. 
        /// </summary>
        protected void grdResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                lblMsgDtl.Text = "";

                int ActID = Session["ActivityIDX"].ConvertOrDefault<int>();
                DropDownList ddlNewChar = (DropDownList)grdResults.FooterRow.FindControl("ddlNewChar");
                DropDownList ddlNewTaxa = (DropDownList)grdResults.FooterRow.FindControl("ddlNewTaxa");
                TextBox txtNewResultVal = (TextBox)grdResults.FooterRow.FindControl("txtNewResultVal");
                DropDownList ddlNewUnit = (DropDownList)grdResults.FooterRow.FindControl("ddlNewUnit");
                DropDownList ddlNewAnalMethod = (DropDownList)grdResults.FooterRow.FindControl("ddlNewAnalMethod");
                TextBox txtNewDetectLimit = (TextBox)grdResults.FooterRow.FindControl("txtNewDetectLimit");
                TextBox txtNewPQL = (TextBox)grdResults.FooterRow.FindControl("txtNewPQL");
                TextBox txtNewLowerQuantLimit = (TextBox)grdResults.FooterRow.FindControl("txtNewLowerQuantLimit");
                TextBox txtNewUpperQuantLimit = (TextBox)grdResults.FooterRow.FindControl("txtNewUpperQuantLimit");


                DropDownList ddlNewSampFraction = (DropDownList)grdResults.FooterRow.FindControl("ddlNewSampFraction");
                DropDownList ddlNewResultValueType = (DropDownList)grdResults.FooterRow.FindControl("ddlNewResultValueType");
                DropDownList ddlNewResultStatus = (DropDownList)grdResults.FooterRow.FindControl("ddlNewResultStatus");
                TextBox txtNewAnalysisDate = (TextBox)grdResults.FooterRow.FindControl("txtNewAnalysisDate");

                DropDownList ddlNewBioIntent = (DropDownList)grdResults.FooterRow.FindControl("ddlNewBioIntent");
                DropDownList ddlNewFreqClass = (DropDownList)grdResults.FooterRow.FindControl("ddlNewFreqClass");

                TextBox txtNewComment = (TextBox)grdResults.FooterRow.FindControl("txtNewComment");

                double num;
                if (!(double.TryParse(txtNewResultVal.Text, out num) || txtNewResultVal.Text == "ND" || txtNewResultVal.Text == "NR" || txtNewResultVal.Text == "PAQL" || txtNewResultVal.Text == "PBQL" || txtNewResultVal.Text == "DNQ"))
                {
                    lblMsgDtl.Text = "Result must be numeric, or one of the following values: ND, NR, PAQL, PBQL, DNQ";
                    return;
                }

                //if numeric, then check if result is within valid range (if available for the char/unit pairing)
                if (double.TryParse(txtNewResultVal.Text, out num))
                {
                    T_WQX_REF_CHAR_LIMITS cl = db_Ref.GetT_WQX_REF_CHAR_LIMITS_ByNameUnit(ddlNewChar.SelectedValue, ddlNewUnit.SelectedValue);
                    if (cl != null)
                    {
                        if ((decimal)num < cl.LOWER_BOUND || (decimal)num > cl.UPPER_BOUND)
                        {
                            lblMsgDtl.Text = "Result value is outside acceptable range. Please provide another value and save again.";
                            return;
                        }
                    }
                }
                //*********************END VALIDATION************************************

                db_WQX.InsertOrUpdateT_WQX_RESULT(null, ActID, null, ddlNewChar.SelectedValue, ddlNewSampFraction.SelectedValue,
                    txtNewResultVal.Text, ddlNewUnit.SelectedValue, ddlNewResultStatus.SelectedValue, ddlNewResultValueType.SelectedValue, txtNewComment.Text,  
                    ddlNewBioIntent.SelectedValue, null, ddlNewTaxa.SelectedValue, null, ddlNewAnalMethod.SelectedValue.ConvertOrDefault<int?>(), null, txtNewAnalysisDate.Text.ConvertOrDefault<DateTime?>(), 
                    txtNewDetectLimit.Text, txtNewPQL.Text, txtNewLowerQuantLimit.Text, txtNewUpperQuantLimit.Text, null, null, null, ddlNewFreqClass.SelectedValue, null, User.Identity.Name);

                //also update activity to set to "U" so it will be flagged for submission to EPA
                db_WQX.UpdateWQX_ACTIVITY_WQXStatus(ActID, "U", chkActInd.Checked, chkWQXInd.Checked);

                grdResults.FooterRow.Visible = false;
                PopulateResultsGrid();
            }

        }

        //population of drop-downs for the currently selected row
        protected void grdResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && 
                ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit))
            {
                //populate more modal
                Utils.BindList(ddlLabName, dsLabName, "LAB_IDX", "LAB_NAME");
                Utils.BindList(ddlPrepMethod, dsPrepMethod, "SAMP_PREP_IDX", "SAMP_PREP_METHOD_ID");

                T_WQX_RESULT r = db_WQX.GetT_WQX_RESULT_ByIDX(grdResults.DataKeys[e.Row.RowIndex].Values[0].ConvertOrDefault<int>());
                if (r != null)
                {
                    ddlLabName.SelectedValue = r.LAB_IDX.ToStringNullSafe();
                    ddlPrepMethod.SelectedValue = r.LAB_SAMP_PREP_IDX.ToStringNullSafe();
                    txtPrepStartDate.Text = r.LAB_SAMP_PREP_START_DT.ToStringNullSafe();
                    txtDilution.Text = r.DILUTION_FACTOR.ToStringNullSafe();
                }

                //Characteristic
                DropDownList d1 = (DropDownList)e.Row.FindControl("ddlChar");
                dsChar.SelectParameters["RBPInd"].DefaultValue = (ddlEntryType.SelectedValue == "H" ? "true" : "false");
                PopulateDropDown(d1, dsChar, grdResults.DataKeys[e.Row.RowIndex].Values[1].ToString(), "CHAR_NAME", "CHAR_NAME");
                d1.Focus(); 

                //Taxa
                int col = GetColumnIndexByName(grdResults,"Taxonomy");
                if (grdResults.Columns[col].Visible)
                {
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlTaxa"), dsTaxa, grdResults.DataKeys[e.Row.RowIndex].Values[1].ToStringNullSafe(), "VALUE", "TEXT");
                }

                //Unit
                dsRefData.SelectParameters["tABLE"].DefaultValue = "MeasureUnit";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlUnit"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[2].ToStringNullSafe(), "VALUE", "VALUE");

                //Anal Method
                col = GetColumnIndexByName(grdResults, "Analytical Method");
                if (grdResults.Columns[col].Visible)
                {
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlAnalMethod"), dsAnalMethod, grdResults.DataKeys[e.Row.RowIndex].Values[3].ToStringNullSafe(), "ANALYTIC_METHOD_IDX", "AnalMethodDisplayName");
                }

                //Samp Fraction
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ResultSampleFraction";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlSampFraction"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[4].ToStringNullSafe(), "VALUE", "VALUE");

                //value type
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ResultValueType";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlResultValueType"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[5].ToStringNullSafe(), "VALUE", "VALUE");

                //result status
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ResultStatus";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlResultStatus"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[6].ToStringNullSafe(), "VALUE", "VALUE");

                
                //Bio Intent
                col = GetColumnIndexByName(grdResults, "Biological Intent");
                if (grdResults.Columns[col].Visible)
                {
                    dsRefData.SelectParameters["tABLE"].DefaultValue = "BiologicalIntent";
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlBioIntent"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[7].ToStringNullSafe(), "VALUE", "VALUE");
                }


                //Bio Intent
                col = GetColumnIndexByName(grdResults, "Frequency Class");
                if (grdResults.Columns[col].Visible)
                {
                    dsRefData.SelectParameters["tABLE"].DefaultValue = "FrequencyClassDescriptor";
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlFreqClass"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[8].ToStringNullSafe(), "VALUE", "VALUE");
                }


            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Characteristic
                dsChar.SelectParameters["RBPInd"].DefaultValue = (ddlEntryType.SelectedValue == "H" ? "true" : "false");
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewChar"), dsChar, null, "CHAR_NAME", "CHAR_NAME");

                //Taxa
                int col = GetColumnIndexByName(grdResults,"Taxonomy");
                if (grdResults.Columns[col].Visible)
                {
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewTaxa"), dsTaxa, null, "VALUE", "TEXT");
                }

                //Unit
                dsRefData.SelectParameters["tABLE"].DefaultValue = "MeasureUnit";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewUnit"), dsRefData, null, "VALUE", "VALUE");

                //Anal Method
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewAnalMethod"), dsAnalMethod, null, "ANALYTIC_METHOD_IDX", "AnalMethodDisplayName");

                //Samp Fraction
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ResultSampleFraction";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewSampFraction"), dsRefData, null, "VALUE", "VALUE");

                //value type
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ResultValueType";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewResultValueType"), dsRefData, null, "VALUE", "VALUE");

                //result status
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ResultStatus";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewResultStatus"), dsRefData, null, "VALUE", "VALUE");


                //Bio Intent
                col = GetColumnIndexByName(grdResults, "Biological Intent");
                if (grdResults.Columns[col].Visible)
                {
                    dsRefData.SelectParameters["tABLE"].DefaultValue = "BiologicalIntent";
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewBioIntent"), dsRefData, null, "VALUE", "VALUE");
                }

                //Frequency Class
                col = GetColumnIndexByName(grdResults, "Frequency Class");
                if (grdResults.Columns[col].Visible)
                {
                    dsRefData.SelectParameters["tABLE"].DefaultValue = "FrequencyClassDescriptor";
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewFreqClass"), dsRefData, null, "VALUE", "VALUE");
                }
            }            

        }

        private static void PopulateDropDown (DropDownList ddl, ObjectDataSource ds, string selVal, string listVal, string listName)
        {
            if (ddl != null)
            {
                Utils.BindList(ddl, ds, listVal, listName);
                if (selVal != null) ddl.SelectedValue = selVal;
            }
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("ResultsExport.xls", grdResults);
        }

        private int GetColumnIndexByName(GridView grid, string name)
        {
            foreach (DataControlField col in grid.Columns)
            {
                if (col.HeaderText.ToLower().Trim() == name.ToLower().Trim())
                {
                    return grid.Columns.IndexOf(col);
                }
            }

            return -1;
        }

        protected void ddlNewChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlNewChar = (DropDownList)sender;

            T_WQX_REF_CHAR_ORG c = db_Ref.GetT_WQX_REF_CHAR_ORGByName(Session["OrgID"].ToString(), ddlNewChar.SelectedValue);
            if (c != null)
            {
                if (c.DEFAULT_UNIT != null)
                {
                    DropDownList ddlNewUnit = (DropDownList)grdResults.FooterRow.FindControl("ddlNewUnit");
                    ddlNewUnit.SelectedValue = c.DEFAULT_UNIT;
                }
                if (c.DEFAULT_DETECT_LIMIT != null)
                {
                    TextBox txtNewDetect = (TextBox)grdResults.FooterRow.FindControl("txtNewDetectLimit");
                    txtNewDetect.Text = c.DEFAULT_DETECT_LIMIT;
                }
                if (c.DEFAULT_ANAL_METHOD_IDX != null)
                {
                    DropDownList ddlNewAnalMethod = (DropDownList)grdResults.FooterRow.FindControl("ddlNewAnalMethod");
                    ddlNewAnalMethod.SelectedValue = c.DEFAULT_ANAL_METHOD_IDX.ToString();
                }
                if (c.DEFAULT_SAMP_FRACTION != null)
                {
                    DropDownList ddlNewSampFraction = (DropDownList)grdResults.FooterRow.FindControl("ddlNewSampFraction");
                    ddlNewSampFraction.SelectedValue = c.DEFAULT_SAMP_FRACTION;
                }
                if (c.DEFAULT_RESULT_VALUE_TYPE != null)
                {
                    DropDownList ddlNewResultValueType = (DropDownList)grdResults.FooterRow.FindControl("ddlNewResultValueType");
                    ddlNewResultValueType.SelectedValue = c.DEFAULT_RESULT_VALUE_TYPE;
                }
                if (c.DEFAULT_RESULT_STATUS != null)
                {
                    DropDownList ddlNewResultStatus = (DropDownList)grdResults.FooterRow.FindControl("ddlNewResultStatus");
                    ddlNewResultStatus.SelectedValue = c.DEFAULT_RESULT_STATUS;
                }
            }

            TextBox txtNewResultVal = (TextBox)grdResults.FooterRow.FindControl("txtNewResultVal");
            txtNewResultVal.Focus();
        }


    }
}