using System;
using System.Linq;
using OpenEnvironment.gov.epa.cdx;
using Ionic.Zip;
using System.IO;
using System.Xml.Linq;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.App_Logic.BusinessLogicLayer;


namespace OpenEnvironment
{
    public partial class RefData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
                
        protected void btnGetRefData_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            //******* ORGANIZATION LEVEL *********************
            int SuccID = GetAndStoreRefTable("Tribe", "Code", "Name", null);

            //if it fails on the first, it will likely fail for all - so exit code
            if (SuccID == 0)
                return;

            //******* PROJECT LEVEL *********************
            GetAndStoreRefTable("SamplingDesignType", "Code", "Code", null);

            //******* MON LOC LEVEL *********************
            GetAndStoreRefTable("County", "CountyFIPSCode", "CountyName", "County");
            GetAndStoreRefTable("Country", "Code", "Name", null);
            GetAndStoreRefTable("HorizontalCollectionMethod", "Name", "Description", null);
            GetAndStoreRefTable("HorizontalCoordinateReferenceSystemDatum", "Name", "Description", null);
            GetAndStoreRefTable("MonitoringLocationType", "Name", "Name", null);
            GetAndStoreRefTable("State", "Code", "Name", null);
            GetAndStoreRefTable("VerticalCollectionMethod", "Name", "Name", null);
            GetAndStoreRefTable("VerticalCoordinateReferenceSystemDatum", "Name", "Description", null);
            GetAndStoreRefTable("WellFormationType", "Name", "Name", null);
            GetAndStoreRefTable("WellType", "Name", "Name", null);
            
