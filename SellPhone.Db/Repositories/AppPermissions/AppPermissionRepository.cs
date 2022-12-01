using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Repositories.AppPermissions
{
    public class AppPermissionRepository : BaseRepository<AppPermission, int>, IAppPermissionRepository
    {
        public AppPermissionRepository(SellPhoneContext context) : base(context)
        {
        }
    }
}
