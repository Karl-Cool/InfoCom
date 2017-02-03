using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccess.Models;

namespace InfoCom.Services
{
    public static class MailService
    {
        private static string _sender = "oru.it.notis@outlook.com";
        private static string _password = "sand12345";
        public static async Task<bool> Mail(Email mail)
        {

            var success = false;
            var message = new MailMessage();
            message.To.Add(new MailAddress(mail.Recipient)); 
            message.From = new MailAddress(_sender);
            message.Subject = mail.Title;
            message.Body = string.Format(mail.Text);
            message.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _sender,
                        Password = _password
                    };

                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return success;
        }
    }
}