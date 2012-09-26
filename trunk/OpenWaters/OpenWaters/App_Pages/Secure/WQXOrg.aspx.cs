using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class WQXOrg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkOrgList");
                if (hl != null) hl.CssClass = "divBody sel";

                if (HttpContext.Current.User.IsInRole("READONLY"))
                {
                    btnAdd.Enabled = false;
                    grdOrg.Columns[0].Visible = false;
                }
            }

        }

        protected void grdOrg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                Session.Add("OrgEditID", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/WQXOrgEdit.aspx");
            }
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("OrgExport.xls", grdOrg);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session.Add("OrgEditID", "-1");
            Response.Redirect("~/App_Pages/Secure/WQXOrgEdit.aspx");
        }
    }
}