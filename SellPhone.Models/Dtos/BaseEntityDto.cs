using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.Dtos
{
    public class BaseEntityDto
    {
        [Required]
        public Guid? Id { get; set; }
    }
}
