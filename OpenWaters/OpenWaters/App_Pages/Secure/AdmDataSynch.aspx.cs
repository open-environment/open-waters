using System;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Ionic.Zip;
using OpenEnvironment.App_Logic.DataAccessLayer;
using OpenEnvironment.gov.epa.cdx;

namespace OpenEnvironment
{
    public partial class AdmDataSynch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // forms-based authorization
                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    Response.Redirect("~/App_Pages/Public/AccessDenied.aspx");
            }

            DisplayDates();

        }

        private void DisplayDates()
        {
            DateTime? maxRefDate = db_Ref.GetT_WQX_REF_DATA_LastUpdate();
            lblLastRefDate.Text = maxRefDate == null ? "Data has not yet been retrieved from EPA" : maxRefDate.ToString();

            DateTime? maxOrgDate = db_WQX.GetT_EPA_ORGS_LastUpdateDate();
            lblLastOrgDate.Text = maxOrgDate == null ? "Data has not yet been retrieved from EPA" : maxOrgDate.ToString();
        }

        protected void btnPullOrgs_Click(object sender, EventArgs e)
        {
            WQXImport_Org();
            DisplayDates();
        }

        private bool WQXImport_Org()
        {
            try
            {
                //*******************************************
                //grab latest Organizations from WQX Portal
                //*******************************************
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;
                WebRequest request = WebRequest.Create("https://www.waterqualitydata.us/Codes/Organization?mimeType=xml");





                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                //read the response as XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(responseFromServer);
                XmlNodeList codes = doc.SelectNodes("Codes")[0].SelectNodes("Code");

                //if we got this far without error, then it's time to delete the previous organizations
                int SuccID = db_WQX.DeleteT_EPA_ORGS();
                if (SuccID > 0)
                {
                    //iterate through each organization to add to the table
                    foreach (XmlNode code in codes)
                    {
                        string orgID = code.Attributes[0].Value;
                        string orgName = code.Attributes[1].Value;
                        db_WQX.InsertOrUpdateT_EPA_ORGS(orgID, orgName);
                    }

                    lblMsg.Text = "Import successful";
                    return true;
                }
                else
                {
                    lblMsg.Text = "Import failed to remove orgs.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Import failed.";
                return false;
            }
        }

        protected void btnRefData_Click(object sender, EventArgs e)
        {
            int SuccID = 1;

            //******* ORGANIZATION LEVEL *********************
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "Tribe")
                SuccID = GetAndStoreRefTable("Tribe", "Code", "Name", null);

            //if it fails on the first, it will likely fail for all - so exit code
            if (SuccID == 0)
            {
                lblMsg.Text = "Data retrieval failed";
                return;
            }

            lblMsg.Text = "";

            //******* PROJECT LEVEL *********************
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "SamplingDesignType")
                GetAndStoreRefTable("SamplingDesignType", "Code", "Code", null);


            //******* MON LOC LEVEL *********************
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "County")
                GetAndStoreRefTable("County", "CountyFIPSCode", "CountyName", "County");
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "Country")
                GetAndStoreRefTable("Country", "Code", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "HorizontalCollectionMethod")
                GetAndStoreRefTable("HorizontalCollectionMethod", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "HorizontalCoordinateReferenceSystemDatum")
                GetAndStoreRefTable("HorizontalCoordinateReferenceSystemDatum", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "MonitoringLocationType")
                GetAndStoreRefTable("MonitoringLocationType", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "State")
                GetAndStoreRefTable("State", "Code", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "VerticalCollectionMethod")
                GetAndStoreRefTable("VerticalCollectionMethod", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "VerticalCoordinateReferenceSystemDatum")
                GetAndStoreRefTable("VerticalCoordinateReferenceSystemDatum", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "WellFormationType")
                GetAndStoreRefTable("WellFormationType", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "WellType")
                GetAndStoreRefTable("WellType", "Name", "Name", null);

            //******* ACTIVITY/RESULTS LEVEL *************            
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ActivityMedia")
                GetAndStoreRefTable("ActivityMedia", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ActivityMediaSubdivision")
                GetAndStoreRefTable("ActivityMediaSubdivision", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ActivityType")
                GetAndStoreRefTable("ActivityType", "Code", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ActivityRelativeDepth")
                GetAndStoreRefTable("ActivityRelativeDepth", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "AnalyticalMethod")
                GetAndStoreRefTable("AnalyticalMethod", "ID", "Name", "AnalMethod");
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "Assemblage")
                GetAndStoreRefTable("Assemblage", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "BiologicalIntent")
                GetAndStoreRefTable("BiologicalIntent", "Name", "Name", null);          
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "CellForm")
                GetAndStoreRefTable("CellForm", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "CellShape")
                GetAndStoreRefTable("CellShape", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "Characteristic")
                GetAndStoreRefTable("Characteristic", "Name", "Name", "Characteristic");
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "DetectionQuantitationLimitType")
                GetAndStoreRefTable("DetectionQuantitationLimitType", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "FrequencyClassDescriptor")
                GetAndStoreRefTable("FrequencyClassDescriptor", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "Habit")
                GetAndStoreRefTable("Habit", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "MeasureUnit")
                GetAndStoreRefTable("MeasureUnit", "Code", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "MethodSpeciation")
                GetAndStoreRefTable("MethodSpeciation", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "MetricType")
                GetAndStoreRefTable("MetricType", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "NetType")
                GetAndStoreRefTable("NetType", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultDetectionCondition")
                GetAndStoreRefTable("ResultDetectionCondition", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultLaboratoryComment")
                GetAndStoreRefTable("ResultLaboratoryComment", "Code", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultMeasureQualifier")
                GetAndStoreRefTable("ResultMeasureQualifier", "Code", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultSampleFraction")
                GetAndStoreRefTable("ResultSampleFraction", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultStatus")
                GetAndStoreRefTable("ResultStatus", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultTemperatureBasis")
                GetAndStoreRefTable("ResultTemperatureBasis", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultTimeBasis")
                GetAndStoreRefTable("ResultTimeBasis", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultValueType")
                GetAndStoreRefTable("ResultValueType", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ResultWeightBasis")
                GetAndStoreRefTable("ResultWeightBasis", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "SampleCollectionEquipment")
                GetAndStoreRefTable("SampleCollectionEquipment", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "SampleContainerColor")
                GetAndStoreRefTable("SampleContainerColor", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "SampleContainerType")
                GetAndStoreRefTable("SampleContainerType", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "SampleTissueAnatomy")
                GetAndStoreRefTable("SampleTissueAnatomy", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "StatisticalBase")
                GetAndStoreRefTable("StatisticalBase", "Code", "Code", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "Taxon")
                GetAndStoreRefTable("Taxon", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ThermalPreservativeUsed")
                GetAndStoreRefTable("ThermalPreservativeUsed", "Name", "Description", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "TimeZone")
                GetAndStoreRefTable("TimeZone", "Code", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "ToxicityTestType")
                GetAndStoreRefTable("ToxicityTestType", "Name", "Name", null);
            if (ddlRefTable.SelectedValue == "ALL" || ddlRefTable.SelectedValue == "Voltinism")
                GetAndStoreRefTable("Voltinism", "Name", "Description", null);

            DisplayDates();

            if (lblMsg.Text == "")
                lblMsg.Text = "Data Retrieval Complete.";
        }

        protected int GetAndStoreRefTable(string tableName, string ValueString, string TextString, string CustomParseName)
        {
            try
            {
                //get file
                DomainValuesService d = new DomainValuesService();
                d.Url = db_Ref.GetT_OE_APP_SETTING("CDX Ref Data URL");

                XDocument xdoc = null;

                byte[] b = d.GetDomainValues(tableName);

                using (System.IO.Stream stream = new System.IO.MemoryStream(b))
                {
                    using (var zip = ZipFile.Read(stream))
                    {
                        foreach (var entry in zip)
                        {
                            //cleanup any previous files
                            if (File.Exists(Server.MapPath("~/tmp/" + entry.FileName)))
                                File.Delete(Server.MapPath("~/tmp/" + entry.FileName));

                            entry.Extract(Server.MapPath("~/tmp"));

                            xdoc = XDocument.Load(Server.MapPath("~/tmp/" + entry.FileName));
                        }
                    }
                }


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

                    var lv1sALT = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRow")
                               select new
                               {
                                   ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == ValueString).Attribute("value"),
                                   Text = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == TextString).Attribute("value"),
                               };

                    foreach (var lv1 in lv1sALT)
                    {
                        db_Ref.InsertOrUpdateT_WQX_REF_DATA(tableName, lv1.ID.Value, lv1.Text.Value, null);
                    }
                }

                // ***************** CUSTOM PARSING for CHARACTERSTIC **************************************
                else if (CustomParseName == "Characteristic")
                {
                    var lv1s = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRow")
                               select new
                               {
                                   ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == ValueString).Attribute("value"),
                                   SampFracReq = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "SampleFractionRequired").Attribute("value"),
                                   MethodSpeciationRequired = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "MethodSpeciationRequired").Attribute("value"),
                                   PickList = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "PickList").Attribute("value")
                               };

                    foreach (var lv1 in lv1s)
                        db_Ref.InsertOrUpdateT_WQX_REF_CHARACTERISTIC(lv1.ID.Value, null, null, null, true, lv1.SampFracReq.Value, lv1.PickList.Value, lv1.MethodSpeciationRequired.Value);

                }

                // ***************** CUSTOM PARSING for ANALYTICAL METHOD **************************************
                else if (CustomParseName == "AnalMethod")
                {
                    var lv1s = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRow")
                               select new
                               {
                                   ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == "ID").Attribute("value"),
                                   Name = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "Name").Attribute("value"),
                                   CTX = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(CTX2 => CTX2.Attribute("colname").Value == "ContextCode").Attribute("value"),
                                   Desc = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/3}WQXElementRowColumn").First(Desc2 => Desc2.Attribute("colname").Value == "Description").Attribute("value"),
                               };

                    foreach (var lv1 in lv1s)
                    {
                        db_Ref.InsertOrUpdateT_WQX_REF_ANAL_METHOD(null, lv1.ID.Value, lv1.CTX.Value, lv1.Name.Value, lv1.Desc.Value, true);
                    }
                }

                // ***************** CUSTOM PARSING for COUNTY **************************************
                else if (CustomParseName == "County")
                {
                    var lv1s = from lv1 in xdoc.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRow")
                               select new
                               {
                                   ID = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(ID2 => ID2.Attribute("colname").Value == "CountyFIPSCode").Attribute("value"),
                                   Text = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "CountyName").Attribute("value"),
                                   State = lv1.Descendants("{http://www.exchangenetwork.net/schema/wqx/2}WQXElementRowColumn").First(Text2 => Text2.Attribute("colname").Value == "StateCode").Attribute("value"),
                               };


                    foreach (var lv1 in lv1s)
                    {
                        db_Ref.InsertOrUpdateT_WQX_REF_COUNTY(lv1.State.Value, lv1.ID.Value, lv1.Text.Value, null);
                    }
                }

                return 1;
            }
            catch (Exception e)
            {
                lblMsg.Text = "Error" + e.Message;
                return 0;
            }
        }
    }
}