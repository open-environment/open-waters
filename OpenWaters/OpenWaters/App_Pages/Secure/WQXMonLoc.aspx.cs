using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ionic.Zip;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.net.epacdxnode.test;
using System.Web.Services.Protocols;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment
{
    public partial class WQXMonLoc : System.Web.UI.Page
    {
        protected void Page_PreRender(object o, System.EventArgs e)
        {
            FillGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OrgID"] == null)
            {
                lblMsg.Text = "Please select or create an organization first.";
                btnAdd.Visible = false;
                return;
            }

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkMonLocList");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                grdMonLoc.Columns[5].Visible = (Session["MLOC_HUC_EIGHT"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[6].Visible = (Session["MLOC_HUC_TWELVE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[7].Visible = (Session["MLOC_TRIBAL_LAND"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[10].Visible = (Session["MLOC_SOURCE_MAP_SCALE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[11].Visible = (Session["MLOC_HORIZ_COLL_METHOD"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[12].Visible = (Session["MLOC_HORIZ_REF_DATUM"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[13].Visible = (Session["MLOC_VERT_MEASURE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[14].Visible = (Session["MLOC_VERT_MEASURE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[15].Visible = (Session["MLOC_VERT_MEASURE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[16].Visible = (Session["MLOC_VERT_MEASURE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[17].Visible = (Session["MLOC_COUNTY_CODE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[18].Visible = (Session["MLOC_STATE_CODE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[19].Visible = (Session["MLOC_COUNTRY_CODE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[20].Visible = (Session["MLOC_WELL_TYPE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[21].Visible = (Session["MLOC_AQUIFER_NAME"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[22].Visible = (Session["MLOC_FORMATION_TYPE"].ConvertOrDefault<Boolean>());
                grdMonLoc.Columns[23].Visible = (Session["MLOC_WELLHOLE_DEPTH"].ConvertOrDefault<Boolean>());

                chkFields.Items[0].Selected = (Session["MLOC_HUC_EIGHT"].ConvertOrDefault<Boolean>());
                chkFields.Items[1].Selected = (Session["MLOC_HUC_TWELVE"].ConvertOrDefault<Boolean>());
                chkFields.Items[2].Selected = (Session["MLOC_TRIBAL_LAND"].ConvertOrDefault<Boolean>());
                chkFields.Items[3].Selected = (Session["MLOC_SOURCE_MAP_SCALE"].ConvertOrDefault<Boolean>());
                chkFields.Items[4].Selected = (Session["MLOC_HORIZ_COLL_METHOD"].ConvertOrDefault<Boolean>());
                chkFields.Items[5].Selected = (Session["MLOC_HORIZ_REF_DATUM"].ConvertOrDefault<Boolean>());
                chkFields.Items[6].Selected = (Session["MLOC_VERT_MEASURE"].ConvertOrDefault<Boolean>());
                chkFields.Items[7].Selected = (Session["MLOC_COUNTRY_CODE"].ConvertOrDefault<Boolean>());
                chkFields.Items[8].Selected = (Session["MLOC_STATE_CODE"].ConvertOrDefault<Boolean>());
                chkFields.Items[9].Selected = (Session["MLOC_COUNTY_CODE"].ConvertOrDefault<Boolean>());
                chkFields.Items[10].Selected = (Session["MLOC_WELL_TYPE"].ConvertOrDefault<Boolean>());
                chkFields.Items[11].Selected = (Session["MLOC_AQUIFER_NAME"].ConvertOrDefault<Boolean>());
                chkFields.Items[12].Selected = (Session["MLOC_FORMATION_TYPE"].ConvertOrDefault<Boolean>());
                chkFields.Items[13].Selected = (Session["MLOC_WELLHOLE_DEPTH"].ConvertOrDefault<Boolean>());


                if (HttpContext.Current.User.IsInRole("READONLY"))
                {
                    btnAdd.Enabled = false;
                    grdMonLoc.Columns[0].Visible = false;
                }
            }
        }

        protected void grdMonLoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int MonLocID = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Edits")
            {
                Session.Add("MonLocIDX", e.CommandArgument.ToString());
                Response.Redirect("~/App_Pages/Secure/WQXMonLocEdit.aspx");
            }

            if (e.CommandName == "Deletes")
            {
                int SuccID = db_WQX.DeleteT_WQX_MONLOC(MonLocID);

                if (SuccID > 0)
                    lblMsg.Text = "";
                else if (SuccID < 0)
                    lblMsg.Text = "Activities found for this monitoring location - location has been set to inactive.";
                else
                    lblMsg.Text = "Unable to delete monitoring location";

            }

            if (e.CommandName == "WQX")
            {
                Session.Add("MonLocIDX", e.CommandArgument.ToString());
                Session.Add("TableCD", "MLOC");
                Response.Redirect("~/App_Pages/Secure/WQX_Hist.aspx");
            }

        }

        public void FillGrid()
        {
            grdMonLoc.DataSource = db_WQX.GetWQX_MONLOC(!chkDeletedInd.Checked, Session["OrgID"].ToString(), false);
            grdMonLoc.DataBind();
        }

        protected void btnConfigSave_Click(object sender, EventArgs e)
        {
            Session["MLOC_HUC_EIGHT"] = chkFields.Items[0].Selected;
            Session["MLOC_HUC_TWELVE"] = chkFields.Items[1].Selected;
            Session["MLOC_TRIBAL_LAND"] = chkFields.Items[2].Selected;
            Session["MLOC_SOURCE_MAP_SCALE"] = chkFields.Items[3].Selected;
            Session["MLOC_HORIZ_COLL_METHOD"] = chkFields.Items[4].Selected;
            Session["MLOC_HORIZ_REF_DATUM"] = chkFields.Items[5].Selected;
            Session["MLOC_VERT_MEASURE"] = chkFields.Items[6].Selected;
            Session["MLOC_COUNTRY_CODE"] = chkFields.Items[7].Selected;
            Session["MLOC_STATE_CODE"] = chkFields.Items[8].Selected;
            Session["MLOC_COUNTY_CODE"] = chkFields.Items[9].Selected;
            Session["MLOC_WELL_TYPE"] = chkFields.Items[10].Selected;
            Session["MLOC_AQUIFER_NAME"] = chkFields.Items[11].Selected;
            Session["MLOC_FORMATION_TYPE"] = chkFields.Items[12].Selected;
            Session["MLOC_WELLHOLE_DEPTH"] = chkFields.Items[13].Selected;

            Response.Redirect(Page.Request.Url.ToString(), true); 

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
            {
                return "~/App_Images/0.png";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //disable if no pending WQX records
            try
            {
                Timer1.Enabled = db_WQX.GetT_WQX_MONLOC_PendingInd(Session["OrgID"].ToString());
            }
            catch
            {
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session.Add("MonLocIDX", -1);
            Response.Redirect("~/App_Pages/Secure/WQXMonLocEdit.aspx");
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("MonLocExport.xls", grdMonLoc);
        }

        protected void chkDeletedInd_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        } 

    }
}