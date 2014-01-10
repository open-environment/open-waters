using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenEnvironment.App_Logic.DataAccessLayer;

namespace OpenEnvironment
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetFocus(this.LoginUser.FindControl("UserName"));
        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(((System.Web.UI.WebControls.Login)(sender)).UserName);
            if (u != null)
            {
                bool initFlg = u.INITAL_PWD_FLAG;
                if (initFlg)
                    LoginUser.DestinationPageUrl = "~/Account/ChangePassword.aspx?t=ini";
                else
                {
                    LoginUser.DestinationPageUrl = "~/App_Pages/Secure/Dashboard.aspx";
                    db_Accounts.UpdateT_OE_USERS(u.USER_IDX, null, null, null, null, null, null, null, null, System.DateTime.Now, null, null, "system");
                    
                    //set important session variables
                    Session["UserIDX"] = u.USER_IDX;
                    //Session["OrgID"] = db_Ref.GetT_OE_APP_SETTING("Default Org ID"); 
                    Session["OrgID"] = u.DEFAULT_ORG_ID; //added 1/6/2014
                    Session["MLOC_HUC_EIGHT"] = false;
                    Session["MLOC_HUC_TWELVE"] = false;
                    Session["MLOC_TRIBAL_LAND"] = false;
                    Session["MLOC_SOURCE_MAP_SCALE"] = false;
                    Session["MLOC_HORIZ_COLL_METHOD"] = true;
                    Session["MLOC_HORIZ_REF_DATUM"] = true;
                    Session["MLOC_VERT_MEASURE"] = false;
                    Session["MLOC_COUNTRY_CODE"] = true;
                    Session["MLOC_STATE_CODE"] = true;
                    Session["MLOC_COUNTY_CODE"] = true;
                    Session["MLOC_WELL_DATA"] = false;
                    Session["MLOC_WELL_TYPE"] = false;
                    Session["MLOC_AQUIFER_NAME"] = false;
                    Session["MLOC_FORMATION_TYPE"] = false;
                    Session["MLOC_WELLHOLE_DEPTH"] = false;

                    Session["PROJ_SAMP_DESIGN_TYPE_CD"] = false;
                    Session["PROJ_QAPP_APPROVAL"] = false;

                    Session["SAMP_ACT_END_DT"] = false;
                    Session["SAMP_COLL_METHOD"] = false;
                    Session["SAMP_COLL_EQUIP"] = false;
                    Session["SAMP_PREP"] = false;
                    Session["SAMP_DEPTH"] = false;

                }
            }
            else
                LoginUser.DestinationPageUrl = "~/App_Pages/Secure/Dashboard.aspx";
        }

        protected void LoginUser_LoginError(object sender, EventArgs e)
        {
            //There was a problem logging in the user 
        }



    }
}
