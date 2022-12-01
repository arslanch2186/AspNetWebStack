using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class AdPostings : SoftDeletedEntity<Guid>
    {
        [ForeignKey("AspNetUserId")]
        public virtual AppUser AppUser { get; set; }
        [ForeignKey("CountryId")]
        public virtual LU_Countries Country { get; set; }
        [ForeignKey("CityId")]
        public LU_Cities City { get; set; }
        [ForeignKey("BrandId")]
        public virtual LU_Brands Brand { get; set; }
        [ForeignKey("ModelId")]
        public virtual LU_Models Model { get; set; }
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
}
