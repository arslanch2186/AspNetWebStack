using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class LU_Models : SoftDeletedEntity<int>
    {
        public LU_Models()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public string Model { get; set; }
        [ForeignKey("BrandId")]
        public virtual LU_Brands Brand { get; set; }
    }
}
