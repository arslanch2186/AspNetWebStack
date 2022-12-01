using SellPhone.Models.DbModels;
using SellPhone.Models.Dtos.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.Dtos.AdPosting
{
    public class AdPostingDto
    {
        public class AdPostingRequestDto
        {
            public Guid AspNetUserId { get; set; }
            public int CountryId { get; set; }
            public int CityId { get; set; }
            public int BrandId { get; set; }
            public int ModelId { get; set; }
            public int? Storage { get; set; }
            public int? BatteryHealth { get; set; }
            public string Condition { get; set; }
            public string Color { get; set; }
            public double? Price { get; set; }
            public string Description { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public bool IsApprovedByAdmin { get; set; }
            public bool IsRejectedByAdmin { get; set; }
            public string RejectionReason { get; set; }
            public int AddStatus { get; set; }
        }
        public class AdPostingUpdateDto : BaseEntity<Guid>
        {
            public Guid AspNetUserId { get; set; }
            public int? CountryId { get; set; }
            public int? CityId { get; set; }
            public int? BrandId { get; set; }
            public int? ModelId { get; set; }
            public int? Storage { get; set; }
            public int? BatteryHealth { get; set; }
            public string Condition { get; set; }
            public string Color { get; set; }
            public double? Price { get; set; }
            public string Description { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public bool? IsApprovedByAdmin { get; set; }
            public bool? IsRejectedByAdmin { get; set; }
            public string RejectionReason { get; set; }
            public int? AddStatus { get; set; }
        }

        public class AdPostingResponseDto : BaseEntity<Guid>
        {
            public Guid AspNetUserId { get; set; }
            public CoutryResponseDto Country { get; set; }
            public CityResponseDto City { get; set; }
            public BrandResponseDto Brand { get; set; }
            public ModelResponseDto Model { get; set; }
            public int? Storage { get; set; }
            public int? BatteryHealth { get; set; }
            public string Condition { get; set; }
            public string Color { get; set; }
            public double? Price { get; set; }
            public string Description { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public bool? IsApprovedByAdmin { get; set; }
            public bool? IsRejectedByAdmin { get; set; }
            public string RejectionReason { get; set; }
            public int? AddStatus { get; set; }
        }
    }
}
