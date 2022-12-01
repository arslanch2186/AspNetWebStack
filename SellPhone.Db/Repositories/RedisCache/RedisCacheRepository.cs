using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Repositories.RedisCache
{
    public class RedisCacheRepository : IRedisCacheRepository
    {
        IConnectionMultiplexer _redis;
        public RedisCacheRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void SetValue(string key, string value)
        {
            var db = _redis.GetDatabase();
            db.StringSet(key, value);
        }
        public string GetValue(string key)
        {
            var db = _redis.GetDatabase();
            return db.StringGet(key);
        }

        public void DeleteKey(string key)
        {
            var db = _redis.GetDatabase();
            db.KeyDelete(key);
        }
    }
}
