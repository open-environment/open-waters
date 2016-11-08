using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment.App_Pages.Secure
{
    public partial class WQXImportNew : System.Web.UI.Page
    {
        struct HeaderValuePair
        {
            public string Value1;
            public string Value2;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //display left menu as selected
            if (!IsPostBack)
            {
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

            }

            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select or create an organization first.";
                return;
            }


            if (!IsPostBack)
            {
                //redirect to sample page if any are in progress
                if (db_WQX.GetWQX_IMPORT_TEMP_SAMPLE_CountByUserID(User.Identity.Name) > 0)
                    Response.Redirect("~/App_Pages/Secure/WQXImportSample.aspx");
            }
        }

        protected void ddlImportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            pnlSampOptions.Visible = (ddlImportType.SelectedValue == "SAMP");
            pnlPasteData.Visible = true;
            hlTemplate.Visible = true;

            if (ddlImportType.SelectedValue == "MLOC")
                hlTemplate.NavigateUrl = "~/App_Docs/MonLoc_ImportTemplate.xlsx";
            else if (ddlImportType.SelectedValue == "SAMP")
                hlTemplate.NavigateUrl = "~/App_Docs/Samp_ImportTemplate.xlsx";

        }

        protected void btnParse_Click(object sender, EventArgs e)
        {
            Utils.GetFieldConfig_Fields();

            //initialize variables
            int i = 0, j = 0;
            bool monlocInd = false, actInd = false, resultInd = false, charInd = false;

            //define org and project
            string OrgID = Session["OrgID"].ToString();
            int? ProjectID = ddlProject.SelectedValue.ConvertOrDefault<int?>();
            string ProjectIDName = ddlProject.SelectedItem == null ? "" : ddlProject.SelectedItem.Text;
            bool bioIndicator = false;

            //set dictionaries used to store stuff in memory
            Dictionary<string, Tuple<string, string>> headersPool = GetHeadersPool();  //pool of all possible column headers in Open Waters
            Dictionary<string, HeaderValuePair> charsPool = GetCharacteristicsPool();  //list of all possible column headers that are characteristics in Open Waters
            Dictionary<string, int> headerDefs = new Dictionary<string, int>();  //identifies the column number for each field to be imported
            List<string> translateFields = db_WQX.GetWQX_IMPORT_TRANSLATE_byColName(OrgID);  //list of fields that have translations defined

            //loop through each row
            foreach (string row in txtPaste.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                //split the rows columns into string array
                string[] cols = row.Split(new char[] { '\t' }, StringSplitOptions.None);
                if (cols.Length > 0) //skip blank rows
                {
                    if (i == 0)
                    {
                        //**********************************************************
                        //HEADER ROW - LOGIC TO DETERMING WHAT IS IN EACH COLUMN
                        //**********************************************************

                        foreach (string colHeader in cols)
                        {
                            Tuple<string, string> v;
                            if (headersPool.TryGetValue(colHeader.ToUpper(), out v)) // Returns true.
                            {
                                if (v.Item2 == "M") monlocInd = true;
                                else if (v.Item2 == "A") actInd = true;
                                else if (v.Item2 == "R") resultInd = true;
                                else if (v.Item2 == "C") charInd = true;

                                headerDefs.Add(v.Item1, j);
                            }
                            j++; //increment col index
                        }

                        //after header row read, exit if certain conditions met
                        if (monlocInd && actInd) { lblMsg.Text = "Cannot import monitoring location and activity columns at the same time."; return; }
                    }
                    else
                    {
                        //**********************************************************
                        //NOT HEADER ROW - READING IN VALUES
                        //**********************************************************
                        string monlocVal = GetColVal(headerDefs, "MON_LOC", cols, translateFields, OrgID);

                        //import sample record
                        int TempImportSampID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(null, User.Identity.Name, OrgID, ProjectID, ProjectIDName, null,
                            GetColVal(headerDefs, "MON_LOC", cols, translateFields, OrgID),
                            null,
                            GetColVal(headerDefs, "ACTIVITY_ID", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "ACT_TYPE", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "ACT_MEDIA", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "ACT_SUBMEDIA", cols, translateFields, OrgID),
                            (GetColVal(headerDefs, "ACT_START_DT", cols, translateFields, OrgID) + " " + (GetColVal(headerDefs, "ACT_START_TIME", cols, translateFields, OrgID) ?? "")).ConvertOrDefault<DateTime?>(),
                            (GetColVal(headerDefs, "ACT_END_DT", cols, translateFields, OrgID) + " " + (GetColVal(headerDefs, "ACT_END_TIME", cols, translateFields, OrgID) ?? "")).ConvertOrDefault<DateTime?>(),
                            GetColVal(headerDefs, "ACT_TIME_ZONE", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "RELATIVE_DEPTH_NAME", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "ACT_DEPTHHEIGHT_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "ACT_DEPTHHEIGHT_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "TOP_DEPTHHEIGHT_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "TOP_DEPTHHEIGHT_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BOT_DEPTHHEIGHT_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BOT_DEPTHHEIGHT_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "DEPTH_REF_POINT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "ACT_COMMENT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_ASSEMBLAGE_SAMPLED", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_DURATION_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_DURATION_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_SAMP_COMPONENT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_SAMP_COMPONENT_SEQ", cols, translateFields, OrgID).ConvertOrDefault<int?>(),
                            GetColVal(headerDefs, "BIO_REACH_LEN_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_REACH_LEN_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_REACH_WID_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_REACH_WID_MSR_UNIT", cols, translateFields, OrgID),                            
                            GetColVal(headerDefs, "BIO_PASS_COUNT", cols, translateFields, OrgID).ConvertOrDefault<int?>(),
                            GetColVal(headerDefs, "BIO_NET_TYPE", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_NET_AREA_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_NET_AREA_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_NET_MESHSIZE_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_MESHSIZE_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_BOAT_SPEED_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_BOAT_SPEED_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_CURR_SPEED_MSR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_CURR_SPEED_MSR_UNIT", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "BIO_TOXICITY_TEST_TYPE", cols, translateFields, OrgID),
                            null,
                            GetColVal(headerDefs, "SAMP_COLL_METHOD_ID", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "SAMP_COLL_METHOD_CTX", cols, translateFields, OrgID),
                            null,
                            GetColVal(headerDefs, "SAMP_COLL_EQUIP", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "SAMP_COLL_EQUIP_COMMENT", cols, translateFields, OrgID),
                            null,
                            GetColVal(headerDefs, "SAMP_PREP_ID", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "SAMP_PREP_CTX", cols, translateFields, OrgID),
                            null,
                            GetColVal(headerDefs, "SAMP_PREP_CONT_TYPE", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "SAMP_PREP_CONT_COLOR", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "SAMP_PREP_CHEM_PRESERV", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "SAMP_PREP_THERM_PRESERV", cols, translateFields, OrgID),
                            GetColVal(headerDefs, "SAMP_PREP_STORAGE_DESC", cols, translateFields, OrgID),
                            "P", "", bioIndicator, false);
                    }
                }

                i++;
            }
        }

        protected void btnDefaults_Click(object sender, EventArgs e)
        {

        }

        protected void btnNewTemplate_Click(object sender, EventArgs e)
        {

        }

        //gets the reported value for a given WQX field, taking into account translations
        private static string GetColVal(Dictionary<string,int> dic, string key, string[] cols, List<string> translateFields, string OrgID)
        {
            //get the column index this field is reported in 
            int ret;
            bool found = dic.TryGetValue(key, out ret);

            //if column found, return that column value, else return null
            if (found)
            {
                string colVal = cols[ret];
                if (translateFields.Contains(key)) //if this field has translations, get the translation from DB
                    return db_WQX.GetWQX_IMPORT_TRANSLATE_byColNameAndValue(OrgID, key, colVal);
                else
                    return colVal;
            }
            else
                return null;
        }


        private static Dictionary<string, HeaderValuePair> GetCharacteristicsPool()
        {
            Dictionary<string, HeaderValuePair> charPool = new Dictionary<string, HeaderValuePair>();
            charPool.Add("Benzene", new HeaderValuePair { Value1 = "Benzene", Value2 = "C" });
            charPool.Add("pH", new HeaderValuePair { Value1 = "pH", Value2 = "C" });
            return charPool;
        }

        private static Dictionary<string, Tuple<string, string>> GetHeadersPool()
        {
            Dictionary<string, Tuple<string, string>> headersPool = new Dictionary<string, Tuple<string, string>>();
            headersPool.Add("MONITORING LOCATION", new Tuple<string, string>("MON_LOC", "A"));
            headersPool.Add("MON LOC", new Tuple<string, string>("MON_LOC", "A"));
            headersPool.Add("SAMPLE ID", new Tuple<string, string>("ACTIVITY_ID", "A"));
            headersPool.Add("CHARACTERISTIC", new Tuple<string, string>("CHAR_NAME", "R"));
            headersPool.Add("RESULT", new Tuple<string, string>("RESULT", "R"));
            return headersPool;
        }

    }
}