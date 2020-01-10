using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.RedisRepository
{
    public interface IIndustryRepository
    {
        IEnumerable<RedisKeyValue> Get(string key);
    }

    public class IndustryRepository : IIndustryRepository
    {
        private readonly IRedisRepository _redisRepository;

        public IndustryRepository(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }
        public IEnumerable<RedisKeyValue> Get(string key)
        {
            return _redisRepository.GetHash(key);
        }
    }
}
