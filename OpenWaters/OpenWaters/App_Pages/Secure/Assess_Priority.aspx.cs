using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment.App_Pages.Secure
{
    public partial class Assess_Priority : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkAssess");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                hdnReportIDX.Value = (Session["AssessRptIDX"] ?? "").ToString();
                hdnPriorityIDX.Value = (Session["PriorityIDX"] ?? "").ToString();
                PopulateForm();
            }

        }

        private void PopulateForm()
        {
            if (hdnReportIDX.Value.ConvertOrDefault<int>() > 0)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int SuccID = 1;

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx");
            else
                lblMsg.Text = "Unable to delete priority.";

        }

    }
}