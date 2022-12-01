using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Repositories.AdPosting
{
    public class AdPostingRepository : BaseRepository<AdPostings, Guid>, IAdPostingRepository
    {
        public AdPostingRepository(SellPhoneContext context) : base(context)
        {
        }
    }
}
