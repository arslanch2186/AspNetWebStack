using SellPhone.Db.Repositories.AdPosting;
using SellPhone.Db.Repositories.AppPermissions;
using SellPhone.Db.Repositories.AppRoles;
using SellPhone.Db.Repositories.IUserProfileRepository;
using SellPhone.Db.Repositories.Lookup.LU_City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Data
{
    public interface IUnitOfWork
    {
        IAppRoleRepository RoleRepository { get; }
        IAppPermissionRepository PermissionRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        ILU_CityRepository LU_CityRepository { get; }
        ILU_CountryRepository LU_CountryRepository { get; }
        IAdPostingRepository AdPostingRepository { get; }
        Task SaveAsync();
    }
}
