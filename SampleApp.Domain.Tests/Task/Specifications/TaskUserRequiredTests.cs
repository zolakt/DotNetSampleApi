using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Domain.Task;
using SampleApp.Domain.Task.Specifications;

namespace SampleApp.Domain.Tests.Task.Specifications
{
    [TestClass()]
    public class TaskUserRequiredTests
    {
        [TestMethod]
        public void GetBrokenRulesValidInputTest()
        {
            var task = new Domain.Task.Task
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = new Domain.User.User {
                    Id = Guid.NewGuid()
                }
            };

            var spec = new TaskUserRequired();
            var broken = spec.GetBrokenRules(task);

            Assert.IsTrue(!broken.Any());
        }

        [TestMethod]
        public void GetBrokenRulesMissingUserTest()
        {
            var task = new Domain.Task.Task
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now
            };

            var spec = new TaskUserRequired();
            var broken = spec.GetBrokenRules(task);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(TaskBusinessRules.TaskUserRequired.Description, broken.First().Description);
        }

        [TestMethod]
        public void GetBrokenRulesBlankUserTest()
        {
            var task = new Domain.Task.Task
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now,
                User = new Domain.User.User()
            };

            var spec = new TaskUserRequired();
            var broken = spec.GetBrokenRules(task);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(TaskBusinessRules.TaskUserRequired.Description, broken.First().Description);
        }
    }
}