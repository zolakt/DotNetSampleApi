using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Service.Exceptions;
using SampleApp.WebAPI.Helper;

namespace SampleApp.WebAPI.Tests.Helper
{
    [TestClass()]
    public class ExceptionDictionaryTests
    {
        [TestMethod()]
        public void ConvertToHttpStatusCodeEntityInvalidTest()
        {
            var exception = new ValidationException("test");
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.ConvertToHttpStatusCode());
        }

        [TestMethod()]
        public void ConvertToHttpStatusCodeEntityNotFoundTest()
        {
            var exception = new ResourceNotFoundException();
            Assert.AreEqual(HttpStatusCode.NotFound, exception.ConvertToHttpStatusCode());
        }
    }
}