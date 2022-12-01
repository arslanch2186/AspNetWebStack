using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SellPhone.Db.Data;
using SellPhone.Models.DbModels;

namespace SellPhone.Db.Repositories.Lookup.LU_City
{
    public class LU_CityRepository : BaseRepository<LU_Cities, int>, ILU_CityRepository
    {
        public LU_CityRepository(SellPhoneContext context) : base(context)
        {
        }

        //public List<LU_Cities> GetCities()
        //{
        //    var cities = _context.LU_Cities
        //                            .Where(x => x.Deleted == false)
        //                            .ToList();
        //    return cities;
        //}
    }
}
