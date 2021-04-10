using System;
using System.Web;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class WQXImportConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
                db_Accounts.SetOrgSessionID(User.Identity.Name, HttpContext.Current.Request.Url.LocalPath);

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkImport");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate drop-downs
                Utils.BindList(ddlColChar, dsChar, "CHAR_NAME", "CHAR_NAME");
                Utils.BindList(ddlColCharUnit, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ResultSampleFraction";
                Utils.BindList(ddlSampFraction, dsRefData, "VALUE", "VALUE");

                dsRefData.SelectParameters["tABLE"].DefaultValue = "MethodSpeciation";
                Utils.BindList(ddlSpeciation, dsRefData, "VALUE", "VALUE");


                DisplayTemplate();
            }
        }

        private void DisplayTemplate()
        {
            grdImport.DataSource = db_WQX.GetWQX_IMPORT_TEMPLATE(Session["OrgID"].ToString());
            grdImport.DataBind();
            pnlModTtl.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXImport.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void grdImport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            hdnTemplateID.Value = ID.ToString();

            if (e.CommandName == "Select")
            {
                //DISPLAY THE DATAGRIDS FOR THE SELECTED
                grdTemplateDtl.DataSource = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_DynamicByTemplateID(ID);
                grdTemplateDtl.DataBind();

                grdHardCode.DataSource = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_HardCodeByTemplateID(ID);
                grdHardCode.DataBind();

                pnlDtl.Visible = true;
            }
            if (e.CommandName == "Deletes")
            {
                int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMPLATE(ID);

                if (SuccID > 0)
                {
                    DisplayTemplate();
                    pnlDtl.Visible = false;
                }
                else
                    lblMsg.Text = "Unable to delete template.";
            }

        }

        protected void btnTemplateAdd_Click(object sender, EventArgs e)
        {
            int SuccID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMPLATE(null, Session["OrgID"].ToString(), chkTemplateType.SelectedValue, txtTemplateNew.Text, User.Identity.Name);
            if (SuccID > 0)
                DisplayTemplate();
            else
                lblMsg.Text = "Error adding new template.";

        }


        //******************************* MAPPED COL ********************************************
        protected void btnAddColumn_Click(object sender, EventArgs e)
        {
            int TemplateID = hdnTemplateID.Value.ConvertOrDefault<int>();

            if (TemplateID > 0)
            {
                int SuccID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMPLATE_DTL(null, TemplateID, txtColumn.Text.ConvertOrDefault<int>(), ddlFieldMap.SelectedValue, ddlColChar.SelectedValue, ddlColCharUnit.SelectedValue, User.Identity.Name, ddlSampFraction.SelectedValue, ddlSpeciation.SelectedValue);
                if (SuccID > 0)
                {
                    grdTemplateDtl.DataSource = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_DynamicByTemplateID(TemplateID);
                    grdTemplateDtl.DataBind();

                    ddlFieldMap.SelectedValue = "ACT_COMMENTS";
                    ddlColChar.SelectedValue = "";
                    ddlColCharUnit.SelectedValue = "";
                }
                else
                    lblMsg.Text = "Error adding new value.";
            }
            else
                lblMsg.Text = "Please select a template first.";
        }

        protected void grdTemplateDtl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int TemplateID = hdnTemplateID.Value.ConvertOrDefault<int>();
            int ID = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "Deletes")
            {
                int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMPLATE_DTL(ID);

                if (SuccID > 0)
                {
                    grdTemplateDtl.DataSource = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_DynamicByTemplateID(TemplateID);
                    grdTemplateDtl.DataBind();
                }
                else
                    lblMsg.Text = "Unable to delete template mapping.";
            }
        }


        //******************************* HARD CODE COL ******************************************
        protected void btnHardCodeAdd_Click(object sender, EventArgs e)
        {
            int TemplateID = hdnTemplateID.Value.ConvertOrDefault<int>();

            if (TemplateID > 0)
            {
                //get value to insert
                string HCVal = txtHardCodeValue.Visible ? txtHardCodeValue.Text : (ddlHardActID.Visible ? ddlHardActID.SelectedValue : (ddlHardCodeValue.Visible ? ddlHardCodeValue.SelectedValue : ""));

                int SuccID = db_WQX.InsertOrUpdateWQX_IMPORT_TEMPLATE_DTL(null, TemplateID, 0, ddlFieldMapHC.SelectedValue, HCVal, "", User.Identity.Name, null, null);
                if (SuccID > 0)
                {
                    grdHardCode.DataSource = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_HardCodeByTemplateID(TemplateID);
                    grdHardCode.DataBind();

                    ddlFieldMapHC.SelectedValue = "";
                }
                else
                    lblMsg.Text = "Error adding new value.";
            }
            else
                lblMsg.Text = "Please select a template first.";
        }

        protected void grdHardCode_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int TemplateID = hdnTemplateID.Value.ConvertOrDefault<int>();
            int ID = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "Deletes")
            {
                int SuccID = db_WQX.DeleteT_WQX_IMPORT_TEMPLATE_DTL(ID);

                if (SuccID > 0)
                {
                    grdHardCode.DataSource = db_WQX.GetWQX_IMPORT_TEMPLATE_DTL_HardCodeByTemplateID(TemplateID);
                    grdHardCode.DataBind();
                }
                else
                    lblMsg.Text = "Unable to delete template mapping.";
            }

        }

        protected void ddlFieldMapHC_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //initialize
            HC1.Visible = false;
            HC2.Visible = false;
            HC3.Visible = false;
            txtHardCodeValue.Text = "";
            ddlHardCodeValue.SelectedIndex = -1;

            //make controls visible based on selection
            if (ddlFieldMapHC.SelectedValue == "ACTIVITY_ID")
                HC2.Visible = true;
            else if (ddlFieldMapHC.SelectedValue == "ACT_COMMENTS")
                HC1.Visible = true;
            else if (ddlFieldMapHC.SelectedValue == "SAMP_COLL_METHOD_IDX")
            {
                HC3.Visible = true;
                Utils.BindList(ddlHardCodeValue, dsSampColl, "SAMP_COLL_METHOD_IDX", "SAMP_COLL_METHOD_ID");
            }
            else
            {
                HC3.Visible = true;
                dsRefData.SelectParameters["tABLE"].DefaultValue = ddlFieldMapHC.SelectedItem.Text;
                Utils.BindList(ddlHardCodeValue, dsRefData, "VALUE", "VALUE");
            }

            //ensure modal popup is visible after postback
            MPE_HardCode.Show();
        }
    }
}