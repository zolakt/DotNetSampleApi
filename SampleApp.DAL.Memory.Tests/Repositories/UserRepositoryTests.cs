using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.DAL.Memory.DataContext;
using SampleApp.DAL.Memory.DbModels;
using SampleApp.DAL.Memory.Mapper.User;
using SampleApp.DAL.Memory.Repositories;
using SampleApp.Domain.Address;
using SampleApp.Domain.User.QueryParams;

namespace SampleApp.DAL.Memory.Tests.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void FindTest()
        {
            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker streer",
                HouseNumber = "221B"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2",
                LastName = "test2",
                Country = "US",
                City = "New York",
                Street = "Test street",
                HouseNumber = "123"
            };

            var users = new List<User> {
                user1,
                user2
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockContext = new Mock<IDomainContext>();
            mockContext.Setup(x => x.Users).Returns(users);

            var mockFactory = new Mock<IDataContextFactory<IDomainContext>>();
            mockFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockMapper = new Mock<IUserDatabaseMapper>();
            mockMapper.Setup(x => x.ConvertToDomainType(user1, mockContext.Object))
                .Returns(new Domain.User.User {
                    Id = user1.Id,
                    FirstName = user1.FirstName,
                    LastName = user1.LastName,
                    Address = new Address {
                        Country = user1.Country,
                        City = user1.City,
                        Street = user1.Street,
                        HouseNumber = user1.HouseNumber
                    }
                });

            mockMapper.Setup(x => x.ConvertToDomainType(user2, mockContext.Object))
                .Returns(new Domain.User.User {
                    Id = user2.Id,
                    FirstName = user2.FirstName,
                    LastName = user2.LastName,
                    Address = new Address {
                        Country = user2.Country,
                        City = user2.City,
                        Street = user2.Street,
                        HouseNumber = user2.HouseNumber
                    }
                });

            var repo = new UserRepository(mockUow.Object, mockFactory.Object, mockMapper.Object);

            var queryParams = new UserQueryParam();

            var result = repo.Find(queryParams);

            Assert.AreEqual(2, result.Count());

            var first = result.First();
            Assert.AreEqual(user1.Id, first.Id);
            Assert.AreEqual(user1.FirstName, first.FirstName);
            Assert.AreEqual(user1.LastName, first.LastName);
            Assert.AreEqual(user1.Country, first.Address.Country);
            Assert.AreEqual(user1.City, first.Address.City);
            Assert.AreEqual(user1.Street, first.Address.Street);
            Assert.AreEqual(user1.HouseNumber, first.Address.HouseNumber);

            var last = result.Last();
            Assert.AreEqual(user2.Id, last.Id);
            Assert.AreEqual(user2.FirstName, last.FirstName);
            Assert.AreEqual(user2.LastName, last.LastName);
            Assert.AreEqual(user2.Country, last.Address.Country);
            Assert.AreEqual(user2.City, last.Address.City);
            Assert.AreEqual(user2.Street, last.Address.Street);
            Assert.AreEqual(user2.HouseNumber, last.Address.HouseNumber);
        }

        [TestMethod]
        public void FindByCountryTest()
        {
            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Country = "UK",
                City = "London",
                Street = "Baker streer",
                HouseNumber = "221B"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2",
                LastName = "test2",
                Country = "US",
                City = "New York",
                Street = "Test street",
                HouseNumber = "123"
            };

            var users = new List<User> {
                user1,
                user2
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockContext = new Mock<IDomainContext>();
            mockContext.Setup(x => x.Users).Returns(users);

            var mockFactory = new Mock<IDataContextFactory<IDomainContext>>();
            mockFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockMapper = new Mock<IUserDatabaseMapper>();
            mockMapper.Setup(x => x.ConvertToDomainType(user2, mockContext.Object))
                .Returns(new Domain.User.User {
                    Id = user2.Id,
                    FirstName = user2.FirstName,
                    LastName = user2.LastName,
                    Address = new Address {
                        Country = user2.Country,
                        City = user2.City,
                        Street = user2.Street,
                        HouseNumber = user2.HouseNumber
                    }
                });

            var repo = new UserRepository(mockUow.Object, mockFactory.Object, mockMapper.Object);

            var queryParams = new UserQueryParam {
                Filter = new UserQueryParam.FilterOptions {
                    Country = "US"
                }
            };

            var result = repo.Find(queryParams);

            Assert.AreEqual(1, result.Count());

            var first = result.First();
            Assert.AreEqual(user2.Id, first.Id);
            Assert.AreEqual(user2.FirstName, first.FirstName);
            Assert.AreEqual(user2.LastName, first.LastName);
            Assert.AreEqual(user2.Country, first.Address.Country);
            Assert.AreEqual(user2.City, first.Address.City);
            Assert.AreEqual(user2.Street, first.Address.Street);
            Assert.AreEqual(user2.HouseNumber, first.Address.HouseNumber);
        }
    }
}