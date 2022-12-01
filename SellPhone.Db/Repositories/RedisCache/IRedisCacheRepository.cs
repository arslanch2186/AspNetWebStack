using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Repositories.RedisCache
{
    public interface IRedisCacheRepository
    {
        void SetValue(string key, string value);
        string GetValue(string key);
        void DeleteKey(string key);
    }
}
