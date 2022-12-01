using SellPhone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class LU_Countries : SoftDeletedEntity<int>
    {
        public LU_Countries()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public string Country { get; set; } 
    }
}
