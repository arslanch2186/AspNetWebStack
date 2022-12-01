using SellPhone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.RedisCache
{
    public interface IRedisCacheService
    {
        string GetRedisCacheValue(Guid id, string typeName);
        void SetRedishCacheValue(BaseEntity quizDetail);
        void DeleteRedisCacheValue(Guid userId, string typeName);
    }
}
