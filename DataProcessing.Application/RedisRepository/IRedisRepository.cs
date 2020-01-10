using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.RedisRepository
{
    public interface IRedisRepository
    {
        IEnumerable<RedisKeyValue> GetHash(string hashKey);
    }

    public class RedisRepository : IRedisRepository
    {
        public RedisRepository()
        {
        }

        public IEnumerable<RedisKeyValue> GetHash(string hashKey)
        {
            var _storage = Connection.GetDatabase();
            var hashsubIndustryValue = _storage.HashGetAll(hashKey);
            var redisKeyandValue = hashsubIndustryValue.Select(x =>
                new RedisKeyValue
                {
                    Key = x.Name,
                    Value = x.Value
                });
            return redisKeyandValue;
        }

        private Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string _cacheConnection = "127.0.0.1:6379";
            return ConnectionMultiplexer.Connect(_cacheConnection);
        });

        public ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }

    public class RedisKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
