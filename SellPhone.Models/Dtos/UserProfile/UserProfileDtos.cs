using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.Dtos.UserProfile
{
    public class UserProfileRequestDto
    {
        //public Guid AspNetUserId { get; set; }
        [Required]
        public string FullName { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserProfileResponseDto : BaseEntityDto
    {
        //public Guid AspNetUserId { get; set; } 
        public string FullName { get; set; }
        public CoutryResponseDto Country { get; set; }
        public CityResponseDto City { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserProfileUpdateDto
    {
        //public Guid AspNetUserId { get; set; }
        public string FullName { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
    }
}
