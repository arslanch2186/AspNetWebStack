using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.DbModels
{
    public class AppPermission : SoftDeletedEntity<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public ICollection<AppRolePermission> AppRolePermissions { get; set; }
    }
}
