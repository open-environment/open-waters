using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.ComponentModel;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;


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
        public static bool SendEmail(string from, string to, string subj, string body)
        {
            try
            {
                //get system app settings
                if (from == null)
                    from = db_Ref.GetT_OE_APP_SETTING("EMAIL FROM");

                string mailServer = db_Ref.GetT_OE_APP_SETTING("EMAIL SERVER");

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new System.Net.Mail.MailAddress(from);
                message.To.Add(to);
                message.Subject = subj;
                message.Body = body;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);

                return true;
            }
            catch
            {
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


        public static void LogoutUser()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.Roles.DeleteCookie();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
        }


        public static string GetTimeZone(DateTime dt, string TimeZoneName)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneName);

            if (TimeZoneName == "Central Standard Time")
            {
                if (tz.IsDaylightSavingTime(dt))
                    return "CDT";
                else
                    return "CST";
            }

            if (TimeZoneName == "Eastern Standard Time")
            {
                if (tz.IsDaylightSavingTime(dt))
                    return "EDT";
                else
                    return "EST";
            }

            if (TimeZoneName == "Mountain Standard Time")
            {
                if (tz.IsDaylightSavingTime(dt))
                    return "MDT";
                else
                    return "MST";
            }

            if (TimeZoneName == "Pacific Standard Time")
            {
                if (tz.IsDaylightSavingTime(dt))
                    return "PDT";
                else
                    return "PST";
            }

            return "";
        }

        //***************** EXCEL EXPORT *****************************************
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




    }
}