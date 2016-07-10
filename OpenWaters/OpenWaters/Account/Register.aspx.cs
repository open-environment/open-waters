using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //only require Beta invite code if hosted at open waters
            if (db_Ref.GetT_OE_APP_SETTING("Beta Program") == "Y")
                pnlBeta.Visible = true;

            lblSignupMsg.Text = db_Ref.GetT_OE_APP_SETTING("Signup Message");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            //Validation *************************************
            if (db_Ref.GetT_OE_APP_SETTING("Beta Program") == "Y")
            {
                if (txtBetaKey.Text.ToUpper() != db_Ref.GetT_OE_APP_SETTING("Beta Invite Code").ToUpper())
                {
                    lblMsg.Text = "Invalid Beta Invite Code. Please contact " + db_Ref.GetT_OE_APP_SETTING("Email From") + " to obtain one.";
                    return;
                }
            }

            if (txtFName.Text.Length == 0 || txtLName.Text.Length == 0 || txtUserID.Text.Length == 0 || txtEmail.Text.Length == 0)
            {
                lblMsg.Text = "You must supply a user name, first name, last name, and email.";
                return;
            }
            //End Validation *************************************


            //create user 
            MembershipCreateStatus status;
            CustMembershipProvider c = new CustMembershipProvider();
            c.CreateUser(txtUserID.Text, "", txtEmail.Text, null, null, true, null, out status);

            if (status == MembershipCreateStatus.Success)
            {
                int UserIDX = (int)Membership.GetUser(txtUserID.Text).ProviderUserKey;

                db_Accounts.UpdateT_OE_USERS(UserIDX, null, null, txtFName.Text, txtLName.Text, null, null, null, null, null, txtPhone.Text, txtPhoneExt.Text, null);

                //show registration success
                pnl1.Visible = false;
                pnl2.Visible = true;
                lblTitle.Visible = false;
            }
            else
            {
                if (status.ToString() == "DuplicateUserName")
                    lblMsg.Text = "An account already exists with that email address. Please recover lost password.";
                else if (status.ToString() == "InvalidEmail")
                    lblMsg.Text = "Unable to send verification email. Please try again later.";
                else
                    lblMsg.Text = status.ToString();
            }

        }

    }
}
