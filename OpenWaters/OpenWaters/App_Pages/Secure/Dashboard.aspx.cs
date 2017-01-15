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
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //****************************************************************************
                //************* Data Collection Metrics Panel ***********************************
                //****************************************************************************
                lblOrg.Text = db_WQX.GetWQX_ORGANIZATION().Count().ToString();
                int UserIDX = Session["UserIDX"].ConvertOrDefault<int>();

                if (!string.IsNullOrEmpty(Session["OrgID"] as string))
                {
                    string orgID = Session["OrgID"].ToString();
                    pnlOrgSpecific.Visible = true;
                    lblOrgName.Text = db_WQX.GetWQX_ORGANIZATION_ByID(orgID).ORG_FORMAL_NAME;
                    lblProject2.Text = db_WQX.GetWQX_PROJECT(true, orgID, false).Count.ToString();
                    lblSamp.Text = db_WQX.GetWQX_ACTIVITY(true, orgID, null, null, null, null, false, null).Count().ToString();
                    lblSampPend2.Text = db_WQX.GetWQX_ACTIVITY(true, orgID, null, null, null, null, true, null).Count().ToString();
                    lblResult.Text = db_WQX.GetT_WQX_RESULTCount(orgID).ToString();
                }
                else
                    pnlOrgSpecific.Visible = false;
                //****************************************************************************



                //****************************************************************************
                //**************Admin Tasks Panel ***************************************
                //****************************************************************************
                if (HttpContext.Current.User.IsInRole("ADMINS") || db_WQX.CanUserAdminOrgs(UserIDX))
                {
                    pnlAdminTasks.Visible = true;
                    DisplayPendingUsersGrid();
                }

                

                //****************************************************************************
                //**************Getting started wizard ***************************************
                //****************************************************************************

                //STEP 1 ***********************************
                List<T_WQX_ORGANIZATION> o1s = db_WQX.GetWQX_USER_ORGS_ByUserIDX(UserIDX, false);
                if (o1s.Count == 0)
                {
                    lblWiz1.Text = "To use Open Waters, you must first be linked with an Organization. This is the water monitoring agency you represent. If you intend to submit your data to EPA, this organization must first be created by EPA in their WQX system. Otherwise, if you never intend to submit your data to EPA, you can create any Organization ID you wish.";
                    spnWiz1.Attributes["class"] = "signup_header_cross";
                    btnWiz1.Text = "Get Started";
                    spnWiz2.Attributes["class"] = "signup_header_crossbw";
                    btnWiz2.Visible = false;
                    spnWiz3.Attributes["class"] = "signup_header_crossbw";
                    btnWiz3.Visible = false;
                    btnWiz3b.Visible = false;
                    spnWiz4.Attributes["class"] = "signup_header_crossbw";
                    btnWiz4.Visible = false;
                    btnWiz4b.Visible = false;
                    spnWiz5.Attributes["class"] = "signup_header_crossbw";
                    btnWiz5.Visible = false;
                    spnWiz6.Attributes["class"] = "signup_header_crossbw";
                    btnWiz6.Visible = false;
                    btnWiz6b.Visible = false;
                }
                else
                {
                    List<T_WQX_ORGANIZATION> oNotPends = db_WQX.GetWQX_USER_ORGS_ByUserIDX(UserIDX, true);
                    if (oNotPends.Count == 0)
                    {
                        //only organization user is associated with is pending
                        btnWiz1.Visible = false;
                        lblWiz1.Text = "Your request to view/submit data for an organization is pending. You must wait for an administrator to approve your request.";
                        spnWiz1.Attributes["class"] = "signup_header_progress";
                        spnWiz2.Attributes["class"] = "signup_header_crossbw";
                        btnWiz2.Visible = false;
                        spnWiz3.Attributes["class"] = "signup_header_crossbw";
                        btnWiz3.Visible = false;
                        btnWiz3b.Visible = false;
                        spnWiz4.Attributes["class"] = "signup_header_crossbw";
                        btnWiz4.Visible = false;
                        btnWiz4b.Visible = false;
                        spnWiz5.Attributes["class"] = "signup_header_crossbw";
                        btnWiz5.Visible = false;
                        spnWiz6.Attributes["class"] = "signup_header_crossbw";
                        btnWiz6.Visible = false;
                        btnWiz6b.Visible = false;
                    }
                    else
                    {
                        //STEP 1 IS COMPLETE, now try out tests 2-6

                        btnWiz1.Text = "View";
                        lblWiz1.Text = "Congrats! You are associated with an Organization. Click to view its details.";
                        spnWiz1.Attributes["class"] = "signup_header_check";


                        //STEP 2: submit authorization ******************************************
                        foreach (T_WQX_ORGANIZATION oNotPend in oNotPends)
                        {
                            if (oNotPend.CDX_SUBMIT_IND == true)
                            {
                                spnWiz2.Attributes["class"] = "signup_header_check";
                                lblWiz2.Text = "Congrats! Your organization is authorized to submit to EPA-WQX.";
                                btnWiz2.Text = "Change Credentials";
                            }
                            else
                            {
                                spnWiz2.Attributes["class"] = "signup_header_cross";
                                lblWiz2.Text = "In order to submit data to EPA using Open Waters, you must contact EPA and request that they authorize Open Waters to submit data.";
                                btnWiz2.Text = "Get Started";
                            }


                        }

                        //STEP 3:Mon Loc******************************************
                        bool MonLocOK = false;
                        if (db_WQX.GetWQX_MONLOC_MyOrgCount(UserIDX) > 0)
                        {
                            lblWiz3.Text = "One or more monitoring locations have been created. Click to view.";
                            spnWiz3.Attributes["class"] = "signup_header_check";
                            btnWiz3.Text = "View";
                            MonLocOK = true;
                        }
                        else
                        {
                            lblWiz3.Text = "Click to enter a monitoring location record.";
                            spnWiz3.Attributes["class"] = "signup_header_cross";
                        }


                        //STEP 4:Project ******************************************
                        bool ProjOK = false;
                        if (db_WQX.GetWQX_PROJECT_MyOrgCount(UserIDX) > 0)
                        {
                            lblWiz4.Text = "One or more projects have been created. Click to view.";
                            spnWiz4.Attributes["class"] = "signup_header_check";
                            btnWiz4.Text = "View";
                            ProjOK = true;
                        }
                        else
                        {
                            lblWiz4.Text = "Click to manually enter a project record or import records from a spreadsheet or EPA. ";
                            spnWiz4.Attributes["class"] = "signup_header_cross";
                        }

                        //STEP 5: Organization Starter Data ******************************************

                        if ((oNotPends[0].DEFAULT_TIMEZONE ?? "").Length > 0)
                        {
                            lblWiz5.Text = "Organization default data (e.g. Default Timezone) has been defined. Click to view.";
                            spnWiz5.Attributes["class"] = "signup_header_check";
                        }
                        else
                        {
                            lblWiz5.Text = "Click to enter default organization data (e.g. Default Timezone, characteristics) that will be helpful during activity data entry.";
                            spnWiz5.Attributes["class"] = "signup_header_cross";
                        }



                        //STEP 6: Activity ******************************************
                        if (ProjOK == true && MonLocOK == true)
                        {
                            if (db_WQX.GetWQX_ACTIVITY_MyOrgCount(UserIDX) > 0)
                            {
                                lblWiz6.Text = "One or more activities have been created. Click to view.";
                                spnWiz6.Attributes["class"] = "signup_header_check";
                                btnWiz6.Text = "View";
                            }
                            else
                            {
                                lblWiz6.Text = "Click to enter an activity record.";
                                spnWiz6.Attributes["class"] = "signup_header_cross";
                            }
                        }
                        else
                        {
                            lblWiz6.Text = "You must enter a monitoring location and a project before you begin to create activities.";
                            spnWiz6.Attributes["class"] = "signup_header_crossbw";
                            btnWiz6.Visible = false;
                        }

                    }

                }
                //**************End Getting started wizard *******************************
            }
        }

        private void DisplayPendingUsersGrid()
        {

            string thisOrg = HttpContext.Current.User.IsInRole("ADMINS") ? null : Session["OrgID"].ToString();

            grdPendingUsers.DataSource = db_WQX.GetT_OE_USERSPending(thisOrg);
            grdPendingUsers.DataBind();
            if (grdPendingUsers.Rows.Count == 0)
            {
                lblAdminMsg.Text = "No Admin tasks at this time.";
                pnlPendingUsers.Visible = false;
            }
        }

        protected void btnWiz1_Click(object sender, EventArgs e)
        {
            if (btnWiz1.Text == "Get Started")
                Response.Redirect("~/App_Pages/Secure/WQXOrgNew.aspx");
            else
                Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");

        }

        protected void btnWiz2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrgEdit.aspx?c=1");
        }

        protected void btnWiz3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXMonLoc.aspx");
        }

        protected void btnWiz4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXProject.aspx");
        }

        protected void btnWiz4b_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx");
        }

        protected void btnWiz5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrgData.aspx");
        }

        protected void btnWiz6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");
        }

        protected void grdPendingUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int UserIDX = commandArgs[0].ConvertOrDefault<int>();
            string orgID = commandArgs[1];
            string AproveRejectCode = "P";

            if (e.CommandName == "Approve")
                AproveRejectCode = "U";
            else if (e.CommandName == "Reject")
                AproveRejectCode = "R";

            int SuccID = db_WQX.ApproveRejectT_WQX_USER_ORGS(orgID, UserIDX, AproveRejectCode);
            if (SuccID == -1)
                lblAdminMsg.Text = "User's request has been declined.";
            else if (SuccID == 1)
                lblAdminMsg.Text = "User's request has been accepted.";
            else if (SuccID == 0)
                lblAdminMsg.Text = "Unable to complete this action.";

            DisplayPendingUsersGrid();

        }

    }
}