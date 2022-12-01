using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Repositories.AppRoles
{
    public class AppRoleRepository : BaseRepository<AppRole, int>, IAppRoleRepository
    {
        public AppRoleRepository(SellPhoneContext context) : base(context)
        {
        }
    }
}
