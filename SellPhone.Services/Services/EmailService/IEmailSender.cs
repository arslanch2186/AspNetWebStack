using SellPhone.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.EmailService
{
    public interface IEmailSender
    {
        bool SendEmail(Message message);
    }
}
