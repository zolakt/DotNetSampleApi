using System;
using System.Web;

namespace SampleApp.Common.Caching
{
    public class HttpContextCache : ICacheStorage
    {
        private readonly HttpContext _context;

        public HttpContextCache()
        {
            _context = HttpContext.Current;
        }


        public bool ContainsKey(string key)
        {
            return _context.Items.Contains(key);
        }

        public T Retrieve<T>(string key) where T : class
        {
            if (ContainsKey(key))
            {
                return _context.Items[key] as T;
            }

            return null;
        }

        public void Store(string key, object data)
        {
            if (ContainsKey(key))
            {
                _context.Items[key] = data;
            }
            else
            {
                _context.Items.Add(key, data);
            }
        }

        public void Store(string key, object data, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException("HttpContext items have no expiration");
        }

        public void Store(string key, object data, DateTime absoluteExpiration)
        {
            throw new NotImplementedException("HttpContext items have no expiration");
        }

        public void Remove(string key)
        {
            if (ContainsKey(key))
            {
                _context.Items.Remove(key);
            }
        }
    }
}