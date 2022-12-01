using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CountryCode { get; set; }
        public int? OTP { get; set; }
        public DateTime? OTPCreatedAt { get; set; }
    }
}
