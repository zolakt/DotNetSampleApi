using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Caching;

namespace SampleApp.Common.Tests.Caching
{
    [TestClass]
    public class HttpContextCacheTests
    {
        private const string TestKey = "test1";
        private const string TestValue = "test1";

        [TestMethod]
        public void ContainsKeyTest()
        {
            GenerateFakeHttpContext();

            var cache = new HttpContextCache();

            Assert.IsTrue(cache.ContainsKey(TestKey));
        }

        [TestMethod]
        public void ContainsKeyNonExistingTest()
        {
            var tmpKey = TestKey + "123";

            GenerateFakeHttpContext();

            var cache = new HttpContextCache();

            Assert.IsFalse(cache.ContainsKey(tmpKey));
        }

        [TestMethod]
        public void RetrieveTest()
        {
            GenerateFakeHttpContext();

            var cache = new HttpContextCache();
            var retrieved = cache.Retrieve<string>(TestKey);

            Assert.AreEqual(TestValue, retrieved);
        }

        [TestMethod]
        public void RetrieveNonExistingTest()
        {
            var tmpKey = TestKey + "123";

            GenerateFakeHttpContext();

            var cache = new HttpContextCache();
            var retrieved = cache.Retrieve<string>(tmpKey);

            Assert.IsNull(retrieved);
        }

        [TestMethod]
        public void StoreTest()
        {
            const string expectedKey = "test2";
            const string expectedValue = "test2";

            GenerateFakeHttpContext();

            var cache = new HttpContextCache();
            cache.Store(expectedKey, expectedValue);

            var retrieved = cache.Retrieve<string>(expectedKey);

            Assert.AreEqual(expectedValue, retrieved);
        }

        [TestMethod]
        public void RemoveTest()
        {
            GenerateFakeHttpContext();

            var cache = new HttpContextCache();
            cache.Remove(TestKey);

            var retrieved = cache.Retrieve<string>(TestKey);
            Assert.IsNull(retrieved);
        }

        [TestMethod]
        public void RemoveNonExistingTest()
        {
            var tmpKey = TestKey + "123";
            GenerateFakeHttpContext();

            var cache = new HttpContextCache();
            cache.Remove(tmpKey);

            var retrieved = cache.Retrieve<string>(tmpKey);
            Assert.IsNull(retrieved);
        }


        private void GenerateFakeHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://localhost", ""),
                new HttpResponse(new StringWriter())
            );

            HttpContext.Current.Items[TestKey] = TestValue;
        }
    }
}