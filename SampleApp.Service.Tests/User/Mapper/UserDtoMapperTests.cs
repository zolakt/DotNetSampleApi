using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Domain.Address;
using SampleApp.Service.User.DTO;
using SampleApp.Service.User.Mapper;

namespace SampleApp.Service.Tests.User.Mapper
{
    [TestClass]
    public class UserDtoMapperTests
    {
        [TestMethod]
        public void ConvertToDTOTest()
        {
            var user = new Domain.User.User {
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

            var mapper = new UserDtoMapper();

            var result = mapper.ConvertToDTO(user);

            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Address.Country, result.Country);
            Assert.AreEqual(user.Address.City, result.City);
            Assert.AreEqual(user.Address.Street, result.Street);
            Assert.AreEqual(user.Address.HouseNumber, result.HouseNumber);
        }

        [TestMethod]
        public void ConvertToDTOMutipleTest()
        {
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
                    Country = "US",
                    City = "New York",
                    Street = "Test street",
                    HouseNumber = "123"
                }
            };

            var users = new List<Domain.User.User> {
                user1,
                user2
            };

            var mapper = new UserDtoMapper();

            var results = mapper.ConvertToDTO(users);

            Assert.AreEqual(2, results.Count());


            var firstDto = results.First();

            Assert.AreEqual(user1.Id, firstDto.Id);
            Assert.AreEqual(user1.FirstName, firstDto.FirstName);
            Assert.AreEqual(user1.LastName, firstDto.LastName);
            Assert.AreEqual(user1.Address.Country, firstDto.Country);
            Assert.AreEqual(user1.Address.City, firstDto.City);
            Assert.AreEqual(user1.Address.Street, firstDto.Street);
            Assert.AreEqual(user1.Address.HouseNumber, firstDto.HouseNumber);


            var lastDto = results.Last();

            Assert.AreEqual(user2.Id, lastDto.Id);
            Assert.AreEqual(user2.FirstName, lastDto.FirstName);
            Assert.AreEqual(user2.LastName, lastDto.LastName);
            Assert.AreEqual(user2.Address.Country, lastDto.Country);
            Assert.AreEqual(user2.Address.City, lastDto.City);
            Assert.AreEqual(user2.Address.Street, lastDto.Street);
            Assert.AreEqual(user2.Address.HouseNumber, lastDto.HouseNumber);
        }

        [TestMethod]
        public void ConvertToDomainObjectTest()
        {
            var dto = new UserEditDTO {
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var mapper = new UserDtoMapper();

            var result = mapper.ConvertToDomainObject(dto);

            Assert.AreEqual(dto.FirstName, result.FirstName);
            Assert.AreEqual(dto.LastName, result.LastName);
            Assert.AreEqual(dto.Country, result.Address.Country);
            Assert.AreEqual(dto.City, result.Address.City);
            Assert.AreEqual(dto.Street, result.Address.Street);
            Assert.AreEqual(dto.HouseNumber, result.Address.HouseNumber);
        }

        [TestMethod]
        public void PopulateDomainObjectTest()
        {
            var user = new Domain.User.User();

            var dto = new UserEditDTO {
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var mapper = new UserDtoMapper();

            var result = mapper.PopulateDomainObject(user, dto);

            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Address.Country, result.Address.Country);
            Assert.AreEqual(user.Address.City, result.Address.City);
            Assert.AreEqual(user.Address.Street, result.Address.Street);
            Assert.AreEqual(user.Address.HouseNumber, result.Address.HouseNumber);
        }
    }
}