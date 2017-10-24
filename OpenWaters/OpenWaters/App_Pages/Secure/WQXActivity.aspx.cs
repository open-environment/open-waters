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
    public partial class WQXActivity : System.Web.UI.Page
    {
        public const int PAGE_SIZE = 30;

        protected void Page_PreRender(object o, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkActivityList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate drop-downs                
                Utils.BindList(ddlMonLoc, dsMonLoc, "MONLOC_IDX", "MONLOC_ID");
                Utils.BindList(ddlActType, dsActType, "VALUE", "VALUE");
                Utils.BindList(ddlProject, dsProject, "PROJECT_IDX", "PROJECT_ID"); 

                if (Session["filtMonLoc"] != null)
                    ddlMonLoc.SelectedValue = Session["filtMonLoc"].ToString();

                if (Session["filtStartDt"] != null)
                    txtStartDt.Text = Session["filtStartDt"].ToString();

                if (Session["filtEndDt"] != null)
                    txtEndDt.Text = Session["filtEndDt"].ToString();

                if (Session["filtProject"] != null)
                    ddlMonLoc.SelectedValue = Session["filtProject"].ToString();

                FillGrid();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);

            if (!IsPostBack)
            {
                grdActivity.Columns[8].Visible = (Session["SAMP_ACT_END_DT"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[9].Visible = (Session["SAMP_COLL_METHOD"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[10].Visible = (Session["SAMP_COLL_EQUIP"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[11].Visible = (Session["SAMP_COLL_EQUIP"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[12].Visible = (Session["SAMP_PREP"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[13].Visible = (Session["SAMP_DEPTH"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[14].Visible = (Session["SAMP_DEPTH"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[15].Visible = (Session["SAMP_DEPTH"].ConvertOrDefault<Boolean>());
                grdActivity.Columns[16].Visible = (Session["SAMP_DEPTH"].ConvertOrDefault<Boolean>());

                chkFields.Items[0].Selected = (Session["SAMP_ACT_END_DT"].ConvertOrDefault<Boolean>());
                chkFields.Items[1].Selected = (Session["SAMP_COLL_METHOD"].ConvertOrDefault<Boolean>());
                chkFields.Items[2].Selected = (Session["SAMP_COLL_EQUIP"].ConvertOrDefault<Boolean>());
                chkFields.Items[3].Selected = (Session["SAMP_PREP"].ConvertOrDefault<Boolean>());
                chkFields.Items[4].Selected = (Session["SAMP_DEPTH"].ConvertOrDefault<Boolean>());

                //ONLY ALLOW EDIT FOR AUTHORIZED USERS OF ORG
                int UserIDX = Utils.GetUserIDX(User);
                bool CanEditOrg = db_WQX.CanUserEditOrg(UserIDX, Session["OrgID"].ToString());
                btnAdd.Visible = CanEditOrg;
            }

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session.Add("ActivityIDX", -1);
            Response.Redirect("~/App_Pages/Secure/WQXActivityEdit.aspx");
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
                return "~/App_Images/0.png";
        }

        protected void btnConfigSave_Click(object sender, EventArgs e)
        {
            Session["SAMP_ACT_END_DT"] = chkFields.Items[0].Selected;
            Session["SAMP_COLL_METHOD"] = chkFields.Items[1].Selected;
            Session["SAMP_COLL_EQUIP"] = chkFields.Items[2].Selected;
            Session["SAMP_PREP"] = chkFields.Items[3].Selected;
            Session["SAMP_DEPTH"] = chkFields.Items[4].Selected;

            Response.Redirect(Page.Request.Url.ToString(), true); 
        }

        protected void grdActivity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ActivityIDX = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Edits")
            {
                Session.Add("ActivityIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/WQXActivityEdit.aspx");
            }

            if (e.CommandName == "Deletes")
            {
                int SuccID = db_WQX.DeleteT_WQX_ACTIVITY(ActivityIDX, User.Identity.Name);
                if (SuccID == 1)
                {
                    FillGrid();
                    lblMsg.Text = "Record successfully deleted.";
                }
                else
                    lblMsg.Text = "Unable to delete activity.";

            }

            if (e.CommandName == "WQX")
            {
                Session.Add("ActivityIDX", e.CommandArgument.ToString());
                Session.Add("TableCD", "ACT");
                Response.Redirect("~/App_Pages/Secure/WQX_Hist.aspx");
            }

        }

        public void FillGrid()
        {
            lblMsg.Text = "";
            DateTime? dateFrom = txtStartDt.Text.ConvertOrDefault<DateTime?>();
            DateTime? dateTo = txtEndDt.Text.ConvertOrDefault<DateTime?>() != null ? ((txtEndDt.Text.ConvertOrDefault<DateTime>()).Date.AddHours(23).AddMinutes(59).AddSeconds(59)).ConvertOrDefault<DateTime?>() : null;
            
            grdActivity.DataSource = db_WQX.GetWQX_ACTIVITYDisplay(!chkDeletedInd.Checked, Session["OrgID"].ConvertOrDefault<string>(), ddlMonLoc.SelectedValue.ConvertOrDefault<int?>(),
                dateFrom, dateTo, ddlActType.SelectedValue == "" ? null : ddlActType.SelectedValue, false, 
                ddlProject.SelectedValue.ConvertOrDefault<int?>(), ddlWQXStatus.SelectedValue);
            grdActivity.DataBind();
            
            Session["filtMonLoc"] = ddlMonLoc.SelectedValue;
            Session["filtStartDt"] = txtStartDt.Text;
            Session["filtEndDt"] = txtEndDt.Text;
            Session["filtProject"] = ddlProject.SelectedValue;
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("ActivitiesExport.xls", grdActivity);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            grdActivity.PageIndex = 0;
            FillGrid();
        }

        protected void grdActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdActivity.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void dsActivity_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["ActInd"] = !chkDeletedInd.Checked;
            e.InputParameters["MonLocIDX"] = ddlMonLoc.SelectedValue.ConvertOrDefault<int?>();
           //e.Arguments.TotalRowCount = 999;

        }


    }
}