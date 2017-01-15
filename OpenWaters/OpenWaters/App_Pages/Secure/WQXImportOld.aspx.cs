using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using System.Xml.Linq;
using System.Data;


namespace OpenEnvironment
{
    public class ImportSampleColumnArray
    {
        public int? MONLOC_IDCol { get; set; }
        public int? ACTIVITY_IDCol { get; set; }
        public int? ACT_TYPECol { get; set; }
        public int? ACT_MEDIACol { get; set; }
        public int? ACT_SUBMEDIACol { get; set; }
        public int? ACTIVITY_START_DTCol { get; set; }
        public int? ACTIVITY_START_TIMECol { get; set; }
        public int? ACT_END_DTCol { get; set; }
        public int? ACT_END_TIMECol { get; set; }
        public int? ACT_TIME_ZONECol { get; set; }
        public int? RELATIVE_DEPTH_NAMECol { get; set; }
        public int? ACT_DEPTHHEIGHT_MSRCol { get; set; }
        public int? ACT_DEPTHHEIGHT_MSR_UNITCol { get; set; }
        public int? TOP_DEPTHHEIGHT_MSRCol { get; set; }
        public int? TOP_DEPTHHEIGHT_MSR_UNITCol { get; set; }
        public int? BOT_DEPTHHEIGHT_MSRCol { get; set; }
        public int? BOT_DEPTHHEIGHT_MSR_UNITCol { get; set; }
        public int? DEPTH_REF_POINTCol { get; set; }
        public int? ACT_COMMENTCol { get; set; }
        public int? BIO_ASSEMBLAGE_SAMPLEDCol { get; set; }
        public int? BIO_DURATION_MSRCol { get; set; }
        public int? BIO_DURATION_MSR_UNITCol { get; set; }
        public int? BIO_SAMP_COMPONENTCol { get; set; }
        public int? BIO_SAMP_COMPONENT_SEQCol { get; set; }
        public int? BIO_REACH_LEN_MSRCol { get; set; }
        public int? BIO_REACH_LEN_MSR_UNITCol { get; set; }
        public int? BIO_REACH_WID_MSRCol { get; set; }
        public int? BIO_REACH_WID_MSR_UNITCol { get; set; }
        public int? BIO_PASS_COUNTCol { get; set; }
        public int? BIO_NET_TYPECol { get; set; }
        public int? BIO_NET_AREA_MSRCol { get; set; }
        public int? BIO_NET_AREA_MSR_UNITCol { get; set; }
        public int? BIO_NET_MESHSIZE_MSRCol { get; set; }
        public int? BIO_MESHSIZE_MSR_UNITCol { get; set; }
        public int? BIO_BOAT_SPEED_MSRCol { get; set; }
        public int? BIO_BOAT_SPEED_MSR_UNITCol{ get; set; }
        public int? BIO_CURR_SPEED_MSRCol { get; set; }
        public int? BIO_CURR_SPEED_MSR_UNITCol { get; set; }
        public int? BIO_TOXICITY_TEST_TYPECol { get; set; }
        public int? SAMP_COLL_METHOD_IDCol { get; set; }
        public int? SAMP_COLL_METHOD_CTXCol { get; set; }
        public int? SAMP_COLL_EQUIPCol { get; set; }
        public int? SAMP_COLL_EQUIP_COMMENTCol { get; set; }
        public int? SAMP_PREP_IDCol { get; set; }
        public int? SAMP_PREP_CTXCol { get; set; }
        public int? SAMP_PREP_CONT_TYPECol { get; set; }
        public int? SAMP_PREP_CONT_COLORCol { get; set; }
        public int? SAMP_PREP_CHEM_PRESERVCol { get; set; }
        public int? SAMP_PREP_THERM_PRESERVCol { get; set; }
        public int? SAMP_PREP_STORAGE_DESCCol { get; set; }
        public int? DATA_LOGGER_LINECol { get; set; }
        public int? RESULT_DETECT_CONDITIONCol { get; set; }
        public int? CHAR_NAMECol { get; set; }
        public int? METHOD_SPECIATION_NAMECol { get; set; }
        public int? RESULT_SAMP_FRACTIONCol { get; set; }
        public int? RESULT_MSRCol { get; set; }
        public int? RESULT_MSR_UNITCol { get; set; }
        public int? RESULT_MSR_QUALCol { get; set; }
        public int? RESULT_STATUSCol { get; set; }
        public int? STATISTIC_BASE_CODECol { get; set; }
        public int? RESULT_VALUE_TYPECol { get; set; }
        public int? WEIGHT_BASISCol { get; set; }
        public int? TIME_BASISCol { get; set; }
        public int? TEMP_BASISCol { get; set; }
        public int? PARTICLESIZE_BASISCol { get; set; }
        public int? PRECISION_VALUECol { get; set; }
        public int? BIAS_VALUECol { get; set; }
        public int? CONFIDENCE_INTERVAL_VALUECol { get; set; }
        public int? UPPER_CONFIDENCE_LIMITCol { get; set; }
        public int? LOWER_CONFIDENCE_LIMITCol { get; set; }
        public int? RESULT_COMMENTCol { get; set; }
        public int? RESULT_DEPTH_HEIGHT_MSRCol { get; set; }
        public int? RESULT_DEPTH_HEIGHT_MSR_UNITCol { get; set; }
        public int? RESULT_DEPTH_REF_POINTCol { get; set; }
        public int? BIO_INTENT_NAMECol { get; set; }
        public int? BIO_INDIVIDUAL_IDCol { get; set; }
        public int? TAXON_NameCol { get; set; }
        public int? UNIDENT_SPECIES_IDCol { get; set; }
        public int? TISSUE_ANATOMYCol { get; set; }
        public int? GROUP_SUMM_TOTALCol { get; set; }
        public int? GROUP_SUMM_TOTAL_UNITCol { get; set; }
        public int? CELL_FORMCol { get; set; }
        public int? CELL_SHAPECol { get; set; }
        public int? HABIT_NAMECol { get; set; }
        public int? VOLTINISM_NAMECol { get; set; }
        public int? POLL_TOLERANCECol { get; set; }
        public int? POLL_TOLERANCE_SCALECol { get; set; }
        public int? TROPHIC_LEVELCol { get; set; }
        public int? FEEDING_GROUP1Col { get; set; }
        public int? FEEDING_GROUP2Col { get; set; }
        public int? FEEDING_GROUP3Col { get; set; }
        public int? FREQ_CLASS_CODECol { get; set; }
        public int? FREQ_CLASS_UNITCol { get; set; }
        public int? FREQ_CLASS_UPPERCol { get; set; }
        public int? FREQ_CLASS_LOWERCol { get; set; }
        public int? ANALYTIC_METHOD_IDCol { get; set; }
        public int? ANALYTIC_METHOD_CTXCol { get; set; }
        public int? ANALYTIC_METHOD_NAMECol { get; set; }
        public int? LAB_NAMECol { get; set; }
        public int? LAB_ANALYSIS_START_DTCol { get; set; }
        public int? LAB_ANALYSIS_START_TIMECol { get; set; }
        public int? LAB_ANALYSIS_END_DTCol{ get; set; }
        public int? LAB_ANALYSIS_TIMEZONECol { get; set; }
        public int? RESULT_LAB_COMMENT_CODECol { get; set; }
        public int? METHOD_DETECTION_LEVELCol { get; set; }
        public int? LAB_REPORTING_LEVELCol { get; set; }
        public int? PQLCol{ get; set; }
        public int? LOWER_QUANT_LIMITCol { get; set; }
        public int? UPPER_QUANT_LIMITCol { get; set; }
        public int? DETECTION_LIMIT_UNITCol { get; set; }
        public int? LAB_SAMP_PREP_IDCol { get; set; }
        public int? LAB_SAMP_PREP_CTXCol { get; set; }
        public int? LAB_SAMP_PREP_START_DTCol { get; set; }
        public int? LAB_SAMP_PREP_START_TIMECol { get; set; }     //added in case they are separate
        public int? LAB_SAMP_PREP_END_DTCol { get; set; }
        public int? DILUTION_FACTORCol { get; set; }

