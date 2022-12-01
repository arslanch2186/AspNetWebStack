using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.ViewModels
{
    public class BaseEntity
    {
        [Required]
        public Guid? Id { get; set; }
    }
}
