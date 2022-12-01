using SellPhone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class LU_Cities : SoftDeletedEntity<int>
    {
        public LU_Cities()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public string City { get; set; }
        public int? CountryId { get; set; }
        //[ForeignKey("CountryId")]
        //public virtual LU_Countries Country { get; set; }
    }
}
