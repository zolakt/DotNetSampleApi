using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.DAL.Memory.Mapper.User;
using SampleApp.Domain.Address;

namespace SampleApp.DAL.Memory.Tests.Mapper.User
{
    [TestClass]
    public class UserDatabaseMapperTests
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

            var mapper = new UserDatabaseMapper();

            var result = mapper.ConvertToDatabaseType(domainUser);

            Assert.AreEqual(domainUser.Id, result.Id);
            Assert.AreEqual(domainUser.FirstName, result.FirstName);
            Assert.AreEqual(domainUser.LastName, result.LastName);
            Assert.AreEqual(domainUser.Address.Country, result.Country);
            Assert.AreEqual(domainUser.Address.City, result.City);
            Assert.AreEqual(domainUser.Address.Street, result.Street);
            Assert.AreEqual(domainUser.Address.HouseNumber, result.HouseNumber);
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

            var mockContext = new Mock<IDataContext>();

            var mapper = new UserDatabaseMapper();

            var result = mapper.ConvertToDomainType(databaseUser, mockContext.Object);

            Assert.AreEqual(databaseUser.Id, result.Id);
            Assert.AreEqual(databaseUser.FirstName, result.FirstName);
            Assert.AreEqual(databaseUser.LastName, result.LastName);
            Assert.AreEqual(databaseUser.Country, result.Address.Country);
            Assert.AreEqual(databaseUser.City, result.Address.City);
            Assert.AreEqual(databaseUser.Street, result.Address.Street);
            Assert.AreEqual(databaseUser.HouseNumber, result.Address.HouseNumber);
        }
    }
}