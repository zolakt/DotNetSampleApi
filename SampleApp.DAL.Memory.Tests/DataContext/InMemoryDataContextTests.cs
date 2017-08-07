using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.DAL.Memory.DataContext;
using SampleApp.DAL.Memory.DataContext.Initializer;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.Tests.DataContext
{
    [TestClass]
    public class InMemoryDataContextTests
    {
        [TestMethod]
        public void GetAllEntitiesTest()
        {
            var userList = new List<User> {
                new User {
                    Id = Guid.NewGuid()
                },
                new User {
                    Id = Guid.NewGuid()
                }
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var result = context.GetAllEntities<User>();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetAllEntitiesInvalidTypeTest()
        {
            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(new List<User>());

            var context = new InMemoryDataContext(mockInitialiter.Object);

            context.GetAllEntities<string>();
        }

        [TestMethod]
        public void GetEntityTest()
        {
            var testGuid = Guid.NewGuid();
            var testName = "test2";

            var userList = new List<User> {
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = "test1"
                },
                new User {
                    Id = testGuid,
                    FirstName = testName
                }
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var result = context.GetEntity<User>(testGuid);

            Assert.AreEqual(testGuid, result.Id);
            Assert.AreEqual(testName, result.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetEntityInvalidTypeTest()
        {
            var testGuid = Guid.NewGuid();
            var testName = "test2";

            var userList = new List<User> {
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = "test1"
                },
                new User {
                    Id = testGuid,
                    FirstName = testName
                }
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            context.GetEntity<string>(testGuid);
        }

        [TestMethod]
        public void GetEntityNotFoundTest()
        {
            var testGuid = Guid.NewGuid();
            var testName = "test2";

            var userList = new List<User> {
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = "test1"
                },
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = testName
                }
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var result = context.GetEntity<User>(testGuid);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddEntityTest()
        {
            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(new List<User>());

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var testObj = new User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Country = "UK"
            };

            context.AddEntity(testObj);

            Assert.AreEqual(1, context.Users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddEntityInvalidTypeTest()
        {
            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(new List<User>());

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var testObj = "test";

            context.AddEntity(testObj);
        }

        [TestMethod]
        public void UpdateEntityTest()
        {
            var expectedName = "test3";

            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test1"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2"
            };

            var userList = new List<User> {
                user1,
                user2
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var user3 = new User {
                Id = user2.Id,
                FirstName = expectedName
            };

            context.UpdateEntity(user3);

            var last = context.Users.Last();

            Assert.AreEqual(2, context.Users.Count);
            Assert.AreEqual(user2.Id, last.Id);
            Assert.AreEqual(expectedName, last.FirstName);
        }

        [TestMethod]
        public void UpdateEntityNotFoundTest()
        {
            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test1"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2"
            };

            var userList = new List<User> {
                user1,
                user2
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var user3 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test3"
            };

            context.UpdateEntity(user3);

            Assert.AreEqual(3, context.Users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateEntityInvalidTypeTest()
        {
            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test1"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2"
            };

            var userList = new List<User> {
                user1,
                user2
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var user3 = "test3";

            context.UpdateEntity(user3);
        }

        [TestMethod]
        public void DeleteEntityTest()
        {
            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test1"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2"
            };

            var userList = new List<User> {
                user1,
                user2
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var user3 = new User {
                Id = user1.Id,
                FirstName = "test3"
            };

            context.DeleteEntity(user3);

            Assert.AreEqual(1, context.Users.Count);
        }

        [TestMethod]
        public void DeleteEntityNotFoundTest()
        {
            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test1"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2"
            };

            var userList = new List<User> {
                user1,
                user2
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var user3 = new User {
                Id = Guid.NewGuid(),
                FirstName = "user3"
            };

            context.DeleteEntity(user3);

            Assert.AreEqual(2, context.Users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteEntityInvalidTypeTest()
        {
            var user1 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test1"
            };

            var user2 = new User {
                Id = Guid.NewGuid(),
                FirstName = "test2"
            };

            var userList = new List<User> {
                user1,
                user2
            };

            var mockInitialiter = new Mock<IDatabaseInitializer>();
            mockInitialiter.Setup(x => x.InitializeTasks()).Returns(new List<Task>());
            mockInitialiter.Setup(x => x.InitializeUsers()).Returns(userList);

            var context = new InMemoryDataContext(mockInitialiter.Object);

            var user3 = "test";

            context.DeleteEntity(user3);
        }
    }
}