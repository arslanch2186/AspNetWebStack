using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace SellPhone.Models.Helpers
{
    public class EmailHelper
    {
        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("send.testing.email@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link + " test data";

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("send.testing.email@gmail.com", "vhraumqmefxwmstt");
            client.Host = "smtp.gmail.com";
            client.Port = 465;
            client.UseDefaultCredentials = false;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
