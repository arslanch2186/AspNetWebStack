using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.Dtos.UserProfile
{
    public class CityRequestDto
    {
        [Required]
        public string City { get; set; }
    }

    public class CityResponseDto /*: SoftDeletedEntity<int>*/
    {
        public int Id { get; set; }
        public string City { get; set; }
    }

    public class CityUpdateDto
    {
        public int Id { get; set; }
        public string City { get; set; }

    }

    // Move these to respective classes after creation and CRUD of these tables
    public class CoutryResponseDto
    {
        public int Id { get; set; }
        public string Country { get; set; }

    }

    public class BrandResponseDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }

    }
    public class ModelResponseDto
    {
        public int Id { get; set; }
        public string Model { get; set; }

    }
}
