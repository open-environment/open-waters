using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenEnvironment
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkUserList");
                if (hl != null) hl.CssClass = "divBody sel";
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session.Add("UserEditID", -1);
            Response.Redirect("~/Account/UserEdit.aspx");
        }

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                if (e.CommandArgument.ToString().Length > 0)
                {
                    Session.Add("UserEditID", e.CommandArgument.ToString());
                    Response.Redirect("~/Account/UserEdit.aspx");
                }
            }
        }
    }
}