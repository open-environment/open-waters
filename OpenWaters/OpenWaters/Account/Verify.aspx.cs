using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using System.Web.Security;
using OpenEnvironment.Account;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string oauthcrd = Request.QueryString["oauthcrd"];

                //decrypt oauth string
                string oauthDecode = System.Web.HttpUtility.UrlDecode(oauthcrd);
                oauthDecode = oauthDecode.Replace(" ", "+");   //fix situations where spaces and plus get mixed up
                string decryptStr = new SimpleAES().Decrypt(oauthDecode);

                //split into password and username
                string[] s = System.Text.RegularExpressions.Regex.Split(decryptStr, "\\|\\|");

                CustMembershipProvider c = new CustMembershipProvider();
                if (c.ValidateUser(s[1], s[0]) == false)
                {
                    lblMsg.Text = "Verification failed";
                    pnl1.Visible = false;
                }
                else
                    pnl1.Visible = true;
            }
            catch
            {
                lblMsg.Text = "Verification failed";
                pnl1.Visible = false;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //*************** VALIDATION ********************************
            if (txtPwd.Text != txtPwd2.Text)
            {
                lblMsg.Text = "Passwords do not match";
                return;
            }
            //*************** END VALIDATION ********************************

            string oauthcrd = Request.QueryString["oauthcrd"];

            //decrypt oauth string
            string oauthDecode = System.Web.HttpUtility.UrlDecode(oauthcrd);
            oauthDecode = oauthDecode.Replace(" ", "+");   //fix situations where spaces and plus get mixed up
            string decryptStr = new SimpleAES().Decrypt(oauthDecode);

            //split into password and username
            string[] s = System.Text.RegularExpressions.Regex.Split(decryptStr, "\\|\\|");

            CustMembershipProvider c = new CustMembershipProvider();
            if (c.ValidateUser(s[1], s[0]) == true)
            {
                if (c.ChangePassword(s[1], s[0], txtPwd.Text))
                {
                    if (Membership.ValidateUser(s[1], txtPwd.Text))
                    {
                        
                        FormsAuthentication.SetAuthCookie(s[1], false);
                        FormsAuthentication.RedirectFromLoginPage(s[1], false);
                        string ddd = User.Identity.Name;

                        T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(s[1]);
                        if (u != null)
                        {

                            db_Accounts.UpdateT_OE_USERS(u.USER_IDX, null, null, null, null, null, null, null, null, System.DateTime.Now, null, null, "system");
                            //set important session variables
                            Session["UserIDX"] = u.USER_IDX;

                            Response.Redirect("~/App_Pages/Secure/Dashboard.aspx");

                        }

                    }

                }
            }

        }
    }
}