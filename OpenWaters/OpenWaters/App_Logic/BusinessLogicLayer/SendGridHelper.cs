using System;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Threading.Tasks;

namespace OpenEnvironment.App_Logic.BusinessLogicLayer
{
    public class SendGridHelper
    {

        /// <summary>
        /// Sends out an email using SendGrid. 
        /// Note: Updated to work with SendGrid version 9.8
        /// </summary>
        /// <returns>true if successful</returns>
        public static async Task<bool> SendEmailUsingSendGrid(string from, List<string> to, List<string> cc, List<string> bcc, string subj, string body, string apiKey)
        {
            try
            {
                var client = new SendGridClient(apiKey);

                //******************** CONSTRUCT EMAIL ********************************************               
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(from),
                    Subject = subj
                };

                msg.AddContent(MimeType.Html, body);

                foreach (string to1 in to ?? Enumerable.Empty<string>())
                    msg.AddTo(to1);

                foreach (string cc1 in cc ?? Enumerable.Empty<string>())
                    msg.AddCc(cc1);

                foreach (string bcc1 in bcc ?? Enumerable.Empty<string>())
                    msg.AddBcc(bcc1);


                // Disable click tracking. See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
                msg.TrackingSettings = new TrackingSettings
                {
                    ClickTracking = new ClickTracking { Enable = false }
                };


                //******************** SEND EMAIL ****************************************************
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

                //******************** RETURN RESPONSE ***********************************************
                if (response.StatusCode == HttpStatusCode.Accepted)
                    return true;
                else
                    return false;
                //************************************************************************************

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