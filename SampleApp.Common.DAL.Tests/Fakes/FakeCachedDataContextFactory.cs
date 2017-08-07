using SampleApp.Common.Caching;
using SampleApp.Common.DAL.DataContext;

namespace SampleApp.Common.DAL.Tests.Fakes
{
    public class FakeCachedDataContextFactory : CachedDataContextFactory<FakeDataContext>
    {
        public FakeCachedDataContextFactory(ICacheStorage cacheStorage) : base(cacheStorage) {}

        protected override FakeDataContext CreateContext()
        {
            return new FakeDataContext();
        }
    }
}