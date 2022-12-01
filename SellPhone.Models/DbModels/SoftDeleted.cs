using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class SoftDeletedEntity<T> : BaseEntity<T>
    {
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
