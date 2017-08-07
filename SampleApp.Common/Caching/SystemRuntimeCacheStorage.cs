using System;
using System.Runtime.Caching;

namespace SampleApp.Common.Caching
{
    public class SystemRuntimeCacheStorage : ICacheStorage, IDisposable
    {
        private readonly MemoryCache _cache;

        public SystemRuntimeCacheStorage()
        {
            _cache = MemoryCache.Default;
        }


        public bool ContainsKey(string key)
        {
            return _cache.Contains(key);
        }

        public T Retrieve<T>(string key) where T : class
        {
            return _cache.Contains(key) ? (T)_cache[key] : default(T);
        }

        public void Store(string key, object data)
        {
            _cache.Add(key, data, null);
        }

        public void Store(string key, object data, DateTime absoluteExpiration)
        {
            var policy = new CacheItemPolicy {
                AbsoluteExpiration = absoluteExpiration
            };

            if (_cache.Contains(key))
            {
                _cache.Remove(key);
            }

            _cache.Add(key, data, policy);
        }

        public void Store(string key, object data, TimeSpan slidingExpiration)
        {
            var policy = new CacheItemPolicy {
                SlidingExpiration = slidingExpiration
            };

            if (_cache.Contains(key))
            {
                _cache.Remove(key);
            }

            _cache.Add(key, data, policy);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }


        public void Dispose()
        {
            _cache.Dispose();
        }
    }
}