using SellPhone.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.City
{
    public interface ICityService : IBaseService<LU_Cities, int>
    {
        Task<IEnumerable<LU_Cities>> GetCities();
    }
}
