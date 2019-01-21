using System;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using System.Configuration;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace OpenEnvironment
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //test database connectivity
            try
            {
                string env = db_Ref.GetT_OE_APP_SETTING("Log Level");
            }
            catch
            {
                lblTestWarn.Visible = true;
                lblTestWarn.Text = "System is currently unavailable.";
            }

            //if integrated with Identity Server, then redirect to Dashboard
            if (ConfigurationManager.AppSettings["UseIdentityServer"] == "true")
                Response.Redirect("~/App_Pages/Secure/Dashboard.aspx");

            SetFocus(this.LoginUser.FindControl("UserName"));
        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            LogIn(((System.Web.UI.WebControls.Login)(sender)).UserName);
        }

        private void LogIn(string UserID)
        {
            LoginUser.DestinationPageUrl = "~/App_Pages/Secure/Dashboard.aspx";

            T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(UserID);
            if (u != null)
                if (u.INITAL_PWD_FLAG)
                    LoginUser.DestinationPageUrl = "~/Account/ChangePassword.aspx?t=ini";

                Utils.PostLoginUser(UserID);
        }


        protected void LoginUser_LoginError(object sender, EventArgs e)
        {
            //There was a problem logging in the user 
        }

    }
}
