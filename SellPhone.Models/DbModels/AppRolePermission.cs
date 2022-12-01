using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class AppRolePermission : SoftDeletedEntity<int>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public AppPermission Permission { get; set; }
        public AppRole Role { get; set; }
    }
}
