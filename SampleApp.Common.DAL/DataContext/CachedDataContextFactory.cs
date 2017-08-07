using System;
using SampleApp.Common.Caching;

namespace SampleApp.Common.DAL.DataContext
{
    public abstract class CachedDataContextFactory<TContext> : IDataContextFactory<TContext> 
        where TContext : class, IDataContext
    {
        private readonly ICacheStorage _cacheStorage;
        private readonly string _dataContextKey;

        public CachedDataContextFactory(ICacheStorage cacheStorage)
        {
            if (cacheStorage == null)
            {
                throw new ArgumentNullException("cacheStorage");
            }

            _cacheStorage = cacheStorage;
            _dataContextKey = typeof(TContext).FullName;
        }

        public TContext Create()
        {
            if (!_cacheStorage.ContainsKey(_dataContextKey))
            {
                var context = CreateContext();
                _cacheStorage.Store(_dataContextKey, context);
            }

            return _cacheStorage.Retrieve<TContext>(_dataContextKey);
        }

        protected abstract TContext CreateContext();
    }
}