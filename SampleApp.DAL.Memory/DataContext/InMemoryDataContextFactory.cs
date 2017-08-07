using System;
using SampleApp.Common.Caching;
using SampleApp.Common.DAL.DataContext;
using SampleApp.DAL.Memory.DataContext.Initializer;

namespace SampleApp.DAL.Memory.DataContext
{
    public class InMemoryDataContextFactory : CachedDataContextFactory<IDomainContext>
    {
        private readonly IDatabaseInitializer _databaseInitializer;

        public InMemoryDataContextFactory(ICacheStorage cacheStorage, IDatabaseInitializer databaseInitializer)
            : base(cacheStorage)
        {
            if (databaseInitializer == null)
            {
                throw new ArgumentNullException("databaseInitializer");
            }

            _databaseInitializer = databaseInitializer;
        }

        protected override IDomainContext CreateContext()
        {
            return new InMemoryDataContext(_databaseInitializer);
        }
    }
}