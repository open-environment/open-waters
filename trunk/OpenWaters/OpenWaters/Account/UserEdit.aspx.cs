using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class UserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //displays the left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkUserList");
                if (hl != null)
                    hl.CssClass = "on";

                // forms-based authorization
                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    Response.Redirect("~/App_Pages/Public/AccessDenied.aspx");

                int UserID = int.Parse(Session["UserEditID"].ToString());

                T_OE_USERS u = db_Accounts.GetT_OE_USERSByIDX(UserID);
                if (u != null)
                {
                    txtUserIDX.Text = u.USER_IDX.ToString();
                    txtUserID.Text = u.USER_ID;
                    txtFName.Text = u.FNAME;
                    txtLName.Text = u.LNAME;
                    txtEmail.Text = u.EMAIL;
                    txtPhone.Text = u.PHONE;
                    txtPhoneExt.Text = u.PHONE_EXT;
                    chkActive.Checked = u.ACT_IND;
                    txtPassword.Visible = false;
                }
                else //add new case
                {
                    txtUserID.Enabled = true;
                    btnDelete.Visible = false;
                    txtPassword.Visible = true;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/UserList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int SuccID;

                if (txtUserIDX.Text.Length > 0)
                    SuccID = db_Accounts.UpdateT_OE_USERS(int.Parse(txtUserIDX.Text), null, null, txtFName.Text, txtLName.Text, txtEmail.Text, chkActive.Checked, null, null, null, txtPhone.Text, txtPhoneExt.Text, User.Identity.Name);
                else
                {
                    if (txtPassword.Text.Length == 0 || txtFName.Text.Length==0 || txtLName.Text.Length==0 || txtUserID.Text.Length==0)
                    {
                        lblMsg.Text = "You must supply a user ID, user's name, and password.";
                        return;
                    }

                    //first create user 
                    MembershipCreateStatus t;
                    Membership.CreateUser(txtUserID.Text, txtPassword.Text, txtEmail.Text, null, null, true, out t);
                    if (t == MembershipCreateStatus.InvalidPassword)
                    {
                        lblMsg.Text = "Invalid password. Password must be at least 8 characters long.";
                        return;
                    }

                    T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(txtUserID.Text);
                    if (u != null)
                        SuccID = db_Accounts.UpdateT_OE_USERS(u.USER_IDX, null, null, txtFName.Text, txtLName.Text, txtEmail.Text, true, false, System.DateTime.Now, null, txtPhone.Text, txtPhoneExt.Text, User.Identity.Name);
                    else
                        SuccID = 0;
                }

                if (SuccID == 1)
                    lblMsg.Text = "User updated successfully.";
                else
                    lblMsg.Text = "Error updating user.";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (db_Accounts.DeleteT_OE_USERS(int.Parse(Session["UserEditID"].ToString())) == 1)  //user deletion successful 
                    Response.Redirect("~/Account/UserList.aspx");
                else
                    lblMsg.Text = "Error deleting user.";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }
    }
}