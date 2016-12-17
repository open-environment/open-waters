using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenEnvironment
{
    public partial class SetOrg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                hdlURL.Value = Request.QueryString["ReturnUrl"];
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~" + hdlURL.Value, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}