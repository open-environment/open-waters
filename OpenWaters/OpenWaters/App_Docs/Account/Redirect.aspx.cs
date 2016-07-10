using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace OpenEnvironment
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie("gru", true);
            string ddd = User.Identity.Name;

            Response.Redirect("~/App_Pages/Secure/Dashboard.aspx");
        }
    }
}