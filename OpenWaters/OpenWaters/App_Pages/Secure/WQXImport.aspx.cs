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

                //redirect to sample page if any are in progress
                if (db_WQX.GetWQX_IMPORT_TEMP_MONLOC_CountByUserID(User.Identity.Name) > 0)
                    Response.Redirect("~/App_Pages/Secure/WQXImportMonLoc.aspx");

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

            if (ddlImportType.SelectedValue == "M")
                hlTemplate.NavigateUrl = "~/App_Docs/MonLoc_ImportTemplate.xlsx";
            else if (ddlImportType.SelectedValue == "S")
                hlTemplate.NavigateUrl = "~/App_Docs/Samp_ImportTemplate.xlsx";

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
            if (ddlImportType.SelectedValue == "S" && ddlProject.SelectedValue == "") { lblMsg.Text = "Please select a project into which this data will be imported."; return; }
            if (txtPaste.Text.Length == 0) { lblMsg.Text = "You must copy and paste data from a spreadsheet into the large textbox."; return; }
            if (ddlImportType.SelectedValue == "S" && rbTemplateInd.SelectedValue == "") { lblMsg.Text = "Please indicate whether you will use a custom import template."; return; }
            if (ddlImportType.SelectedValue == "S" && rbTemplateInd.SelectedValue == "Y" && ddlTemplate.SelectedValue == "") { lblMsg.Text = "Please select an import template."; return; }
            //********************************* END VALIDATION *************************************

            //delete previous temp data stored for the user
            if (!DeleteTempImportData(ddlImportType.SelectedValue, User.Identity.Name)) { lblMsg.Text = "Unable to proceed with import."; return; }

            //define org and project
            string OrgID = Session["OrgID"].ToString();

            //set dictionaries used to store stuff in memory
            Dictionary<string, Tuple<string, string>> charsPool = GetCharacteristicsPool();  //list of all possible column headers that are characteristics in Open Waters
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
                        else if (ddlImportType.SelectedValue == "S")
                        {
                            int? ProjectID = ddlProject.SelectedValue.ConvertOrDefault<int?>();
                            string ProjectName = ddlProject.SelectedItem == null ? "" : ddlProject.SelectedItem.Text;
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
                Response.Redirect("~/App_Pages/Secure/WQXImport" + (ddlImportType.SelectedValue == "M" ? "MonLoc" : "Sample") + ".aspx");
            else
                lblMsg.Text = "No valid data found. You must include column headers.";
        }

        private static Dictionary<string, Tuple<string, string>> GetCharacteristicsPool()
        {
            Dictionary<string, Tuple<string, string>> charPool = new Dictionary<string, Tuple<string, string>>();
            charPool.Add("Benzene", new Tuple<string, string>("Benzene", "C"));
            charPool.Add("pH", new Tuple<string, string>("pH", "C"));
            return charPool;
        }

        private bool DeleteTempImportData (string ImportType, string User)
        {
            if (ImportType == "M")
                return (db_WQX.DeleteT_WQX_IMPORT_TEMP_MONLOC(User) != 0);
            else if (ImportType == "S")
                return (db_WQX.DeleteT_WQX_IMPORT_TEMP_SAMPLE(User) != 0);
            else
                return false;
        }

    }
}