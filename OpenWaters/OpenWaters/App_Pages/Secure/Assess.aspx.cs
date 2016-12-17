using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment.App_Pages.Secure
{
    public partial class Assess : System.Web.UI.Page
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

                grdAssessmentReports.DataSource = db_Attains.GetT_ATTAINS_REPORT_byORG_ID(Session["OrgID"].ToString());
                grdAssessmentReports.DataBind();
            }
        }

        protected void grdAssessmentReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                AssessEdit(e.CommandArgument.ToString());
            }
        }

        public static string GetImage(string value)
        {
            if (value == "U")
                return "~/App_Images/progress.gif";
            else if (value == "Y")
                return "~/App_Images/ico_pass.png";
            else
                return "~/App_Images/ico_alert.png";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AssessEdit("-1");
        }

        private void AssessEdit(string RptIDX)
        {
            Session.Add("AssessRptIDX", -1);
            Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}