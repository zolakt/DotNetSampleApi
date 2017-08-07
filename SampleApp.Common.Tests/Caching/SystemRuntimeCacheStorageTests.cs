using System;
using System.Runtime.Caching;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Caching;

namespace SampleApp.Common.Tests.Caching
{
    [TestClass()]
    public class SystemRuntimeCacheStorageTests
    {
        private const string TestKey = "test1";
        private const string TestValue = "test1";

        [TestMethod]
        public void ContainsKeyTest()
        {
            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();

            Assert.IsTrue(cache.ContainsKey(TestKey));
        }

        [TestMethod]
        public void ContainsKeyNonExistingTest()
        {
            var tmpKey = TestKey + "123";

            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();

            Assert.IsFalse(cache.ContainsKey(tmpKey));
        }

        [TestMethod()]
        public void RetrieveTest()
        {
            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();
            var retrieved = cache.Retrieve<string>(TestKey);

            Assert.AreEqual(TestValue, retrieved);
        }

        [TestMethod]
        public void RetrieveNonExistingTest()
        {
            var tmpKey = TestKey + "123";

            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();
            var retrieved = cache.Retrieve<string>(tmpKey);

            Assert.IsNull(retrieved);
        }

        [TestMethod()]
        public void StoreTest()
        {
            const string expectedKey = "test2";
            const string expectedValue = "test2";

            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();
            cache.Store(expectedKey, expectedValue);

            var retrieved = cache.Retrieve<string>(expectedKey);

            Assert.AreEqual(expectedValue, retrieved);
        }

        [TestMethod()]
        public void StoreTestWithAbsouluteExpiration()
        {
            const string expectedKey = "test2";
            const string expectedValue = "test2";
            const int expirationSeconds = 2;

            var startTime = DateTime.Now;
            var time = startTime.AddSeconds(expirationSeconds);

            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();
            cache.Store(expectedKey, expectedValue, time);

            var retrieved = cache.Retrieve<string>(expectedKey);

            Assert.AreEqual(expectedValue, retrieved);


            var timespan = time - startTime;
            Thread.Sleep(timespan);

            var retrieved2 = cache.Retrieve<string>(expectedKey);
            Assert.IsNull(retrieved2);
        }

        [TestMethod()]
        public void StoreTestWithSlidingExpiration()
        {
            const string expectedKey = "test2";
            const string expectedValue = "test2";
            const int expirationSeconds = 2;

            var timespan = new TimeSpan(0, 0, expirationSeconds);

            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();
            cache.Store(expectedKey, expectedValue, timespan);

            var retrieved = cache.Retrieve<string>(expectedKey);

            Assert.AreEqual(expectedValue, retrieved);
            

            Thread.Sleep(timespan);

            var retrieved2 = cache.Retrieve<string>(expectedKey);
            Assert.IsNull(retrieved2);
        }

        [TestMethod]
        public void RemoveTest()
        {
            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();
            cache.Remove(TestKey);

            var retrieved = cache.Retrieve<string>(TestKey);
            Assert.IsNull(retrieved);
        }

        [TestMethod]
        public void RemoveNonExistingTest()
        {
            var tmpKey = TestKey + "123";
            GenerateFakeMemoryContext();

            var cache = new SystemRuntimeCacheStorage();
            cache.Remove(tmpKey);

            var retrieved = cache.Retrieve<string>(tmpKey);
            Assert.IsNull(retrieved);
        }


        private void GenerateFakeMemoryContext()
        {
            MemoryCache.Default.Add(TestKey, TestValue, null);
        }
    }
}