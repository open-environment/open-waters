using System;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class Terms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblHost.Text = db_Ref.GetT_OE_APP_SETTING("Hosting Org");
            lblPubSite.Text = db_Ref.GetT_OE_APP_SETTING("Public App Path");
            lblEmail.Text = db_Ref.GetT_OE_APP_SETTING("Email From");

        }
    }
}