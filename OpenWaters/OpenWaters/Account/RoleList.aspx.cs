using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenEnvironment
{
    public partial class RoleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // forms-based authorization
                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    Response.Redirect("~/App_Pages/Public/AccessDenied.aspx");

                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkRoleList");
                if (hl != null) hl.CssClass = "divBody sel";
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RoleAdd.aspx");
        }

        protected void grdRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                if (e.CommandArgument.ToString().Length > 0)
                {
                    Response.Redirect("~/Account/RoleEdit.aspx?id=" + e.CommandArgument.ToString());
                }
            }
        }
    }
}