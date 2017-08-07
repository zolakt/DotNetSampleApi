using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Service.Exceptions;
using SampleApp.WebAPI.ResponseFormat;
using SampleApp.WebAPI.Tests.Fakes;

namespace SampleApp.WebAPI.Tests.ResponseFormat
{
    [TestClass()]
    public class HttpResponseBuilderTests
    {
        [TestMethod()]
        public void BuildResponseOkTest()
        {
            var request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());

            var serviceResponse = new FakeServiceResponse {
                Result = true
            };

            var builder = new HttpResponseBuilder();

            var response = builder.BuildResponse(request, serviceResponse);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("true", response.Content.ReadAsStringAsync().Result);
        }

        [TestMethod()]
        public void BuildResponseNotFoundTest()
        {
            var request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());

            var serviceResponse = new FakeServiceResponse
            {
                Result = false,
                Exception = new ResourceNotFoundException("test")
            };

            var builder = new HttpResponseBuilder();

            var response = builder.BuildResponse(request, serviceResponse);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("test", response.Content.ReadAsStringAsync().Result);
        }

        [TestMethod()]
        public void BuildResponseEntityInvalidTest()
        {
            var request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());

            var serviceResponse = new FakeServiceResponse
            {
                Result = false,
                Exception = new ValidationException("test")
            };

            var builder = new HttpResponseBuilder();

            var response = builder.BuildResponse(request, serviceResponse);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("test", response.Content.ReadAsStringAsync().Result);
        }
    }
}