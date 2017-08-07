using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.DAL.Memory.DataContext.Initializer;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.Tests.DataContext.Initializer
{
    [TestClass]
    public class DatabaseInitializerTests
    {
        [TestMethod]
        public void InitializeTasksTest()
        {
            var users = new List<User> {
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = "test1"
                },
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = "test2"
                }
            };

            var tasks = new List<Task> {
                new Task {
                    Id = Guid.NewGuid(),
                    Name = "test1"
                },
                new Task {
                    Id = Guid.NewGuid(),
                    Name = "test2"
                }
            };

            var mockUserInit = new Mock<IDatabaseUsersInitializer>();
            mockUserInit.Setup(x => x.Initialize()).Returns(users);

            var mockTaskInit = new Mock<IDatabaseTasksInitializer>();
            mockTaskInit.Setup(x => x.Initialize(users)).Returns(tasks);

            var initializer = new DatabaseInitializer(mockTaskInit.Object, mockUserInit.Object);

            var result = initializer.InitializeTasks();

            Assert.AreEqual(tasks, result);
        }

        [TestMethod]
        public void InitializeUsersTest()
        {
            var users = new List<User> {
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = "test1"
                },
                new User {
                    Id = Guid.NewGuid(),
                    FirstName = "test2"
                }
            };

            var tasks = new List<Task> {
                new Task {
                    Id = Guid.NewGuid(),
                    Name = "test1"
                },
                new Task {
                    Id = Guid.NewGuid(),
                    Name = "test2"
                }
            };

            var mockUserInit = new Mock<IDatabaseUsersInitializer>();
            mockUserInit.Setup(x => x.Initialize()).Returns(users);

            var mockTaskInit = new Mock<IDatabaseTasksInitializer>();
            mockTaskInit.Setup(x => x.Initialize(users)).Returns(tasks);

            var initializer = new DatabaseInitializer(mockTaskInit.Object, mockUserInit.Object);

            var result = initializer.InitializeUsers();

            Assert.AreEqual(users, result);
        }
    }
}