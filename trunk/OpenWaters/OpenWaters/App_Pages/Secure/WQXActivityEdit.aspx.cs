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
    public partial class WQXActivityEdit : System.Web.UI.Page
    {

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            foreach (GridViewRow row in grdResults.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow &&
                    row.RowState.HasFlag(DataControlRowState.Edit) == false)
                {
                    // enable click on row to enter edit mode 
                    row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(grdResults, "Edit$" + row.DataItemIndex, true);
                }
            }
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ActivityIDX"] == null)
                Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");

            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkActivityList");
                if (hl != null) hl.CssClass = "divBody sel";

                if (HttpContext.Current.User.IsInRole("READONLY"))
                {
                    btnSave.Visible = false;
                    btnAdd.Enabled = false;
                    grdResults.Columns[8].Visible = false;
                }

                //populate drop-downs                
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ActivityType";
                Utils.BindList(ddlActivityType, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ActivityMedia";
                Utils.BindList(ddlActMedia, dsRefData, "VALUE", "VALUE");
                dsRefData.SelectParameters["tABLE"].DefaultValue = "ActivityMediaSubdivision";
                Utils.BindList(ddlActSubMedia, dsRefData, "VALUE", "VALUE");
                Utils.BindList(ddlMonLoc, dsMonLoc, "MONLOC_IDX", "MONLOC_ID");
                Utils.BindList(ddlProject, dsProject, "PROJECT_IDX", "PROJECT_ID");

                //populate activities
                int ActID = Session["ActivityIDX"].ConvertOrDefault<int>();
                T_WQX_ACTIVITY a = db_WQX.GetWQX_ACTIVITY_ByID(ActID);
                if (a != null)
                {
                    txtActivityID.Text = a.ACTIVITY_ID;
                    ddlActivityType.SelectedValue = a.ACT_TYPE;
                    ddlMonLoc.SelectedValue = a.MONLOC_IDX.ToString();
                    ddlProject.SelectedValue = a.PROJECT_IDX.ToString();
                    ddlActMedia.SelectedValue = a.ACT_MEDIA;
                    ddlActSubMedia.SelectedValue = a.ACT_SUBMEDIA;
                    txtStartDate.Text = a.ACT_START_DT.ToString("MM/dd/yyyy hh:mm tt");
                    if (a.ACT_END_DT != null)
                    {
                        DateTime d = (DateTime)a.ACT_END_DT;
                        txtEndDate.Text = d.ToString("MM/dd/yyyy hh:mm tt");
                    }

                    txtActComments.Text = a.ACT_COMMENT;
                    if (a.ACT_IND != null) chkActInd.Checked = (bool)a.ACT_IND;
                    if (a.WQX_IND != null) chkWQXInd.Checked = (bool)a.WQX_IND;

                }


                if (ddlActivityType.SelectedValue == "Field Msr/Obs-Habitat Assessment")
                {
                    grdResults.Columns[2].Visible = true;
                    grdResults.Columns[1].Visible = false;
                    pnlMetrics.Visible = true;
                }
                else
                {
                    grdResults.Columns[2].Visible = false;
                    grdResults.Columns[1].Visible = true;
                    pnlMetrics.Visible = false;
                }

                //populate results
                PopulateResultsGrid();

                //metrics
                //       dsMetric.Where = "it.ACTIVITY_IDX = " + ActID;

                txtActivityID.Focus();
            }
        }

        private void PopulateResultsGrid()
        {

            grdResults.DataSource = db_WQX.GetT_WQX_RESULT(Session["ActivityIDX"].ConvertOrDefault<int>());
            grdResults.DataBind();

            if (grdResults.Rows.Count == 0)
            {
                List<T_WQX_RESULT> rows = new List<T_WQX_RESULT>();
                T_WQX_RESULT row = new T_WQX_RESULT();
                row.ACTIVITY_IDX = Session["ActivityIDX"].ConvertOrDefault<int>();
                row.CHAR_NAME = "";
                row.DETECTION_LIMIT = "";
                row.RESULT_MSR = "";
                row.RESULT_MSR_UNIT = "";
                rows.Add(row);

                grdResults.DataSource = rows;
                grdResults.DataBind();


            }
        }

        protected void grdResults_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdResults.EditIndex = -1;
            PopulateResultsGrid(); 
        }

        protected void grdResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            db_WQX.DeleteT_WQX_RESULT(grdResults.DataKeys[e.RowIndex].Values[0].ConvertOrDefault<int>());
            PopulateResultsGrid();
        }

        protected void grdResults_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdResults.EditIndex = e.NewEditIndex;
            PopulateResultsGrid();
        }

        /// <summary>
        /// Update an existing result record.
        /// </summary>
        protected void grdResults_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMsgDtl.Text = "";

            int ActID = Session["ActivityIDX"].ConvertOrDefault<int>();

            DropDownList ddlChar = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlChar");
            DropDownList ddlTaxa = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlTaxa");
            TextBox txtResultVal = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtResultVal");
            DropDownList ddlUnit = (DropDownList)grdResults.Rows[e.RowIndex].FindControl("ddlUnit");

            TextBox txtDetectLimit = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtDetectLimit");
            TextBox txtComment = (TextBox)grdResults.Rows[e.RowIndex].FindControl("txtComment");

            //*********************VALIDATION****************************************
            double num;
            if (!(double.TryParse(txtResultVal.Text, out num) || txtResultVal.Text == "ND" || txtResultVal.Text =="NR" || txtResultVal.Text == "PAQL" || txtResultVal.Text == "PBQL" || txtResultVal.Text == "DNQ"))
            {
                lblMsgDtl.Text = "Result must be numeric, or one of the following values: ND, NR, PAQL, PBQL, DNQ";
                return;
            }

            //if numeric, then check if result is within valid range (if available for the char/unit pairing)
            if (double.TryParse(txtResultVal.Text, out num))
            {
                T_WQX_REF_CHAR_LIMITS cl = db_Ref.GetT_WQX_REF_CHAR_LIMITS_ByNameUnit(ddlChar.SelectedValue, ddlUnit.SelectedValue);
                if (cl != null)
                {
                    if ((decimal)num < cl.LOWER_BOUND || (decimal)num > cl.UPPER_BOUND)
                    {
                        lblMsgDtl.Text = "Result value is outside acceptable range. " + cl.LOWER_BOUND + " - " + cl.UPPER_BOUND + " Please provide another value and save again.";
                        return;
                    }
                }
            }
            //*********************END VALIDATION************************************

            db_WQX.InsertOrUpdateT_WQX_RESULT(grdResults.DataKeys[e.RowIndex].Values[0].ConvertOrDefault<int>(), ActID, ddlChar.SelectedValue, txtResultVal.Text, ddlUnit.SelectedValue, null, txtDetectLimit.Text, txtComment.Text, null, null, ddlTaxa.SelectedValue, null, User.Identity.Name);

            //also update activity to set to "U" so it will be flagged for submission to EPA
            db_WQX.InsertOrUpdateWQX_ACTIVITY(ActID, null, null, null, null, null, null, null, null, null, null, null, "U", null, null, User.Identity.Name);

            grdResults.EditIndex = -1;
            PopulateResultsGrid();

        }

        /// <summary>
        /// Add a new result.
        /// </summary>
        protected void grdResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMsgDtl.Text = "";
            if (e.CommandName.Equals("AddNew"))
            {
                int ActID = Session["ActivityIDX"].ConvertOrDefault<int>();
                DropDownList ddlNewChar = (DropDownList)grdResults.FooterRow.FindControl("ddlNewChar");
                DropDownList ddlNewTaxa = (DropDownList)grdResults.FooterRow.FindControl("ddlNewTaxa");
                TextBox txtNewResultVal = (TextBox)grdResults.FooterRow.FindControl("txtNewResultVal");
                DropDownList ddlNewUnit = (DropDownList)grdResults.FooterRow.FindControl("ddlNewUnit");

                TextBox txtNewDetectLimit = (TextBox)grdResults.FooterRow.FindControl("txtNewDetectLimit");
                TextBox txtNewComment = (TextBox)grdResults.FooterRow.FindControl("txtNewComment");

                double num;
                if (!(double.TryParse(txtNewResultVal.Text, out num) || txtNewResultVal.Text == "ND" || txtNewResultVal.Text == "NR" || txtNewResultVal.Text == "PAQL" || txtNewResultVal.Text == "PBQL" || txtNewResultVal.Text == "DNQ"))
                {
                    lblMsgDtl.Text = "Result must be numeric, or one of the following values: ND, NR, PAQL, PBQL, DNQ";
                    return;
                }

                //if numeric, then check if result is within valid range (if available for the char/unit pairing)
                if (double.TryParse(txtNewResultVal.Text, out num))
                {
                    T_WQX_REF_CHAR_LIMITS cl = db_Ref.GetT_WQX_REF_CHAR_LIMITS_ByNameUnit(ddlNewChar.SelectedValue, ddlNewUnit.SelectedValue);
                    if (cl != null)
                    {
                        if ((decimal)num < cl.LOWER_BOUND || (decimal)num > cl.UPPER_BOUND)
                        {
                            lblMsgDtl.Text = "Result value is outside acceptable range. Please provide another value and save again.";
                            return;
                        }
                    }
                }
                //*********************END VALIDATION************************************

                db_WQX.InsertOrUpdateT_WQX_RESULT(null, ActID, ddlNewChar.SelectedValue, txtNewResultVal.Text, ddlNewUnit.SelectedValue, null, txtNewDetectLimit.Text, txtNewComment.Text, null, null, ddlNewTaxa.SelectedValue, null, User.Identity.Name);

                //also update activity to set to "U" so it will be flagged for submission to EPA
                db_WQX.InsertOrUpdateWQX_ACTIVITY(ActID, null, null, null, null, null, null, null, null, null, null, null, "U", chkActInd.Checked, chkWQXInd.Checked);

                grdResults.FooterRow.Visible = false;
                PopulateResultsGrid();
            }

        }

        protected void grdResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Characteristic
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlChar"), dsChar, grdResults.DataKeys[e.Row.RowIndex].Values[1].ToString(), "CHAR_NAME", "CHAR_NAME");

                //Taxa
                int col = GetColumnIndexByName(grdResults,"Taxonomy");
                if (grdResults.Columns[col].Visible)
                {
                    dsRefData.SelectParameters["tABLE"].DefaultValue = "Taxon";
                    PopulateDropDown((DropDownList)e.Row.FindControl("ddlTaxa"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[1].ToString(), "VALUE", "TEXT");
                }

                //Unit
                dsRefData.SelectParameters["tABLE"].DefaultValue = "MeasureUnit";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlUnit"), dsRefData, grdResults.DataKeys[e.Row.RowIndex].Values[2].ToString(), "VALUE", "VALUE");

                //Anal Method
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlAnalMethod"), dsAnalMethod, grdResults.DataKeys[e.Row.RowIndex].Values[1].ToString(), "ANALYTIC_METHOD_IDX", "ANALYTIC_METHOD_ID");

                //set focus if this is the row being edited
                if (grdResults.EditIndex == e.Row.RowIndex)
                {
                    DropDownList d1 = (DropDownList)e.Row.FindControl("ddlChar");
                    d1.Focus(); 
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)

            {
                //Characteristic
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewChar"), dsChar, null, "CHAR_NAME", "CHAR_NAME");

                //Taxa
                dsRefData.SelectParameters["tABLE"].DefaultValue = "Taxon";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewTaxa"), dsRefData, null, "VALUE", "TEXT");

                //Unit
                dsRefData.SelectParameters["tABLE"].DefaultValue = "MeasureUnit";
                PopulateDropDown((DropDownList)e.Row.FindControl("ddlNewUnit"), dsRefData, null, "VALUE", "VALUE");

            }            

        }

        private static void PopulateDropDown (DropDownList ddl, ObjectDataSource ds, string selVal, string listVal, string listName)
        {
            if (ddl != null)
            {
                Utils.BindList(ddl, ds, listVal, listName);
                if (selVal != null) ddl.SelectedValue = selVal;
            }
        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("ResultsExport.xls", grdResults);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            grdResults.FooterRow.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? ActID = (Session["ActivityIDX"].ToString() == "-1" ? null : Session["ActivityIDX"].ConvertOrDefault<int?>());

            int SuccID = db_WQX.InsertOrUpdateWQX_ACTIVITY(ActID, Session["OrgID"].ToString(), ddlProject.SelectedValue.ConvertOrDefault<int>(), ddlMonLoc.SelectedValue.ConvertOrDefault<int>(), txtActivityID.Text, ddlActivityType.SelectedValue,
                ddlActMedia.SelectedValue, ddlActSubMedia.SelectedValue, txtStartDate.Text.ConvertOrDefault<DateTime?>(), txtEndDate.Text.ConvertOrDefault<DateTime?>(), "", txtActComments.Text, "U", chkActInd.Checked, chkWQXInd.Checked, User.Identity.Name);

            if (SuccID > 0)
                Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");
            else
                lblMsg.Text = "Error updating record.";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");
        }

        private int GetColumnIndexByName(GridView grid, string name)
        {
            foreach (DataControlField col in grid.Columns)
            {
                if (col.HeaderText.ToLower().Trim() == name.ToLower().Trim())
                {
                    return grid.Columns.IndexOf(col);
                }
            }

            return -1;
        }

        protected void ddlNewChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlNewChar = (DropDownList)sender;

            T_WQX_REF_CHARACTERISTIC c = db_Ref.GetT_WQX_REF_CHARACTERISTIC_ByName(ddlNewChar.SelectedValue);

            if (c != null)
            {
                if (c.DEFAULT_UNIT != null)
                {
                    DropDownList ddlNewUnit = (DropDownList)grdResults.FooterRow.FindControl("ddlNewUnit");
                    ddlNewUnit.SelectedValue = c.DEFAULT_UNIT;
                }
                if (c.DEFAULT_DETECT_LIMIT != null)
                {
                    TextBox txtNewDetect = (TextBox)grdResults.FooterRow.FindControl("txtNewDetectLimit");
                    txtNewDetect.Text = c.DEFAULT_DETECT_LIMIT.ToString();
                }
            }
        }

    }
}