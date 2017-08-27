using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //display application version
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblVer.Text = version;

            //only show registration if allowed
            if (NavigationMenu.Items.Count > 0)
                if (db_Ref.GetT_OE_APP_SETTING("Allow Self Reg") != "Y")
                    NavigationMenu.Items.RemoveAt(0);
        }
    }
}
