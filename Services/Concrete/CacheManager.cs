using Entities.CacheModels;
using Entities.LinkModels;
using Entities.RequestFeatures;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Services.Concrete
{
    public class CacheManager : ICacheService
    {
        private IDatabase _cacheDb;
        IConfiguration configuration;

        public CacheManager()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb = redis.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        //public CacheData<T> GetDatas<T>(string key)
        //{
        //    var value = _cacheDb.StringGet(key);
        //    if (!value.IsNullOrEmpty)
        //    {
        //        return new CacheData<T>()
        //        { 
        //            Entity = JsonSerializer.Deserialize<T>(value), 
        //            LinkResponse = value
        //        };
        //    }
        //    return new CacheData<T>()
        //    { 
        //        Data = default, 
        //        IsCached = false 
        //    };
        //}

        public object RemoveData(string key)
        {
            var _exist = _cacheDb.KeyExists(key);
            if(_exist)
            {
                return _cacheDb.KeyDelete(key);
            }
            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);
        }
    }
}
