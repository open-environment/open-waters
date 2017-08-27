using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenEnvironment
{
    public partial class License : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");

        }
    }
}