            //******* ACTIVITY/RESULTS LEVEL *************            
            GetAndStoreRefTable("ActivityMedia", "Name", "Name", null);
            GetAndStoreRefTable("ActivityMediaSubdivision", "Name", "Name", null);
            GetAndStoreRefTable("ActivityType", "Code", "Description", null);
            GetAndStoreRefTable("ActivityRelativeDepth", "Name", "Name", null);
            GetAndStoreRefTable("AnalyticalMethod", "ID", "Name", "AnalMethod");
            GetAndStoreRefTable("Characteristic", "Name", "Name", "Characteristic");
            GetAndStoreRefTable("MeasureUnit", "Code", "Description", null);
            GetAndStoreRefTable("NetType", "Name", "Name", null);
            GetAndStoreRefTable("ResultDetectionCondition", "Name", "Name", null);
            GetAndStoreRefTable("ResultLaboratoryComment", "Code", "Description", null);
            GetAndStoreRefTable("ResultMeasureQualifier", "Code", "Description", null);
            GetAndStoreRefTable("ResultSampleFraction", "Name", "Description", null);
            GetAndStoreRefTable("ResultStatus", "Name", "Description", null);
            GetAndStoreRefTable("ResultTemperatureBasis", "Name", "Description", null);
            GetAndStoreRefTable("ResultTimeBasis", "Name", "Description", null);
            GetAndStoreRefTable("ResultValueType", "Name", "Description", null);
            GetAndStoreRefTable("ResultWeightBasis", "Name", "Description", null);
            GetAndStoreRefTable("SampleCollectionEquipment", "Name", "Name", null);
            GetAndStoreRefTable("SampleContainerColor", "Name", "Description", null);
            GetAndStoreRefTable("SampleContainerType", "Name", "Description", null);
            GetAndStoreRefTable("SampleTissueAnatomy", "Name", "Name", null);
            GetAndStoreRefTable("Taxon", "Name", "Name", null);
            GetAndStoreRefTable("TimeZone", "Code", "Name", null);

        }

        protected void grdRef_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int RefID = e.CommandArgument.ToString().ConvertOrDefault<int>();

            if (e.CommandName == "Deletes")
            {
                db_Ref.UpdateT_WQX_REF_DATAByIDX(RefID, null, null, false);
            }

        }

        protected void ddlRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                grdRef.PageIndex = 0;

                grdRef.Visible = (ddlRef.SelectedValue != "Characteristic") && (ddlRef.SelectedValue != "AnalyticalMethod");
                grdChar.Visible = (ddlRef.SelectedValue == "Characteristic");
                grdAnalMethod.Visible = (ddlRef.SelectedValue == "AnalyticalMethod");
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected int GetAndStoreRefTable(string tableName, string ValueString, string TextString, string CustomParseName)
        {
            //get file
            DomainValuesService d = new DomainValuesService();

            try
            {
                byte[] b = d.GetDomainValues(tableName);
                //cleanup any previous files
                if (File.Exists(Server.MapPath("~/tmp/Results.xml")))
                    File.Delete(Server.MapPath("~/tmp/Results.xml"));

                using (System.IO.Stream stream = new System.IO.MemoryStream(b))
                {
                    using (var zip = ZipFile.Read(stream))
                    {
                        foreach (var entry in zip)
                        {
                            entry.Extract(Server.MapPath("~/tmp"));
                        }
                    }
                }

                XDocument xdoc = XDocument.Load(Server.MapPath("~/tmp/Results.xml"));

                // ***************** DEFAULT PARSING **************************************
                if (CustomParseName == null)
                {
                    var lv1s = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRow")
                                select new
                                {
                                    ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == ValueString).Attribute("value"),
                                    Text = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == TextString).Attribute("value"),
                                };

                    foreach (var lv1 in lv1s)
                    {
                        db_Ref.InsertOrUpdateT_WQX_REF_DATA(tableName, lv1.ID.Value, lv1.Text.Value, null);
                    }
                }


                // ***************** CUSTOM PARSING for CHARACTERSTIC **************************************
                if (CustomParseName == "Characteristic")
                {
                    var lv1s = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRow")
                               select new
                               {
                                   ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == ValueString).Attribute("value"),
                               };

                    foreach (var lv1 in lv1s)
                    {
                        db_Ref.InsertOrUpdateT_WQX_REF_CHARACTERISTIC(lv1.ID.Value, null, null, null, true);
                    }
                }

                // ***************** CUSTOM PARSING for ANALYTICAL METHOD **************************************
                if (CustomParseName == "AnalMethod")
                {
                    var lv1s = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRow")
                               select new
                               {
                                   ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == "ID").Attribute("value"),
                                   Name = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "Name").Attribute("value"),
                                   CTX = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(CTX2 => CTX2.Attribute("colname").Value == "ContextCode").Attribute("value"),
                                   Desc = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(Desc2 => Desc2.Attribute("colname").Value == "Description").Attribute("value"),
                               };

                    foreach (var lv1 in lv1s)
                    {
                        db_Ref.InsertOrUpdateT_WQX_REF_ANAL_METHOD(null, lv1.ID.Value, lv1.CTX.Value, lv1.Name.Value, lv1.Desc.Value, true);
                    }
                }

                // ***************** CUSTOM PARSING for COUNTY **************************************
                if (CustomParseName == "County")
                {
                    string StateVal = db_Ref.GetT_OE_APP_SETTING("Default State");
                    var lv1s = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRow")
                               select new
                               {
                                   ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == "CountyFIPSCode").Attribute("value"),
                                   Text = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "CountyName").Attribute("value"),
                                   State = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "StateCode").Attribute("value"),
                               };


                    foreach (var lv1 in lv1s)
                    {
                        if (lv1.State.Value == StateVal)
                            db_Ref.InsertOrUpdateT_WQX_REF_DATA(tableName, lv1.ID.Value, lv1.Text.Value, null);
                    }
                }
                
                
                return 1;
            }
            catch (Exception e)
            {
                lblMsg.Text = e.Message;
                return 0;
            }

        }

        protected void grdRef_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdRef.PageIndex = e.NewPageIndex;
        }

        protected void grdChar_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string charName = e.CommandArgument.ToString();

            if (e.CommandName == "Deletes")
            {
                db_Ref.InsertOrUpdateT_WQX_REF_CHARACTERISTIC(charName, null, null, null, false);
            }

        }

        protected void grdChar_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdChar.PageIndex = e.NewPageIndex;
        }

        protected void grdAnalMethod_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int AnalIDX = e.CommandArgument.ToString().ConvertOrDefault<int>();

            if (e.CommandName == "Deletes")
            {
                db_Ref.InsertOrUpdateT_WQX_REF_ANAL_METHOD(AnalIDX, null, null, null, null, false);
            }

        }

        protected void grdAnalMethod_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grdAnalMethod.PageIndex = e.NewPageIndex;
        }



    }
}