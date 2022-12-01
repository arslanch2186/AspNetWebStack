using Microsoft.AspNetCore.Identity;
using SellPhone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class UserProfiles : SoftDeletedEntity<Guid>
    {
        [ForeignKey("AspNetUserId")]
        public virtual AppUser AppUser { get; set; }
        public string FullName { get; set; }
        [ForeignKey("CountryId")]
        public virtual LU_Countries Country { get; set; }
        [ForeignKey("CityId")]
        public LU_Cities City { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; } 
    }
}
