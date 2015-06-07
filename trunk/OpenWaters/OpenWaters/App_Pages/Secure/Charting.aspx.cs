using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;


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
                //Utils.BindList(ddlCharacteristic2, dsChar, "CHAR_NAME", "CHAR_NAME");
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

            if (ddlChartType.SelectedValue == "SERIES")
            {
                Chart1.ChartAreas[0].AxisY.ScaleBreakStyle.StartFromZero = System.Web.UI.DataVisualization.Charting.StartFromZero.No;
                Chart1.DataSource = dsChartTS;
                Chart1.DataBind();
                Chart1.ChartAreas[0].AxisY.ScaleBreakStyle.StartFromZero = System.Web.UI.DataVisualization.Charting.StartFromZero.No;

                //second characteristic
                //System.Web.UI.DataVisualization.Charting.Series s1 = new System.Web.UI.DataVisualization.Charting.Series();
                //s1.Name = "Series2";
                //s1.ChartType = Chart1.Series[0].ChartType;
                //s1.XValueMember = Chart1.Series[0].XValueMember;
                //s1.XValueType = Chart1.Series[0].XValueType;
                //s1.MarkerSize = 3;
                //s1.MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Circle;
                //s1.YValueMembers = "RESULT_MSR";
                //Chart1.Series.Add(s1);

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