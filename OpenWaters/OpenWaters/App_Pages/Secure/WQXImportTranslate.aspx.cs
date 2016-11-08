using OpenEnvironment.App_Logic.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenEnvironment.App_Pages.Secure
{
    public partial class WQXImportTranslate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                Response.Redirect("~/App_Pages/Secure/Dashboard.aspx");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkMonLocList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                PopulateGrid();
            }
        }

        private void PopulateGrid()
        {
            grdImport.DataSource = db_WQX.GetWQX_IMPORT_TRANSLATE_byOrg(Session["OrgID"].ToString());
            grdImport.DataBind();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
        }

        protected void btnAddTranslate_Click(object sender, EventArgs e)
        {
            if (ddlField.SelectedValue != "")
            {
                int SuccID = db_WQX.InsertOrUpdateWQX_IMPORT_TRANSLATE(null, Session["OrgID"].ToString(), ddlField.SelectedValue, txtFrom.Text, txtTo.Text, User.Identity.Name);
                lblMsg.Text = (SuccID > 0) ? "Record successfully added" : "Unable to add translation";
                PopulateGrid();
            }
        }

        protected void grdImport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int TransID = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "Deletes")
            {
                int SuccID = db_WQX.DeleteT_WQX_IMPORT_TRANSLATE(TransID);
                lblMsg.Text = (SuccID == 1) ? "Record successfully deleted" : "Unable to delete translation";
                PopulateGrid();
            }

        }
    }
}