using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.DAL.Memory.DataContext;
using SampleApp.DAL.Memory.DbModels;
using SampleApp.DAL.Memory.Mapper.Task;
using SampleApp.DAL.Memory.Repositories;
using SampleApp.Domain.Address;
using SampleApp.Domain.Task.QueryParams;

namespace SampleApp.DAL.Memory.Tests.Repositories
{
    [TestClass]
    public class TaskRepositoryTests
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

            var task1 = new Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = user1,
                UserId = user1.Id
            };

            var task2 = new Task {
                Id = Guid.NewGuid(),
                Name = "test2",
                Time = DateTime.Now,
                User = user2,
                UserId = user2.Id
            };

            var tasks = new List<Task> {
                task1,
                task2
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockContext = new Mock<IDomainContext>();
            mockContext.Setup(x => x.Tasks).Returns(tasks);

            var mockFactory = new Mock<IDataContextFactory<IDomainContext>>();
            mockFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockMapper = new Mock<ITaskDatabaseMapper>();
            mockMapper.Setup(x => x.ConvertToDomainType(task1, mockContext.Object))
                .Returns(new Domain.Task.Task {
                    Id = task1.Id,
                    Name = task1.Name,
                    Time = task1.Time,
                    User = new Domain.User.User {
                        Id = task1.User.Id,
                        FirstName = task1.User.FirstName,
                        LastName = task1.User.LastName,
                        Address = new Address {
                            Country = task1.User.Country,
                            City = task1.User.City,
                            Street = task1.User.Street,
                            HouseNumber = task1.User.HouseNumber
                        }
                    }
                });

            mockMapper.Setup(x => x.ConvertToDomainType(task2, mockContext.Object))
                .Returns(new Domain.Task.Task {
                    Id = task2.Id,
                    Name = task2.Name,
                    Time = task2.Time,
                    User = new Domain.User.User {
                        Id = task2.User.Id,
                        FirstName = task2.User.FirstName,
                        LastName = task2.User.LastName,
                        Address = new Address {
                            Country = task2.User.Country,
                            City = task2.User.City,
                            Street = task2.User.Street,
                            HouseNumber = task2.User.HouseNumber
                        }
                    }
                });

            var repo = new TaskRepository(mockUow.Object, mockFactory.Object, mockMapper.Object);

            var queryParams = new TaskQueryParam();

            var result = repo.Find(queryParams);

            Assert.AreEqual(2, result.Count());

            var first = result.First();
            Assert.AreEqual(task1.Id, first.Id);
            Assert.AreEqual(task1.Name, first.Name);
            Assert.AreEqual(task1.Time, first.Time);
            Assert.AreEqual(task1.User.FirstName, first.User.FirstName);
            Assert.AreEqual(task1.User.LastName, first.User.LastName);
            Assert.AreEqual(task1.User.Country, first.User.Address.Country);
            Assert.AreEqual(task1.User.City, first.User.Address.City);
            Assert.AreEqual(task1.User.Street, first.User.Address.Street);
            Assert.AreEqual(task1.User.HouseNumber, first.User.Address.HouseNumber);

            var last = result.Last();
            Assert.AreEqual(task2.Id, last.Id);
            Assert.AreEqual(task2.Name, last.Name);
            Assert.AreEqual(task2.Time, last.Time);
            Assert.AreEqual(task2.User.FirstName, last.User.FirstName);
            Assert.AreEqual(task2.User.LastName, last.User.LastName);
            Assert.AreEqual(task2.User.Country, last.User.Address.Country);
            Assert.AreEqual(task2.User.City, last.User.Address.City);
            Assert.AreEqual(task2.User.Street, last.User.Address.Street);
            Assert.AreEqual(task2.User.HouseNumber, last.User.Address.HouseNumber);
        }

        [TestMethod]
        public void FindByUserIdTest()
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

            var task1 = new Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = user1,
                UserId = user1.Id
            };

            var task2 = new Task {
                Id = Guid.NewGuid(),
                Name = "test2",
                Time = DateTime.Now,
                User = user2,
                UserId = user2.Id
            };

            var tasks = new List<Task> {
                task1,
                task2
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockContext = new Mock<IDomainContext>();
            mockContext.Setup(x => x.Tasks).Returns(tasks);

            var mockFactory = new Mock<IDataContextFactory<IDomainContext>>();
            mockFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockMapper = new Mock<ITaskDatabaseMapper>();
            mockMapper.Setup(x => x.ConvertToDomainType(task2, mockContext.Object))
                .Returns(new Domain.Task.Task {
                    Id = task2.Id,
                    Name = task2.Name,
                    Time = task2.Time,
                    User = new Domain.User.User {
                        Id = task2.User.Id,
                        FirstName = task2.User.FirstName,
                        LastName = task2.User.LastName,
                        Address = new Address {
                            Country = task2.User.Country,
                            City = task2.User.City,
                            Street = task2.User.Street,
                            HouseNumber = task2.User.HouseNumber
                        }
                    }
                });

            var repo = new TaskRepository(mockUow.Object, mockFactory.Object, mockMapper.Object);

            var queryParams = new TaskQueryParam {
                Filter = new TaskQueryParam.FilterOptions {
                    UserId = user2.Id
                }
            };

            var result = repo.Find(queryParams);

            Assert.AreEqual(1, result.Count());

            var first = result.First();
            Assert.AreEqual(task2.Id, first.Id);
            Assert.AreEqual(task2.Name, first.Name);
            Assert.AreEqual(task2.Time, first.Time);
            Assert.AreEqual(task2.User.FirstName, first.User.FirstName);
            Assert.AreEqual(task2.User.LastName, first.User.LastName);
            Assert.AreEqual(task2.User.Country, first.User.Address.Country);
            Assert.AreEqual(task2.User.City, first.User.Address.City);
            Assert.AreEqual(task2.User.Street, first.User.Address.Street);
            Assert.AreEqual(task2.User.HouseNumber, first.User.Address.HouseNumber);
        }
    }
}