using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SellPhone.Db.Data;
using SellPhone.Models.DbModels;

namespace SellPhone.Db.Repositories.IUserProfileRepository
{
    public class UserProfileRepository : BaseRepository<UserProfiles, Guid>, IUserProfileRepository
    {
        public UserProfileRepository(SellPhoneContext context) : base(context)
        {
        }

        public async Task<UserProfiles> GetUserProfileByAspNetUserId(Guid Id)
        {
            var UserProfile = _context.UserProfiles
                                      .Include(p => p.City)
                                      .Include(p => p.Country)
                                      .Where(x => x.AppUser.Id == Id).FirstOrDefault();
            return UserProfile;
        }
    }
}
