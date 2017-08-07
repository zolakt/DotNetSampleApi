using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.Configuration;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Email;
using SampleApp.Common.Logging;
using SampleApp.Service.Configuration;
using SampleApp.Service.Task;
using SampleApp.Service.Task.DTO;
using SampleApp.Service.Task.Messaging;

namespace SampleApp.Service.Tests.Task
{
    [TestClass]
    public class EnrichedTaskServiceTests
    {
        [TestMethod]
        public void GetTaskTest()
        {
            var mockService = new Mock<ITaskService>();

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var request = new GetTaskRequest(Guid.NewGuid());

            service.GetTask(request);

            mockService.Verify(x => x.GetTask(request), Times.Once);
        }

        [TestMethod]
        public void GetTasksTest()
        {
            var mockService = new Mock<ITaskService>();

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var request = new GetTasksRequest();

            service.GetTasks(request);

            mockService.Verify(x => x.GetTasks(request), Times.Once);
        }

        [TestMethod]
        public void InsertTaskTest()
        {
            var request = new InsertTaskRequest {
                TaskProperties = new TaskEditDTO {
                    Name = "test",
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedResonse = new InsertTaskResponse {
                Exception = null
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.InsertTask(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.InsertTask(request);

            Assert.IsNull(response.Exception);
            Assert.AreEqual(expectedResonse.Exception, response.Exception);
            mockService.Verify(x => x.InsertTask(request), Times.Once);
        }

        [TestMethod]
        public void InsertTaskInvalidTest()
        {
            var request = new InsertTaskRequest {
                TaskProperties = new TaskEditDTO {
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedResonse = new InsertTaskResponse {
                Exception = new ValidationException("invalid")
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.InsertTask(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();
            mockEmail.Setup(x => x.SendEmail(It.IsAny<EmailArguments>()));

            var mockConfigOptions = new Mock<IServiceConfigOptions>();
            mockConfigOptions.Setup(x => x.AdminEmail).Returns("test");
            mockConfigOptions.Setup(x => x.ErrorEmailSubject).Returns("test");
            mockConfigOptions.Setup(x => x.SmtpServer).Returns("test");
            mockConfigOptions.Setup(x => x.SystemEmail).Returns("test");

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();
            mockConfig.Setup(x => x.Options).Returns(mockConfigOptions.Object);

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.InsertTask(request);

            Assert.IsNotNull(response.Exception);

            mockLogger.Verify(x => x.LogError(It.IsAny<object>(), "invalid", It.IsAny<ValidationException>()),
                Times.Once);

            mockEmail.Verify(x => x.SendEmail(It.IsAny<EmailArguments>()), Times.Once);
        }

        [TestMethod]
        public void UpdateTaskTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new UpdateTaskRequest(testGuid) {
                TaskProperties = new TaskEditDTO {
                    Name = "test",
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedResonse = new UpdateTaskResponse {
                Exception = null
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.UpdateTask(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.UpdateTask(request);

            Assert.IsNull(response.Exception);
            Assert.AreEqual(expectedResonse.Exception, response.Exception);
            mockService.Verify(x => x.UpdateTask(request), Times.Once);
        }

        [TestMethod]
        public void UpdateTaskInvalidTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new UpdateTaskRequest(testGuid) {
                TaskProperties = new TaskEditDTO {
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedResonse = new UpdateTaskResponse {
                Exception = new ValidationException("invalid")
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.UpdateTask(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();
            mockEmail.Setup(x => x.SendEmail(It.IsAny<EmailArguments>()));

            var mockConfigOptions = new Mock<IServiceConfigOptions>();
            mockConfigOptions.Setup(x => x.AdminEmail).Returns("test");
            mockConfigOptions.Setup(x => x.ErrorEmailSubject).Returns("test");
            mockConfigOptions.Setup(x => x.SmtpServer).Returns("test");
            mockConfigOptions.Setup(x => x.SystemEmail).Returns("test");

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();
            mockConfig.Setup(x => x.Options).Returns(mockConfigOptions.Object);

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.UpdateTask(request);

            Assert.IsNotNull(response.Exception);

            mockLogger.Verify(x => x.LogError(It.IsAny<object>(), "invalid", It.IsAny<ValidationException>()),
                Times.Once);

            mockEmail.Verify(x => x.SendEmail(It.IsAny<EmailArguments>()), Times.Once);
        }

        [TestMethod]
        public void DeleteTaskTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new DeleteTaskRequest(testGuid);

            var expectedResonse = new DeleteTaskResponse {
                Exception = null
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.DeleteTask(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.DeleteTask(request);

            Assert.IsNull(response.Exception);
            Assert.AreEqual(expectedResonse.Exception, response.Exception);
            mockService.Verify(x => x.DeleteTask(request), Times.Once);
        }

        [TestMethod]
        public void DeleteTaskInvalidTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new DeleteTaskRequest(testGuid);

            var expectedResonse = new DeleteTaskResponse {
                Exception = new ValidationException("invalid")
            };

            var mockService = new Mock<ITaskService>();
            mockService.Setup(x => x.DeleteTask(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfigOptions = new Mock<IServiceConfigOptions>();
            mockConfigOptions.Setup(x => x.AdminEmail).Returns("test");
            mockConfigOptions.Setup(x => x.ErrorEmailSubject).Returns("test");
            mockConfigOptions.Setup(x => x.SmtpServer).Returns("test");
            mockConfigOptions.Setup(x => x.SystemEmail).Returns("test");

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();
            mockConfig.Setup(x => x.Options).Returns(mockConfigOptions.Object);

            var service = new EnrichedTaskService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.DeleteTask(request);

            Assert.IsNotNull(response.Exception);

            mockLogger.Verify(x => x.LogError(It.IsAny<object>(), "invalid", It.IsAny<ValidationException>()),
                Times.Once);

            mockEmail.Verify(x => x.SendEmail(It.IsAny<EmailArguments>()), Times.Once);
        }
    }
}