        public int? specialACTIVITY_ID__ACT_TYPECol { get; set; }   //special column that merges activity ID and activity type
    }

    public class TranslateArray {
        public Dictionary<string, string> MONLOC_IDTrans { get; set; }
        public Dictionary<string, string> ACT_TYPETrans { get; set; }
        public Dictionary<string, string> ACT_MEDIATrans { get; set; }
        public Dictionary<string, string> ACT_SUBMEDIATrans { get; set; }
    }

    public partial class WQXImportOld : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);


            //display left menu as selected
            if (!IsPostBack)
            {
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //redirect to sample page if any are in progress
                if (db_WQX.GetWQX_IMPORT_TEMP_SAMPLE_CountByUserID(User.Identity.Name) > 0)
                    Response.Redirect("~/App_Pages/Secure/WQXImportSample.aspx");

                grdImport.DataSource = db_Ref.GetWQX_IMPORT_LOG(Session["OrgID"].ToString());
                grdImport.DataBind();

                //disable the import from EPA option if results already exist
                if (db_WQX.GetT_WQX_RESULTCount(Session["OrgID"].ToString()) > 0)
                {
                    rbImportType.Items[1].Enabled = false;
                    rbImportType.Items[1].Text = "Import data directly from EPA-WQX (only available for initial synchronization)";
                    rbImportType.Items[0].Selected = true;
                    pnlLab.Visible = true;
                    pnlWQX.Visible = false;
                }
            }
        }

        protected void rbImportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWQX.Visible = rbImportType.SelectedValue == "2";
            pnlLab.Visible = rbImportType.SelectedValue == "1";
        }

        protected void ddlImportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            pnlImportLogic.Visible = (ddlImportType.SelectedValue == "SAMP_CT");
            pnlProject.Visible = (ddlImportType.SelectedValue == "SAMP" || ddlImportType.SelectedValue == "SAMP_CT" || ddlImportType.SelectedValue == "BIO_METRIC");

            hlTemplate.Visible = (ddlImportType.SelectedValue.Length > 0);
            btnParse.Visible = (ddlImportType.SelectedValue.Length > 0);
            btnDefaults.Visible = ddlImportType.SelectedValue == "SAMP";
            btnTranslate.Visible = ddlImportType.SelectedValue == "SAMP";

            if (ddlImportType.SelectedValue == "MLOC")
                hlTemplate.NavigateUrl = "~/App_Docs/MonLoc_ImportTemplate.xlsx";

            if (ddlImportType.SelectedValue == "SAMP_CT")
                hlTemplate.NavigateUrl = "~/App_Docs/SampCT_ImportTemplate.xlsx";

            if (ddlImportType.SelectedValue == "SAMP")
                hlTemplate.NavigateUrl = "~/App_Docs/Samp_ImportTemplate.xlsx";

            if (ddlImportType.SelectedValue == "BIO_METRIC")
                hlTemplate.NavigateUrl = "~/App_Docs/SampBio_ImportTemplate.xlsx";

        }

        protected void btnNewTemplate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImportConfig.aspx");
        }


        //*************************** IMPORT LOGIC FROM SPREADSHEETS ******************************************************
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

            //**************** IMPORTING MONITORING LOCATION or RESULTS *********************************
            if (ddlImportType.SelectedValue == "MLOC")
                ImportMonLoc(OrgID, rows);

            if (ddlImportType.SelectedValue == "SAMP_CT")
                ImportSampleCT(OrgID, TemplateID, ProjectID, ProjectIDName, rows);

            if (ddlImportType.SelectedValue == "SAMP")
                ImportSample(OrgID, TemplateID, ProjectID, ProjectIDName, rows);

            if (ddlImportType.SelectedValue == "BIO_METRIC")
                ImportBiologicalData(OrgID, ProjectID, rows, colCount);

        }

        private bool ImportSample(string OrgID, int TemplateID, int? ProjectID, string ProjectIDName, string[] rows)
        {
            //delete any previous temporary sample import data
            if (db_WQX.DeleteT_WQX_IMPORT_TEMP_SAMPLE(User.Identity.Name) == 0) { lblMsg.Text = "Unable to proceed with import."; return false; }

            int i = 0;

            //initialize column array object 
            ImportSampleColumnArray cols = new ImportSampleColumnArray();

            //initialize translation object 
            TranslateArray trans = new TranslateArray
            {
                MONLOC_IDTrans = db_WQX.GetWQX_IMPORT_TRANSLATE_byColName(OrgID, "Station ID"),
                ACT_TYPETrans = db_WQX.GetWQX_IMPORT_TRANSLATE_byColName(OrgID, "Activity Type Code"),
                ACT_MEDIATrans = db_WQX.GetWQX_IMPORT_TRANSLATE_byColName(OrgID, "Activity Media"),
                ACT_SUBMEDIATrans = db_WQX.GetWQX_IMPORT_TRANSLATE_byColName(OrgID, "Activity Submedia")
            };

            //initialize indicators
            bool bioIndicator = false;

            //loop through each record
            foreach (string row in rows)
            {
                char[] delimiters = new char[] { '\t' };   //define tab delimiter
                string[] parts = row.Split(delimiters, StringSplitOptions.None); //split row into parts (columns)
                if (parts.Length > 0)
                {
                    //**********************************************************
                    //HEADER ROW - LOGIC TO DETERMING WHAT IS IN EACH COLUMN
                    //**********************************************************
                    if (i == 0)
                    {
                        int j = 0;  //j is the header column index

                        //For columns that support aliases, get the array of alias names
                        List<string> ActivityIDList = null;// db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Activity ID");
                        List<string> ActivityMediaList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Activity Media");
                        List<string> ActivityStartDateList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Activity Start Date");
                        List<string> ActivityStartTimeList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Activity Start Time");
                        List<string> SampPrepList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Sample Prep ID");
                        List<string> LabNameList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Lab Name");
                        List<string> CharacteristicList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Characteristic");
                        List<string> ResultUnitList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Unit");
                        List<string> DetectionConditionList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Result Detection Condition");
                        List<string> DetectionLimitUnit = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Detection Threshold Limit Unit");
                        List<string> DilutionList = null;//db_WQX.GetWQX_IMPORT_COL_ALIAS_byField("Dilution Factor");

                        //loop through every column in the header row
                        foreach (string part in parts)
                        {
                            SetColInfo(cols, part, bioIndicator, j, ActivityIDList, ActivityMediaList, ActivityStartDateList, ActivityStartTimeList, SampPrepList, LabNameList, CharacteristicList, ResultUnitList, DetectionConditionList, DetectionLimitUnit, DilutionList);
                            j = j + 1;
                        }

                        //temp fix for biological monitoring 
                        if (cols.BIO_ASSEMBLAGE_SAMPLEDCol != null || cols.TAXON_NameCol != null || cols.TISSUE_ANATOMYCol != null)
                            bioIndicator = true;
                    }
                    //**********************************************************
                    //NOT HEADER ROW - READING IN VALUES
                    //**********************************************************
                    else
                    {
                        //****************** RESULT COLUMN VALIDATION ************************
                        if (cols.MONLOC_IDCol == null) { lblMsg.Text = "No column with header of 'Station ID' found. Make sure you include the column header row when pasting data."; return false; }
                        if (cols.ACTIVITY_IDCol == null && cols.specialACTIVITY_ID__ACT_TYPECol == null) { lblMsg.Text = "No column with header of 'Activity ID' found. Make sure you include the column header row when pasting data."; return false; }
                        if (cols.ACTIVITY_START_DTCol == null) { lblMsg.Text = "No column with header of 'Activity Start Date' found. Make sure you include the column header row when pasting data."; return false; }
                        //if (cols.RESULTCol == null) { lblMsg.Text = "No column with header of 'Result' found. Make sure you include the column header row when pasting data."; return false; }
                        //if (cols.CHAR_NAMECol == null) { lblMsg.Text = "No column with header of 'Characteristic' found. Make sure you include the column header row when pasting data."; return false; }
                        //****************** END RESULT COLUMN VALIDATION ************************


                        //import sample record
                        int TempImportSampID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(null, User.Identity.Name, OrgID, ProjectID, ProjectIDName, null,
                            (cols.MONLOC_IDCol != null ? TranslateField(parts[cols.MONLOC_IDCol.ConvertOrDefault<int>()], trans.MONLOC_IDTrans) : null),
                            null,
                            (cols.ACTIVITY_IDCol != null ? parts[cols.ACTIVITY_IDCol.ConvertOrDefault<int>()] : null),
                            (cols.ACT_TYPECol != null ? TranslateField(parts[cols.ACT_TYPECol.ConvertOrDefault<int>()], trans.ACT_TYPETrans) : null),
                            (cols.ACT_MEDIACol != null ? TranslateField(parts[cols.ACT_MEDIACol.ConvertOrDefault<int>()], trans.ACT_MEDIATrans) : null),
                            (cols.ACT_SUBMEDIACol != null ? TranslateField(parts[cols.ACT_SUBMEDIACol.ConvertOrDefault<int>()], trans.ACT_SUBMEDIATrans) : null),
                            ((cols.ACTIVITY_START_DTCol != null ? parts[cols.ACTIVITY_START_DTCol.ConvertOrDefault<int>()] : null) + " " + (cols.ACTIVITY_START_TIMECol != null ? parts[cols.ACTIVITY_START_TIMECol.ConvertOrDefault<int>()] : null)).ConvertOrDefault<DateTime?>(),
                            ((cols.ACT_END_DTCol != null ? parts[cols.ACT_END_DTCol.ConvertOrDefault<int>()] : null) + " " + (cols.ACT_END_TIMECol != null ? parts[cols.ACT_END_TIMECol.ConvertOrDefault<int>()] : null)).ConvertOrDefault<DateTime?>(),
                            (cols.ACT_TIME_ZONECol != null ? parts[cols.ACT_TIME_ZONECol.ConvertOrDefault<int>()] : null),
                            (cols.RELATIVE_DEPTH_NAMECol != null ? parts[cols.RELATIVE_DEPTH_NAMECol.ConvertOrDefault<int>()] : null),
                            (cols.ACT_DEPTHHEIGHT_MSRCol != null ? parts[cols.ACT_DEPTHHEIGHT_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.ACT_DEPTHHEIGHT_MSR_UNITCol != null ? parts[cols.ACT_DEPTHHEIGHT_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.TOP_DEPTHHEIGHT_MSRCol != null ? parts[cols.TOP_DEPTHHEIGHT_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.TOP_DEPTHHEIGHT_MSR_UNITCol != null ? parts[cols.TOP_DEPTHHEIGHT_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BOT_DEPTHHEIGHT_MSRCol != null ? parts[cols.BOT_DEPTHHEIGHT_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BOT_DEPTHHEIGHT_MSR_UNITCol != null ? parts[cols.BOT_DEPTHHEIGHT_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.DEPTH_REF_POINTCol != null ? parts[cols.DEPTH_REF_POINTCol.ConvertOrDefault<int>()] : null),
                            (cols.ACT_COMMENTCol != null ? parts[cols.ACT_COMMENTCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_ASSEMBLAGE_SAMPLEDCol != null ? parts[cols.BIO_ASSEMBLAGE_SAMPLEDCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_DURATION_MSRCol != null ? parts[cols.BIO_DURATION_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_DURATION_MSR_UNITCol != null ? parts[cols.BIO_DURATION_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_SAMP_COMPONENTCol != null ? parts[cols.BIO_SAMP_COMPONENTCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_SAMP_COMPONENT_SEQCol != null ? parts[cols.BIO_SAMP_COMPONENT_SEQCol.ConvertOrDefault<int>()].ConvertOrDefault<int?>() : null),
                            (cols.BIO_REACH_LEN_MSRCol != null ? parts[cols.BIO_REACH_LEN_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_REACH_LEN_MSR_UNITCol != null ? parts[cols.BIO_REACH_LEN_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_REACH_WID_MSRCol != null ? parts[cols.BIO_REACH_WID_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_REACH_WID_MSR_UNITCol != null ? parts[cols.BIO_REACH_WID_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_PASS_COUNTCol != null ? parts[cols.BIO_PASS_COUNTCol.ConvertOrDefault<int>()].ConvertOrDefault<int?>() : null),
                            (cols.BIO_NET_TYPECol != null ? parts[cols.BIO_NET_TYPECol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_NET_AREA_MSRCol != null ? parts[cols.BIO_NET_AREA_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_NET_AREA_MSR_UNITCol != null ? parts[cols.BIO_NET_AREA_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_NET_MESHSIZE_MSRCol != null ? parts[cols.BIO_NET_MESHSIZE_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_MESHSIZE_MSR_UNITCol != null ? parts[cols.BIO_MESHSIZE_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_BOAT_SPEED_MSRCol != null ? parts[cols.BIO_BOAT_SPEED_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_BOAT_SPEED_MSR_UNITCol != null ? parts[cols.BIO_BOAT_SPEED_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_CURR_SPEED_MSRCol != null ? parts[cols.BIO_CURR_SPEED_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_CURR_SPEED_MSR_UNITCol != null ? parts[cols.BIO_CURR_SPEED_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_TOXICITY_TEST_TYPECol != null ? parts[cols.BIO_TOXICITY_TEST_TYPECol.ConvertOrDefault<int>()] : null),
                            null,
                            (cols.SAMP_COLL_METHOD_IDCol != null ? parts[cols.SAMP_COLL_METHOD_IDCol.ConvertOrDefault<int>()] : null),
                            (cols.SAMP_COLL_METHOD_CTXCol != null ? parts[cols.SAMP_COLL_METHOD_CTXCol.ConvertOrDefault<int>()] : null),
                            null,
                            (cols.SAMP_COLL_EQUIPCol != null ? parts[cols.SAMP_COLL_EQUIPCol.ConvertOrDefault<int>()] : null),
                            (cols.SAMP_COLL_EQUIP_COMMENTCol != null ? parts[cols.SAMP_COLL_EQUIP_COMMENTCol.ConvertOrDefault<int>()] : null),
                            null,
                            (cols.LAB_SAMP_PREP_IDCol != null ? parts[cols.LAB_SAMP_PREP_IDCol.ConvertOrDefault<int>()] : null),
                            (cols.LAB_SAMP_PREP_CTXCol != null ? parts[cols.LAB_SAMP_PREP_CTXCol.ConvertOrDefault<int>()] : null),
                            null,
                            (cols.SAMP_PREP_CONT_TYPECol != null ? parts[cols.SAMP_PREP_CONT_TYPECol.ConvertOrDefault<int>()] : null),
                            (cols.SAMP_PREP_CONT_COLORCol != null ? parts[cols.SAMP_PREP_CONT_COLORCol.ConvertOrDefault<int>()] : null),
                            (cols.SAMP_PREP_CHEM_PRESERVCol != null ? parts[cols.SAMP_PREP_CHEM_PRESERVCol.ConvertOrDefault<int>()] : null),
                            (cols.SAMP_PREP_THERM_PRESERVCol != null ? parts[cols.SAMP_PREP_THERM_PRESERVCol.ConvertOrDefault<int>()] : null),
                            (cols.SAMP_PREP_STORAGE_DESCCol != null ? parts[cols.SAMP_PREP_STORAGE_DESCCol.ConvertOrDefault<int>()] : null),
                            "P", "", bioIndicator, false);

                        //import result record
                        int TempImportResultID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_RESULT(null, TempImportSampID, null,
                            (cols.DATA_LOGGER_LINECol != null ? parts[cols.DATA_LOGGER_LINECol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_DETECT_CONDITIONCol != null ? parts[cols.RESULT_DETECT_CONDITIONCol.ConvertOrDefault<int>()] : null),
                            (cols.CHAR_NAMECol != null ? parts[cols.CHAR_NAMECol.ConvertOrDefault<int>()] : null),
                            (cols.METHOD_SPECIATION_NAMECol != null ? parts[cols.METHOD_SPECIATION_NAMECol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_SAMP_FRACTIONCol != null ? parts[cols.RESULT_SAMP_FRACTIONCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_MSRCol != null ? parts[cols.RESULT_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_MSR_UNITCol != null ? parts[cols.RESULT_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_MSR_QUALCol != null ? parts[cols.RESULT_MSR_QUALCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_STATUSCol != null ? parts[cols.RESULT_STATUSCol.ConvertOrDefault<int>()] : null),
                            (cols.STATISTIC_BASE_CODECol != null ? parts[cols.STATISTIC_BASE_CODECol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_VALUE_TYPECol != null ? parts[cols.RESULT_VALUE_TYPECol.ConvertOrDefault<int>()] : null),
                            (cols.WEIGHT_BASISCol != null ? parts[cols.WEIGHT_BASISCol.ConvertOrDefault<int>()] : null),
                            (cols.TIME_BASISCol != null ? parts[cols.TIME_BASISCol.ConvertOrDefault<int>()] : null),
                            (cols.TEMP_BASISCol != null ? parts[cols.TEMP_BASISCol.ConvertOrDefault<int>()] : null),
                            (cols.PARTICLESIZE_BASISCol != null ? parts[cols.PARTICLESIZE_BASISCol.ConvertOrDefault<int>()] : null),
                            (cols.PRECISION_VALUECol != null ? parts[cols.PRECISION_VALUECol.ConvertOrDefault<int>()] : null),
                            (cols.BIAS_VALUECol != null ? parts[cols.BIAS_VALUECol.ConvertOrDefault<int>()] : null),
                            (cols.CONFIDENCE_INTERVAL_VALUECol != null ? parts[cols.CONFIDENCE_INTERVAL_VALUECol.ConvertOrDefault<int>()] : null),
                            (cols.UPPER_CONFIDENCE_LIMITCol != null ? parts[cols.UPPER_CONFIDENCE_LIMITCol.ConvertOrDefault<int>()] : null),
                            (cols.LOWER_CONFIDENCE_LIMITCol != null ? parts[cols.LOWER_CONFIDENCE_LIMITCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_COMMENTCol != null ? parts[cols.RESULT_COMMENTCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_DEPTH_HEIGHT_MSRCol != null ? parts[cols.RESULT_DEPTH_HEIGHT_MSRCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_DEPTH_HEIGHT_MSR_UNITCol != null ? parts[cols.RESULT_DEPTH_HEIGHT_MSR_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_DEPTH_REF_POINTCol != null ? parts[cols.RESULT_DEPTH_REF_POINTCol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_INTENT_NAMECol != null ? parts[cols.BIO_INTENT_NAMECol.ConvertOrDefault<int>()] : null),
                            (cols.BIO_INDIVIDUAL_IDCol != null ? parts[cols.BIO_INDIVIDUAL_IDCol.ConvertOrDefault<int>()] : null),
                            (cols.TAXON_NameCol != null ? parts[cols.TAXON_NameCol.ConvertOrDefault<int>()] : null),
                            (cols.UNIDENT_SPECIES_IDCol != null ? parts[cols.UNIDENT_SPECIES_IDCol.ConvertOrDefault<int>()] : null),
                            (cols.TISSUE_ANATOMYCol != null ? parts[cols.TISSUE_ANATOMYCol.ConvertOrDefault<int>()] : null),
                            (cols.GROUP_SUMM_TOTALCol != null ? parts[cols.GROUP_SUMM_TOTALCol.ConvertOrDefault<int>()] : null),
                            (cols.GROUP_SUMM_TOTAL_UNITCol != null ? parts[cols.GROUP_SUMM_TOTAL_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.CELL_FORMCol != null ? parts[cols.CELL_FORMCol.ConvertOrDefault<int>()] : null),
                            (cols.CELL_SHAPECol != null ? parts[cols.CELL_SHAPECol.ConvertOrDefault<int>()] : null),
                            (cols.HABIT_NAMECol != null ? parts[cols.HABIT_NAMECol.ConvertOrDefault<int>()] : null),
                            (cols.VOLTINISM_NAMECol != null ? parts[cols.VOLTINISM_NAMECol.ConvertOrDefault<int>()] : null),
                            (cols.POLL_TOLERANCECol != null ? parts[cols.POLL_TOLERANCECol.ConvertOrDefault<int>()] : null),
                            (cols.POLL_TOLERANCE_SCALECol != null ? parts[cols.POLL_TOLERANCE_SCALECol.ConvertOrDefault<int>()] : null),
                            (cols.TROPHIC_LEVELCol != null ? parts[cols.TROPHIC_LEVELCol.ConvertOrDefault<int>()] : null),
                            (cols.FEEDING_GROUP1Col != null ? parts[cols.FEEDING_GROUP1Col.ConvertOrDefault<int>()] : null),
                            (cols.FEEDING_GROUP2Col != null ? parts[cols.FEEDING_GROUP2Col.ConvertOrDefault<int>()] : null),
                            (cols.FEEDING_GROUP3Col != null ? parts[cols.FEEDING_GROUP3Col.ConvertOrDefault<int>()] : null),
                            (cols.FREQ_CLASS_CODECol != null ? parts[cols.FREQ_CLASS_CODECol.ConvertOrDefault<int>()] : null),
                            (cols.FREQ_CLASS_UNITCol != null ? parts[cols.FREQ_CLASS_UNITCol.ConvertOrDefault<int>()] : null),
                            (cols.FREQ_CLASS_UPPERCol != null ? parts[cols.FREQ_CLASS_UPPERCol.ConvertOrDefault<int>()] : null),
                            (cols.FREQ_CLASS_LOWERCol != null ? parts[cols.FREQ_CLASS_LOWERCol.ConvertOrDefault<int>()] : null),
                            null,
                            (cols.ANALYTIC_METHOD_IDCol != null ? parts[cols.ANALYTIC_METHOD_IDCol.ConvertOrDefault<int>()] : null),
                            (cols.ANALYTIC_METHOD_CTXCol != null ? parts[cols.ANALYTIC_METHOD_CTXCol.ConvertOrDefault<int>()] : null),
                            (cols.ANALYTIC_METHOD_NAMECol != null ? parts[cols.ANALYTIC_METHOD_NAMECol.ConvertOrDefault<int>()] : null),
                            null,
                            (cols.LAB_NAMECol != null ? parts[cols.LAB_NAMECol.ConvertOrDefault<int>()] : null),
                            (cols.LAB_ANALYSIS_START_DTCol != null ? 
                                (parts[cols.LAB_ANALYSIS_START_DTCol.ConvertOrDefault<int>()] + " " + 
                                (cols.LAB_ANALYSIS_START_TIMECol != null ? parts[cols.LAB_ANALYSIS_START_TIMECol.ConvertOrDefault<int>()] : null)
                                ) : null).ConvertOrDefault<DateTime?>(),
                            (cols.LAB_ANALYSIS_END_DTCol != null ? parts[cols.LAB_ANALYSIS_END_DTCol.ConvertOrDefault<int>()] : null).ConvertOrDefault<DateTime?>(),
                            (cols.LAB_ANALYSIS_TIMEZONECol != null ? parts[cols.LAB_ANALYSIS_TIMEZONECol.ConvertOrDefault<int>()] : null),
                            (cols.RESULT_LAB_COMMENT_CODECol != null ? parts[cols.RESULT_LAB_COMMENT_CODECol.ConvertOrDefault<int>()] : null),
                            (cols.METHOD_DETECTION_LEVELCol != null ? parts[cols.METHOD_DETECTION_LEVELCol.ConvertOrDefault<int>()] : null),
                            (cols.LAB_REPORTING_LEVELCol != null ? parts[cols.LAB_REPORTING_LEVELCol.ConvertOrDefault<int>()] : null),
                            (cols.PQLCol != null ? parts[cols.PQLCol.ConvertOrDefault<int>()] : null),
                            (cols.LOWER_QUANT_LIMITCol != null ? parts[cols.LOWER_QUANT_LIMITCol.ConvertOrDefault<int>()] : null),
                            (cols.UPPER_QUANT_LIMITCol != null ? parts[cols.UPPER_QUANT_LIMITCol.ConvertOrDefault<int>()] : null),
                            (cols.DETECTION_LIMIT_UNITCol != null ? parts[cols.DETECTION_LIMIT_UNITCol.ConvertOrDefault<int>()] : null),
                            null,
                            (cols.LAB_SAMP_PREP_IDCol != null ? parts[cols.LAB_SAMP_PREP_IDCol.ConvertOrDefault<int>()] : null),
                            (cols.LAB_SAMP_PREP_CTXCol != null ? parts[cols.LAB_SAMP_PREP_CTXCol.ConvertOrDefault<int>()] : null),
                            (cols.LAB_SAMP_PREP_START_DTCol != null ? 
                                (parts[cols.LAB_SAMP_PREP_START_DTCol.ConvertOrDefault<int>()] + " " + 
                                 (cols.LAB_SAMP_PREP_START_TIMECol != null ? parts[cols.LAB_SAMP_PREP_START_TIMECol.ConvertOrDefault<int>()] : null)
                                 ) : null).ConvertOrDefault<DateTime?>(),
                            (cols.LAB_SAMP_PREP_END_DTCol != null ? parts[cols.LAB_SAMP_PREP_END_DTCol.ConvertOrDefault<int>()] : null).ConvertOrDefault<DateTime?>(),
                            (cols.DILUTION_FACTORCol != null ? parts[cols.DILUTION_FACTORCol.ConvertOrDefault<int>()] : null),
                            "P", "", bioIndicator, OrgID, false);

                        if (TempImportResultID == 0)
                            db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE_Status(TempImportSampID, "F", "Unable to validate result [" + parts[cols.CHAR_NAMECol.ConvertOrDefault<int>()] + "]. Contact admin.");
                    }

                }

                i = i + 1;
            }

            if (i > 1)  //can only continue if 2 rows (1 plus 1 header) are imported
                Response.Redirect("~/App_Pages/Secure/WQXImportSample.aspx");
            else
            {
                lblMsg.Text = "No valid data found. You must include column headers.";
                return false;
            }


            return true;
        }

        private static ImportSampleColumnArray SetColInfo(ImportSampleColumnArray cols, string part, bool bioIndicator, int j, List<string> ActivityIDList, List<string> ActivityMediaList, List<string> ActivityStartDateList, List<string> ActivityStartTimeList, List<string> SampPrepList, List<string> LabNameList, List<string> CharacteristicList, List<string> ResultUnitList, List<string> DetectionConditionList, List<string> DetectionLimitUnit, List<string> DilutionList)
        {
            string partUpper = part.ToUpper();

            if (partUpper == "STATION ID") cols.MONLOC_IDCol = j;
            if (ActivityIDList.Contains(partUpper)) cols.ACTIVITY_IDCol = j;
            if (partUpper == "ACTIVITY TYPE CODE") cols.ACT_TYPECol = j;
            if (ActivityMediaList.Contains(partUpper)) cols.ACT_MEDIACol = j;
            if (partUpper == "ACTIVITY SUBMEDIA") cols.ACT_SUBMEDIACol = j;
            if (ActivityStartDateList.Contains(partUpper)) cols.ACTIVITY_START_DTCol = j;
            if (ActivityStartTimeList.Contains(partUpper)) cols.ACTIVITY_START_TIMECol = j;
            if (partUpper == "ACTIVITY END DATE") cols.ACT_END_DTCol = j;
            if (partUpper == "ACTIVITY END TIME") cols.ACT_END_TIMECol = j;
            if (partUpper == "ACTIVITY TIME ZONE") cols.ACT_TIME_ZONECol = j;
            if (partUpper == "RELATIVE DEPTH") cols.RELATIVE_DEPTH_NAMECol = j;
            if (partUpper == "DEPTH MEASURE") cols.ACT_DEPTHHEIGHT_MSRCol = j;
            if (partUpper == "DEPTH MEASURE UNIT") cols.ACT_DEPTHHEIGHT_MSR_UNITCol = j;
            if (partUpper == "TOP DEPTH MEASURE") cols.TOP_DEPTHHEIGHT_MSRCol = j;
            if (partUpper == "TOP DEPTH MEASURE UNIT") cols.TOP_DEPTHHEIGHT_MSR_UNITCol = j;
            if (partUpper == "BOTTOM DEPTH MEASURE") cols.BOT_DEPTHHEIGHT_MSRCol = j;
            if (partUpper == "BOTTOM DEPTH MEASURE UNIT") cols.BOT_DEPTHHEIGHT_MSR_UNITCol = j;
            if (partUpper == "DEPTH REF POINT") cols.DEPTH_REF_POINTCol = j;
            if (partUpper == "ACTIVITY COMMENT") cols.ACT_COMMENTCol = j;
            if (partUpper == "BIO ASSEMBLAGE SAMPLED") { cols.BIO_ASSEMBLAGE_SAMPLEDCol = j; bioIndicator = true; }
            if (partUpper == "BIO DURATION") { cols.BIO_DURATION_MSRCol = j; bioIndicator = true; }
            if (partUpper == "BIO DURATION UNIT") { cols.BIO_DURATION_MSR_UNITCol = j; bioIndicator = true; }
            if (partUpper == "SAMPLING COMPONENT") { cols.BIO_SAMP_COMPONENTCol = j; bioIndicator = true; }
            if (partUpper == "SAMPLING COMPONENT PLACE") { cols.BIO_SAMP_COMPONENT_SEQCol = j; bioIndicator = true; }
            if (partUpper == "REACH LENGTH") { cols.BIO_REACH_LEN_MSRCol = j; bioIndicator = true; }
            if (partUpper == "REACH LENGTH UNIT") { cols.BIO_REACH_LEN_MSR_UNITCol = j; bioIndicator = true; }
            if (partUpper == "REACH WIDTH") { cols.BIO_REACH_WID_MSRCol = j; bioIndicator = true; }
            if (partUpper == "REACH WIDTH UNIT") { cols.BIO_REACH_WID_MSR_UNITCol = j; bioIndicator = true; }
            if (partUpper == "PASS COUNT") { cols.BIO_PASS_COUNTCol = j; bioIndicator = true; }
            if (partUpper == "NET TYPE") { cols.BIO_NET_TYPECol = j; bioIndicator = true; }
            if (partUpper == "NET SURFACE AREA") { cols.BIO_NET_AREA_MSRCol = j; bioIndicator = true; }
            if (partUpper == "SURFACE AREA UNIT") { cols.BIO_NET_AREA_MSR_UNITCol = j; bioIndicator = true; }
            if (partUpper == "NET MESH SIZE") { cols.BIO_NET_MESHSIZE_MSRCol = j; bioIndicator = true; }
            if (partUpper == "MESH SIZE UNIT") { cols.BIO_MESHSIZE_MSR_UNITCol = j; bioIndicator = true; }
            if (partUpper == "BOAT SPEED") { cols.BIO_BOAT_SPEED_MSRCol = j; bioIndicator = true; }
            if (partUpper == "BOAT SPEED UNIT") { cols.BIO_BOAT_SPEED_MSR_UNITCol = j; bioIndicator = true; }
            if (partUpper == "CURRENT SPEED") { cols.BIO_CURR_SPEED_MSRCol = j; bioIndicator = true; }
            if (partUpper == "CURRENT SPEED UNIT") { cols.BIO_CURR_SPEED_MSR_UNITCol = j; bioIndicator = true; }
            if (partUpper == "TOXICITY TEST TYPE") { cols.BIO_TOXICITY_TEST_TYPECol = j; bioIndicator = true; }
            if (partUpper == "COLLECTION METHOD ID") cols.SAMP_COLL_METHOD_IDCol = j;
            if (partUpper == "COLLECTION METHOD CONTEXT") cols.SAMP_COLL_METHOD_CTXCol = j;
            if (partUpper == "COLLECTION EQUIPMENT") cols.SAMP_COLL_EQUIPCol = j;
            if (partUpper == "COLLECTION EQUIPMENT COMMENT") cols.SAMP_COLL_EQUIP_COMMENTCol = j;
            if (SampPrepList.Contains(partUpper)) cols.SAMP_PREP_IDCol = j;
            if (partUpper == "SAMPLE PREP CONTEXT") cols.SAMP_PREP_CTXCol = j;
            if (partUpper == "CONTAINER TYPE") cols.SAMP_PREP_CONT_TYPECol = j;
            if (partUpper == "CONTAINER COLOR") cols.SAMP_PREP_CONT_COLORCol = j;
            if (partUpper == "CHEM PRESERVATIVE USED") cols.SAMP_PREP_CHEM_PRESERVCol = j;
            if (partUpper == "THERMAL PRESERVATIVE USED") cols.SAMP_PREP_THERM_PRESERVCol = j;
            if (partUpper == "TRANSPORT STORAGE DESCRIPTION") cols.SAMP_PREP_STORAGE_DESCCol = j;
            if (partUpper == "DATA LOGGER LINE") cols.DATA_LOGGER_LINECol = j;
            if (DetectionConditionList.Contains(partUpper)) cols.RESULT_DETECT_CONDITIONCol = j;
            if (CharacteristicList.Contains(partUpper)) cols.CHAR_NAMECol = j;
            if (partUpper == "METHOD SPECIATION") cols.METHOD_SPECIATION_NAMECol = j;
            if (partUpper == "SAMPLE FRACTION") cols.RESULT_SAMP_FRACTIONCol = j;
            if (partUpper == "RESULT") cols.RESULT_MSRCol = j;
            if (ResultUnitList.Contains(partUpper)) cols.RESULT_MSR_UNITCol = j;
            if (partUpper == "MEASURE QUALIFIER") cols.RESULT_MSR_QUALCol = j;
            if (partUpper == "RESULT STATUS") cols.RESULT_STATUSCol = j;
            if (partUpper == "STATISTICAL BASE CODE") cols.STATISTIC_BASE_CODECol = j;
            if (partUpper == "RESULT VALUE TYPE") cols.RESULT_VALUE_TYPECol = j;
            if (partUpper == "WEIGHT BASIS") cols.WEIGHT_BASISCol = j;
            if (partUpper == "TIME BASIS") cols.TIME_BASISCol = j;
            if (partUpper == "TEMPERATURE BASIS") cols.TEMP_BASISCol = j;
            if (partUpper == "PARTICLE SIZE BASIS") cols.PARTICLESIZE_BASISCol = j;
            if (partUpper == "PRECISION") cols.PRECISION_VALUECol = j;
            if (partUpper == "BIAS") cols.BIAS_VALUECol = j;
            if (partUpper == "CONFIDENCE INTERVAL") cols.CONFIDENCE_INTERVAL_VALUECol = j;
            if (partUpper == "CONFIDENCE UPPER") cols.UPPER_CONFIDENCE_LIMITCol = j;
            if (partUpper == "CONFIDENCE LOWER") cols.LOWER_CONFIDENCE_LIMITCol = j;
            if (partUpper == "RESULT COMMENTS") cols.RESULT_COMMENTCol = j;
            if (partUpper == "RESULT DEPTH") cols.RESULT_DEPTH_HEIGHT_MSRCol = j;
            if (partUpper == "RESULT DEPTH UNIT") cols.RESULT_DEPTH_HEIGHT_MSR_UNITCol = j;
            if (partUpper == "RESULT DEPTH REF POINT") cols.RESULT_DEPTH_REF_POINTCol = j;
            if (partUpper == "BIO INTENT NAME") { cols.BIO_INTENT_NAMECol = j; bioIndicator = true; }
            if (partUpper == "BIO INDIVIDUAL ID") { cols.BIO_INDIVIDUAL_IDCol = j; bioIndicator = true; }
            if (partUpper == "TAXONOMIC NAME") { cols.TAXON_NameCol = j; bioIndicator = true; }
            if (partUpper == "UNIDENTIFIED SPECIES") { cols.UNIDENT_SPECIES_IDCol = j; bioIndicator = true; }
            if (partUpper == "TISSUE ANATOMY") { cols.TISSUE_ANATOMYCol = j; bioIndicator = true; }
            if (partUpper == "GROUP SUMMARY TOTAL") { cols.GROUP_SUMM_TOTALCol = j; bioIndicator = true; }
            if (partUpper == "GROUP SUMMARY TOTAL UNIT") { cols.GROUP_SUMM_TOTAL_UNITCol = j; bioIndicator = true; }
            if (partUpper == "CELL FORM") { cols.CELL_FORMCol = j; bioIndicator = true; }
            if (partUpper == "CELL SHAPE") { cols.CELL_SHAPECol = j; bioIndicator = true; }
            if (partUpper == "HABIT NAME") { cols.HABIT_NAMECol = j; bioIndicator = true; }
            if (partUpper == "VOLTINISM NAME") { cols.VOLTINISM_NAMECol = j; bioIndicator = true; }
            if (partUpper == "POLLUTION TOLERANCE") { cols.POLL_TOLERANCECol = j; bioIndicator = true; }
            if (partUpper == "POLLUTION TOLERANCE SCALE") { cols.POLL_TOLERANCE_SCALECol = j; bioIndicator = true; }
            if (partUpper == "TROPHIC LEVEL") { cols.TROPHIC_LEVELCol = j; bioIndicator = true; }
            if (partUpper == "FUNCTIONAL FEEDING GROUP") { cols.FEEDING_GROUP1Col = j; bioIndicator = true; }
            if (partUpper == "FUNCTIONAL FEEDING GROUP 2") { cols.FEEDING_GROUP2Col = j; bioIndicator = true; }
            if (partUpper == "FUNCTIONAL FEEDING GROUP 3") { cols.FEEDING_GROUP3Col = j; bioIndicator = true; }
            if (partUpper == "FREQUENCY CLASS") { cols.FREQ_CLASS_CODECol = j; bioIndicator = true; }
            if (partUpper == "FREQUENCY CLASS UNIT") { cols.FREQ_CLASS_UNITCol = j; bioIndicator = true; }
            if (partUpper == "FREQUENCY CLASS UPPER") { cols.FREQ_CLASS_UPPERCol = j; bioIndicator = true; }
            if (partUpper == "FREQUENCY CLASS LOWER") { cols.FREQ_CLASS_LOWERCol = j; bioIndicator = true; }
            if (partUpper == "ANALYTICAL METHOD ID") cols.ANALYTIC_METHOD_IDCol = j;
            if (partUpper == "ANALYTICAL METHOD CONTEXT") cols.ANALYTIC_METHOD_CTXCol = j;
            if (LabNameList.Contains(partUpper)) cols.LAB_NAMECol = j;
            if (partUpper == "ANALYSIS START DATE") cols.LAB_ANALYSIS_START_DTCol = j;
            if (partUpper == "ANALYSIS START TIME") cols.LAB_ANALYSIS_START_TIMECol = j;
            if (partUpper == "ANALYSIS END DATE") cols.LAB_ANALYSIS_END_DTCol = j;
            if (partUpper == "LAB COMMENT CODE") cols.RESULT_LAB_COMMENT_CODECol = j;
            if (partUpper == "METHOD DETECTION LEVEL") cols.METHOD_DETECTION_LEVELCol = j;
            if (partUpper == "LABORATORY REPORTING LEVEL") cols.LAB_REPORTING_LEVELCol = j;
            if (partUpper == "PRACTICAL QUANTITATION LIMIT") cols.PQLCol = j;
            if (partUpper == "LOWER QUANTITATION LIMIT") cols.LOWER_QUANT_LIMITCol = j;
            if (partUpper == "UPPER QUANTITATION LIMIT") cols.UPPER_QUANT_LIMITCol = j;
            if (DetectionLimitUnit.Contains(partUpper)) cols.DETECTION_LIMIT_UNITCol = j;
            if (partUpper == "LAB SAMPLE PREP ID") cols.LAB_SAMP_PREP_IDCol = j;
            if (partUpper == "LAB SAMPLE PREP CONTEXT") cols.LAB_SAMP_PREP_CTXCol = j;
            if (partUpper == "PREPARATION START DATE") cols.LAB_SAMP_PREP_START_DTCol = j;
            if (partUpper == "PREPARATION START TIME") cols.LAB_SAMP_PREP_START_TIMECol = j;
            if (partUpper == "PREPARATION END DATE") cols.LAB_SAMP_PREP_END_DTCol = j;
            if (DilutionList.Contains(partUpper)) cols.DILUTION_FACTORCol = j;
            if (partUpper == "ACTIVITY ID AND TYPE") cols.specialACTIVITY_ID__ACT_TYPECol = j;

            return cols;
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
            T_WQX_IMPORT_TEMPLATE_DTL SampleMethodCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "SAMP_COLL_METHOD_IDX");
            T_WQX_IMPORT_TEMPLATE_DTL SampleEquipmentCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "SAMP_COLL_EQUIP");

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
                string[] parts = row.Split(delimiters, StringSplitOptions.None); //columns split into parts  //2/24/2016 change from RemoveEmptyEntries to None
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
                            ActivityIDVal = (MonLocIDVal ?? "").SubStringPlus(0,21) + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmm");
                        else if (ActivityIDCol.CHAR_NAME == "#M_D_T")
                            ActivityIDVal = (MonLocIDVal ?? "").SubStringPlus(0, 21) + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmm");
                        else if (ActivityIDCol.CHAR_NAME == "#M_D_TS")
                            ActivityIDVal = (MonLocIDVal ?? "").SubStringPlus(0, 19) + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmmss");
                        else
                            ActivityIDVal = GetFieldValue(ActivityIDCol, parts);
                    }

                    int TempImportSampID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(null, User.Identity.Name, OrgID, ProjectID, ProjectIDName, MonLocIDXVal, MonLocIDVal, null, ActivityIDVal,
                        ActivityTypeVal, ActivityMediaVal, ActivitySubMediaVal, ActivityStartDateVal, ActivityStartDateVal, null, null, null, null, null, null, null, null, null,
                        GetFieldValue(ActivityCommentsCol, parts),
                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, GetFieldValue(SampleMethodCol, parts).ConvertOrDefault<int?>(), 
                        null, null, null, GetFieldValue(SampleEquipmentCol, parts), null, null, null, null, null, null, null, null, null, null,
                        (valMsg.Length > 0 ? "F" : "P"), valMsg, false, false);


                    //now loop through any potential characteristics
                    List<T_WQX_IMPORT_TEMPLATE_DTL> chars = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_CharsByTemplateID(TemplateID);
                    foreach (T_WQX_IMPORT_TEMPLATE_DTL character in chars)
                    {
                        string resultVal = GetFieldValue(character, parts);

                        if (!string.IsNullOrEmpty(resultVal))
                        {
                            int TempImportResultID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_RESULT(null, TempImportSampID, null, null, null, character.CHAR_NAME, null,
                            (string.IsNullOrEmpty(character.CHAR_DEFAULT_SAMP_FRACTION) ? null : character.CHAR_DEFAULT_SAMP_FRACTION), resultVal,
                            character.CHAR_DEFAULT_UNIT, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                            "P", "", false, OrgID, false);
                        }
                    }
                }
            }

            Response.Redirect("~/App_Pages/Secure/WQXImportSample.aspx");

            return true;
        }

        private bool ImportMonLoc(string OrgID, string[] rows)
        {
            if (db_WQX.DeleteT_WQX_IMPORT_TEMP_MONLOC(User.Identity.Name) == 0)
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
                            if (HUC_EIGHTCol == null)  //use generated column if main column is not used
                                if (part == "Generated Hydrologic Unit Code") HUC_EIGHTCol = j;

                            if (part == "HUC Twelve Digit Code") HUC_TWELVECol = j;
                            if (HUC_TWELVECol == null) //use generated column if main column is not used
                                if (part == "Generated HUC Twelve Digit Code") HUC_TWELVECol = j;

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
                            if (COUNTRY_CODECol == null)
                                if (part == "Country Name") COUNTRY_CODECol = j;

                            if (part == "State Code") sTATE_CODECol = j;
                            if (sTATE_CODECol == null)
                                if (part == "State") sTATE_CODECol = j;

                            if (part == "County Code") cOUNTY_CODECol = j;
                            if (cOUNTY_CODECol == null)
                                if (part == "County") cOUNTY_CODECol = j;

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

                        ActID = db_WQX.InsertOrUpdateWQX_ACTIVITY(ActID, OrgID, ProjectID, MonLocIDX, sampleIDs[i], "Field Msr/Obs-Habitat Assessment", "Water", "", dates[i].ConvertOrDefault<DateTime>(),
                            null, "", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 
                            "U", true, true, User.Identity.Name, "H");

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
                db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, ddlImportType.SelectedValue, ddlImportType.SelectedValue, 0, "Success", "100", null, null, User.Identity.Name);
            }
            catch (Exception ex)
            {
                //add to import log
                db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, ddlImportType.SelectedValue, ddlImportType.SelectedValue, 0, "Fail", "0", ex.Message.SubStringPlus(0,1000), null, User.Identity.Name);
            }
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

        private static string TranslateField(string fromField, Dictionary<string, string> transPiece)
        {
            if (transPiece.Count > 0)
            {
                if (transPiece.ContainsKey(fromField))
                    return transPiece[fromField];
                else
                    return fromField;
            }
            else
                return fromField;
        }

        protected void btnDefaults_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrgSettings.aspx");
        }

        protected void btnTranslate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImportDefaults.aspx");
        }


        //*************************** IMPORT LOGIC FROM EPA-WQX ******************************************************
        protected void btnWQXContinue_Click(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select or create an organization first.";
                btnParse.Visible = false;
                return;
            }

            string OrgID = Session["OrgID"].ToString();

            if (ddlWQXImportType.SelectedValue == "MLOC")
                WQXImport_MonLoc(OrgID, User.Identity.Name);

            if (ddlWQXImportType.SelectedValue == "PROJ")
                WQXImport_Proj(OrgID);

            if (ddlWQXImportType.SelectedValue == "ACT")
                WQXImport_Activity(OrgID);
        }

        private bool WQXImport_MonLoc(string OrgID, string UserID)
        {
            int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMP_MONLOC(UserID);
            if (SuccID == 0)
            {
                lblMsg.Text = "Unable to proceed with import.";
                return false;
            }

            try
            {
                //get CDX username, password, and CDX destination URL
                CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(OrgID);

                //*******AUTHENTICATE***********************************
                string token = WQXSubmit.AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL);

                //*******QUERY*****************************************
                if (token.Length > 0)
                {
                    List<net.epacdxnode.test.ParameterType> pars = new List<net.epacdxnode.test.ParameterType>();

                    net.epacdxnode.test.ParameterType p = new net.epacdxnode.test.ParameterType();
                    p.parameterName = "organizationIdentifier";
                    p.Value = OrgID;//txtOrgTest.Text
                    pars.Add(p);

                    net.epacdxnode.test.ParameterType p2 = new net.epacdxnode.test.ParameterType();
                    p2.parameterName = "monitoringLocationIdentifier";
                    p2.Value = "";
                    pars.Add(p2);

                    net.epacdxnode.test.ResultSetType queryResp = WQXSubmit.QueryHelper(cred.NodeURL, token, "WQX", "WQX.GetMonitoringLocationByParameters_v2.1", null, null, pars);

                    //handle no response
                    if (queryResp == null)
                    {
                        lblMsg.Text = "No monitoring locations found at EPA for this organization.";
                        return true;
                    }

                    XDocument xdoc = XDocument.Parse(queryResp.results.Any[0].InnerXml);
                    var mlocs = (from mloc
                                in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocation")
                                select new
                                {
                                    ID = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentifier") ?? String.Empty,
                                    Name = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationName") ?? String.Empty,
                                    MonLocType = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationTypeName") ?? String.Empty,
                                    Desc = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationDescriptionText") ?? String.Empty,
                                    HUC8 = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}HUCEightDigitCode") ?? String.Empty,
                                    HUC12 = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}HUCTwelveDigitCode") ?? String.Empty,
                                    TribeLandInd = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}TribalLandIndicator") ?? String.Empty,
                                    TribeLandName = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationIdentity").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}TribalLandName") ?? String.Empty,
                                    Lat = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}LatitudeMeasure") ?? String.Empty,
                                    Long = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}LongitudeMeasure") ?? String.Empty,
                                    SourceMapScale = (int?)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}SourceMapScaleNumeric").ConvertOrDefault<int?>(),
                                    HorizontalAccuracyMeasure = mloc.Element("{http://www.exchangenetwork.net/schema/wqx/2}HorizontalAccuracyMeasure") != null ? (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}HorizontalAccuracyMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                    HorizontalAccuracyMeasureUnit = mloc.Element("{http://www.exchangenetwork.net/schema/wqx/2}HorizontalAccuracyMeasure") != null ? (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}HorizontalAccuracyMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                    HorizontalCollectionMethodName = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}HorizontalCollectionMethodName") ?? String.Empty,
                                    HorizontalCoordinateReferenceSystemDatumName = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}HorizontalCoordinateReferenceSystemDatumName") ?? String.Empty,
                                    VerticalMeasure = mloc.Element("{http://www.exchangenetwork.net/schema/wqx/2}VerticalMeasure") != null ? (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}VerticalMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureValue") ?? String.Empty : "",
                                    VerticalMeasureUnit = mloc.Element("{http://www.exchangenetwork.net/schema/wqx/2}VerticalMeasure") != null ? (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}VerticalMeasure").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}MeasureUnitCode") ?? String.Empty : "",
                                    VerticalCollectionMethodName = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}VerticalCollectionMethodName") ?? String.Empty,
                                    VerticalCoordinateReferenceSystemDatumName = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}VerticalCoordinateReferenceSystemDatumName") ?? String.Empty,
                                    CountryCode = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}CountryCode") ?? String.Empty,
                                    StateCode = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}StateCode") ?? String.Empty,
                                    CountyCode = (string)mloc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}MonitoringLocationGeospatial").FirstOrDefault().Element("{http://www.exchangenetwork.net/schema/wqx/2}CountyCode") ?? String.Empty
                                });

                    //loop through retrieved data and insert into temp table
                    if (mlocs != null)
                    {
                        foreach (var mloc in mlocs)
                        {
                            int Succ = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_MONLOC(null, UserID, null, OrgID, mloc.ID, mloc.Name, mloc.MonLocType, mloc.Desc, mloc.HUC8, mloc.HUC12, mloc.TribeLandInd,
                                mloc.TribeLandName, mloc.Lat, mloc.Long, mloc.SourceMapScale, mloc.HorizontalAccuracyMeasure, mloc.HorizontalAccuracyMeasureUnit, mloc.HorizontalCollectionMethodName,
                                mloc.HorizontalCoordinateReferenceSystemDatumName, mloc.VerticalMeasure, mloc.VerticalMeasureUnit, mloc.VerticalCollectionMethodName, mloc.VerticalCoordinateReferenceSystemDatumName,
                                mloc.CountryCode, mloc.StateCode, mloc.CountyCode, null, null, null, null, null, "P", "");
                        }
                    }
                    Response.Redirect("~/App_Pages/Secure/WQXImportMonLoc.aspx?e=1");
                    return true;

                }
                else
                {
                    lblMsg.Text = "Unable to authenticate to EPA-WQX server.";
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool WQXImport_Proj(string OrgID)
        {
            //first delete any previous temp import data
            int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMP_PROJECT(User.Identity.Name);
            if (SuccID == 0)
            {
                lblMsg.Text = "Unable to proceed with import.";
                return false;
            }

            try
            {
                //get CDX username, password, and CDX destination URL
                CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(OrgID);

                //*******AUTHENTICATE***********************************
                string token = WQXSubmit.AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL);

                //*******QUERY*****************************************
                if (token.Length > 0)
                {
                    List<net.epacdxnode.test.ParameterType> pars = new List<net.epacdxnode.test.ParameterType>();

                    net.epacdxnode.test.ParameterType p = new net.epacdxnode.test.ParameterType();
                    p.parameterName = "organizationIdentifier";
                    p.Value = OrgID;
                    pars.Add(p);

                    net.epacdxnode.test.ResultSetType queryResp = WQXSubmit.QueryHelper(cred.NodeURL, token, "WQX", "WQX.GetProjectByParameters_v2.1", null, null, pars);
                    XDocument xdoc = XDocument.Parse(queryResp.results.Any[0].InnerXml);
                    var projects = (from project
                                in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}Project")
                                 select new
                                 {
                                     ID = (string)project.Element("{http://www.exchangenetwork.net/schema/wqx/2}ProjectIdentifier") ?? String.Empty,
                                     Name = (string)project.Element("{http://www.exchangenetwork.net/schema/wqx/2}ProjectName") ?? String.Empty,
                                     Desc = (string)project.Element("{http://www.exchangenetwork.net/schema/wqx/2}ProjectDescriptionText") ?? String.Empty,
                                     SamplingDesignType = (string)project.Element("{http://www.exchangenetwork.net/schema/wqx/2}SamplingDesignTypeCode") ?? String.Empty,
                                     QAPPInd = (string)project.Element("{http://www.exchangenetwork.net/schema/wqx/2}QAPPApprovedIndicator") ?? String.Empty,
                                     QAPPAgency = (string)project.Element("{http://www.exchangenetwork.net/schema/wqx/2}QAPPApprovalAgencyName") ?? String.Empty,
                                 });

                    //loop through retrieved data and insert into temp table
                    if (projects != null)
                    {
                        foreach (var project in projects)
                        {
                            int Succ = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_PROJECT(null, User.Identity.Name, null, OrgID, project.ID, project.Name, project.Desc, project.SamplingDesignType, 
                                project.QAPPInd.ConvertOrDefault<Boolean?>(), project.QAPPAgency, "P", "");
                        }
                    }
                    Response.Redirect("~/App_Pages/Secure/WQXImportProject.aspx?e=1");
                    return true;

                }
                else
                {
                    lblMsg.Text = "Unable to authenticate to EPA-WQX server.";
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool WQXImport_Activity(string OrgID)
        {
            //first delete any previous temp import data
            int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMP_SAMPLE(User.Identity.Name);
            if (SuccID == 0)
            {
                lblMsg.Text = "Unable to proceed with import.";
                return false;
            }

            //insert to IMPORT LOG
            int importID = db_Ref.InsertUpdateWQX_IMPORT_LOG(null, OrgID, "Sample", "Sample", 0, "New", "0", "Import scheduled", null, User.Identity.Name);

            //temporary until async is put in place****************************************
            WQXSubmit.ImportActivity(OrgID, importID, User.Identity.Name);
            Response.Redirect("~/App_Pages/Secure/WQXImportSample.aspx");

            //*****************************************************************************
            lblMsg.Text = "Import has been scheduled and may take a while to process - please return to this page later to see the import progress. ";
            return true;
        }


        //*************************** IMPORT HISTORY HANDLING ******************************************************
        protected void grdImport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletes")
            {
                db_Ref.DeleteT_WQX_IMPORT_LOG(e.CommandArgument.ToString().ConvertOrDefault<int>());
                Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
            }
        }

    }
}