using SampleApp.Common.Caching;
using SampleApp.Common.DAL.DataContext;

namespace SampleApp.DAL.EF.DataContext
{
    public class EfDataContextFactory : CachedDataContextFactory<IDomainContext>
    {
        public EfDataContextFactory(ICacheStorage cacheStorage) : base(cacheStorage)
        {
        }

        protected override IDomainContext CreateContext()
        {
            return new EfDataContext();
        }
    }
}