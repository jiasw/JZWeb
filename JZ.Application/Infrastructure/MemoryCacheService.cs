using JZ.Application.Contract.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using NetTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Infrastructure
{
    public class MemoryCacheService : ICacheService
    {
        private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public void Set(string key, string value, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null)
        {
            var options = new MemoryCacheEntryOptions();

            if (slidingExpiration.HasValue)
            {
                options.SetSlidingExpiration(slidingExpiration.Value);
            }

            if (absoluteExpiration.HasValue)
            {
                options.SetAbsoluteExpiration(absoluteExpiration.Value);
            }

            _cache.Set(key, value, options);
        }

        public void Set<T>(string key, T value, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null)
        {
            var options = new MemoryCacheEntryOptions();

            if (slidingExpiration.HasValue)
            {
                options.SetSlidingExpiration(slidingExpiration.Value);
            }

            if (absoluteExpiration.HasValue)
            {
                options.SetAbsoluteExpiration(absoluteExpiration.Value);
            }

            _cache.Set(key, value, options);
        }

        public string Get(string key)
        {
            object obj = _cache.Get(key);
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        public T Get<T>(string key)
        {
            object obj = _cache.Get(key);
            if (obj == null)
            {
                return default(T);
            }
            return (T)_cache.Get(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool Contains(string key)
        {
            return _cache.TryGetValue(key, out _);
        }
    }
}
