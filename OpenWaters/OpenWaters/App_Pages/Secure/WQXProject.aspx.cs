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
            }

            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select or join an organization first.";
                btnAdd.Visible = false;
                return;
            }

            if (!IsPostBack)
            {
                grdProject.Columns[4].Visible = (Session["PROJ_SAMP_DESIGN_TYPE_CD"].ConvertOrDefault<Boolean>());
                grdProject.Columns[5].Visible = (Session["PROJ_QAPP_APPROVAL"].ConvertOrDefault<Boolean>());
                grdProject.Columns[6].Visible = (Session["PROJ_QAPP_APPROVAL"].ConvertOrDefault<Boolean>());

                chkFields.Items[0].Selected = (Session["PROJ_SAMP_DESIGN_TYPE_CD"].ConvertOrDefault<Boolean>());
                chkFields.Items[1].Selected = (Session["PROJ_QAPP_APPROVAL"].ConvertOrDefault<Boolean>());

                //ONLY ALLOW EDIT FOR AUTHORIZED USERS OF ORG
                btnAdd.Enabled = false;
                grdProject.Columns[0].Visible = false;
                T_WQX_USER_ORGS uo = db_WQX.GetWQX_USER_ORGS_ByUserIDX_OrgID(Session["UserIDX"].ConvertOrDefault<int>(), Session["OrgID"].ToString());
                if (uo != null)
                {
                    if (uo.ROLE_CD == "A" || uo.ROLE_CD == "U")
                    {
                        btnAdd.Enabled = true;
                        grdProject.Columns[0].Visible = true;
                    }
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
                int SuccID = db_WQX.DeleteT_WQX_PROJECT(ProjectID, User.Identity.Name);

                if (SuccID == 1)
                {
                    lblMsg.Text = "Record successfully deleted.";
                    FillGrid();
                }
                else if (SuccID == -1)
                    lblMsg.Text = "Activities found for this project - record cannot be deleted.";
                else if (SuccID == 0)
                    lblMsg.Text = "Unable to delete project";
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