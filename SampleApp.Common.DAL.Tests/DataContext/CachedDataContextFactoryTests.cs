using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.Caching;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.Tests.Fakes;

namespace SampleApp.Common.DAL.Tests.DataContext
{
    [TestClass]
    public class CachedDataContextFactoryTests
    {
        [TestMethod]
        public void CreateTestNotInCache()
        {
            var cacheKey = typeof(FakeDataContext).FullName;
            var storeCount = 0;

            var mockCache = new Mock<ICacheStorage>();
            mockCache.Setup(x => x.ContainsKey(cacheKey)).Returns(false);
            mockCache.Setup(x => x.Store(cacheKey, It.IsAny<IDataContext>())).Callback(() => storeCount++);
            mockCache.Setup(x => x.Retrieve<FakeDataContext>(cacheKey)).Returns(new FakeDataContext());

            var factory = new FakeCachedDataContextFactory(mockCache.Object);
            var context = factory.Create();

            Assert.AreEqual(cacheKey, context.GetType().FullName);
            Assert.AreEqual(1, storeCount);
        }

        [TestMethod]
        public void CreateTestAlreadyInCache()
        {
            var cacheKey = typeof(FakeDataContext).FullName;
            var storeCount = 0;

            var mockCache = new Mock<ICacheStorage>();
            mockCache.Setup(x => x.ContainsKey(cacheKey)).Returns(true);
            mockCache.Setup(x => x.Store(cacheKey, It.IsAny<IDataContext>())).Callback(() => storeCount++);
            mockCache.Setup(x => x.Retrieve<FakeDataContext>(cacheKey)).Returns(new FakeDataContext());

            var factory = new FakeCachedDataContextFactory(mockCache.Object);
            var context = factory.Create();

            Assert.AreEqual(cacheKey, context.GetType().FullName);
            Assert.AreEqual(0, storeCount);
        }
    }
}