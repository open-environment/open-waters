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
            if (Session["OrgEditID"] == null)
                Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");

            //read session variables
            OrgEditID = Session["OrgEditID"].ToString();
            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkOrgList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate drop-downs                
                Utils.BindList(ddlTribalCode, dsRefData, "VALUE", "TEXT");
                Utils.BindList(ddlChar, dsChar, "CHAR_NAME", "CHAR_NAME");


                //******************* Populate organization information on form
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
                    txtCDX.Text = o.CDX_SUBMITTER_ID;
                    txtCDXPwd.Text = "--------";
                    txtCDXPwd.Attributes["value"] = "--------";
                }

                //populate listbox with users already in organization
                foreach (T_OE_USERS u in db_WQX.GetT_OE_USERSInOrganization(txtOrgID.Text))
                {
                    ListItem li = new ListItem(u.USER_ID.ToString(), u.USER_IDX.ToString());
                    lbUserInRole.Items.Add(li);
                }

                //populate listbox with users not in role
                foreach (T_OE_USERS u in db_WQX.GetT_OE_USERSNotInOrganization(txtOrgID.Text))
                {
                    ListItem li = new ListItem(u.USER_ID.ToString(), u.USER_IDX.ToString());
                    lbAllUsers.Items.Add(li);
                }


                //populate Org Characteristics grid
                PopulateCharGrid();

                //only make visible if editing existing organization
                pnlRoles.Visible = (OrgEditID != "-1");
                pnlChars.Visible = (OrgEditID != "-1");
            }
        }

        private void PopulateCharGrid()
        {
            grdChar.DataSource = db_Ref.GetT_WQX_REF_CHAR_ORG(txtOrgID.Text);
            grdChar.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save updates to Organization
            int SuccID = db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(txtOrgID.Text, txtOrgName.Text, txtOrgDesc.Text, ddlTribalCode.SelectedValue.ToString(), 
                txtOrgEmail.Text, "Email", txtOrgPhone.Text, "Office", txtOrgPhoneExt.Text, txtCDX.Text, txtCDXPwd.Text, null, User.Identity.Name);

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");
            else
                lblMsg.Text = "Error updating record.";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //add user to role
            if (lbAllUsers.SelectedIndex != -1)
            {
                lblMsg.Text = "";

                if (db_WQX.InsertT_WQX_USER_ORGS(txtOrgID.Text, Int32.Parse(lbAllUsers.SelectedValue), "U", User.Identity.Name) == 1)
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

        protected void grdChar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CharName = e.CommandArgument.ToString();

            if (e.CommandName == "Deletes")
            {
                db_Ref.DeleteT_WQX_REF_CHAR_ORG(txtOrgID.Text, CharName);
                PopulateCharGrid();
            }

        }

        protected void btnAddChar_Click(object sender, EventArgs e)
        {
            db_Ref.InsertOrUpdateT_WQX_REF_CHAR_ORG(ddlChar.SelectedValue, txtOrgID.Text, User.Identity.Name);
            PopulateCharGrid();
        }

    }
}