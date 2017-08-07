using System;

namespace SampleApp.Common.Caching
{
    public interface ICacheStorage
    {
        void Remove(string key);

        void Store(string key, object data);

        void Store(string key, object data, TimeSpan slidingExpiration);

        void Store(string key, object data, DateTime absoluteExpiration);

        T Retrieve<T>(string key) where T : class;

        bool ContainsKey(string key);
    }
}