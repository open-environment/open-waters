using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SendGrid;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System.Net;
using System.Net.Mail;

namespace OpenEnvironment.App_Logic.BusinessLogicLayer
{
    public class SendGridHelper
    {

        /// <summary>
        ///  Sends out an email from the application. Returns true if successful.
        /// </summary>
        public static bool SendGridEmail(string from, List<string> to, List<string> cc, List<string> bcc, string subj, string body, string smtpUser, string smtpUserPwd)
        {
            try
            {
                //******************** CONSTRUCT EMAIL ********************************************
                // Create the email object first, then add the properties.
                var myMessage = new SendGridMessage();

                // Add message properties.
                myMessage.From = new MailAddress(from);
                myMessage.AddTo(to);
                if (cc != null)
                {
                    foreach (string cc1 in cc)
                        myMessage.AddCc(cc1);
                }
                if (bcc != null)
                {
                    foreach (string bcc1 in bcc)
                        myMessage.AddBcc(bcc1);
                }

                myMessage.Subject = subj;
                //myMessage.Html = "<p>" + body + "</p>";
                myMessage.Text = body;
                //*********************************************************************************


                //********************* SEND EMAIL ************************************************
                var credentials = new NetworkCredential(smtpUser, smtpUserPwd);
                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                transportWeb.Deliver(myMessage);

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


    }
}