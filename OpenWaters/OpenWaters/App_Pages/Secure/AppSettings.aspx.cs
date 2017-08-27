using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenEnvironment
{
    public partial class AppSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // forms-based authorization
            if (!HttpContext.Current.User.IsInRole("ADMINS"))
                Response.Redirect("~/App_Pages/Public/AccessDenied.aspx");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkAppSettings");
                if (hl != null) hl.CssClass = "leftMnuBody sel";
            }

        }
    }
}