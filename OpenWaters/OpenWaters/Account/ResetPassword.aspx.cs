using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                //Logging Err = new Logging();
                //Err.ErrorLog(Server.MapPath("~/Logs/ErrorLog"), ex.Message);
                lblMsg.Text = "Error resetting account password. ";// +ex.Message;
            }

        }
    }
}