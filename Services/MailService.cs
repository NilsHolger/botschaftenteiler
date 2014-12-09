using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MessageSharer.Services
{
    public class MailService : IMailService
    {
        public bool SendMail(string from, string to, string subject, string body)
        {
            try
            {
                //SendMailLiveClient(from, to, subject, body);
                SendMailHotmailClient(from, to, subject, body);
            }
            catch (Exception ex)
            {
                //add logging
                return false;
            }
            return true;
        }

        private bool SendMailHotmailClient(string from, string to, string subject, string body)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage(from, to, subject, body);
            mail.From = new MailAddress("");
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
            SmtpServer.EnableSsl = true;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //do logging
                return false;
            }
            return true;
        }

        private bool SendMailLiveClient(string from, string to, string subject, string body)
        {
              //live settings
             MailMessage mailMessage = new MailMessage(from, to, subject, body);
             mailMessage.From = new MailAddress("", "");
             mailMessage.To.Add("");
             SmtpClient smtpClient = new SmtpClient();
             smtpClient.Host = "";
             smtpClient.UseDefaultCredentials = false;
             smtpClient.Credentials = new System.Net.NetworkCredential("", "");
             smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //do logging
                return false;
            }
            return true;
        }
    }
}


 

				            