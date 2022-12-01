using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.ViewModels
{
    public class DeleteModel : BaseEntity
    {
        [Required]
        public Guid? UserId { get; set; }
    }
}
