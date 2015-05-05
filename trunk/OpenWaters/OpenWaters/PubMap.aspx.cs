using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System.Web.Services;
using System.Web.Script.Services;

namespace OpenEnvironment.App_Pages.Public
{
    public partial class Map : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //populate drop-down lists
                Utils.BindList(ddlOrg, dsOrg, "ORG_ID", "ORG_FORMAL_NAME");
            }

            //if this installation only has one organization, display on map header
            if (db_WQX.GetWQX_ORGANIZATION().Count == 1)
                lblOrgName.Text = db_WQX.GetWQX_ORGANIZATION().FirstOrDefault().ORG_FORMAL_NAME + " - ";
            else
                lblOrgName.Text = "";
        }

        //Web method used for retrieving sites to place on map
        [WebMethod(EnableSession = true)]
        public static string[] GetSites()
        {
            List<string> myCollection = new List<string>();

            List<T_WQX_MONLOC> ms = db_WQX.GetWQX_MONLOC(true, null, false);

            if (ms != null)
            {
                foreach (T_WQX_MONLOC m in ms)
                {
                    string samps = "";
                    string comments = "";
                    V_WQX_ACTIVITY_LATEST l = db_WQX.GetV_WQX_ACTIVITY_LATESTByMonLocID(m.MONLOC_IDX);
                    if (l != null)
                    {
                        samps = "<br/><u><b>Most recent sampling results:</b></u><br/>Alkalinity: " + l.Alkalinity__total + "<br/>Ammonia: " + l.Ammonia + "<br/>E coli: " + l.Escherichia_coli +
                            "<br/>Nitrate: " + l.Nitrate +
                            "<br/>Nitrite: " + l.Nitrite +
                            "<br/>pH: " + l.pH +
                            "<br/>Phosphorus: " + l.Phosphorus +
                            "<br/>Salinity: " + l.Salinity +
                            "<br/>Water Temp: " + l.Temperature__water +
                            "<br/>Turbidity: " + l.Turbidity;

                        if (l.ACT_COMMENT != null)
                            comments = "<br/><i>" + l.ACT_COMMENT + "</i><br/>";
                    }

                    myCollection.Add(m.LATITUDE_MSR + "|" + 
                        m.LONGITUDE_MSR + "|" + 
                        m.MONLOC_NAME + " - " + m.MONLOC_TYPE + "|" + 
                        m.MONLOC_DESC + "<br/>" + comments + samps + "|" + 
                        m.MONLOC_ID + "|" + 
                        m.STATE_CODE + "|" + 
                        m.ORG_ID + "|" + 
                        "test2");
                }
            }

            return myCollection.ToArray(); ;
        }


        protected void grdMonLoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["filtMonLoc"] = e.CommandArgument.ToString();

            if (e.CommandName.Equals("Chart"))
            {
                Server.Transfer("~/App_Pages/Secure/Charting.aspx");
            }
            if (e.CommandName.Equals("Data"))
                Response.Redirect("~/App_Pages/Secure/WQXActivity.aspx");     

        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}