using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Service.Exceptions;
using SampleApp.Domain.Address;
using SampleApp.Domain.User;
using SampleApp.Domain.User.QueryParams;
using SampleApp.Service.User;
using SampleApp.Service.User.DTO;
using SampleApp.Service.User.Mapper;
using SampleApp.Service.User.Messaging;

namespace SampleApp.Service.Tests.User
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetUserTest()
        {
            var testGuid = Guid.NewGuid();
            var userRequest = new GetUserRequest(testGuid);

            var expectedUser = new Domain.User.User {
                Id = testGuid,
                FirstName = "test",
                LastName = "test",
                Address = new Address {
                    Country = "UK"
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.FindBy(testGuid)).Returns(expectedUser);

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDTO(expectedUser)).Returns(new UserDTO {
                Id = expectedUser.Id,
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName,
                Country = expectedUser.Address.Country,
                City = expectedUser.Address.City,
                Street = expectedUser.Address.Street,
                HouseNumber = expectedUser.Address.HouseNumber
            });

            var mockValidator = new Mock<IValidator<Domain.User.User>>();

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.GetUser(userRequest);

            Assert.IsNull(response.Exception);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(expectedUser.Id, response.Result.Id);
            Assert.AreEqual(expectedUser.FirstName, response.Result.FirstName);
            Assert.AreEqual(expectedUser.LastName, response.Result.LastName);
            Assert.AreEqual(expectedUser.Address.Country, response.Result.Country);
            Assert.AreEqual(expectedUser.Address.City, response.Result.City);
            Assert.AreEqual(expectedUser.Address.Street, response.Result.Street);
            Assert.AreEqual(expectedUser.Address.HouseNumber, response.Result.HouseNumber);
        }

        [TestMethod]
        public void GetUserNotFoundTest()
        {
            var testGuid = Guid.NewGuid();
            var userRequest = new GetUserRequest(testGuid);

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.FindBy(testGuid)).Returns((Domain.User.User) null);

            var mockMapper = new Mock<IUserDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.User.User>>();

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.GetUser(userRequest);

            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ResourceNotFoundException), response.Exception.GetType());
        }

        [TestMethod]
        public void GetUsersTest()
        {
            var usersRequest = new GetUsersRequest();

            var user1 = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Address = new Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var user2 = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = "test2",
                LastName = "test2",
                Address = new Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street2",
                    HouseNumber = "221BB"
                }
            };

            var users = new List<Domain.User.User> {
                user1,
                user2
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Find(It.IsAny<UserQueryParam>())).Returns(users);

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDTO(users)).Returns(new List<UserDTO> {
                new UserDTO {
                    Id = user1.Id,
                    FirstName = user1.FirstName,
                    LastName = user1.LastName,
                    Country = user1.Address.Country,
                    City = user1.Address.City,
                    Street = user1.Address.Street,
                    HouseNumber = user1.Address.HouseNumber
                },
                new UserDTO {
                    Id = user2.Id,
                    FirstName = user2.FirstName,
                    LastName = user2.LastName,
                    Country = user2.Address.Country,
                    City = user2.Address.City,
                    Street = user2.Address.Street,
                    HouseNumber = user2.Address.HouseNumber
                }
            });

            var mockValidator = new Mock<IValidator<Domain.User.User>>();

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.GetUsers(usersRequest);

            Assert.IsNull(response.Exception);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(2, response.Result.Count());

            var firstDto = response.Result.First();

            Assert.AreEqual(user1.Id, firstDto.Id);

            var lastDto = response.Result.Last();

            Assert.AreEqual(user2.Id, lastDto.Id);
        }

        [TestMethod]
        public void InsertUserTest()
        {
            var userRequest = new InsertUserRequest {
                UserProperties = new UserEditDTO {
                    FirstName = "test",
                    LastName = "test",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUser = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = userRequest.UserProperties.FirstName,
                LastName = userRequest.UserProperties.LastName,
                Address = new Address {
                    Country = userRequest.UserProperties.Country,
                    City = userRequest.UserProperties.City,
                    Street = userRequest.UserProperties.Street,
                    HouseNumber = userRequest.UserProperties.HouseNumber
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Insert(expectedUser));

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDomainObject(userRequest.UserProperties)).Returns(expectedUser);

            var mockValidator = new Mock<IValidator<Domain.User.User>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedUser)).Returns(new List<BusinessRule>());

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.InsertUser(userRequest);

            Assert.IsTrue(response.Result);
            Assert.IsNull(response.Exception);
            mockRepo.Verify(x => x.Insert(expectedUser), Times.Once);
            mockUow.Verify(x => x.Commit(), Times.Once);
        }

        [TestMethod]
        public void InsertUserInvalidTest()
        {
            var userRequest = new InsertUserRequest {
                UserProperties = new UserEditDTO {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUser = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = userRequest.UserProperties.FirstName,
                LastName = userRequest.UserProperties.LastName,
                Address = new Address {
                    Country = userRequest.UserProperties.Country,
                    City = userRequest.UserProperties.City,
                    Street = userRequest.UserProperties.Street,
                    HouseNumber = userRequest.UserProperties.HouseNumber
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Insert(expectedUser));

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDomainObject(userRequest.UserProperties)).Returns(expectedUser);

            var mockValidator = new Mock<IValidator<Domain.User.User>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedUser)).Returns(new List<BusinessRule> {
                UserBusinessRules.UserNameRequired
            });

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.InsertUser(userRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ValidationException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(expectedUser), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void InsertUserInvalidAddressTest()
        {
            var userRequest = new InsertUserRequest {
                UserProperties = new UserEditDTO {
                    FirstName = "test",
                    LastName = "test",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUser = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = userRequest.UserProperties.FirstName,
                LastName = userRequest.UserProperties.LastName,
                Address = new Address {
                    Country = userRequest.UserProperties.Country,
                    City = userRequest.UserProperties.City,
                    Street = userRequest.UserProperties.Street,
                    HouseNumber = userRequest.UserProperties.HouseNumber
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Insert(expectedUser));

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDomainObject(userRequest.UserProperties)).Returns(expectedUser);

            var mockValidator = new Mock<IValidator<Domain.User.User>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedUser)).Returns(new List<BusinessRule> {
                AddressBusinessRules.AddressCountryRequired
            });

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.InsertUser(userRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ValidationException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(expectedUser), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var testGuid = Guid.NewGuid();

            var userRequest = new UpdateUserRequest(testGuid) {
                UserProperties = new UserEditDTO {
                    FirstName = "test2",
                    LastName = "test2",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUser = new Domain.User.User {
                Id = testGuid,
                FirstName = "test",
                LastName = "test",
                Address = new Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUpdatedUser = expectedUser;
            expectedUpdatedUser.FirstName = userRequest.UserProperties.FirstName;
            expectedUpdatedUser.LastName = userRequest.UserProperties.LastName;

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Update(expectedUser));
            mockRepo.Setup(x => x.FindBy(userRequest.Id)).Returns(expectedUser);

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.PopulateDomainObject(expectedUser, userRequest.UserProperties))
                .Returns(expectedUpdatedUser);

            var mockValidator = new Mock<IValidator<Domain.User.User>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedUser)).Returns(new List<BusinessRule>());

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.UpdateUser(userRequest);

            Assert.IsTrue(response.Result);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(userRequest.UserProperties.FirstName, expectedUpdatedUser.FirstName);
            Assert.AreEqual(userRequest.UserProperties.LastName, expectedUpdatedUser.LastName);
            mockRepo.Verify(x => x.Update(expectedUser), Times.Once);
            mockUow.Verify(x => x.Commit(), Times.Once);
        }

        [TestMethod]
        public void UpdateUserInvalidTest()
        {
            var testGuid = Guid.NewGuid();

            var userRequest = new UpdateUserRequest(testGuid) {
                UserProperties = new UserEditDTO {
                    FirstName = null,
                    LastName = null,
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUser = new Domain.User.User {
                Id = testGuid,
                FirstName = "test",
                LastName = "test",
                Address = new Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUpdatedUser = expectedUser;
            expectedUpdatedUser.FirstName = userRequest.UserProperties.FirstName;
            expectedUpdatedUser.LastName = userRequest.UserProperties.LastName;

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Update(expectedUser));
            mockRepo.Setup(x => x.FindBy(userRequest.Id)).Returns(expectedUser);

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.PopulateDomainObject(expectedUser, userRequest.UserProperties))
                .Returns(expectedUpdatedUser);

            var mockValidator = new Mock<IValidator<Domain.User.User>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedUser)).Returns(new List<BusinessRule> {
                UserBusinessRules.UserNameRequired
            });

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.UpdateUser(userRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ValidationException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(expectedUser), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void UpdateUserInvalidAddressTest()
        {
            var testGuid = Guid.NewGuid();

            var userRequest = new UpdateUserRequest(testGuid) {
                UserProperties = new UserEditDTO {
                    FirstName = "test2",
                    LastName = "test2",
                    Country = null,
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUser = new Domain.User.User {
                Id = testGuid,
                FirstName = "test",
                LastName = "test",
                Address = new Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var expectedUpdatedUser = expectedUser;
            expectedUpdatedUser.FirstName = userRequest.UserProperties.FirstName;
            expectedUpdatedUser.LastName = userRequest.UserProperties.LastName;
            expectedUpdatedUser.Address.Country = userRequest.UserProperties.Country;

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Update(expectedUser));
            mockRepo.Setup(x => x.FindBy(userRequest.Id)).Returns(expectedUser);

            var mockMapper = new Mock<IUserDtoMapper>();
            mockMapper.Setup(x => x.PopulateDomainObject(expectedUser, userRequest.UserProperties))
                .Returns(expectedUpdatedUser);

            var mockValidator = new Mock<IValidator<Domain.User.User>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedUser)).Returns(new List<BusinessRule> {
                AddressBusinessRules.AddressCountryRequired
            });

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.UpdateUser(userRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ValidationException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(expectedUser), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void UpdateUserNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var userRequest = new UpdateUserRequest(testGuid) {
                UserProperties = new UserEditDTO {
                    FirstName = null,
                    LastName = null,
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.FindBy(userRequest.Id)).Returns((Domain.User.User) null);

            var mockMapper = new Mock<IUserDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.User.User>>();

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.UpdateUser(userRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ResourceNotFoundException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(It.IsAny<Domain.User.User>()), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var testGuid = Guid.NewGuid();

            var usersRequest = new DeleteUserRequest(testGuid);

            var expectedUser = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Address = new Address {
                    Country = "UK"
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.Delete(expectedUser));
            mockRepo.Setup(x => x.FindBy(usersRequest.Id)).Returns(expectedUser);

            var mockMapper = new Mock<IUserDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.User.User>>();

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.DeleteUser(usersRequest);

            Assert.IsTrue(response.Result);
            Assert.IsNull(response.Exception);
            mockRepo.Verify(x => x.Delete(expectedUser), Times.Once);
            mockUow.Verify(x => x.Commit(), Times.Once);
        }

        [TestMethod]
        public void DeleteUserNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var usersRequest = new DeleteUserRequest(testGuid);

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.FindBy(usersRequest.Id)).Returns((Domain.User.User) null);

            var mockMapper = new Mock<IUserDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.User.User>>();

            var service = new UserService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.DeleteUser(usersRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ResourceNotFoundException), response.Exception.GetType());
        }
    }
}