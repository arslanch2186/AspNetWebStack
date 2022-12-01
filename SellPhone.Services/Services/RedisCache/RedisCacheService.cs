using SellPhone.Db.Repositories.RedisCache;
using SellPhone.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SellPhone.Services.Services.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IRedisCacheRepository _redisRepository;
        public RedisCacheService(IRedisCacheRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public string GetRedisCacheValue(Guid id, string typeName)
        {
            string key = GetKey(typeName, id);
            return _redisRepository.GetValue(key);
        }

        public void SetRedishCacheValue(BaseEntity entity)
        {
            string name = entity.GetType().Name;
            string key = GetKey(name, (Guid)entity.Id);
            _redisRepository.SetValue(key, JsonSerializer.Serialize(entity, entity.GetType()));
        }

        public void DeleteRedisCacheValue(Guid userId, string typeName)
        {
            string key = GetKey(typeName, userId);
            _redisRepository.DeleteKey(key);
        }

        private string GetKey(string keyType, Guid key)
        {
            return keyType + "_" + key.ToString();
        }
    }
}
