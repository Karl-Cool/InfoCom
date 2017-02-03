using System;
using System.Net;
using System.Net.Mail;
using DataAccess.Models;

namespace InfoCom.Services
{
    public static class MailService
    {
        private static string _sender = "oru.it.notis@outlook.com";
        private static string _password = "sand12345";
        public static void Mail(Email mail)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(mail.Recipient));  // replace with valid value 
            message.From = new MailAddress(_sender); 
            message.Subject = mail.Title;
            message.Body = string.Format(mail.Text);
            message.IsBodyHtml = true;

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
                smtp.SendMailAsync(message);
            }
        }
    }
}