using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment.App_Pages.Secure
{
    public partial class AssessEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);

            if (Session["AssessRptIDX"] == null)
                Response.Redirect("~/App_Pages/Secure/Assess.aspx");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkAssess");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                Form.DefaultFocus = txtRptName.ClientID;
                Form.DefaultButton = btnSave.UniqueID;

                hdnReportIDX.Value = Session["AssessRptIDX"].ToString();
                PopulateForm();
            }

        }

        private void PopulateForm()
        {
            int rptID = hdnReportIDX.Value.ConvertOrDefault<int>();

            if (rptID > 0)
            {
                //general info
                T_ATTAINS_REPORT rpt = db_Attains.GetT_ATTAINS_REPORT_byID(rptID);
                if (rpt != null)
                {
                    txtRptName.Text = rpt.REPORT_NAME;
                    txtRptFrom.Text = rpt.DATA_FROM.ToString();
                    txtRptTo.Text = rpt.DATA_TO.ToString();

                    //assessment units
                    grdAssessUnits.DataSource = db_Attains.GetT_ATTAINS_ASSESS_UNITS_byReportID(rptID);
                    grdAssessUnits.DataBind();

                    //assessments
                    grdAssess.DataSource = db_Attains.GetT_ATTAINS_ASSESS_byReportID(rptID);
                    grdAssess.DataBind();
                }

                pnlTabs.Visible = (rpt != null);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? AssRptIDX = hdnReportIDX.Value.ConvertOrDefault<int>() > 0 ? hdnReportIDX.Value.ConvertOrDefault<int?>() : null;
            
            int SuccID = db_Attains.InsertOrUpdateATTAINS_REPORT(AssRptIDX, Session["OrgID"].ToString(), txtRptName.Text, txtRptFrom.Text.ConvertOrDefault<DateTime?>(),
                txtRptTo.Text.ConvertOrDefault<DateTime?>(), null, null, null, User.Identity.Name);

            if (SuccID > 0)
            {
                Session["AssessRptIDX"] = SuccID.ToString();
                Response.Redirect("~/App_Pages/Secure/AssessEdit.aspx");
            }
            else
                lblMsg.Text = "Error saving report";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/Assess.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int SuccID = db_Attains.DeleteT_ATTAINS_REPORT(hdnReportIDX.Value.ConvertOrDefault<int>());

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/Assess.aspx");
            else if (SuccID == -1)
                lblMsg.Text = "Unable to delete assessment report because it has already been submitted to EPA.";
            else
                lblMsg.Text = "Unable to delete assessment report.";

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //**** validation ****
            int AssRptIDX = hdnReportIDX.Value.ConvertOrDefault<int>();

            if (AssRptIDX > 0)
            {
                ATTAINSSubmit.ATTAINS_byReport(AssRptIDX);

            }
        }


        //************************** ASSESS UNITS TAB *********************************************
        protected void grdAssessUnits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                Session.Add("AssessUnitIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/Assess_Unit.aspx");
            }
        }

        protected void btnAddAssessUnit_Click(object sender, EventArgs e)
        {
            Session.Add("AssessUnitIDX", "-1");
            Response.Redirect("~/App_Pages/Secure/Assess_Unit.aspx");
        }


        //************************** ASSESSMENTS TAB *********************************************
        protected void grdAssess_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                Session.Add("AssessIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/Assess_Assess.aspx");
            }
        }

        protected void btnAddAssess_Click(object sender, EventArgs e)
        {
            Session.Add("AssessIDX", "-1");
            Response.Redirect("~/App_Pages/Secure/Assess_Assess.aspx");
        }


        //************************** ACTIONS TAB *********************************************
        protected void grdAction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                Session.Add("ActionIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/Assess_Action.aspx");
            }
        }

        protected void btnAddAction_Click(object sender, EventArgs e)
        {
            Session.Add("ActionIDX", "-1");
            Response.Redirect("~/App_Pages/Secure/Assess_Action.aspx");
        }

        //************************** PRIORITIES TAB *********************************************
        protected void grdPriority_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                Session.Add("PriorityIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/Assess_Priority.aspx");
            }
        }

        protected void btnAddPriority_Click(object sender, EventArgs e)
        {
            Session.Add("PriorityIDX", "-1");
            Response.Redirect("~/App_Pages/Secure/Assess_Priority.aspx");
        }



    }
}