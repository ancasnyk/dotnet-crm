using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Billing.Web.Smtp
{
    public class GMailer
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = DotEnv.Get("GMAIL_HOST", "smtp.gmail.com");
            // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailPort = DotEnv.GetInt("GMAIL_PORT", 25);
            GmailSSL = DotEnv.GetBool("GMAIL_SSL", true);
            GmailUsername = DotEnv.Get("GMAIL_USERNAME");
            GmailPassword = DotEnv.Get("GMAIL_PASSWORD");
        }

        public void Send()
        {
            if (string.IsNullOrWhiteSpace(GmailUsername) || string.IsNullOrWhiteSpace(GmailPassword))
            {
                throw new InvalidOperationException(
                    "SMTP credentials are not configured. Set GMAIL_USERNAME and GMAIL_PASSWORD in .env, " +
                    "as environment variables, or in Web.config appSettings.");
            }

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = GmailHost;
                smtp.Port = GmailPort;
                smtp.EnableSsl = GmailSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

                using (var message = new MailMessage(GmailUsername, ToEmail))
                {
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = IsHtml;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}