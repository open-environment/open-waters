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
    public partial class WQXOrgNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                grdOrg.DataSource = db_WQX.GetV_WQX_ALL_ORGS();
                grdOrg.DataBind();
            }
        }

        protected void grdOrg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                lblMsg.Text = "";
                txtOrgIDConfirm.Text = e.CommandArgument.ToString();

                //check to see if selected organization is already in Open Waters
                T_WQX_ORGANIZATION org = db_WQX.GetWQX_ORGANIZATION_ByID(e.CommandArgument.ToString());
                if (org != null)
                    //Case 1: org is already in Open Waters
                    lblConfirmText.Text = org.ORG_FORMAL_NAME + " (" + org.ORG_ID + ") is already using Open Waters. Click 'Confirm' to notify an Administrator for this Organization to approve your access request.";
                else  
                    //Case 2: org doesn't exist in Open Waters
                    lblConfirmText.Text = "This Organization does not yet exist in Open Waters. Click 'Confirm' to request access to this organization. ";

                pnl1.Visible = false;
                pnlNewOrgConfirm.Visible = true;
                grdOrg.Visible = false;
                btnAdd.Visible = false;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/Dashboard.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int UserIDX = Session["UserIDX"].ConvertOrDefault<int>();
            string OrgID = txtOrgIDConfirm.Text;
            var emailTo = new List<string>();

            //check to see if selected organization is already in Open Waters
            T_WQX_ORGANIZATION org = db_WQX.GetWQX_ORGANIZATION_ByID(OrgID);
            if (org == null)
            {
                //***************************************************************************
                //*************** CASE 1: CREATE NEW ORG
                //***************************************************************************
                T_EPA_ORGS eo = db_WQX.GetT_EPA_ORGS_ByOrgID(OrgID);
                if (eo != null)
                {
                    int SuccID = db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(eo.ORG_ID, eo.ORG_FORMAL_NAME, null, null, "", null, "", null, null, null, null, false, null, User.Identity.Name);
                    if (SuccID == 0)
                    {
                        pnlNewOrgConfirm.Visible = false;
                        lblMsg.Text = "This request cannot be completed.";
                        return; 
                    }
                }
                else
                {
                    pnlNewOrgConfirm.Visible = false;
                    lblMsg.Text = "This request cannot be completed.";
                    return;
                }

                List<T_OE_USERS> admins = db_Accounts.GetT_OE_USERSInRole(2);
                foreach (T_OE_USERS admin in admins)
                    emailTo.Add(admin.EMAIL);


            }
            else
            {
                //***************************************************************************
                //*************** CASE 2: ORG ALREADY IN OPEN WATERS
                //***************************************************************************

                //now check to see if there are any Admins for this organization
                List<T_OE_USERS> uorgs = db_WQX.GetWQX_USER_ORGS_AdminsByOrg(OrgID);
                if (uorgs.Count > 0)  
                {
                    foreach (T_OE_USERS uorg in uorgs)
                        emailTo.Add(uorg.EMAIL);
                }
                else
                {
                    List<T_OE_USERS> admins = db_Accounts.GetT_OE_USERSInRole(2);
                    foreach (T_OE_USERS admin in admins)
                        emailTo.Add(admin.EMAIL);
                }


            }

            //Org is now in Open Waters, so user added to Org with pending status
            db_WQX.InsertT_WQX_USER_ORGS(OrgID, UserIDX, "P", User.Identity.Name);

            //send email with request
            string msg = "A user has requested to join an organization for which you are an administrator." + "\r\n\r\n";
            msg += "The following user account (" + User.Identity.Name + ") has requested to join your organization (" + OrgID + "). Please log into Open Waters and either accept or reject this request." + "\r\n\r\n";
            bool eMailSucc = Utils.SendEmail(null, emailTo, null, null, "Open Waters: User Requesting to Join Organization", msg, null);

            pnlNewOrgConfirm.Visible = false;
            lblMsg.Text = "Your request has been made. You will be emailed when your request has been approved.";

        }
    }
}