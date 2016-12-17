using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using Ionic.Zip;
using System.IO;
using System.Net;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace OpenEnvironment
{
    public partial class WQX_Hist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TableCD"] == null)
                Response.Redirect("~/App_Pages/Secure/Dashboard.aspx");

            if (!IsPostBack)
            {
                int RecID = 0;

                if (Session["TableCD"].ToString() == "MLOC")
                    RecID = Session["MonLocIDX"].ConvertOrDefault<int>();
                else if (Session["TableCD"].ToString() == "PROJ")
                    RecID = Session["ProjectIDX"].ConvertOrDefault<int>();
                else if (Session["TableCD"].ToString() == "ACT")
                    RecID = Session["ActivityIDX"].ConvertOrDefault<int>();

                GridView1.DataSource = db_Ref.GetWQX_TRANSACTION_LOG(Session["TableCD"].ToString(), RecID);
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GetFile")
            {
                T_WQX_TRANSACTION_LOG aa = db_Ref.GetWQX_TRANSACTION_LOG_ByLogID(e.CommandArgument.ConvertOrDefault<int>());

                if (aa.RESPONSE_FILE != null)
                {
                    lblMsg.Text = "";

                    //attempt to unzip ProcessingReport and just display html report instead of ZIP
                    if (aa.RESPONSE_TXT == "ProcessingReport.zip")
                    {
                        try
                        {

                            if (File.Exists(Server.MapPath("~/tmp/ProcessingReport.xml")))
                                File.Delete(Server.MapPath("~/tmp/ProcessingReport.xml"));
                            if (File.Exists(Server.MapPath("~/tmp/result.html")))
                                File.Delete(Server.MapPath("~/tmp/result.html"));

                            using (System.IO.Stream stream = new System.IO.MemoryStream(aa.RESPONSE_FILE))
                            {
                                using (var zip = ZipFile.Read(stream))
                                {
                                    ZipEntry ent = zip["ProcessingReport.xml"];
                                    ent.Extract(Server.MapPath("~/tmp"));
                                }
                            }

                            XPathDocument myXPathDoc = new XPathDocument(Server.MapPath("~/tmp/ProcessingReport.xml"));
                            XslCompiledTransform myXslTrans = new XslCompiledTransform();
                            myXslTrans.Load(Server.MapPath("~/App_Docs/validation.xsl"));
                            using (XmlTextWriter myWriter = new XmlTextWriter(Server.MapPath("~/tmp/result.html"), null))
                            {
                                myXslTrans.Transform(myXPathDoc, null, myWriter);
                            }

                            Response.ContentType = "text/html";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + "result.html" + "\"");
                            Response.TransmitFile(Server.MapPath("~/tmp/result.html"));
                            Response.End();
                            Response.Close();
                        }
                        catch { }
                    }
                    //end attempt

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