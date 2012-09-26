using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                }
                else //add new case
                {
                    txtUserID.Enabled = true;
                    btnDelete.Visible = false;
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

                if (txtUserIDX.Text.Length>0)
                    SuccID = db_Accounts.UpdateT_OE_USERS(int.Parse(txtUserIDX.Text), null, null, txtFName.Text, txtLName.Text, txtEmail.Text, chkActive.Checked, null, null, null, txtPhone.Text, txtPhoneExt.Text, User.Identity.Name);
                else
                    SuccID = db_Accounts.CreateT_OE_USERS(txtUserID.Text, null, null, txtFName.Text, txtLName.Text, txtEmail.Text, true, true, null, txtPhone.Text, txtPhoneExt.Text, User.Identity.Name);

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