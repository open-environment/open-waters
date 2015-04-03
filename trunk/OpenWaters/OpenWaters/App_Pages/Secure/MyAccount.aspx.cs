using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;


namespace OpenEnvironment
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                T_OE_USERS u = db_Accounts.GetT_OE_USERSByIDX(Session["UserIDX"].ConvertOrDefault<int>());
                if (u != null)
                {
                    txtUserName.Text = u.USER_ID;
                    txtFName.Text = u.FNAME;
                    txtLName.Text = u.LNAME;
                    txtEmail.Text = u.EMAIL;
                    txtPhone.Text = u.PHONE;
                }


                //populate listing of Roles
                lbRoleList.Items.Clear();
                string[] rolelist = System.Web.Security.Roles.GetRolesForUser();
                foreach (string s in rolelist)
                    lbRoleList.Items.Add(s);

                //populate listing of Organizations
                lblOrgList.Items.Clear();
                List<T_WQX_ORGANIZATION> orgs = db_WQX.GetWQX_USER_ORGS_ByUserIDX(Session["UserIDX"].ConvertOrDefault<int>(), true);
                foreach (T_WQX_ORGANIZATION org in orgs)
                    lblOrgList.Items.Add(org.ORG_FORMAL_NAME);
            
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (db_Accounts.UpdateT_OE_USERS(Session["UserIDX"].ConvertOrDefault<int>(), null, null, txtFName.Text, txtLName.Text, txtEmail.Text, true, null, null, null, txtPhone.Text, null, User.Identity.Name) > 0)
                    lblMsg.Text = "User updated successfully.";
                else
                    lblMsg.Text = "Error updating user.";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }

        }

        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ChangePassword.aspx");
        }
    }
}