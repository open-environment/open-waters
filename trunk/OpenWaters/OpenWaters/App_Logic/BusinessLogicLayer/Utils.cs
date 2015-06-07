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

namespace OpenEnvironment.App_Logic.BusinessLogicLayer
{
    internal static class Utils
    {
        internal static bool ValidateParameter(ref string param, int maxSize)
        {
            if (param == null)
                return false;

            if (param.Trim().Length < 1)
                return false;

            if (maxSize > 0 && param.Length > maxSize)
                return false;

            return true;
        }

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
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
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
        /// Converts byte array to string
        /// </summary>
        public static string ByteArrayToString(byte[] input)
        {
            UTF8Encoding enc = new UTF8Encoding();
            string str = enc.GetString(input);
            return str;
        }

        /// <summary>
        /// Converts a stream into a byte array.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StreamToByteArray(Stream str)
        {
            using (var memoryStream = new MemoryStream())
            {
                str.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// This method is to handle if element is missing
        /// </summary>
        public static string ElementValueNull(this XElement element)
        {
            if (element != null)
                return element.Value;

            return "";
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

        public static void LogoutUser()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.Roles.DeleteCookie();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
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


    }
}