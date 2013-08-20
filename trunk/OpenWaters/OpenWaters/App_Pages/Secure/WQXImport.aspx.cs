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
                Response.Redirect("~");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";


            }

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
            string txt = txtPaste.Text;
            string[] lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int colCount = 50;
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
            string[] PctTolerantIndividuals= new string[colCount];
            string[] PctTolerantTaxa = new string[colCount];
            string[] ColeopteraRichness = new string[colCount];


            try
            {
                foreach (string s in lst)
                {
                    char[] delimiters = new char[] { '\t' };
                    string[] parts = s.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
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

                        if (parts[0] == "EcoAnalysts Sample ID")
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
                        int? MonLocIDX = db_WQX.GetWQX_MONLOC_ByImportID(sites[i]);
                        int? ActID;

                        T_WQX_ACTIVITY a = db_WQX.GetWQX_ACTIVITY_ByUnique(Session["OrgID"].ToString(), sampleIDs[i]);
                        if (a != null)
                            ActID = a.ACTIVITY_IDX;
                        else
                            ActID = null;

                        ActID = db_WQX.InsertOrUpdateWQX_ACTIVITY(ActID, Session["OrgID"].ToString(), ddlProject.SelectedValue.ConvertOrDefault<int?>(), MonLocIDX, sampleIDs[i], "Field Msr/Obs-Habitat Assessment", "Water", "", dates[i].ConvertOrDefault<DateTime>(), null, "", "", "U", true, true, User.Identity.Name);

                        //CREATE INDICES
                        db_WQX.InsertOrUpdateWQX_BIO_HABITAT_INDEX(null, Session["OrgID"].ToString(), MonLocIDX, sampleIDs[i] + "_HBI", "Hilsenhoff Biotic Index", "LOCAL", "Hilsenhoff Biotic Index", null, null, null, null, null, null, null, HBIs[i], null, null, dates[i].ConvertOrDefault<DateTime?>(), true, "U", null, true, User.Identity.Name);

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
                db_Ref.InsertUpdateWQX_IMPORT_LOG(null, Session["OrgID"].ToString(), ddlMonLoc.SelectedValue, ddlMonLoc.SelectedValue, 0, "Success", null, User.Identity.Name);
            }
            catch
            {
                //add to import log
                db_Ref.InsertUpdateWQX_IMPORT_LOG(null, Session["OrgID"].ToString(), ddlMonLoc.SelectedValue, ddlMonLoc.SelectedValue, 0, "Fail", null, User.Identity.Name);
            }

            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
        }
    }
}