using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class Help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (db_Ref.GetT_OE_APP_SETTING("Email From") != "")
                {
                    hlEmail.NavigateUrl = "mailto:" + db_Ref.GetT_OE_APP_SETTING("Email From");
                    hlEmail.Text = db_Ref.GetT_OE_APP_SETTING("Email From");
                }
            }
        }
    }
}