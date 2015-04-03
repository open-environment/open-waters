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
    public partial class WQXOrgEdit : System.Web.UI.Page
    {
        string OrgEditID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //return to mon loc listing if none in session
            if (Session["OrgID"] == null)
                Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");

            if (Request.QueryString["c"] == "1")
                pnlCDX.CssClass = "fldErr row";

            //read session variables
            OrgEditID = Session["OrgID"].ToString();
            if (!IsPostBack)
            {
                //display left menu as selected 
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkOrgList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate drop-downs                
                Utils.BindList(ddlTribalCode, dsRefData, "VALUE", "TEXT");

                //******************* Populate organization information on form
                rbCDX.SelectedValue = "1";

                T_WQX_ORGANIZATION o = db_WQX.GetWQX_ORGANIZATION_ByID(OrgEditID);
                if (o != null)
                {
                    txtOrgID.Text = o.ORG_ID;
                    txtOrgID.ReadOnly = true;
                    txtOrgName.Text = o.ORG_FORMAL_NAME;
                    txtOrgDesc.Text = o.ORG_DESC;
                    ddlTribalCode.SelectedValue = o.TRIBAL_CODE;
                    txtOrgEmail.Text = o.ELECTRONICADDRESS;
                    txtOrgPhone.Text = o.TELEPHONE_NUM;
                    txtOrgPhoneExt.Text = o.TELEPHONE_EXT;

                    //CDX submission information*************************************8
                    if (o.CDX_SUBMIT_IND == true)
                    {
                        lblCDXSubmitInd.CssClass = "fldPass";
                        lblCDXSubmitInd.Text = "This Organization is able to submit to EPA.";
                    }
                    else
                    {
                        lblCDXSubmitInd.CssClass = "fldErr";
                        lblCDXSubmitInd.Text = "This Organization is unable to submit to EPA. Please correct this below.";
                    }

                    txtCDX.Text = o.CDX_SUBMITTER_ID;
                    txtCDXPwd.Text = "--------";
                    txtCDXPwd.Attributes["value"] = "--------";
                    if ((o.CDX_SUBMITTER_ID ?? "").Length > 0)
                        rbCDX.SelectedValue = "1";
                    else
                        rbCDX.SelectedValue = "2";
                }

                //populate listbox with users already in organization
                foreach (UserOrgDisplay u in db_WQX.GetT_OE_USERSInOrganization(txtOrgID.Text))
                {
                    ListItem li = new ListItem(u.USER_ID.ToString() + (u.ROLE_CD == "A" ? "(Admin)" : "(User)"), u.USER_IDX.ToString());
                    lbUserInRole.Items.Add(li);
                }

                //populate listbox with users not in role
                foreach (T_OE_USERS u in db_WQX.GetT_OE_USERSNotInOrganization(txtOrgID.Text))
                {
                    ListItem li = new ListItem(u.USER_ID.ToString(), u.USER_IDX.ToString());
                    lbAllUsers.Items.Add(li);
                }


                //only make visible if editing existing organization
                pnlRoles.Visible = (OrgEditID != "-1");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int SuccID = SavePage();

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");
            else
                lblMsg.Text = "Error updating record.";

        }

        protected void btnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrgSettings.aspx");
        }

        private int SavePage()
        {
            if (rbCDX.SelectedValue == "2")
            {
                txtCDX.Text = "";
                txtCDXPwd.Text = "";
            }

            //save updates to Organization
            return db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(txtOrgID.Text, txtOrgName.Text, txtOrgDesc.Text, ddlTribalCode.SelectedValue.ToString(),
                txtOrgEmail.Text, "Email", txtOrgPhone.Text, "Office", txtOrgPhoneExt.Text, txtCDX.Visible ? txtCDX.Text : "", txtCDX.Visible ? txtCDXPwd.Text : "", null, null, User.Identity.Name);
        }


        //************************* user org ********************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //add user to role
            if (lbAllUsers.SelectedIndex != -1)
            {
                lblMsg.Text = "";

                if (db_WQX.InsertT_WQX_USER_ORGS(txtOrgID.Text, Int32.Parse(lbAllUsers.SelectedValue), chkAdmin.Checked ? "A" : "U", User.Identity.Name) == 1)
                    Response.Redirect(Request.RawUrl);
                else
                    lblMsg.Text = "Error Adding User to Organization.";
            }
            else
                lblMsg.Text = "Please select a user to add to the organization.";

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbUserInRole.SelectedIndex != -1)
            {
                lblMsg.Text = "";
                int removedUser = Int32.Parse(lbUserInRole.SelectedValue);

                if (db_WQX.DeleteT_WQX_USER_ORGS(txtOrgID.Text, removedUser) == 1)
                    Response.Redirect(Request.RawUrl);
                else
                    lblMsg.Text = "Error Removing User from Organization.";
            }
            else
            {
                lblMsg.Text = "Please select a user to remove from this organization.";
            }

        }


        //**************************** NAAS testing **************************************
        protected void btnTestNAASLocal_Click(object sender, EventArgs e)
        {
            int SuccID = SavePage();

            //**************** VALIDATION **************************
            if (txtCDX.Text.Length == 0)
            {
                lblMsg.Text = "Please enter a CDX Submitter first. This is your NAAS username provided by EPA.";
                return;
            }
            //***************** END VALIDATION **********************

            ConnectTest("LOCAL");
        }

        private void ConnectTest(string typ)
        {
            try
            {
                //AUTHENTICATION TEST*********************************************
                CDXCredentials cred = WQXSubmit.GetCDXSubmitCredentials2(Session["OrgID"].ToString());
                string token = WQXSubmit.AuthHelper(cred.userID, cred.credential, "Password", "default", cred.NodeURL);
                if (token.Length > 0)
                {
                    spnAuth.Attributes["class"] = "signup_header_check";
                    lblAuthResult.Text = "Authentication passed.";
                    lblCDXSubmitInd.CssClass = "fldPass";
                    lblCDXSubmitInd.Text = "This Organization is able to submit to EPA.";


                    //SUBMIT TEST*********************************************
                    List<net.epacdxnode.test.ParameterType> pars = new List<net.epacdxnode.test.ParameterType>();

                    net.epacdxnode.test.ParameterType p = new net.epacdxnode.test.ParameterType();
                    p.parameterName = "organizationIdentifier";
                    p.Value = Session["OrgID"].ToString();
                    pars.Add(p);

                    net.epacdxnode.test.ParameterType p2 = new net.epacdxnode.test.ParameterType();
                    p2.parameterName = "monitoringLocationIdentifier";
                    p2.Value = "";
                    pars.Add(p2);

                    OpenEnvironment.net.epacdxnode.test.ResultSetType rs = WQXSubmit.QueryHelper(cred.NodeURL, token, "WQX", "WQX.GetMonitoringLocationByParameters_v2.1", null, null, pars);

                    if (rs.rowId == "-99")
                    {
                        //THE NAAS ACCOUNT DOES NOT HAVE RIGHTS TO SUBMIT FOR THIS ORGANIZATION*********************************************
                        spnSubmit.Attributes["class"] = "signup_header_cross";
                        if (typ == "LOCAL")
                            lblSubmitResult.Text = "The NAAS account you supplied is not authorized to submit for this organization. Please contact the STORET Helpdesk to request access.";
                        else
                            lblSubmitResult.Text = "Open Waters is not authorized to submit for your organization. Please contact the STORET Helpdesk to request access.";

                        lblCDXSubmitInd.CssClass = "fldErr";
                        lblCDXSubmitInd.Text = "This Organization is unable to submit to EPA. Please correct this below.";
                        db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(Session["OrgID"].ToString(), null, null, null, null, null, null, null, null, null, null, false, null, User.Identity.Name);
                    }
                    else
                    {
                        spnSubmit.Attributes["class"] = "signup_header_check";
                        lblSubmitResult.Text = "Submit test passed.";
                        lblCDXSubmitInd.CssClass = "fldPass";
                        lblCDXSubmitInd.Text = "This Organization is able to submit to EPA.";

                        //BOTH AUTHENTICATION AND SUBMIT PASSES - UPDATE ORG SUBMIT IND*********************************************
                        db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(Session["OrgID"].ToString(), null, null, null, null, null, null, null, null, null, null, true, null, User.Identity.Name);
                    }
                }
                else  //failed authentication
                {
                    spnAuth.Attributes["class"] = "signup_header_cross";
                    lblAuthResult.Text = "Unable to authenticate to EPA-CDX. Please double-check your username and password.";

                    spnSubmit.Attributes["class"] = "signup_header_crossbw";
                    lblSubmitResult.Text = "Cannot test until authentication is resolved.";
                    lblCDXSubmitInd.CssClass = "fldErr";
                    lblCDXSubmitInd.Text = "This Organization is unable to submit to EPA. Please correct this below.";


                    db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(Session["OrgID"].ToString(), null, null, null, null, null, null, null, null, null, null, false, null, User.Identity.Name);
                }

                pnlCDXResults.Visible = true;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnTestNAASGlobal_Click(object sender, EventArgs e)
        {
            int SuccID = SavePage();

            ConnectTest("GLOBAL");

        }


    }
}