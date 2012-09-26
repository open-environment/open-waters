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
    public partial class WQX_Hist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TableCD"] == null)
                Response.Redirect("~/App_Pages/Secure/Dashboard.aspx");


            dsWQXHistory.SelectParameters["TableCD"].DefaultValue = Session["TableCD"].ToString();

            if (Session["TableCD"].ToString() == "MLOC")
                dsWQXHistory.SelectParameters["TableIdx"].DefaultValue = Session["MonLocIDX"].ToString();

            if (Session["TableCD"].ToString() == "PROJ")
                dsWQXHistory.SelectParameters["TableIdx"].DefaultValue = Session["ProjectIDX"].ToString();

            if (Session["TableCD"].ToString() == "ACT")
                dsWQXHistory.SelectParameters["TableIdx"].DefaultValue = Session["ActivityIDX"].ToString();


        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GetFile")
            {
                T_WQX_TRANSACTION_LOG aa = db_Ref.GetWQX_TRANSACTION_LOG_ByLogID(e.CommandArgument.ConvertOrDefault<int>());

                if (aa.RESPONSE_FILE != null)
                {
                    lblMsg.Text = "";
                    Response.ContentType = "application/x-unknown";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + aa.RESPONSE_TXT + "\"");
                    Response.BinaryWrite(aa.RESPONSE_FILE);
                    Response.End();
                    Response.Close();
                }
                else
                {
                    if (aa.RESPONSE_TXT != null)
                        lblMsg.Text = aa.RESPONSE_TXT;
                    else
                        lblMsg.Text = "No validation details because submission succeeded.";           
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session["TableCD"].ToString() == "MLOC")
                Response.Redirect("~/App_Pages/Secure/WQXMonLoc.aspx");

            if (Session["TableCD"].ToString() == "PROJ")
                Response.Redirect("~/App_Pages/Secure/WQXProject.aspx");

            if (Session["TableCD"].ToString() == "ACT")
                Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");


        }

        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Utils.RenderGridToExcelFormat("WQXHistoryExport.xls", GridView1);
        }
    }
}