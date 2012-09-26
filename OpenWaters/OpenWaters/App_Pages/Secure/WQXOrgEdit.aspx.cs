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
                if (hl != null) hl.CssClass = "divBody sel";

                //populate drop-downs                
                Utils.BindList(ddlTribalCode, dsRefData, "VALUE", "TEXT");


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
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save updates to Organization
            int SuccID = db_WQX.InsertOrUpdateT_WQX_ORGANIZATION(txtOrgID.Text, txtOrgName.Text, txtOrgDesc.Text, ddlTribalCode.SelectedValue.ToString(), 
                txtOrgEmail.Text, "Email", txtOrgPhone.Text, "Office", txtOrgPhoneExt.Text, User.Identity.Name);

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/WQXOrg.aspx");
            else
                lblMsg.Text = "Error updating record.";
        }
    }
}