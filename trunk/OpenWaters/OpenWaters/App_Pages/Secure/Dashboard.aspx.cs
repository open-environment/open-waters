using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOrg.Text = db_WQX.GetWQX_ORGANIZATION().Count().ToString();

            if (Session["OrgID"] != null)
            {
                lblProject2.Text = db_WQX.GetWQX_PROJECT(true, Session["OrgID"].ToString(), false).Count.ToString();
                lblSamp.Text = db_WQX.GetWQX_ACTIVITY(true, Session["OrgID"].ToString(), null, null, null, null, false, null).Count().ToString();
                lblSampPend2.Text = db_WQX.GetWQX_ACTIVITY(true, Session["OrgID"].ToString(), null, null, null, null, true, null).Count().ToString();
                lblResult.Text = db_WQX.GetT_WQX_RESULTCount(Session["OrgID"].ToString()).ToString();
            }
            else
            {
                lblProject2.Text = "0";
                lblSamp.Text = "0";
                lblResult.Text = "0";
            }
        }
    }
}