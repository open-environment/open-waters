using System;
using System.Web;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using System.Web.UI.WebControls;

namespace OpenEnvironment
{
    public partial class RefData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
                
        protected void ddlRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            DisplayGrids();

            txtID.Visible = (ddlRef.SelectedValue != "Laboratory");
            txtDesc.Visible = (ddlRef.SelectedValue != "Laboratory");
            lblID.Visible = (ddlRef.SelectedValue != "Laboratory");
            lblDesc.Visible = (ddlRef.SelectedValue != "Laboratory");
        }

        private void DisplayGrids()
        {
            try
            {
                lblMsg.Text = "";
                grdRef.PageIndex = 0;

                grdRef.Visible = (ddlRef.SelectedValue != "County") && (ddlRef.SelectedValue != "Characteristic") && (ddlRef.SelectedValue != "SampleCollectionMethod") && (ddlRef.SelectedValue != "SamplePrepMethod") && (ddlRef.SelectedValue != "AnalyticalMethod") && (ddlRef.SelectedValue != "Laboratory");
                grdCounty.Visible = (ddlRef.SelectedValue == "County");
                grdChar.Visible = (ddlRef.SelectedValue == "Characteristic");
                grdSampColl.Visible = (ddlRef.SelectedValue == "SampleCollectionMethod");
                grdSampPrep.Visible = (ddlRef.SelectedValue == "SamplePrepMethod");
                grdAnalMethod.Visible = (ddlRef.SelectedValue == "AnalyticalMethod");
                grdLab.Visible = (ddlRef.SelectedValue == "Laboratory");

                btnAdd.Visible = (ddlRef.SelectedValue == "AnalyticalMethod") || (ddlRef.SelectedValue == "SampleCollectionMethod") 
                    || (ddlRef.SelectedValue == "SamplePrepMethod") || (ddlRef.SelectedValue == "Laboratory");

                if (grdRef.Visible)
                {
                    if (!HttpContext.Current.User.IsInRole("ADMINS"))
                        grdRef.Columns[0].Visible = false; 
                }

                if (grdChar.Visible) grdChar.DataBind();
                if (grdAnalMethod.Visible) grdAnalMethod.DataBind();
                if (grdSampPrep.Visible) grdSampPrep.DataBind();
                if (grdSampColl.Visible) grdSampColl.DataBind();
                if (grdLab.Visible) grdLab.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayGrids();
        }

        protected void btnNewSave_Click(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select or create an organization first.";
                return;
            }


            if (ddlRef.SelectedValue == "AnalyticalMethod")
                db_Ref.InsertOrUpdateT_WQX_REF_ANAL_METHOD(null, txtID.Text, Session["OrgID"].ToString(), txtName.Text, txtDesc.Text, true);

            if (ddlRef.SelectedValue == "SampleCollectionMethod")
                db_Ref.InsertOrUpdateT_WQX_REF_SAMP_COL_METHOD(null, txtID.Text, Session["OrgID"].ToString(), txtName.Text, txtDesc.Text, true);

            if (ddlRef.SelectedValue == "SamplePrepMethod")            
                db_Ref.InsertOrUpdateT_WQX_REF_SAMP_PREP(null, txtID.Text, Session["OrgID"].ToString(), txtName.Text, txtDesc.Text, true);

            if (ddlRef.SelectedValue == "Laboratory")
                db_Ref.InsertOrUpdateT_WQX_REF_LAB(null, txtName.Text, null, null, Session["OrgID"].ToString(), true);

            txtID.Text = "";
            txtName.Text = "";
            txtDesc.Text = "";

            DisplayGrids();
        }

        protected void grdRef_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int RefID = e.CommandArgument.ToString().ConvertOrDefault<int>();

            if (e.CommandName == "Deletes")
                db_Ref.UpdateT_WQX_REF_DATAByIDX(RefID, null, null, false);
        }

        protected void grdRef_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdRef.PageIndex = e.NewPageIndex;
        }

        protected void grdChar_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string charName = e.CommandArgument.ToString();

            if (e.CommandName == "Deletes")
                db_Ref.InsertOrUpdateT_WQX_REF_CHARACTERISTIC(charName, null, null, null, false, null, null);
        }

        protected void grdChar_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdChar.PageIndex = e.NewPageIndex;
        }

        protected void grdAnalMethod_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int AnalIDX = e.CommandArgument.ToString().ConvertOrDefault<int>();

            if (e.CommandName == "Deletes")
                db_Ref.InsertOrUpdateT_WQX_REF_ANAL_METHOD(AnalIDX, null, null, null, null, false);
        }

        protected void grdAnalMethod_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdAnalMethod.PageIndex = e.NewPageIndex;
        }

        protected void grdSampPrep_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

        }

        protected void grdSampPrep_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdSampPrep.PageIndex = e.NewPageIndex;
        }

        protected void grdSampColl_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

        }

        protected void grdSampColl_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdSampColl.PageIndex = e.NewPageIndex;
        }

        protected void grdLab_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

        }

        protected void grdLab_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdLab.PageIndex = e.NewPageIndex;
        }

        protected bool CheckOrg(string context)
        {
            return (context == Session["OrgID"].ToString());
        }

        protected void grdAnalMethod_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text != Session["OrgID"].ToString())
                {
                    ImageButton ib = (ImageButton)e.Row.FindControl("EditButton");
                    ib.Visible = false;
                    ImageButton ib2 = (ImageButton)e.Row.FindControl("DelButton");
                    ib2.Visible = false;
                }               
            }
        }
    
    }
}