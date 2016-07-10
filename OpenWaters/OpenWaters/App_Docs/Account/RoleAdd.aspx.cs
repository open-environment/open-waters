using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class RoleAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // forms-based authorization
                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    Response.Redirect("~/App_Pages/Public/AccessDenied.aspx");

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RoleList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //check duplicate role name
                T_OE_ROLES r = db_Accounts.GetT_VCCB_ROLEByName(txtRoleName.Text);

                if (r != null)
                    lblMsg.Text = "Role name already exists. Please try again.";
                else
                {
                    db_Accounts.CreateT_OE_ROLES(txtRoleName.Text, txtRoleDesc.Text, User.Identity.Name);
                    Response.Redirect("~/Account/RoleList.aspx");
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }

        }
    }
}