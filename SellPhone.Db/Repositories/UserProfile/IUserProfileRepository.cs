using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellPhone.Models.DbModels;

namespace SellPhone.Db.Repositories.IUserProfileRepository
{
    public interface IUserProfileRepository : IBaseRepository<UserProfiles, Guid>
    {
        Task<UserProfiles> GetUserProfileByAspNetUserId(Guid Id);

    }
}
