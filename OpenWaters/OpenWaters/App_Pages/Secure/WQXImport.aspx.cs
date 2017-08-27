using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment.App_Pages.Secure
{
    public partial class WQXImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);

            if (!IsPostBack)
            {
                //redirect to sample page if any are in progress
                if (db_WQX.GetWQX_IMPORT_TEMP_SAMPLE_CountByUserID(User.Identity.Name) > 0)
                    Response.Redirect("~/App_Pages/Secure/WQXImportSample.aspx");

                //redirect to monloc page if any are in progress
                if (db_WQX.GetWQX_IMPORT_TEMP_MONLOC_CountByUserID(User.Identity.Name) > 0)
                    Response.Redirect("~/App_Pages/Secure/WQXImportMonLoc.aspx");

                //redirect to monloc page if any are in progress
                if (db_WQX.GetWQX_IMPORT_TEMP_ACTIVITY_METRIC_CountByUserID(User.Identity.Name) > 0)
                    Response.Redirect("~/App_Pages/Secure/WQXImportMetric.aspx");

                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                Utils.BindList(ddlTemplate, dsTemplate, "TEMPLATE_ID", "TEMPLATE_NAME");
            }
        }

        protected void ddlImportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            pnlStep2.Visible = (ddlImportType.SelectedValue != "");
            pnlPasteData.Visible = (ddlImportType.SelectedValue != "");
            hlTemplate.Visible = (ddlImportType.SelectedValue != "");
            pnlSampOptions.Visible = (ddlImportType.SelectedValue == "S");
            pnlText2.Visible = (ddlImportType.SelectedValue == "I");
            pnlProject.Visible = (ddlImportType.SelectedValue == "I" || ddlImportType.SelectedValue == "S");
            lblPasteInst.Text = (ddlImportType.SelectedValue =="I" ? "Import Activity Metrics Here" : "");


            if (ddlImportType.SelectedValue == "M")
                hlTemplate.NavigateUrl = "~/App_Docs/MonLoc_ImportTemplate.xlsx";
            else if (ddlImportType.SelectedValue == "S")
                hlTemplate.NavigateUrl = "~/App_Docs/Samp_ImportTemplate.xlsx";
            else if (ddlImportType.SelectedValue == "I")
                hlTemplate.NavigateUrl = "~/App_Docs/SampBio_ImportTemplate.xlsx";

        }

        protected void rbTemplateInd_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSampTempOptions.Visible = (rbTemplateInd.SelectedValue == "Y");
            lblPasteInst.Text = "When pasting data, " + (rbTemplateInd.SelectedValue == "Y" ? "DO NOT include" : "INCLUDE") + " column headers.";
        }

        protected void btnDefaults_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrgData.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnNewTemplate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImportConfig.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        //generic parse of monitoring locations, regular activities/results, cross-tab and metric indices
        protected void btnParse_Click(object sender, EventArgs e)
        {
            //******************************** VALIDATION *****************************************
            if (Session["OrgID"] == null) { lblMsg.Text = "Please select or create an organization first."; return; }
            if (ddlImportType.SelectedValue == "") { lblMsg.Text = "Please select a data import type."; return; }
            if ((ddlImportType.SelectedValue == "S" || ddlImportType.SelectedValue == "I") && ddlProject.SelectedValue == "") { lblMsg.Text = "Please select a project into which this data will be imported."; return; }
            if (txtPaste.Text.Length == 0) { lblMsg.Text = "You must copy and paste data from a spreadsheet into the large textbox."; return; }
            if (ddlImportType.SelectedValue == "S" && rbTemplateInd.SelectedValue == "") { lblMsg.Text = "Please indicate whether you will use a custom import template."; return; }
            if (ddlImportType.SelectedValue == "S" && rbTemplateInd.SelectedValue == "Y" && ddlTemplate.SelectedValue == "") { lblMsg.Text = "Please select an import template."; return; }
            //********************************* END VALIDATION *************************************

            //delete previous temp data stored for the user
            if (!DeleteTempImportData(ddlImportType.SelectedValue, User.Identity.Name)) { lblMsg.Text = "Unable to proceed with import."; return; }
             
            //define org and project
            string OrgID = Session["OrgID"].ToString();
            int? ProjectID = ddlProject.SelectedValue.ConvertOrDefault<int?>();
            string ProjectName = ddlProject.SelectedItem == null ? "" : ddlProject.SelectedItem.Text;

            //**********************temporary separate handling of cross tab**************
            if (ddlTemplate.SelectedValue != "" && ddlImportType.SelectedValue == "S")
            {
                ImportSampleCT(OrgID, ddlTemplate.SelectedValue.ConvertOrDefault<int>(), ProjectID, ProjectName, txtPaste.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                return;
            }
            //********************** end separate handling of cross tab *******************


            //set dictionaries used to store stuff in memory
            //Dictionary<string, Tuple<string, string>> charsPool = GetCharacteristicsPool();  //list of all possible column headers that are characteristics in Open Waters
            Dictionary<string, int> colMapping = new Dictionary<string, int>();  //identifies the column number for each field to be imported
            List<string> translateFields = db_WQX.GetWQX_IMPORT_TRANSLATE_byColName(OrgID);  //list of fields that have translations defined

            //initialize variables
            bool headInd = true;

            //loop through each row
            foreach (string row in txtPaste.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                //split row's columns into string array
                string[] cols = row.Split(new char[] { '\t' }, StringSplitOptions.None);

                if (cols.Length > 0) //skip blank rows
                {
                    if (headInd)
                    {
                        //**********************************************************
                        //HEADER ROW - LOGIC TO DETERMINE WHAT IS IN EACH COLUMN
                        //**********************************************************
                        colMapping = Utils.GetColumnMapping(ddlImportType.SelectedValue, cols);  

                        headInd = false;
                    }
                    else
                    {
                        //**********************************************************
                        //NOT HEADER ROW - READING IN VALUES
                        //**********************************************************
                        var colList = cols.Select((value, index) => new { value, index });
                        var colDataIndexed = (from f in colMapping
                                              join c in colList on f.Value equals c.index
                                              select new
                                              {
                                                  _Name = f.Key,
                                                  _Val = c.value
                                              }).ToList();

                        Dictionary<string, string> fieldValuesDict = new Dictionary<string, string>();  //identifies the column number for each field to be imported

                        //loop through all values and insert to list
                        foreach (var c in colDataIndexed)
                            fieldValuesDict.Add(c._Name, translateFields.Contains(c._Name) ? db_WQX.GetWQX_IMPORT_TRANSLATE_byColNameAndValue(OrgID, c._Name, c._Val) : c._Val);

                        //IMPORT DATA
                        if (ddlImportType.SelectedValue == "M")
                            db_WQX.InsertWQX_IMPORT_TEMP_MONLOC_New(User.Identity.Name, OrgID, fieldValuesDict);
                        else if (ddlImportType.SelectedValue == "I")
                        {
                            db_WQX.InsertWQX_IMPORT_TEMP_ACTIVITY_METRIC(User.Identity.Name, OrgID, fieldValuesDict);
                        }
                        else if (ddlImportType.SelectedValue == "S")
                        {
                            int _s = db_WQX.InsertUpdateWQX_IMPORT_TEMP_SAMPLE_New(User.Identity.Name, OrgID, ProjectID, ProjectName, fieldValuesDict);
                            if (_s > 0)
                            {
                                int _r = db_WQX.InsertWQX_IMPORT_TEMP_RESULT_New(_s, fieldValuesDict, OrgID);
                                if (_r == 0)
                                    db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE_Status(_s, "F", "Unable to validate result [" + Utils.GetValueOrDefault(fieldValuesDict, "CHAR_NAME") + "]. Contact admin.");
                            }
                        }
                    }
                }
            } //end each row

            if (!headInd)
            {
                string urlz = "Sample";
                if (ddlImportType.SelectedValue == "M") urlz = "MonLoc";
                if (ddlImportType.SelectedValue == "I") urlz = "Metric";
                Response.Redirect("~/App_Pages/Secure/WQXImport" + urlz + ".aspx");
            }
            else
                lblMsg.Text = "No valid data found. You must include column headers.";
        }

        private bool DeleteTempImportData (string ImportType, string User)
        {
            if (ImportType == "M")
                return (db_WQX.DeleteT_WQX_IMPORT_TEMP_MONLOC(User) != 0);
            else if (ImportType == "S")
                return (db_WQX.DeleteT_WQX_IMPORT_TEMP_SAMPLE(User) != 0);
            else if (ImportType == "I")
            {
                db_WQX.DeleteT_WQX_IMPORT_TEMP_ACTIVITY_METRIC(User);
                db_WQX.DeleteT_WQX_IMPORT_TEMP_BIO_INDEX(User);
                return true;
            }
            else
                return false;
        }

        //special handling for cross tab
        private void ImportSampleCT(string OrgID, int TemplateID, int? ProjectID, string ProjectIDName, string[] rows)
        {
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
            T_WQX_IMPORT_TEMPLATE_DTL ActivityDepthCol = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(TemplateID, "ACT_DEPTHHEIGHT_MSR");

            //***********************************
            //loop through each sample
            //***********************************
            foreach (string row in rows)
            {
                //declare variables to store values for the current row
                string valMsg = "";
                string MonLocIDVal = null, ActivityTypeVal = null, ActivityMediaVal = null, ActivitySubMediaVal = null, ActivityIDVal = null;
                string ActivityDepthVal = null, ActivityDepthUnitVal = null;
                int? MonLocIDXVal = null;
                DateTime? ActivityStartDateVal = null, ActivityEndDateVal = null;

                char[] delimiters = new char[] { '\t' };   //tab delimiter
                string[] parts = row.Split(delimiters, StringSplitOptions.None); //columns split into parts  //2/24/2016 change from RemoveEmptyEntries to None
                if (parts.Length > 0)
                {
                    //start of field-by-field validation

                    //monitoring location
                    if (MonLocCol == null)
                    { lblMsg.Text = "Your import logic does not define a monitoring location column - import cannot be performed"; return; }
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
                    ActivityDepthVal = GetFieldValue(ActivityDepthCol, parts);
                    if (ActivityDepthVal != null)
                        if (ActivityDepthVal.Length > 0)
                            ActivityDepthUnitVal = ActivityDepthCol.CHAR_DEFAULT_UNIT;

                    //activity start date 
                    if (ActivityStartDateCol == null)
                    { lblMsg.Text = "Your import logic does not define an activity start date column - import cannot be performed"; return; }
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
                            ActivityIDVal = (MonLocIDVal ?? "").SubStringPlus(0, 21) + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmm");
                        else if (ActivityIDCol.CHAR_NAME == "#M_D_T")
                            ActivityIDVal = (MonLocIDVal ?? "").SubStringPlus(0, 21) + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmm");
                        else if (ActivityIDCol.CHAR_NAME == "#M_D_TS")
                            ActivityIDVal = (MonLocIDVal ?? "").SubStringPlus(0, 19) + "_" + ActivityStartDateVal.Value.ToString("yyyyMMdd_HHmmss");
                        else
                            ActivityIDVal = GetFieldValue(ActivityIDCol, parts);
                    }


                    int TempImportSampID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(null, User.Identity.Name, OrgID, ProjectID, ProjectIDName, MonLocIDXVal, MonLocIDVal, null, ActivityIDVal,
                        ActivityTypeVal, ActivityMediaVal, ActivitySubMediaVal, ActivityStartDateVal, ActivityStartDateVal, null, null, ActivityDepthVal, ActivityDepthUnitVal, null, null, null, null, null,
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


        //private static Dictionary<string, Tuple<string, string>> GetCharacteristicsPool()
        //{
        //    Dictionary<string, Tuple<string, string>> charPool = new Dictionary<string, Tuple<string, string>>();
        //    charPool.Add("Benzene", new Tuple<string, string>("Benzene", "C"));
        //    charPool.Add("pH", new Tuple<string, string>("pH", "C"));
        //    return charPool;
        //}


    }
}