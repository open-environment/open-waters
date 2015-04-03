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
    public partial class SiteAuth : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserIDX"] == null)
                    Response.Redirect("~/Default.aspx");

                //populate drop-down lists
                Utils.BindList(ddlOrg, dsOrg, "ORG_ID", "ORG_FORMAL_NAME");

                if (Session["OrgID"]!=null)
                    ddlOrg.SelectedValue = Session["OrgID"].ToString();

                if (!HttpContext.Current.User.IsInRole("ADMINS"))
                    NavigationMenu.Items.RemoveAt(3);
            }
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrg.SelectedValue.Length > 0)
            {
                //update current session to set active Organization
                Session["OrgID"] = ddlOrg.SelectedValue;

                //update User record in database to set the most recent selected organization as their default organization
                db_Accounts.UpdateT_OE_USERSDefaultOrg(Session["UserIDX"].ConvertOrDefault<int>(), ddlOrg.SelectedValue);
                
                //refresh page because many children pages depend on this updated information
                Response.Redirect(Request.RawUrl);
            }
        }

    }
}