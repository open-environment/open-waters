﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

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
            Session["OrgID"] = ddlOrg.SelectedValue;
            Response.Redirect(Request.RawUrl);
        }

        protected void imgHelp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/App_Docs/UsersGuide.docx");
        }

    }
}