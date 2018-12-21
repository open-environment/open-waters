using System;
using System.Web;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System.Web.Security;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace OpenEnvironment
{
    public partial class SiteAuth : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //populate drop-down lists
                dsOrg.SelectParameters["UserIDX"].DefaultValue = Utils.GetUserIDX(HttpContext.Current.User).ToString();
                Utils.BindList(ddlOrg, dsOrg, "ORG_ID", "ORG_FORMAL_NAME");

                if (Session["OrgID"]!=null)
                    ddlOrg.SelectedValue = Session["OrgID"].ToString();

                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    NavigationMenu.Items.RemoveAt(3);

                //Hide new logout button if IdentityServer is not used
                if (ConfigurationManager.AppSettings["UseIdentityServer"] == "false")
                {
                    btnLogout.Visible = false;
                    btnPortal.Visible = false;
                }

            }
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrg.SelectedValue.Length > 0)
            {
                //update current session to set active Organization
                Session["OrgID"] = ddlOrg.SelectedValue;


                //update User record in database to set the most recent selected organization as their default organization
                int UserIDX = Utils.GetUserIDX(HttpContext.Current.User);
                db_Accounts.UpdateT_OE_USERSDefaultOrg(UserIDX, ddlOrg.SelectedValue);
                
                //refresh page because many children pages depend on this updated information
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            Request.GetOwinContext().Authentication.SignOut();

            // Invalidate the Cache on the Client Side
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Response.Redirect(Request.RawUrl);  
        }

        protected void btnPortal_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["IdentityServerAuthority"]);
        }
    }
}