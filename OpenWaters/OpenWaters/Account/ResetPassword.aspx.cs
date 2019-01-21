using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace OpenEnvironment
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if integrated with Identity Server, then redirect
            if (ConfigurationManager.AppSettings["UseIdentityServer"] == "true")
            {
                string _redirUri = ConfigurationManager.AppSettings["IdentityServerRedirectURI"];
                HttpContext.Current.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = _redirUri }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser u = Membership.GetUser(UserID.Text, false);
                if (u != null)
                {
                    if (u.Email.ToLower() == txtEmail2.Text.ToLower())
                        lblMsg.Text = u.ResetPassword();
                    else
                        lblMsg.Text = "Email address does not match supplied User ID.";
                }
                else
                    lblMsg.Text = "User does not exist.";
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error resetting account password. ";
            }

        }
    }
}