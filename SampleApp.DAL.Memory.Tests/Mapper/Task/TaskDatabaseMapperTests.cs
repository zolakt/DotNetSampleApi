using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.DAL.Memory.Mapper.Task;
using SampleApp.DAL.Memory.Mapper.User;
using SampleApp.Domain.Address;

namespace SampleApp.DAL.Memory.Tests.Mapper.Task
{
    [TestClass]
    public class TaskDatabaseMapperTests
    {
        [TestMethod]
        public void ConvertToDatabaseTypeTest()
        {
            var domainUser = new Domain.User.User {
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

            var domainTask = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = domainUser
            };

            var userMapper = new Mock<IUserDatabaseMapper>();

            var mapper = new TaskDatabaseMapper(userMapper.Object);

            var result = mapper.ConvertToDatabaseType(domainTask);

            Assert.AreEqual(domainTask.Id, result.Id);
            Assert.AreEqual(domainTask.Name, result.Name);
            Assert.AreEqual(domainTask.Time, result.Time);
            Assert.AreEqual(domainTask.User.Id, result.UserId);
        }

        [TestMethod]
        public void ConvertToDomainTypeTest()
        {
            var databaseUser = new DbModels.User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var databaseTask = new DbModels.Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = databaseUser,
                UserId = databaseUser.Id
            };

            var mockUserMapper = new Mock<IUserDatabaseMapper>();

            var mockContext = new Mock<IDataContext>();

            var mapper = new TaskDatabaseMapper(mockUserMapper.Object);

            var result = mapper.ConvertToDomainType(databaseTask, mockContext.Object);

            Assert.AreEqual(databaseTask.Id, result.Id);
            Assert.AreEqual(databaseTask.Name, result.Name);
            Assert.AreEqual(databaseTask.Time, result.Time);
            Assert.AreEqual(databaseTask.User.Id, result.User.Id);
        }

        [TestMethod]
        public void ConvertToDomainTypeWithLoadedUserTest()
        {
            var databaseUser = new DbModels.User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var databaseTask = new DbModels.Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = databaseUser,
                UserId = databaseUser.Id
            };

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.IsLoaded(databaseTask, y => y.User)).Returns(true);

            var mockUserMapper = new Mock<IUserDatabaseMapper>();
            mockUserMapper.Setup(x => x.ConvertToDomainType(databaseTask.User, mockContext.Object))
                .Returns(new Domain.User.User {
                    Id = databaseTask.User.Id,
                    FirstName = databaseTask.User.FirstName,
                    LastName = databaseTask.User.LastName,
                    Address = new Address {
                        Country = databaseTask.User.Country,
                        City = databaseTask.User.City,
                        Street = databaseTask.User.Street,
                        HouseNumber = databaseTask.User.HouseNumber
                    }
                });

            var mapper = new TaskDatabaseMapper(mockUserMapper.Object);

            var result = mapper.ConvertToDomainType(databaseTask, mockContext.Object);

            Assert.AreEqual(databaseTask.Id, result.Id);
            Assert.AreEqual(databaseTask.Name, result.Name);
            Assert.AreEqual(databaseTask.Time, result.Time);
            Assert.AreEqual(databaseTask.User.Id, result.User.Id);
            Assert.AreEqual(databaseTask.User.FirstName, result.User.FirstName);
            Assert.AreEqual(databaseTask.User.LastName, result.User.LastName);
            Assert.AreEqual(databaseTask.User.Country, result.User.Address.Country);
            Assert.AreEqual(databaseTask.User.City, result.User.Address.City);
            Assert.AreEqual(databaseTask.User.Street, result.User.Address.Street);
            Assert.AreEqual(databaseTask.User.HouseNumber, result.User.Address.HouseNumber);
        }
    }
}