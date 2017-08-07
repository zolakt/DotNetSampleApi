using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Service.Exceptions;
using SampleApp.Domain.User.QueryParams;
using SampleApp.Service.User;
using SampleApp.Service.User.DTO;
using SampleApp.Service.User.Messaging;
using SampleApp.WebAPI.Controllers;
using SampleApp.WebAPI.ResponseFormat;

namespace SampleApp.WebAPI.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void GetTest()
        {
            var testGuid = Guid.NewGuid();

            var expected = new GetUserResponse {
                Result = new UserDTO {
                    Id = testGuid,
                    FirstName = "test",
                    LastName = "test",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetUser(It.IsAny<GetUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Get(testGuid);

            UserDTO result;
            response.TryGetContentValue(out result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Result.Id, result.Id);
            Assert.AreEqual(expected.Result.FirstName, result.FirstName);
            Assert.AreEqual(expected.Result.LastName, result.LastName);
            Assert.AreEqual(expected.Result.Country, result.Country);
            Assert.AreEqual(expected.Result.City, result.City);
            Assert.AreEqual(expected.Result.Street, result.Street);
            Assert.AreEqual(expected.Result.HouseNumber, result.HouseNumber);
            mockService.Verify(x => x.GetUser(It.IsAny<GetUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void GetNotFoundTest()
        {
            var errorMessage = "user";

            var expectedResponse = new GetUserResponse {
                Exception = new ResourceNotFoundException(errorMessage)
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetUser(It.IsAny<GetUserRequest>())).Returns(expectedResponse);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(It.IsAny<HttpRequestMessage>(), expectedResponse))
                .Returns(new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent(expectedResponse.Exception.Message)
                });

            var response = controller.Get(Guid.NewGuid());
            var content = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual(errorMessage, content);
            mockService.Verify(x => x.GetUser(It.IsAny<GetUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void GetMultipleTest()
        {
            var user1 = new UserDTO {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var user2 = new UserDTO {
                Id = Guid.NewGuid(),
                FirstName = "test2",
                LastName = "test2",
                Country = "US",
                City = "New York",
                Street = "Test street",
                HouseNumber = "123"
            };

            var expected = new GetUsersResponse {
                Result = new List<UserDTO> {
                    user1,
                    user2
                }
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetUsers(It.IsAny<GetUsersRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Get(new UserQueryParam());

            IEnumerable<UserDTO> result;
            response.TryGetContentValue(out result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, expected.Result.Count());

            var first = expected.Result.First();

            Assert.AreEqual(user1.Id, first.Id);
            Assert.AreEqual(user1.FirstName, first.FirstName);
            Assert.AreEqual(user1.LastName, first.LastName);
            Assert.AreEqual(user1.Country, first.Country);
            Assert.AreEqual(user1.City, first.City);
            Assert.AreEqual(user1.Street, first.Street);
            Assert.AreEqual(user1.HouseNumber, first.HouseNumber);

            var last = expected.Result.Last();

            Assert.AreEqual(user2.Id, last.Id);
            Assert.AreEqual(user2.FirstName, last.FirstName);
            Assert.AreEqual(user2.LastName, last.LastName);
            Assert.AreEqual(user2.Country, last.Country);
            Assert.AreEqual(user2.City, last.City);
            Assert.AreEqual(user2.Street, last.Street);
            Assert.AreEqual(user2.HouseNumber, last.HouseNumber);

            mockService.Verify(x => x.GetUsers(It.IsAny<GetUsersRequest>()), Times.Once);
        }

        [TestMethod]
        public void PostTest()
        {
            var input = new UserEditDTO {
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var expected = new InsertUserResponse {
                Result = true
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.InsertUser(It.IsAny<InsertUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Post(input);

            bool result;
            response.TryGetContentValue(out result);

            Assert.IsTrue(result);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mockService.Verify(x => x.InsertUser(It.IsAny<InsertUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void PostInvalidTest()
        {
            var input = new UserEditDTO {
                FirstName = null,
                LastName = null,
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var expected = new InsertUserResponse {
                Result = false,
                Exception = new ValidationException("name")
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.InsertUser(It.IsAny<InsertUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(It.IsAny<HttpRequestMessage>(), expected))
                .Returns(new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    Content = new StringContent(expected.Exception.Message)
                });

            var response = controller.Post(input);

            bool result;
            response.TryGetContentValue(out result);

            Assert.IsFalse(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            mockService.Verify(x => x.InsertUser(It.IsAny<InsertUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void PutTest()
        {
            var testGuid = Guid.NewGuid();

            var input = new UserDTO {
                Id = testGuid,
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var expected = new UpdateUserResponse {
                Result = true
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.UpdateUser(It.IsAny<UpdateUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Put(input);

            bool result;
            response.TryGetContentValue(out result);

            Assert.IsTrue(result);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mockService.Verify(x => x.UpdateUser(It.IsAny<UpdateUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void PutInvalidTest()
        {
            var testGuid = Guid.NewGuid();

            var input = new UserDTO {
                Id = testGuid,
                FirstName = null,
                LastName = null,
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var expected = new UpdateUserResponse {
                Result = false,
                Exception = new ValidationException("name")
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.UpdateUser(It.IsAny<UpdateUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(It.IsAny<HttpRequestMessage>(), expected))
                .Returns(new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    Content = new StringContent(expected.Exception.Message)
                });

            var response = controller.Put(input);

            bool result;
            response.TryGetContentValue(out result);

            Assert.IsFalse(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            mockService.Verify(x => x.UpdateUser(It.IsAny<UpdateUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void PutNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var input = new UserDTO {
                Id = testGuid,
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var expected = new UpdateUserResponse {
                Result = false,
                Exception = new ResourceNotFoundException("user")
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.UpdateUser(It.IsAny<UpdateUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(It.IsAny<HttpRequestMessage>(), expected))
                .Returns(new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent(expected.Exception.Message)
                });

            var response = controller.Put(input);

            bool result;
            response.TryGetContentValue(out result);

            Assert.IsFalse(result);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mockService.Verify(x => x.UpdateUser(It.IsAny<UpdateUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var testGuid = Guid.NewGuid();

            var expected = new DeleteUserResponse {
                Result = true
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.DeleteUser(It.IsAny<DeleteUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Delete(testGuid);

            bool result;
            response.TryGetContentValue(out result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result);
            mockService.Verify(x => x.DeleteUser(It.IsAny<DeleteUserRequest>()), Times.Once);
        }

        [TestMethod]
        public void DeleteNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var expected = new DeleteUserResponse {
                Result = false,
                Exception = new ResourceNotFoundException("user")
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.DeleteUser(It.IsAny<DeleteUserRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new UserController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent(expected.Exception.Message)
                });

            var response = controller.Delete(testGuid);

            bool result;
            response.TryGetContentValue(out result);

            Assert.IsFalse(result);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mockService.Verify(x => x.DeleteUser(It.IsAny<DeleteUserRequest>()), Times.Once);
        }
    }
}