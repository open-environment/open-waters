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
    public partial class WQXProjectEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //return to mon loc listing if none in session
            if (Session["ProjectIDX"] == null || Session["OrgID"] == null)
                Response.Redirect("~/App_Pages/Secure/WQXProject.aspx");

            if (!IsPostBack)
            {
                //populate drop-downs                
                Utils.BindList(ddlSampDesignTypeCode, dsRefData, "VALUE", "TEXT");

                //******************* Populate project information on form
                T_WQX_PROJECT m = db_WQX.GetWQX_PROJECT_ByID(Session["ProjectIDX"].ConvertOrDefault<int>());
                if (m != null)
                {
                    lblProjectIDX.Text = m.PROJECT_IDX.ToString();
                    txtProjID.Text = m.PROJECT_ID;
                    txtProjName.Text = m.PROJECT_NAME;
                    txtProjDesc.Text = m.PROJECT_DESC;
                    ddlSampDesignTypeCode.SelectedValue = m.SAMP_DESIGN_TYPE_CD;
                    if (m.QAPP_APPROVAL_IND != null)
                        chkQAPPInd.Checked = (bool)m.QAPP_APPROVAL_IND;
                    txtQAPPAgency.Text = m.QAPP_APPROVAL_AGENCY;
                    if (m.WQX_IND != null)
                        chkWQXInd.Checked = (bool)m.WQX_IND;
                    if (m.ACT_IND != null)
                        chkActInd.Checked = (bool)m.ACT_IND;
                }
                else
                {
                    lblProjectIDX.Text = "";
                    chkActInd.Checked = true;
                    chkWQXInd.Checked = true;
                }

                //set focus on first form element
                txtProjID.Focus();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save updates to Project
            int SuccID = db_WQX.InsertOrUpdateWQX_PROJECT(lblProjectIDX.Text.ConvertOrDefault<int?>(), Session["OrgID"].ToString(), txtProjID.Text, txtProjName.Text,
                txtProjDesc.Text, ddlSampDesignTypeCode.SelectedValue, chkQAPPInd.Checked, txtQAPPAgency.Text, "U", null, chkActInd.Checked, chkWQXInd.Checked, User.Identity.Name);

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/WQXProject.aspx");
            else
                lblMsg.Text = "Error updating record.";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXProject.aspx");
        }
    }
}