using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class RoleEdit : System.Web.UI.Page
    {
        int RoleID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //return to role listing if no role is being edited
            if (Request.QueryString["id"] == null)
                Response.Redirect("~/Account/RoleList.aspx");
            else
                RoleID = int.Parse(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                // forms-based authorization
                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    Response.Redirect("~/App_Pages/Public/AccessDenied.aspx");

                //clear listboxes
                lbAllUsers.Items.Clear();
                lbUserInRole.Items.Clear();


                //populate general role information
                txtRoleName.Text = db_Accounts.GetT_VCCB_ROLEByIDX(RoleID).ROLE_NAME;
                txtRoleDesc.Text = db_Accounts.GetT_VCCB_ROLEByIDX(RoleID).ROLE_DESC;

                //populate listbox with users already in role
                foreach (T_OE_USERS u in db_Accounts.GetT_OE_USERSInRole(RoleID))
                {
                    ListItem li = new ListItem(u.USER_ID.ToString(), u.USER_IDX.ToString());
                    lbUserInRole.Items.Add(li);
                }

                //populate listbox with users not in role
                foreach (T_OE_USERS u in db_Accounts.GetT_OE_USERSNotInRole(RoleID))
                {
                    ListItem li = new ListItem(u.USER_ID.ToString(), u.USER_IDX.ToString());
                    lbAllUsers.Items.Add(li);
                }


            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (db_Accounts.DeleteT_VCCB_ROLE(RoleID) == 1)  //role deletion successful 
                    Response.Redirect("~/Account/RoleList.aspx");
                else
                    lblMsg.Text = "Error deleting role.";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (db_Accounts.UpdateT_VCCB_ROLE(RoleID, txtRoleName.Text, txtRoleDesc.Text, User.Identity.Name) == 1)
                    Response.Redirect("~/Account/RoleList.aspx");
                else
                    lblMsg.Text = "Error updating role.";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RoleList.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //add user to role
            if (lbAllUsers.SelectedIndex != -1)
            {
                lblMsg.Text = "";

                if (db_Accounts.CreateT_VCCB_USER_ROLE(RoleID, Int32.Parse(lbAllUsers.SelectedValue), User.Identity.Name) == 1)
                    Response.Redirect("~/Account/RoleEdit.aspx?id=" + RoleID);
                else
                    lblMsg.Text = "Error Adding User to Role.";
            }
            else
                lblMsg.Text = "Please select a user to add to the role.";

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbUserInRole.SelectedIndex != -1)
            {
                lblMsg.Text = "";
                int removedUser = Int32.Parse(lbUserInRole.SelectedValue);

                if (db_Accounts.DeleteT_VCCB_USER_ROLE(removedUser, RoleID) == 1)
                    Response.Redirect("~/Account/RoleEdit.aspx?id=" + RoleID);
                else
                    lblMsg.Text = "Error Removing User from Role.";
            }
            else
            {
                lblMsg.Text = "Please select a user to remove from this role.";
            }

        }
    }
}