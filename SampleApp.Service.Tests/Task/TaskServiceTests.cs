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
using SampleApp.Domain.Task;
using SampleApp.Domain.Task.QueryParams;
using SampleApp.Service.Task;
using SampleApp.Service.Task.DTO;
using SampleApp.Service.Task.Mapper;
using SampleApp.Service.Task.Messaging;

namespace SampleApp.Service.Tests.Task
{
    [TestClass]
    public class TaskServiceTests
    {
        [TestMethod]
        public void GetTaskTest()
        {
            var testGuid = Guid.NewGuid();
            var taskRequest = new GetTaskRequest(testGuid);

            var expectedTask = new Domain.Task.Task {
                Id = testGuid,
                Name = "test",
                Time = DateTime.Now,
                User = new Domain.User.User {
                    Id = Guid.NewGuid(),
                    FirstName = "test",
                    LastName = "test",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.FindBy(testGuid)).Returns(expectedTask);

            var mockMapper = new Mock<ITaskDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDTO(expectedTask)).Returns(new TaskDTO {
                Id = expectedTask.Id,
                Name = expectedTask.Name,
                Time = expectedTask.Time,
                UserId = expectedTask.User.Id,
                UserFirstName = expectedTask.User.FirstName,
                UserLastName = expectedTask.User.LastName
            });

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.GetTask(taskRequest);

            Assert.IsNull(response.Exception);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(expectedTask.Id, response.Result.Id);
            Assert.AreEqual(expectedTask.User.Id, response.Result.UserId);
        }

        [TestMethod]
        public void GetTaskNotFoundTest()
        {
            var testGuid = Guid.NewGuid();
            var taskRequest = new GetTaskRequest(testGuid);

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.FindBy(testGuid)).Returns((Domain.Task.Task) null);

