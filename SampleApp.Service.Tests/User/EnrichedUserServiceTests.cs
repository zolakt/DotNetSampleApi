using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.Configuration;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Email;
using SampleApp.Common.Logging;
using SampleApp.Service.Configuration;
using SampleApp.Service.User;
using SampleApp.Service.User.DTO;
using SampleApp.Service.User.Messaging;

namespace SampleApp.Service.Tests.User
{
    [TestClass]
    public class EnrichedUserServiceTests
    {
        [TestMethod]
        public void GetUserTest()
        {
            var mockService = new Mock<IUserService>();

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var request = new GetUserRequest(Guid.NewGuid());

            service.GetUser(request);

            mockService.Verify(x => x.GetUser(request), Times.Once);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            var mockService = new Mock<IUserService>();

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var request = new GetUsersRequest();

            service.GetUsers(request);

            mockService.Verify(x => x.GetUsers(request), Times.Once);
        }

        [TestMethod]
        public void InsertUserTest()
        {
            var request = new InsertUserRequest {
                UserProperties = new UserEditDTO {
                    FirstName = "test",
                    LastName = "test",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedResonse = new InsertUserResponse {
                Exception = null
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.InsertUser(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.InsertUser(request);

            Assert.IsNull(response.Exception);
            Assert.AreEqual(expectedResonse.Exception, response.Exception);
            mockService.Verify(x => x.InsertUser(request), Times.Once);
        }

        [TestMethod]
        public void InsertUserInvalidTest()
        {
            var request = new InsertUserRequest {
                UserProperties = new UserEditDTO {
                    FirstName = "test",
                    LastName = "test",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedResonse = new InsertUserResponse {
                Exception = new ValidationException("invalid")
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.InsertUser(request)).Returns(expectedResonse);

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

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.InsertUser(request);

            Assert.IsNotNull(response.Exception);

            mockLogger.Verify(x => x.LogError(It.IsAny<object>(), "invalid", It.IsAny<ValidationException>()),
                Times.Once);

            mockEmail.Verify(x => x.SendEmail(It.IsAny<EmailArguments>()), Times.Once);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new UpdateUserRequest(testGuid) {
                UserProperties = new UserEditDTO {
                    FirstName = "test",
                    LastName = "test",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedResonse = new UpdateUserResponse {
                Exception = null
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.UpdateUser(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.UpdateUser(request);

            Assert.IsNull(response.Exception);
            Assert.AreEqual(expectedResonse.Exception, response.Exception);
            mockService.Verify(x => x.UpdateUser(request), Times.Once);
        }

        [TestMethod]
        public void UpdateUserInvalidTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new UpdateUserRequest(testGuid) {
                UserProperties = new UserEditDTO {
                    FirstName = "test",
                    LastName = "test",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedResonse = new UpdateUserResponse {
                Exception = new ValidationException("invalid")
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.UpdateUser(request)).Returns(expectedResonse);

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

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.UpdateUser(request);

            Assert.IsNotNull(response.Exception);

            mockLogger.Verify(x => x.LogError(It.IsAny<object>(), "invalid", It.IsAny<ValidationException>()),
                Times.Once);

            mockEmail.Verify(x => x.SendEmail(It.IsAny<EmailArguments>()), Times.Once);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new DeleteUserRequest(testGuid);

            var expectedResonse = new DeleteUserResponse {
                Exception = null
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.DeleteUser(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.DeleteUser(request);

            Assert.IsNull(response.Exception);
            Assert.AreEqual(expectedResonse.Exception, response.Exception);
            mockService.Verify(x => x.DeleteUser(request), Times.Once);
        }

        [TestMethod]
        public void DeleteUserInvalidTest()
        {
            var testGuid = Guid.NewGuid();
            var request = new DeleteUserRequest(testGuid);

            var expectedResonse = new DeleteUserResponse {
                Exception = new ValidationException("invalid")
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.DeleteUser(request)).Returns(expectedResonse);

            var mockLogger = new Mock<ILoggingService>();

            var mockEmail = new Mock<IEmailService>();

            var mockConfigOptions = new Mock<IServiceConfigOptions>();
            mockConfigOptions.Setup(x => x.AdminEmail).Returns("test");
            mockConfigOptions.Setup(x => x.ErrorEmailSubject).Returns("test");
            mockConfigOptions.Setup(x => x.SmtpServer).Returns("test");
            mockConfigOptions.Setup(x => x.SystemEmail).Returns("test");

            var mockConfig = new Mock<IAppConfig<IServiceConfigOptions>>();
            mockConfig.Setup(x => x.Options).Returns(mockConfigOptions.Object);

            var service = new EnrichedUserService(mockService.Object, mockLogger.Object, mockEmail.Object,
                mockConfig.Object);

            var response = service.DeleteUser(request);

            Assert.IsNotNull(response.Exception);

            mockLogger.Verify(x => x.LogError(It.IsAny<object>(), "invalid", It.IsAny<ValidationException>()),
                Times.Once);

            mockEmail.Verify(x => x.SendEmail(It.IsAny<EmailArguments>()), Times.Once);
        }
    }
}