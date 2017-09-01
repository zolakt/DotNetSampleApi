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
using SampleApp.Domain.Task.QueryParams;
using SampleApp.Service.Task;
using SampleApp.Service.Task.DTO;
using SampleApp.Service.Task.Messaging;
using SampleApp.WebAPI.Controllers;
using SampleApp.WebAPI.ResponseFormat;

namespace SampleApp.WebAPI.Tests.Controllers
{
    [TestClass]
    public class TaskControllerTests
    {
        [TestMethod]
        public void GetTest()
        {
            var testGuid = Guid.NewGuid();

            var expected = new GetTaskResponse {
                Result = new TaskDTO {
                    Id = testGuid,
                    Name = "test",
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid(),
                    UserFirstName = "test",
                    UserLastName = "test"
                }
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.GetTask(It.IsAny<GetTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Get(testGuid);

            TaskDTO result;
            response.TryGetContentValue(out result);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expected.Result.Id, result.Id);
            Assert.AreEqual(expected.Result.Name, result.Name);
            Assert.AreEqual(expected.Result.Time, result.Time);
            Assert.AreEqual(expected.Result.UserId, result.UserId);
            Assert.AreEqual(expected.Result.UserFirstName, result.UserFirstName);
            Assert.AreEqual(expected.Result.UserLastName, result.UserLastName);
            mockService.Verify(x => x.GetTask(It.IsAny<GetTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void GetNotFoundTest()
        {
            var errorMessage = "user";

            var expectedResponse = new GetTaskResponse {
                Exception = new ResourceNotFoundException(errorMessage)
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.GetTask(It.IsAny<GetTaskRequest>())).Returns(expectedResponse);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
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
            mockService.Verify(x => x.GetTask(It.IsAny<GetTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void GetMultipleTest()
        {
            var task1 = new TaskDTO {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserFirstName = "test",
                UserLastName = "test"
            };

            var task2 = new TaskDTO {
                Id = Guid.NewGuid(),
                Name = "test2",
                Time = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserFirstName = "test2",
                UserLastName = "test2"
            };

            var expected = new GetTasksResponse {
                Result = new List<TaskDTO> {
                    task1,
                    task2
                }
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.GetTasks(It.IsAny<GetTasksRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Get(new TaskQueryParam());

            IEnumerable<TaskDTO> result;
            response.TryGetContentValue(out result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, expected.Result.Count());

            var first = expected.Result.First();

            Assert.AreEqual(task1.Id, first.Id);
            Assert.AreEqual(task1.Name, first.Name);
            Assert.AreEqual(task1.Time, first.Time);
            Assert.AreEqual(task1.UserId, first.UserId);
            Assert.AreEqual(task1.UserFirstName, first.UserFirstName);
            Assert.AreEqual(task1.UserLastName, first.UserLastName);

            var last = expected.Result.Last();

            Assert.AreEqual(task2.Id, last.Id);
            Assert.AreEqual(task2.Name, last.Name);
            Assert.AreEqual(task2.Time, last.Time);
            Assert.AreEqual(task2.UserId, last.UserId);
            Assert.AreEqual(task2.UserFirstName, last.UserFirstName);
            Assert.AreEqual(task2.UserLastName, last.UserLastName);
            mockService.Verify(x => x.GetTasks(It.IsAny<GetTasksRequest>()), Times.Once);
        }

        [TestMethod]
        public void PostTest()
        {
            var input = new TaskEditDTO {
                Name = "test",
                Time = DateTime.Now,
                UserId = Guid.NewGuid()
            };

            var expected = new InsertTaskResponse {
                Result = true
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.InsertTask(It.IsAny<InsertTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
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
            mockService.Verify(x => x.InsertTask(It.IsAny<InsertTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void PostInvalidTest()
        {
            var input = new TaskEditDTO {
                Time = DateTime.Now,
                UserId = Guid.NewGuid()
            };

            var expected = new InsertTaskResponse {
                Result = true,
                Exception = new ValidationException("name")
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.InsertTask(It.IsAny<InsertTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
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
            mockService.Verify(x => x.InsertTask(It.IsAny<InsertTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void PutTest()
        {
            var testGuid = Guid.NewGuid();

            var input = new TaskDTO {
                Id = testGuid,
                Name = "test",
                Time = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserFirstName = "test",
                UserLastName = "test"
            };

            var expected = new UpdateTaskResponse {
                Result = true
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.UpdateTask(It.IsAny<UpdateTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
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
            mockService.Verify(x => x.UpdateTask(It.IsAny<UpdateTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void PutInvalidTest()
        {
            var testGuid = Guid.NewGuid();

            var input = new TaskDTO {
                Id = testGuid,
                Name = null,
                Time = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserFirstName = "test",
                UserLastName = "test"
            };

            var expected = new UpdateTaskResponse {
                Result = false,
                Exception = new ValidationException("task")
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.UpdateTask(It.IsAny<UpdateTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
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
            mockService.Verify(x => x.UpdateTask(It.IsAny<UpdateTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void PutNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var input = new TaskDTO {
                Id = testGuid,
                Name = "test",
                Time = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserFirstName = "test",
                UserLastName = "test"
            };

            var expected = new UpdateTaskResponse {
                Result = false,
                Exception = new ResourceNotFoundException("task")
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.UpdateTask(It.IsAny<UpdateTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
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
            mockService.Verify(x => x.UpdateTask(It.IsAny<UpdateTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var testGuid = Guid.NewGuid();

            var expected = new DeleteTaskResponse {
                Result = true
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.DeleteTask(It.IsAny<DeleteTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            mockRespoonseBuilder.Setup(x => x.BuildResponse(controller.Request, expected))
                .Returns(controller.Request.CreateResponse(HttpStatusCode.OK, expected.Result));

            var response = controller.Delete(testGuid);

            bool result;
            response.TryGetContentValue(out result);

            Assert.IsTrue(result);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mockService.Verify(x => x.DeleteTask(It.IsAny<DeleteTaskRequest>()), Times.Once);
        }

        [TestMethod]
        public void DeleteNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var expected = new DeleteTaskResponse {
                Result = false,
                Exception = new ResourceNotFoundException("task")
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.DeleteTask(It.IsAny<DeleteTaskRequest>())).Returns(expected);

            var mockRespoonseBuilder = new Mock<IHttpResponseBuilder>();

            var controller = new TaskController(mockService.Object, mockRespoonseBuilder.Object) {
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
            mockService.Verify(x => x.DeleteTask(It.IsAny<DeleteTaskRequest>()), Times.Once);
        }
    }
}