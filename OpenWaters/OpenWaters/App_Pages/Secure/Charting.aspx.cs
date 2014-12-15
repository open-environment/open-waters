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
    public partial class Charting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //display left menu as selected
                ContentPlaceHolder cp = this.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                HyperLink hl = (HyperLink)cp.FindControl("lnkChart");
                if (hl != null) hl.CssClass = "leftMnuBody sel";

                //populate drop-downs                
                Utils.BindList(ddlMonLoc, dsMonLoc, "MONLOC_IDX", "MONLOC_NAME");
                Utils.BindList(ddlCharacteristic, dsChar, "CHAR_NAME", "CHAR_NAME");

            }
        }

        protected void btnChart_Click(object sender, EventArgs e)
        {
            DisplayChart();
        }

        protected void DisplayChart()
        {
            //SET CHART AS LINE OR POINT
            if (ddlLineType.SelectedValue == "POINT")
                Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Point;
            else if (ddlLineType.SelectedValue == "LINE")
                Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
            else
                Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;

            Chart1.Series[0].MarkerSize = 4;

            if (ddlChartType.SelectedValue == "SERIES")
            {
                dsChartTS.SelectParameters["startDt"].DefaultValue = txtStartDt.Text;
                dsChartTS.SelectParameters["endDt"].DefaultValue = txtEndDt.Text;
                Chart1.DataSource = dsChartTS;
                Chart1.DataBind();
                grdResults.DataSource = dsChartTS;
                grdResults.DataBind();
            }

            if (grdResults.Rows.Count > 0)
            {
                pnlResults.Visible = true;
                Chart1.ChartAreas[0].AxisY.Title = "Result";
            }

        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("ChartingDataExport.xls", grdResults);
        }

        protected void ddlLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayChart();
        }


    }
}