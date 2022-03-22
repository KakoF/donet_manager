using Data.Interfaces.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Data.Redis
{
    public class RedisIntegrator : IRedisIntegrator
    {
        private readonly IDistributedCache _redisCache;
        private readonly IConfiguration _configuration;
        private readonly string NAME;
        public RedisIntegrator(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _configuration = configuration;
            NAME = _configuration["redisSolution"];
            _redisCache = distributedCache;
        }
        public async Task<T> GetAsync<T>(string key)
        {
            try
            {
                var json = await _redisCache.GetStringAsync($"{NAME}{key}");
                return (json == null) ? default(T) : JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                //loggin
                return default(T);
            }
        }
        public void Set(string key, object value, int expirationMinutes = 1)
        {
            DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationMinutes));

            var json = JsonConvert.SerializeObject(value);
            _redisCache.SetStringAsync($"{NAME}{key}", json, opcoesCache);
        }
        public void Remove(string key)
        {
            try
            {
                _redisCache.RemoveAsync($"{NAME}{key}");
            }
            catch
            {
                //loggin
            }

        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string key)
        {
            try
            {
                var json = await _redisCache.GetStringAsync($"{NAME}{key}");
                return (json == null) ? default(IEnumerable<T>) : JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }
            catch
            {
                return default(IEnumerable<T>);
                //loggin
            }
        }

        public void SetList(string key, IEnumerable<object> value, int expirationMinutes = 1)
        {
            try
            {
                DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
                opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationMinutes));

                var json = JsonConvert.SerializeObject(value);
                _redisCache.SetStringAsync($"{NAME}{key}", json, opcoesCache);
            }
            catch
            {
                //loggin
            }
        }
    }
}
