using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;

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
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate grid
                if (HttpContext.Current.User.IsInRole("ADMINS"))
                    grdOrg.DataSource = dsOrg;
                else
                    grdOrg.DataSource = db_WQX.GetWQX_USER_ORGS_ByUserIDX(Session["UserIDX"].ConvertOrDefault<int>(), true);

                grdOrg.DataBind();

                btnAdd.Visible = HttpContext.Current.User.IsInRole("ADMINS");
            }

        }

        protected void grdOrg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                Session.Add("OrgID", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/WQXOrgEdit.aspx");
            }
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("OrgExport.xls", grdOrg);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session.Add("OrgID", "-1");
            Response.Redirect("~/App_Pages/Secure/WQXOrgEdit.aspx");
        }
    }
}