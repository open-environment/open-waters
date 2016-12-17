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
                HyperLink hl = (HyperLink)cp.FindControl("lnkMonLocList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate dropdown on translation modal
                List<string> ds = Utils.GetAllColumnBasic("S");
                foreach (string ds1 in ds)
                    ddlField.Items.Add(new ListItem(ds1, ds1));

                //populate drop-downs                
                Utils.BindList(ddlTimeZone, dsTimeZone, "TIME_ZONE_NAME", "TIME_ZONE_NAME");


                PopulateTabsData();
            }
        }

        private void PopulateTabsData()
        {
            //tab 1
            T_WQX_ORGANIZATION o = db_WQX.GetWQX_ORGANIZATION_ByID(Session["OrgID"].ToString());
            if (o != null)
                ddlTimeZone.SelectedValue = o.DEFAULT_TIMEZONE;


            //tab 4
            grdTranslate.DataSource = db_WQX.GetWQX_IMPORT_TRANSLATE_byOrg(Session["OrgID"].ToString());
            grdTranslate.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(Session["OrgID"].ToString(), null, null, null, null, null, null, null, null, null, null, null, ddlTimeZone.SelectedValue, User.Identity.Name) == 1)
                lblMsg.Text = "Data saved";
            else
                lblMsg.Text = "Error encountered";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        //TAB 4: TRANSLATIONS ********************************
        protected void btnAddTranslate_Click(object sender, EventArgs e)
        {
            if (ddlField.SelectedValue != "")
            {
                int SuccID = db_WQX.InsertOrUpdateWQX_IMPORT_TRANSLATE(null, Session["OrgID"].ToString(), ddlField.SelectedValue, txtFrom.Text, txtTo.Text, User.Identity.Name);
                lblMsg.Text = (SuccID > 0) ? "Record successfully added" : "Unable to add translation";
                PopulateTabsData();
            }
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

        }


    }
}