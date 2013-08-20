using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class WQXProject : System.Web.UI.Page
    {
        protected void Page_PreRender(object o, System.EventArgs e)
        {
            FillGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkProjectList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";


                grdProject.Columns[4].Visible = (Session["PROJ_SAMP_DESIGN_TYPE_CD"].ConvertOrDefault<Boolean>());
                grdProject.Columns[5].Visible = (Session["PROJ_QAPP_APPROVAL"].ConvertOrDefault<Boolean>());
                grdProject.Columns[6].Visible = (Session["PROJ_QAPP_APPROVAL"].ConvertOrDefault<Boolean>());

                chkFields.Items[0].Selected = (Session["PROJ_SAMP_DESIGN_TYPE_CD"].ConvertOrDefault<Boolean>());
                chkFields.Items[1].Selected = (Session["PROJ_QAPP_APPROVAL"].ConvertOrDefault<Boolean>());

                if (HttpContext.Current.User.IsInRole("READONLY"))
                {
                    btnAdd.Enabled = false;
                    grdProject.Columns[0].Visible = false;
                }
            }

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }

        public static string GetImage(string value, Boolean WQXInd)
        {
            if (WQXInd)
            {
                if (value == "U")
                    return "~/App_Images/progress.gif";
                else if (value == "N")
                    return "~/App_Images/ico_alert.png";
                else if (value == "Y")
                    return "~/App_Images/ico_pass.png";
                else
                    return "~/App_Images/ico_alert.png";
            }
            else
            {
                return "~/App_Images/0.png";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session.Add("ProjectIDX", 0);
            Response.Redirect("~/App_Pages/Secure/WQXProjectEdit.aspx");
        }

        protected void btnConfigSave_Click(object sender, EventArgs e)
        {
            Session["PROJ_SAMP_DESIGN_TYPE_CD"] = chkFields.Items[0].Selected;
            Session["PROJ_QAPP_APPROVAL"] = chkFields.Items[1].Selected;

            Response.Redirect(Page.Request.Url.ToString(), true); 
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("ProjectsExport.xls", grdProject);
        }

        protected void grdProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ProjectID = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Edits")
            {
                Session.Add("ProjectIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/WQXProjectEdit.aspx");
            }

            if (e.CommandName == "Deletes")
            {
                List<T_WQX_ACTIVITY> a = db_WQX.GetWQX_ACTIVITY(false, Session["OrgID"].ConvertOrDefault<string>(), null, null, null, null, false, ProjectID);
                if (a.Count==0)
                {
                    db_WQX.DeleteT_WQX_PROJECT(ProjectID);
                    lblMsg.Text = "";
                    FillGrid();
                }
                else
                    lblMsg.Text = "You cannot delete a project that has samples/activities. You can instead make the project inactive at the project details screen.";
            }

            if (e.CommandName == "WQX")
            {
                Session.Add("TableCD", "PROJ");
                Session.Add("ProjectIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/WQX_Hist.aspx");
            }

        }

        public void FillGrid()
        {
            grdProject.DataSource = dsProject;
            grdProject.DataBind();
        }

        protected void grdProject_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProject.EditIndex = e.NewEditIndex;
        }

        protected void grdProject_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProject.EditIndex = -1;
        }

        protected void grdProject_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

    }
}