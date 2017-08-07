using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Domain.Address;
using SampleApp.Service.Task.DTO;
using SampleApp.Service.Task.Mapper;

namespace SampleApp.Service.Tests.Task.Mapper
{
    [TestClass()]
    public class TaskDtoMapperTests
    {
        [TestMethod()]
        public void ConvertToDTOTest()
        {
            var user = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Address = new Address {
                    Country = "UK"
                }
            };

            var task = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = user
            };

            var mapper = new TaskDtoMapper();

            var result = mapper.ConvertToDTO(task);

            Assert.AreEqual(task.Id, result.Id);
            Assert.AreEqual(task.Name, result.Name);
            Assert.AreEqual(task.Time, result.Time);
            Assert.AreEqual(user.Id, result.UserId);
            Assert.AreEqual(user.FirstName, result.UserFirstName);
            Assert.AreEqual(user.LastName, result.UserLastName);
        }

        [TestMethod()]
        public void ConvertToDTOMutipleTest()
        {
            var user = new Domain.User.User
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Address = new Address
                {
                    Country = "UK"
                }
            };

            var task1 = new Domain.Task.Task
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = user
            };

            var task2 = new Domain.Task.Task
            {
                Id = Guid.NewGuid(),
                Name = "test2",
                Time = DateTime.Now,
                User = user
            };

            var tasks = new List<Domain.Task.Task> {
                task1,
                task2
            };

            var mapper = new TaskDtoMapper();

            var results = mapper.ConvertToDTO(tasks);

            Assert.AreEqual(2, results.Count());


            var firstDto = results.First();

            Assert.AreEqual(task1.Name, firstDto.Name);
            Assert.AreEqual(task1.Time, firstDto.Time);
            Assert.AreEqual(task1.User.Id, firstDto.UserId);
            Assert.AreEqual(task1.User.FirstName, firstDto.UserFirstName);
            Assert.AreEqual(task1.User.LastName, firstDto.UserLastName);


            var lastDto = results.Last();

            Assert.AreEqual(task2.Name, lastDto.Name);
            Assert.AreEqual(task2.Time, lastDto.Time);
            Assert.AreEqual(task2.User.Id, lastDto.UserId);
            Assert.AreEqual(task2.User.FirstName, lastDto.UserFirstName);
            Assert.AreEqual(task2.User.LastName, lastDto.UserLastName);
        }

        [TestMethod()]
        public void ConvertToDomainObjectTest()
        {
            var dto = new TaskEditDTO
            {
                Name = "test",
                Time = DateTime.Now,
                UserId = Guid.NewGuid()
            };

            var mapper = new TaskDtoMapper();

            var result = mapper.ConvertToDomainObject(dto);

            Assert.AreEqual(dto.Name, result.Name);
            Assert.AreEqual(dto.Time, result.Time);
            Assert.AreEqual(dto.UserId, result.User.Id);
        }

        [TestMethod()]
        public void PopulateDomainObjectTest()
        {
            var task = new Domain.Task.Task();

            var dto = new TaskEditDTO {
                Name = "test",
                Time = DateTime.Now,
                UserId = Guid.NewGuid()
            };

            var mapper = new TaskDtoMapper();

            var result = mapper.PopulateDomainObject(task, dto);

            Assert.AreEqual(dto.Name, result.Name);
            Assert.AreEqual(dto.Time, result.Time);
            Assert.AreEqual(dto.UserId, result.User.Id);
        }
    }
}