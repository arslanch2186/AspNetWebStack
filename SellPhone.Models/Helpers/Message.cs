using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace SellPhone.Models.Helpers
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string senderName { get; set; }

        public Message(IEnumerable<string> to, string subject, string content,string senderName)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(senderName, x)));
            Subject = subject;
            Content = content;
            senderName = senderName;
        }
    }
}
