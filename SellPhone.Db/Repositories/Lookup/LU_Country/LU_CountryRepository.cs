using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellPhone.Db.Data;
using SellPhone.Models.DbModels;

namespace SellPhone.Db.Repositories.Lookup.LU_City
{
    public class LU_CountryRepository : BaseRepository<LU_Countries, int>, ILU_CountryRepository
    {
        public LU_CountryRepository(SellPhoneContext context) : base(context)
        {
        }
    }
}
