using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.ComponentModel;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Xml.Linq;
using System.Linq;
using System.Data;

namespace OpenEnvironment.App_Logic.BusinessLogicLayer
{
    public class ConfigInfoType
    {
        public string _name { get; set; }
        public string _req { get; set; }
        public int? _length { get; set; }
        public string _datatype { get; set; }
        public string _fkey { get; set; }
        public string _addfield { get; set; }
    }

    internal static class Utils
    {
        internal static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                if (checkForNull)
                    return false;

                return true;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
                 (maxSize > 0 && param.Length > maxSize) ||
                 (checkForCommas && param.IndexOf(",") != -1))
            {
                return false;
            }

            return true;
        }

        internal static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, int minSize)
        {
            if (param == null)
            {
                if (checkForNull)
                    return false;

                return true;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
                 (maxSize > 0 && param.Length > maxSize) ||
                 (checkForCommas && param.IndexOf(",") != -1))
            {
                return false;
            }

            if (minSize > 0 && param.Length < minSize)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///  Sends out an email from the application. Returns true if successful.
        /// </summary>
        public static bool SendEmail(string from, List<string> to, List<string> cc, List<string> bcc, string subj, string body, string htmlBody)
        {
            try
            {
                //************* GET SMTP SERVER SETTINGS ****************************
                string mailServer = db_Ref.GetT_OE_APP_SETTING("Email Server");
                string Port = db_Ref.GetT_OE_APP_SETTING("Email Port");
                string smtpUser = db_Ref.GetT_OE_APP_SETTING("Email Secure User");
                string smtpUserPwd = db_Ref.GetT_OE_APP_SETTING("Email Secure Pwd");

                //*************SET MESSAGE SENDER*********************
                if (from == null)
                    from = db_Ref.GetT_OE_APP_SETTING("Email From");


                if (mailServer == "smtp.sendgrid.net")
                {
                    SendGridHelper.SendGridEmail(from, to, cc, bcc, subj, body, smtpUser, smtpUserPwd);
                    return true;
                }


                //*************SET MESSAGE RECIPIENTS*********************
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                if (to != null)
                {
                    foreach (string to1 in to)
                        message.To.Add(to1);
                }                

                if (cc != null)
                {
                    foreach (string cc1 in cc)
                        message.CC.Add(cc1);
                }
                if (bcc != null)
                {
                    foreach (string bcc1 in bcc)
                        message.Bcc.Add(bcc1);
                }

                message.From = new System.Net.Mail.MailAddress(from);
                message.Subject = subj;
                message.Body = body;

                //***************SET SMTP SERVER *************************

                if (string.IsNullOrEmpty(smtpUser) == false)  //smtp server requires authentication
                {
                    var smtp = new System.Net.Mail.SmtpClient(mailServer, Port.ConvertOrDefault<int>())
                    {
                        Credentials = new System.Net.NetworkCredential(smtpUser, smtpUserPwd),
                        EnableSsl = true
                    };
                    smtp.Send(message);
                }
                else
                {
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                    smtp.Send(message);
                }

                return true;

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    db_Ref.InsertT_OE_SYS_LOG("EMAIL ERR", ex.InnerException.ToString());
                else if (ex.Message != null)
                    db_Ref.InsertT_OE_SYS_LOG("EMAIL ERR", ex.Message.ToString());
                else
                    db_Ref.InsertT_OE_SYS_LOG("EMAIL ERR", "Unknown error");

                return false;
            }
        }

        /// <summary>
        ///  Binds a dropdownlist to a datasource and adds a blank item at the beginning.
        /// </summary>
        public static void BindList(DropDownList list, ObjectDataSource datasource, string valueName, string textName)
        {
            list.Items.Clear();
            list.Items.Insert(0, "");
            list.AppendDataBoundItems = true;
            list.DataValueField = valueName;
            list.DataTextField = textName;
            list.DataSource = datasource;
            list.DataBind();
        }

        /// <summary>
        ///  Generic data type converter 
        /// </summary>
        public static bool TryConvert<T>(object value, out T result)
        {
            result = default(T);
            if (value == null || value == DBNull.Value) return false;

            if (typeof(T) == value.GetType())
            {
                result = (T)value;
                return true;
            }

            string typeName = typeof(T).Name;

            try
            {
                if (typeName.IndexOf(typeof(System.Nullable).Name, StringComparison.Ordinal) > -1 ||
                    typeof(T).BaseType.Name.IndexOf(typeof(System.Enum).Name, StringComparison.Ordinal) > -1)
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)tc.ConvertFrom(value);
                }
                else
                    result = (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///  Converts to another datatype or returns default value
        /// </summary>
        public static T ConvertOrDefault<T>(this object value)
        {
            T result = default(T);
            TryConvert<T>(value, out result);
            return result;
        }

        /// <summary>
        ///  Better than built-in SubString by handling cases where string is too short
        /// </summary>
        public static string SubStringPlus(this string str, int index, int length)
        {
            if (index >= str.Length)
                return String.Empty;

            if (index + length > str.Length)
                return str.Substring(index);

            return str.Substring(index, length);
        }

        /// <summary>
        /// Safe tostring with null handling
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStringNullSafe(this object obj)
        {
            return obj != null ? obj.ToString() : String.Empty;
        }

        /// <summary>
        /// Converts string to byte array
        /// </summary>
        public static byte[] StrToByteArray(string str)
        {
            if (str != null)
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                return encoding.GetBytes(str);
            }
            else
                return null;
        }

        /// <summary>
        /// Checks whether the given Email-Parameter is a valid E-Mail address.
        /// </summary>
        /// <param name="email">Parameter-string that contains an E-Mail address.</param>
        /// <returns>True, when Parameter-string is not null and 
        /// contains a valid E-Mail address;
        /// otherwise false.</returns>
        public static bool IsEmail(string email)
        {
            string MatchEmailPattern = 
			@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            if (email != null) return System.Text.RegularExpressions.Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }

        /// <summary>
        /// Returns the WQX timezone code based on the supplied time zone and date
        /// </summary>
        /// <param name="dt">Sample Date</param>
        /// <param name="TimeZoneName"></param>
        /// <param name="TimeZoneStandardCode">WQX Standard Code</param>
        /// <param name="TimeZoneDaylightCode">WQX Daylight Savings Code</param>
        /// <returns></returns>
        public static string GetWQXTimeZoneByDate(DateTime dt)
        {
            try
            {
                string OrgID = (HttpContext.Current.Session["OrgID"] ?? "").ToString();

                //see if session has any timezone value
                if ((HttpContext.Current.Session[OrgID + "_TZ"] ?? "") == "")
                {
                    //no default time zone found in session, need to retrieve from database
                    string TimeZoneID = "";

                    T_WQX_ORGANIZATION org = db_WQX.GetWQX_ORGANIZATION_ByID(OrgID);
                    if (org != null)
                    {
                        if ((org.DEFAULT_TIMEZONE ?? "") != "")
                            TimeZoneID = org.DEFAULT_TIMEZONE;
                        else
                            TimeZoneID = db_Ref.GetT_OE_APP_SETTING("Default Timezone");
                    }

                    T_WQX_REF_DEFAULT_TIME_ZONE tz = db_Ref.GetT_WQX_REF_DEFAULT_TIME_ZONE_ByName(TimeZoneID);
                    if (tz != null)
                    {
                        HttpContext.Current.Session[OrgID + "_TZ"] = tz.OFFICIAL_TIME_ZONE_NAME;
                        HttpContext.Current.Session[OrgID + "_TZ_S"] = tz.WQX_CODE_STANDARD;
                        HttpContext.Current.Session[OrgID + "_TZ_D"] = tz.WQX_CODE_DAYLIGHT;
                    }
                }

                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(HttpContext.Current.Session[OrgID + "_TZ"].ToString());
                if (tzi.IsDaylightSavingTime(dt))
                    return HttpContext.Current.Session[OrgID + "_TZ_S"].ToString();
                else
                    return HttpContext.Current.Session[OrgID + "_TZ_D"].ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Returns the lookup value from a dictionary if found, otherwise returns default value based on datatype
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static V GetValueOrDefault<K, V>(this Dictionary<K, V> dict, K key)
        {
            V ret;
            dict.TryGetValue(key, out ret);
            return ret;
        }

        //***************** EXCEL EXPORT *****************************************
        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="gv"></param>
        public static void RenderGridToExcelFormat(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
//            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid 
                    Table table = new Table();

                    //  add the header row to the table 
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table 
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table 
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter 
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response 
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }

        }

        /// <summary> 
        /// Replace any of the contained controls with literals 
        /// </summary> 
        /// <param name="control"></param> 
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is Image)
                {
                    control.Controls.Remove(current);
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }


        /// <summary>
        /// Returns the internal ID of the authenticated user. If using membership, returns membership user. If using external ID provider, returns IPrincipal USERIDX claim
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static int GetUserIDX(System.Security.Principal.IPrincipal User)
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["UseIdentityServer"] == "true")
                {
                    var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
                    IEnumerable<System.Security.Claims.Claim> claims2 = identity.Claims;
                    var UserIDXLoc = (from p in claims2 where p.Type == "UserIDX" select p.Value).FirstOrDefault();
                    return UserIDXLoc.ConvertOrDefault<int>();
                }
                else
                    return (int)System.Web.Security.Membership.GetUser().ProviderUserKey;
            }
            catch
            {
                //if fails, we don't care why, but need to return 0 to indicate not authenticated
                return 0;
            }
        }



        public static void PostLoginUser(string UserID)
        {
            T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(UserID);
            if (u != null)
            {
                //if user only belongs to 1 org, update the default org id
                if (u.DEFAULT_ORG_ID == null)
                {
                    List<T_WQX_ORGANIZATION> os = db_WQX.GetWQX_USER_ORGS_ByUserIDX(u.USER_IDX, false);
                    if (os.Count == 1)
                    {
                        db_Accounts.UpdateT_OE_USERSDefaultOrg(u.USER_IDX, os[0].ORG_ID);
                        HttpContext.Current.Session["OrgID"] = os[0].ORG_ID; //added 1/6/2014
                    }
                }

                if (u.INITAL_PWD_FLAG == false)
                {
                    db_Accounts.UpdateT_OE_USERS(u.USER_IDX, null, null, null, null, null, null, null, null, System.DateTime.Now, null, null, "system");

                    //set important session variables
                    HttpContext.Current.Session["UserIDX"] = u.USER_IDX;
                    HttpContext.Current.Session["OrgID"] = u.DEFAULT_ORG_ID; //added 1/6/2014
                    HttpContext.Current.Session["MLOC_HUC_EIGHT"] = false;
                    HttpContext.Current.Session["MLOC_HUC_TWELVE"] = false;
                    HttpContext.Current.Session["MLOC_TRIBAL_LAND"] = false;
                    HttpContext.Current.Session["MLOC_SOURCE_MAP_SCALE"] = false;
                    HttpContext.Current.Session["MLOC_HORIZ_COLL_METHOD"] = true;
                    HttpContext.Current.Session["MLOC_HORIZ_REF_DATUM"] = true;
                    HttpContext.Current.Session["MLOC_VERT_MEASURE"] = false;
                    HttpContext.Current.Session["MLOC_COUNTRY_CODE"] = true;
                    HttpContext.Current.Session["MLOC_STATE_CODE"] = true;
                    HttpContext.Current.Session["MLOC_COUNTY_CODE"] = true;
                    HttpContext.Current.Session["MLOC_WELL_DATA"] = false;
                    HttpContext.Current.Session["MLOC_WELL_TYPE"] = false;
                    HttpContext.Current.Session["MLOC_AQUIFER_NAME"] = false;
                    HttpContext.Current.Session["MLOC_FORMATION_TYPE"] = false;
                    HttpContext.Current.Session["MLOC_WELLHOLE_DEPTH"] = false;
                    HttpContext.Current.Session["PROJ_SAMP_DESIGN_TYPE_CD"] = false;
                    HttpContext.Current.Session["PROJ_QAPP_APPROVAL"] = false;
                    HttpContext.Current.Session["SAMP_ACT_END_DT"] = false;
                    HttpContext.Current.Session["SAMP_COLL_METHOD"] = false;
                    HttpContext.Current.Session["SAMP_COLL_EQUIP"] = false;
                    HttpContext.Current.Session["SAMP_PREP"] = false;
                    HttpContext.Current.Session["SAMP_DEPTH"] = false;
                }
            }
        }

        //******************** GRID EXTENSION **************************************
        public static GridView RemoveEmptyColumns(this GridView gv, bool setTableWidth, string exclHeaderText)
        {
            int visColCount = 0;
            
            // Make sure there are at least header row
            if (gv.HeaderRow != null)
            {
                int columnIndex = 0;

                // For each column
                foreach (DataControlFieldCell clm in gv.HeaderRow.Cells)
                {
                    //skip column if specified to exclude checking this one
                    if (clm.ContainingField.HeaderText == exclHeaderText)
                    {
                        columnIndex++;
                        continue;
                    }

                    bool notAvailable = true;

                    // For each row
                    foreach (GridViewRow row in gv.Rows)
                    {
                        string columnData = row.Cells[columnIndex].Text;
                        if (!(string.IsNullOrEmpty(columnData) || columnData == "&nbsp;"))
                        {
                            notAvailable = false;
                            visColCount++;
                            break;
                        }
                    }

                    if (notAvailable)
                    {
                        // Hide the target header cell
                        gv.HeaderRow.Cells[columnIndex].Visible = false;

                        // Hide the target cell of each row
                        foreach (GridViewRow row in gv.Rows)
                            row.Cells[columnIndex].Visible = false;
                    }

                    columnIndex++;
                }
            }

            if (setTableWidth)
                gv.Width = visColCount <= 14 ? Unit.Percentage(100) : Unit.Percentage(100 + (visColCount-14) * 5);

            return gv;
        }


        //******************* XML IMPORT CONFIG FILE HANDLING**********************************
        public static Dictionary<string, int> GetColumnMapping(string ImportType, string[] headerCols)
        {
            // Loading configuration file listing all data import columns
            var xml = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Docs/ImportColumnsConfig.xml"));

            // Query list of all columns for the type
            var allFields = (from c in xml.Root.Descendants("Alias")
                          .Where(i => i.Parent.Attribute("Level").Value == ImportType)
                          select new
                          {
                              Name = c.Parent.Attribute("FieldName").Value,
                              Alias = c.Value.ToUpper()
                          }).ToList();

            //list of fields supplied by user
            var headerColList = headerCols.Select((value, index) => new { value, index }).ToList();

            //return matches with index
            var colMapping = (from f in allFields
                              join h in headerColList
                              on f.Alias.Trim() equals h.value.ToUpper().Trim()
                              select new
                              {
                                  _Name = f.Name.Trim(),
                                  _Col = h.index
                              }).ToDictionary(x => x._Name, x => x._Col.ConvertOrDefault<int>());

            return colMapping;
        }



        //******************* DATA IMPORT HELPERS ********************************************
        public static List<ConfigInfoType> GetAllColumnInfo(string ImportType)
        {
            // Loading configuration file listing all data import columns
            var xml = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Docs/ImportColumnsConfig.xml"));

            // Query list of all columns for the type
            return (from c in xml.Root.Descendants("Alias")
                    .Where(i => i.Parent.Attribute("Level").Value == ImportType)
                    select new ConfigInfoType
                    {
                        _name = c.Parent.Attribute("FieldName").Value,
                        _req = c.Parent.Attribute("ReqInd").Value,
                        _length = c.Parent.Attribute("Length").Value.ConvertOrDefault<int?>(),
                        _datatype = c.Parent.Attribute("DataType").Value,
                        _fkey = c.Parent.Attribute("FKey").Value,
                        _addfield = c.Parent.Attribute("AddField") != null ? c.Parent.Attribute("AddField").Value : ""
                    }).ToList();
        }


        public static List<string> GetAllColumnBasic(string ImportType)
        {
            // Loading configuration file listing all data import columns
            var xml = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Docs/ImportColumnsConfig.xml"));

            return (from c in xml.Root.Descendants("Field")
                    .Where(i => i.Attribute("Level").Value == ImportType)
                    select c.Attribute("FieldName").Value
                    ).ToList();
        }

    }

}