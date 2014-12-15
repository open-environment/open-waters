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
    public partial class WQXImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select or create an organization first.";
                btnParse.Visible = false;
                return;
            }


            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                grdImport.DataSource = db_Ref.GetWQX_IMPORT_LOG(Session["OrgID"].ToString());
                grdImport.DataBind();
            }
        }

        protected void ddlImportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select or create an organization first.";
                btnParse.Visible = false;
                return;
            }

            pnlImportLogic.Visible = (ddlImportType.SelectedValue == "SAMP" || ddlImportType.SelectedValue == "SAMP_CT");
            pnlProject.Visible = (ddlImportType.SelectedValue == "SAMP" || ddlImportType.SelectedValue == "SAMP_CT" || ddlImportType.SelectedValue == "BIO_METRIC");

            hlTemplate.Visible = (ddlImportType.SelectedValue.Length > 0);
            btnParse.Visible = (ddlImportType.SelectedValue.Length > 0);

            if (ddlImportType.SelectedValue == "MLOC")
                hlTemplate.NavigateUrl = "~/App_Docs/MonLoc_ImportTemplate.xlsx";

            if (ddlImportType.SelectedValue == "SAMP_CT")
                hlTemplate.NavigateUrl = "~/App_Docs/SampCT_ImportTemplate.xlsx";

            if (ddlImportType.SelectedValue == "SAMP")
            {
                hlTemplate.NavigateUrl = "~/App_Docs/Samp_ImportTemplate.xlsx";
                lblMsg.Text = "This import option is not yet available.";
            }

            if (ddlImportType.SelectedValue == "BIO_METRIC")
                hlTemplate.NavigateUrl = "~/App_Docs/SampBio_ImportTemplate.xlsx";

        }

        protected void grdImport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                db_Ref.DeleteT_WQX_IMPORT_LOG(e.CommandArgument.ToString().ConvertOrDefault<int>());
                Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
            }

        }

        protected void btnParse_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            //******************** VALIDATION *********************************************
            if (ddlImportType.SelectedValue == "")
            {
                lblMsg.Text = "Please select an import data structure.";
                return;
            }

            if (ddlImportType.SelectedValue == "SAMP" || ddlImportType.SelectedValue == "SAMP_CT" || ddlImportType.SelectedValue == "BIO_METRIC")
            {
                if (ddlProject.SelectedValue == "")
                {
                    lblMsg.Text = "Please select a project into which this data will be imported.";
                    return;
                }
            }

            if (txtPaste.Text.Length == 0)
            {
                lblMsg.Text = "You must copy and paste data from a spreadsheet into the large textbox.";
                return;
            }
            //******************** END VALIDATION *****************************************

            string OrgID = Session["OrgID"].ToString();
            int TemplateID = ddlTemplate.SelectedValue.ConvertOrDefault<int>();
            int? ProjectID = ddlProject.SelectedValue.ConvertOrDefault<int?>();
            string ProjectIDName = ddlProject.SelectedItem == null ? "" : ddlProject.SelectedItem.Text;
            
            string txt = txtPaste.Text;
            string[] rows = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int colCount = 50;

            //**************** IMPORTING MONITORING LOCATION RESULTS *********************************
            if (ddlImportType.SelectedValue == "MLOC")
            {
                if (ImportMonLoc(OrgID, rows) == false)
                    return;
            }

            if (ddlImportType.SelectedValue == "SAMP_CT")
            {
                if (ImportSampleCT(OrgID, TemplateID, ProjectID, ProjectIDName, rows) == false)
                    return;
            }

            if (ddlImportType.SelectedValue == "SAMP")
            {
                if (ImportSample(OrgID, TemplateID, ProjectID, ProjectIDName, rows) == false)
                    return;
            }            


            if (ddlImportType.SelectedValue=="BIO_METRIC")
            {
                ImportBiologicalData(OrgID, ProjectID, rows, colCount);
            }

            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
        }

        private bool ImportSample(string OrgID, int TemplateID, int? ProjectID, string ProjectIDName, string[] rows)
        {
            lblMsg.Text = "This import option is not yet available";
            return false;

            //delete any previous temporary sample import data
            if (db_WQX.DeleteT_WQX_IMPORT_TEMP_SAMPLE(User.Identity.Name) == 0) { lblMsg.Text = "Unable to proceed with import."; return false; }

            int i = 0;
            int? MONLOC_IDCol = null, ACTIVITY_IDCol = null, ACTIVITY_START_DTCol = null, ACTIVITY_START_TIMECol = null, CHAR_NAMECol = null, RESULTCol = null, RESULT_UNITCol = null;

            //declare columns
            //loop through each record
            foreach (string row in rows)
            {
                char[] delimiters = new char[] { '\t' };   //tab delimiter
                string[] parts = row.Split(delimiters, StringSplitOptions.None); //columns split into parts
                if (parts.Length > 0)
                {
                    //start of field-by-field validation

                    //special logic to read header to determine what is in each column
                    if (i == 0)
                    {
                        int j = 0;
                        foreach (string part in parts)
                        {
                            if (part == "Station ID") MONLOC_IDCol = j;
                            if (part == "Activity ID") ACTIVITY_IDCol = j;
                            if (part == "Activity Start Date") ACTIVITY_START_DTCol = j;
                            if (part == "Activity Start Time") ACTIVITY_START_TIMECol = j;
                            if (part == "Characteristic") CHAR_NAMECol = j;
                            if (part == "Result") RESULTCol = j;
                            if (part == "Unit") RESULT_UNITCol = j;

                            j = j + 1;
                        }
                    }
                    else
                    {
                        string valMsg = "";
                        if (MONLOC_IDCol == null) { lblMsg.Text = "No column with header of 'Station ID' found. Make sure you include the column header row when pasting data."; return false; }
                        if (RESULTCol == null) { lblMsg.Text = "No column with header of 'Result' found. Make sure you include the column header row when pasting data."; return false; }
                        if (CHAR_NAMECol == null) { lblMsg.Text = "No column with header of 'Characteristic' found. Make sure you include the column header row when pasting data."; return false; }

                        string MonLocIDVal = (MONLOC_IDCol != null ? parts[MONLOC_IDCol.ConvertOrDefault<int>()] : null);
                        int? MonLocIDXVal = null;

                        T_WQX_MONLOC mm = db_WQX.GetWQX_MONLOC_ByIDString(OrgID, MonLocIDVal);
                        if (mm != null)
                            MonLocIDXVal = mm.MONLOC_IDX;

                        int TempImportSampID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(null, User.Identity.Name, OrgID, ProjectID, ProjectIDName,
                            MonLocIDXVal, 
                            (MONLOC_IDCol != null ? parts[MONLOC_IDCol.ConvertOrDefault<int>()] : null),
                            null,
                            (ACTIVITY_IDCol != null ? parts[ACTIVITY_IDCol.ConvertOrDefault<int>()] : null),
                            null, null, null,
                            (ACTIVITY_START_DTCol != null ? parts[ACTIVITY_START_DTCol.ConvertOrDefault<int>()] : null).ConvertOrDefault<DateTime>(),
                            (ACTIVITY_START_DTCol != null ? parts[ACTIVITY_START_DTCol.ConvertOrDefault<int>()] : null).ConvertOrDefault<DateTime>(), 
                            null, null, "P", valMsg);

                        int TempImportResultID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_RESULT(null, TempImportSampID, null, null, null, 
                            (CHAR_NAMECol != null ? parts[CHAR_NAMECol.ConvertOrDefault<int>()] : null), 
                            null, null,
                            (RESULTCol != null ? parts[RESULTCol.ConvertOrDefault<int>()] : null),
                            (RESULT_UNITCol != null ? parts[RESULT_UNITCol.ConvertOrDefault<int>()] : null), 
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "P", "");

                    }

                }

                i = i + 1;
            }

            if (i > 1)  //can only continue if 2 rows (1 plus 1 header) are imported
                Response.Redirect("~/App_Pages/Secure/WQXImportSamp.aspx");
            else
            {
                lblMsg.Text = "No valid data found. You must include column headers.";
                return false;
            }


            return true;
        }

        private bool ImportSampleCT(string OrgID, int TemplateID, int? ProjectID, string ProjectIDName, string[] rows)
        {
            //delete any previous temporary sample import data
            if (db_WQX.DeleteT_WQX_IMPORT_TEMP_SAMPLE(User.Identity.Name) == 0) { lblMsg.Text = "Unable to proceed with import."; return false; }

            //GET the column configuration for all SAMPLE and RESULT-LEVEL FIELDS (only need to do this data retrieval once per import)
            T_WQX_IMPORT_TEMPLATE_DTL MonLocCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "MONLOC_ID");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityIDCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACTIVITY_ID");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityTypeCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_TYPE");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityMediaCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_MEDIA");
            T_WQX_IMPORT_TEMPLATE_DTL ActivitySubMediaCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_SUBMEDIA");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityStartDateCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_START_DATE");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityStartTimeCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_START_TIME");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityEndDateCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_END_DATE");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityEndTimeCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_TIME_TIME");
            T_WQX_IMPORT_TEMPLATE_DTL ActivityCommentsCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_COMMENTS");

            //***********************************
            //loop through each sample
            //***********************************
            foreach (string row in rows)
            {
                //declare variables to store values for the current row
                string valMsg = "";
                string MonLocIDVal = null, ActivityTypeVal=null, ActivityMediaVal=null, ActivitySubMediaVal=null, ActivityIDVal=null;
                int? MonLocIDXVal=null;
                DateTime? ActivityStartDateVal = null, ActivityEndDateVal = null;

                char[] delimiters = new char[] { '\t' };   //tab delimiter
                string[] parts = row.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); //columns split into parts
                if (parts.Length > 0)
                {
                    //start of field-by-field validation

                    //monitoring location
                    if (MonLocCol == null) 
                        { lblMsg.Text = "Your import logic does not define a monitoring location column - import cannot be performed"; return false; }
                    else
                    {
                        MonLocIDVal = GetFieldValue(MonLocCol, parts);
                        T_WQX_MONLOC mloc = db_WQX.GetWQX_MONLOC_ByIDString(OrgID, MonLocIDVal);
                        if (mloc == null)
                            valMsg = "Monitoring Location not found.;";
                        else
                            MonLocIDXVal = mloc.MONLOC_IDX;
                    }

                    ActivityTypeVal = GetFieldValue(ActivityTypeCol, parts);
                    ActivityMediaVal = GetFieldValue(ActivityMediaCol, parts);
                    ActivitySubMediaVal = GetFieldValue(ActivitySubMediaCol, parts);

                    //activity start date 
                    if (ActivityStartDateCol == null)
                         { lblMsg.Text = "Your import logic does not define an activity start date column - import cannot be performed"; return false; }
                    else
                    {
                        string sActivityStartDateVal = GetFieldValue(ActivityStartDateCol, parts);
                        string sActivityStartTimeVal = GetFieldValue(ActivityStartTimeCol, parts);
                        ActivityStartDateVal = (sActivityStartDateVal + " " + sActivityStartTimeVal ?? "").ConvertOrDefault<DateTime?>();
                        if (ActivityStartDateVal == null) { valMsg = "Activity Start Date cannot be converted to DateTime"; }
                    }

                    //activity end date 
                    if (ActivityEndDateCol == null)
                        ActivityEndDateVal = ActivityStartDateVal;
                    else
                    {
                        string sActivityEndDateVal = GetFieldValue(ActivityEndDateCol, parts);
                        string sActivityEndTimeVal = GetFieldValue(ActivityEndTimeCol, parts);
                        ActivityEndDateVal = (sActivityEndDateVal + " " + sActivityEndTimeVal ?? "").ConvertOrDefault<DateTime?>();
                        if (ActivityEndDateCol.COL_NUM > 0 && ActivityStartDateVal == null) { valMsg = "Activity Start Date cannot be converted to DateTime"; }
                    }


                    //activity id (done after sample date and monloc)
                    if (ActivityStartDateVal != null)
                    {
                        if (ActivityIDCol == null)
                            ActivityIDVal = MonLocIDVal + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmm");
                        else if (ActivityIDCol.CHAR_NAME == "#M_D_T")
                            ActivityIDVal = MonLocIDVal + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmm");
                        else
                            ActivityIDVal = GetFieldValue(ActivityIDCol, parts);
                    }

                    int TempImportSampID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(null, User.Identity.Name, OrgID, ProjectID, ProjectIDName, MonLocIDXVal, MonLocIDVal, null, ActivityIDVal,
                        ActivityTypeVal, ActivityMediaVal, ActivitySubMediaVal, ActivityStartDateVal, ActivityStartDateVal, null,
                        GetFieldValue(ActivityCommentsCol, parts), (valMsg.Length > 0 ? "F" : "P"), valMsg);


                    //now loop through any potential characteristics
                    List<T_WQX_IMPORT_TEMPLATE_DTL> chars = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_CharsByTemplateID(TemplateID);
                    foreach (T_WQX_IMPORT_TEMPLATE_DTL character in chars)
                    {
                        string resultVal = GetFieldValue(character, parts);
                        int TempImportResultID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_RESULT(null, TempImportSampID, null, null, null, character.CHAR_NAME, null, null, resultVal,
                            character.CHAR_DEFAULT_UNIT, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "P", "");
                    }


                }
            }

            Response.Redirect("~/App_Pages/Secure/WQXImportSamp.aspx");

            return true;
        }

        private bool ImportMonLoc(string OrgID, string[] rows)
        {
            int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMP_MONLOC(User.Identity.Name);
            if (SuccID == 0)
            {
                lblMsg.Text = "Unable to proceed with import.";
                return false;
            }

            int i = 0;
            int? MONLOC_IDCol = null, MONLOC_NAMECol = null, MONLOC_TYPECol = null, MONLOC_DESCCol = null, HUC_EIGHTCol = null, HUC_TWELVECol = null, TRIBAL_LAND_INDCol = null, TRIBAL_LAND_NAMECol = null, LATITUDE_MSRCol = null, LONGITUDE_MSRCol = null, SOURCE_MAP_SCALECol = null;
            int? HORIZ_ACCURACYCol = null, HORIZ_ACCURACY_UNITCol = null, HORIZ_COLL_METHODCol = null, HORIZ_REF_DATUMCol = null, VERT_MEASURECol = null, VERT_MEASURE_UNITCol = null, VERT_COLL_METHODCol = null, VERT_REF_DATUMCol = null, COUNTRY_CODECol = null;
            int? sTATE_CODECol = null, cOUNTY_CODECol = null, wELL_TYPECol = null, aQUIFER_NAMECol = null, fORMATION_TYPECol = null, wELLHOLE_DEPTH_MSRCol = null, wELLHOLE_DEPTH_MSR_UNITCol = null;

            //loop through each record
            foreach (string row in rows)
            {
                char[] delimiters = new char[] { '\t' };   //tab delimiter
                string[] parts = row.Split(delimiters, StringSplitOptions.None); //columns split into parts
                if (parts.Length > 0)
                {
                    //start of field-by-field validation

                    //special logic to read header to determine what is in each column
                    if (i == 0)
                    {
                        int j = 0;
                        foreach (string part in parts)
                        {
                            if (part == "Station ID") MONLOC_IDCol = j;
                            if (part == "Station Name") MONLOC_NAMECol = j;
                            if (part == "Primary Type") MONLOC_TYPECol = j;
                            if (part == "Description Text") MONLOC_DESCCol = j;
                            if (part == "HUC Eight Digit Code") HUC_EIGHTCol = j;
                            if (part == "HUC Twelve Digit Code") HUC_TWELVECol = j;
                            if (part == "Tribal Land Indicator") TRIBAL_LAND_INDCol = j;
                            if (part == "Tribal Land Name") TRIBAL_LAND_NAMECol = j;
                            if (part == "Latitude") LATITUDE_MSRCol = j;
                            if (part == "Longitude") LONGITUDE_MSRCol = j;
                            if (part == "Map Scale") SOURCE_MAP_SCALECol = j;
                            if (part == "Geopositioning Method") HORIZ_COLL_METHODCol = j;
                            if (part == "Horizontal Datum") HORIZ_REF_DATUMCol = j;
                            if (part == "Elevation") VERT_MEASURECol = j;
                            if (part == "Elevation Unit") VERT_MEASURE_UNITCol = j;
                            if (part == "Elevation Method") VERT_COLL_METHODCol = j;
                            if (part == "Elevation Datum") VERT_REF_DATUMCol = j;
                            if (part == "Country Code") COUNTRY_CODECol = j;
                            if (part == "State Code") sTATE_CODECol = j;
                            if (part == "County Code") cOUNTY_CODECol = j;
                            if (part == "Well Type") wELL_TYPECol = j;
                            if (part == "Aquifer Name") aQUIFER_NAMECol = j;
                            if (part == "Formation Type") fORMATION_TYPECol = j;
                            if (part == "Well Hole Depth Measure") wELLHOLE_DEPTH_MSRCol = j;
                            if (part == "Well Hole Depth Measure Unit") wELLHOLE_DEPTH_MSR_UNITCol = j;

                            j = j + 1;
                        }
                    }
                    else
                    {
                        if (MONLOC_IDCol == null) { lblMsg.Text = "No column with header of 'Station ID' found. Make sure you include the column header row when pasting data."; return false; }
                        if (MONLOC_NAMECol == null) { lblMsg.Text = "No column with header of 'Station Name' found. Make sure you include the column header row when pasting data."; return false; }
                        db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_MONLOC(null, User.Identity.Name, null, OrgID,
                            (MONLOC_IDCol != null ? parts[MONLOC_IDCol.ConvertOrDefault<int>()] : null),
                            (MONLOC_NAMECol != null ? parts[MONLOC_NAMECol.ConvertOrDefault<int>()] : null),
                            (MONLOC_TYPECol != null ? parts[MONLOC_TYPECol.ConvertOrDefault<int>()] : null),
                            (MONLOC_DESCCol != null ? parts[MONLOC_DESCCol.ConvertOrDefault<int>()] : null),
                            (HUC_EIGHTCol != null ? parts[HUC_EIGHTCol.ConvertOrDefault<int>()] : null),
                            (HUC_TWELVECol != null ? parts[HUC_TWELVECol.ConvertOrDefault<int>()] : null),
                            (TRIBAL_LAND_INDCol != null ? parts[TRIBAL_LAND_INDCol.ConvertOrDefault<int>()] : null),
                            (TRIBAL_LAND_NAMECol != null ? parts[TRIBAL_LAND_NAMECol.ConvertOrDefault<int>()] : null),
                            (LATITUDE_MSRCol != null ? parts[LATITUDE_MSRCol.ConvertOrDefault<int>()] : null),
                            (LONGITUDE_MSRCol != null ? parts[LONGITUDE_MSRCol.ConvertOrDefault<int>()] : null),
                            (SOURCE_MAP_SCALECol != null ? parts[SOURCE_MAP_SCALECol.ConvertOrDefault<int>()].ConvertOrDefault<int?>() : null),
                            (HORIZ_ACCURACYCol != null ? parts[HORIZ_ACCURACYCol.ConvertOrDefault<int>()] : null),
                            (HORIZ_ACCURACY_UNITCol != null ? parts[HORIZ_ACCURACY_UNITCol.ConvertOrDefault<int>()] : null),
                            (HORIZ_COLL_METHODCol != null ? parts[HORIZ_COLL_METHODCol.ConvertOrDefault<int>()] : null),
                            (HORIZ_REF_DATUMCol != null ? parts[HORIZ_REF_DATUMCol.ConvertOrDefault<int>()] : null),
                            (VERT_MEASURECol != null ? parts[VERT_MEASURECol.ConvertOrDefault<int>()] : null),
                            (VERT_MEASURE_UNITCol != null ? parts[VERT_MEASURE_UNITCol.ConvertOrDefault<int>()] : null),
                            (VERT_COLL_METHODCol != null ? parts[VERT_COLL_METHODCol.ConvertOrDefault<int>()] : null),
                            (VERT_REF_DATUMCol != null ? parts[VERT_REF_DATUMCol.ConvertOrDefault<int>()] : null),
                            (COUNTRY_CODECol != null ? parts[COUNTRY_CODECol.ConvertOrDefault<int>()] : null),
                            (sTATE_CODECol != null ? parts[sTATE_CODECol.ConvertOrDefault<int>()] : null),
                            (cOUNTY_CODECol != null ? parts[cOUNTY_CODECol.ConvertOrDefault<int>()] : null),
                            (wELL_TYPECol != null ? parts[wELL_TYPECol.ConvertOrDefault<int>()] : null),
                            (aQUIFER_NAMECol != null ? parts[aQUIFER_NAMECol.ConvertOrDefault<int>()] : null),
                            (fORMATION_TYPECol != null ? parts[fORMATION_TYPECol.ConvertOrDefault<int>()] : null),
                            (wELLHOLE_DEPTH_MSRCol != null ? parts[wELLHOLE_DEPTH_MSRCol.ConvertOrDefault<int>()] : null),
                            (wELLHOLE_DEPTH_MSR_UNITCol != null ? parts[wELLHOLE_DEPTH_MSR_UNITCol.ConvertOrDefault<int>()] : null), "P", "");
                    }

                }

                i = i + 1;
            }

            if (i > 1)  //can only continue if 2 rows (1 plus 1 header) are imported
                Response.Redirect("~/App_Pages/Secure/WQXImportMonLoc.aspx");
            else
            {
                lblMsg.Text = "No valid data found. You must include column headers.";
                return false;
            }

            return true;
        }

        private void ImportBiologicalData(string OrgID, int? ProjectID, string[] rows, int colCount)
        {
            string[] sites = new string[colCount];
            string[] dates = new string[colCount];
            string[] sampleIDs = new string[colCount];
            string[] HBIs = new string[colCount];
            string[] CorrectedAbundances = new string[colCount];
            string[] EPTAbundances = new string[colCount];
            string[] LongLivedTaxaRichness = new string[colCount];
            string[] ClingerRichness = new string[colCount];
            string[] PctClingers = new string[colCount];
            string[] IntolerantTaxaRichness = new string[colCount];
            string[] PctTolerantIndividuals = new string[colCount];
            string[] PctTolerantTaxa = new string[colCount];
            string[] ColeopteraRichness = new string[colCount];


            try
            {
                foreach (string row in rows)
                {
                    char[] delimiters = new char[] { '\t' };
                    string[] parts = row.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        //sample level stuff
                        if (parts[0] == "Site")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                sites[i] = parts[i];
                        }

                        if (parts[0] == "Collection Date")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                dates[i] = parts[i];
                        }

                        if (parts[0] == "Sample ID")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                sampleIDs[i] = parts[i];
                        }

                        //Metric stuff
                        if (parts[0] == "Corrected Abundance")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                CorrectedAbundances[i] = parts[i];
                        }

                        if (parts[0] == "EPT Abundance")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                EPTAbundances[i] = parts[i];
                        }

                        if (parts[0] == "Long-Lived Taxa Richness")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                LongLivedTaxaRichness[i] = parts[i];
                        }

                        if (parts[0] == "Clinger Richness")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                ClingerRichness[i] = parts[i];
                        }

                        if (parts[0] == "% Clingers")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                PctClingers[i] = parts[i];
                        }

                        if (parts[0] == "Intolerant Taxa Richness")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                IntolerantTaxaRichness[i] = parts[i];
                        }

                        if (parts[0] == "% Tolerant Individuals")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                PctTolerantIndividuals[i] = parts[i];
                        }

                        if (parts[0] == "% Tolerant Taxa")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                PctTolerantTaxa[i] = parts[i];
                        }

                        if (parts[0] == "Coleoptera Richness")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                ColeopteraRichness[i] = parts[i];
                        }


                        //Index level stuff
                        if (parts[0] == "Hilsenhoff Biotic Index")
                        {
                            for (int i = 1; i < parts.Length; i++)
                                HBIs[i] = parts[i];
                        }
                    }
                }

                for (int i = 1; i < colCount; i++)
                {
                    if (sites[i] != null)
                    {
                        int? MonLocIDX = null;
                        int? ActID = null;

                        T_WQX_MONLOC mm = db_WQX.GetWQX_MONLOC_ByIDString(OrgID, sites[i]);
                        if (mm != null)
                            MonLocIDX = mm.MONLOC_IDX;

                        T_WQX_ACTIVITY a = db_WQX.GetWQX_ACTIVITY_ByUnique(OrgID, sampleIDs[i]);
                        if (a != null)
                            ActID = a.ACTIVITY_IDX;

                        ActID = db_WQX.InsertOrUpdateWQX_ACTIVITY(ActID, OrgID, ProjectID, MonLocIDX, sampleIDs[i], "Field Msr/Obs-Habitat Assessment", "Water", "", dates[i].ConvertOrDefault<DateTime>(), null, "", "", "U", true, true, User.Identity.Name);

                        //CREATE INDICES
                        db_WQX.InsertOrUpdateWQX_BIO_HABITAT_INDEX(null, OrgID, MonLocIDX, sampleIDs[i] + "_HBI", "Hilsenhoff Biotic Index", "LOCAL", "Hilsenhoff Biotic Index", null, null, null, null, null, null, null, HBIs[i], null, null, dates[i].ConvertOrDefault<DateTime?>(), true, "U", null, true, User.Identity.Name);

                        //CREATE METRICS
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "Corrected Abundance", "LOCAL", "Corrected Abundance", null, null, null, null, null, null, null, null, CorrectedAbundances[i], null, CorrectedAbundances[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "EPT Abundance", "LOCAL", "EPT Abundance", null, null, null, null, null, null, null, null, EPTAbundances[i], null, EPTAbundances[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "Long-Lived Taxa Richness", "LOCAL", "Long-Lived Taxa Richness", null, null, null, null, null, null, null, null, LongLivedTaxaRichness[i], null, LongLivedTaxaRichness[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "Clinger Richness", "LOCAL", "Clinger Richness", null, null, null, null, null, null, null, null, ClingerRichness[i], null, ClingerRichness[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "% Clingers", "LOCAL", "% Clingers", null, null, null, null, null, null, null, null, PctClingers[i], null, PctClingers[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "Intolerant Taxa Richness", "LOCAL", "Intolerant Taxa Richness", null, null, null, null, null, null, null, null, IntolerantTaxaRichness[i], null, IntolerantTaxaRichness[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "% Tolerant Individuals", "LOCAL", "% Tolerant Individuals", null, null, null, null, null, null, null, null, PctTolerantIndividuals[i], null, PctTolerantIndividuals[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "% Tolerant Taxa", "LOCAL", "% Tolerant Taxa", null, null, null, null, null, null, null, null, PctTolerantTaxa[i], null, PctTolerantTaxa[i], null, true, "U", null, true, User.Identity.Name);
                        db_WQX.InsertOrUpdateWQX_ACTIVITY_METRIC(null, (int)ActID, "Coleoptera Richness", "LOCAL", "Coleoptera Richness", null, null, null, null, null, null, null, null, ColeopteraRichness[i], null, ColeopteraRichness[i], null, true, "U", null, true, User.Identity.Name);

                    }
                }

                //add to import log
                db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, ddlImportType.SelectedValue, ddlImportType.SelectedValue, 0, "Success", null, User.Identity.Name);
            }
            catch
            {
                //add to import log
                db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, ddlImportType.SelectedValue, ddlImportType.SelectedValue, 0, "Fail", null, User.Identity.Name);
            }
        }

        protected void btnNewTemplate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImportConfig.aspx");
        }

        private static string GetFieldValue(T_WQX_IMPORT_TEMPLATE_DTL FieldType, string[] parts)
        {
            try
            {
                return (FieldType == null ? null : (FieldType.COL_NUM == 0 ? FieldType.CHAR_NAME : parts[FieldType.COL_NUM.ConvertOrDefault<int>() - 1]));
            }
            catch
            {
                return null;
            }
        }


    }
}