            var mockMapper = new Mock<ITaskDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.GetTask(taskRequest);

            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ResourceNotFoundException), response.Exception.GetType());
        }

        [TestMethod]
        public void GetTasksTest()
        {
            var tasksRequest = new GetTasksRequest();

            var task1 = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = new Domain.User.User {
                    Id = Guid.NewGuid(),
                    FirstName = "test",
                    LastName = "test",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var task2 = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = "test2",
                Time = DateTime.Now,
                User = new Domain.User.User {
                    Id = Guid.NewGuid(),
                    FirstName = "test2",
                    LastName = "test2",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var tasks = new List<Domain.Task.Task> {
                task1,
                task2
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Find(It.IsAny<TaskQueryParam>())).Returns(tasks);

            var mockMapper = new Mock<ITaskDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDTO(tasks)).Returns(new List<TaskDTO> {
                new TaskDTO {
                    Id = task1.Id,
                    Name = task1.Name,
                    Time = task1.Time,
                    UserId = task1.User.Id,
                    UserFirstName = task1.User.FirstName,
                    UserLastName = task1.User.LastName
                },
                new TaskDTO {
                    Id = task2.Id,
                    Name = task2.Name,
                    Time = task2.Time,
                    UserId = task2.User.Id,
                    UserFirstName = task2.User.FirstName,
                    UserLastName = task2.User.LastName
                }
            });

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.GetTasks(tasksRequest);

            Assert.IsNull(response.Exception);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(2, response.Result.Count());

            var firstDto = response.Result.First();

            Assert.AreEqual(task1.Id, firstDto.Id);
            Assert.AreEqual(task1.User.Id, firstDto.UserId);

            var lastDto = response.Result.Last();

            Assert.AreEqual(task2.Id, lastDto.Id);
            Assert.AreEqual(task2.User.Id, lastDto.UserId);
        }

        [TestMethod]
        public void InsertTaskTest()
        {
            var taskRequest = new InsertTaskRequest {
                TaskProperties = new TaskEditDTO {
                    Name = "test",
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedTask = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = taskRequest.TaskProperties.Name,
                Time = taskRequest.TaskProperties.Time,
                User = new Domain.User.User {
                    Id = taskRequest.TaskProperties.UserId,
                    FirstName = "test",
                    LastName = "test",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Insert(expectedTask));

            var mockMapper = new Mock<ITaskDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDomainObject(taskRequest.TaskProperties)).Returns(expectedTask);

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedTask)).Returns(new List<BusinessRule>());

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.InsertTask(taskRequest);

            Assert.IsTrue(response.Result);
            Assert.IsNull(response.Exception);
            mockRepo.Verify(x => x.Insert(expectedTask), Times.Once);
            mockUow.Verify(x => x.Commit(), Times.Once);
        }

        [TestMethod]
        public void InsertTaskInvalidTest()
        {
            var taskRequest = new InsertTaskRequest {
                TaskProperties = new TaskEditDTO {
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedTask = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = taskRequest.TaskProperties.Name,
                Time = taskRequest.TaskProperties.Time,
                User = new Domain.User.User {
                    Id = taskRequest.TaskProperties.UserId,
                    FirstName = "test",
                    LastName = "test",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Insert(expectedTask));

            var mockMapper = new Mock<ITaskDtoMapper>();
            mockMapper.Setup(x => x.ConvertToDomainObject(taskRequest.TaskProperties)).Returns(expectedTask);

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedTask)).Returns(new List<BusinessRule> {
                TaskBusinessRules.TaskNameRequired
            });

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.InsertTask(taskRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ValidationException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(expectedTask), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void UpdateTaskTest()
        {
            var testGuid = Guid.NewGuid();

            var taskRequest = new UpdateTaskRequest(testGuid) {
                TaskProperties = new TaskEditDTO {
                    Name = "test2",
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedTask = new Domain.Task.Task {
                Id = testGuid,
                Name = "test",
                Time = taskRequest.TaskProperties.Time,
                User = new Domain.User.User {
                    Id = taskRequest.TaskProperties.UserId,
                    FirstName = "test",
                    LastName = "test",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var expectedUpdatedTask = expectedTask;
            expectedUpdatedTask.Name = taskRequest.TaskProperties.Name;

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Update(expectedTask));
            mockRepo.Setup(x => x.FindBy(taskRequest.Id)).Returns(expectedTask);

            var mockMapper = new Mock<ITaskDtoMapper>();
            mockMapper.Setup(x => x.PopulateDomainObject(expectedTask, taskRequest.TaskProperties))
                .Returns(expectedUpdatedTask);

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedTask)).Returns(new List<BusinessRule>());

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.UpdateTask(taskRequest);

            Assert.IsTrue(response.Result);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(taskRequest.TaskProperties.Name, expectedUpdatedTask.Name);
            mockRepo.Verify(x => x.Update(expectedTask), Times.Once);
            mockUow.Verify(x => x.Commit(), Times.Once);
        }

        [TestMethod]
        public void UpdateTaskInvalidTest()
        {
            var testGuid = Guid.NewGuid();

            var taskRequest = new UpdateTaskRequest(testGuid) {
                TaskProperties = new TaskEditDTO {
                    Name = null,
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var expectedTask = new Domain.Task.Task {
                Id = testGuid,
                Name = "test",
                Time = taskRequest.TaskProperties.Time,
                User = new Domain.User.User {
                    Id = taskRequest.TaskProperties.UserId,
                    FirstName = "test",
                    LastName = "test",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var expectedUpdatedTask = expectedTask;
            expectedUpdatedTask.Name = taskRequest.TaskProperties.Name;

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Update(expectedTask));
            mockRepo.Setup(x => x.FindBy(taskRequest.Id)).Returns(expectedTask);

            var mockMapper = new Mock<ITaskDtoMapper>();
            mockMapper.Setup(x => x.PopulateDomainObject(expectedTask, taskRequest.TaskProperties))
                .Returns(expectedUpdatedTask);

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedTask)).Returns(new List<BusinessRule> {
                TaskBusinessRules.TaskNameRequired
            });

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.UpdateTask(taskRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ValidationException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(expectedTask), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void UpdateTaskNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var taskRequest = new UpdateTaskRequest(testGuid)
            {
                TaskProperties = new TaskEditDTO
                {
                    Name = "test",
                    Time = DateTime.Now,
                    UserId = Guid.NewGuid()
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.FindBy(taskRequest.Id)).Returns((Domain.Task.Task) null);

            var mockMapper = new Mock<ITaskDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.UpdateTask(taskRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ResourceNotFoundException), response.Exception.GetType());
            mockRepo.Verify(x => x.Insert(It.IsAny<Domain.Task.Task>()), Times.Never);
            mockUow.Verify(x => x.Commit(), Times.Never);
        }

        [TestMethod]
        public void DeleteTaskTest()
        {
            var testGuid = Guid.NewGuid();

            var taskRequest = new DeleteTaskRequest(testGuid);

            var expectedTask = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = new Domain.User.User {
                    Id = Guid.NewGuid(),
                    FirstName = "test",
                    LastName = "test",
                    Address = new Address {
                        Country = "UK"
                    }
                }
            };

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Delete(expectedTask));
            mockRepo.Setup(x => x.FindBy(taskRequest.Id)).Returns(expectedTask);

            var mockMapper = new Mock<ITaskDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();
            mockValidator.Setup(x => x.GetBrokenRules(expectedTask)).Returns(new List<BusinessRule>());

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.DeleteTask(taskRequest);

            Assert.IsTrue(response.Result);
            Assert.IsNull(response.Exception);
            mockRepo.Verify(x => x.Delete(expectedTask), Times.Once);
            mockUow.Verify(x => x.Commit(), Times.Once);
        }

        [TestMethod]
        public void DeleteTaskNotFoundTest()
        {
            var testGuid = Guid.NewGuid();

            var taskRequest = new DeleteTaskRequest(testGuid);

            var mockUow = new Mock<IUnitOfWork>();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.FindBy(taskRequest.Id)).Returns((Domain.Task.Task) null);

            var mockMapper = new Mock<ITaskDtoMapper>();

            var mockValidator = new Mock<IValidator<Domain.Task.Task>>();

            var service = new TaskService(mockUow.Object, mockRepo.Object, mockMapper.Object, mockValidator.Object);

            var response = service.DeleteTask(taskRequest);

            Assert.IsFalse(response.Result);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual(typeof(ResourceNotFoundException), response.Exception.GetType());
        }
    }
}