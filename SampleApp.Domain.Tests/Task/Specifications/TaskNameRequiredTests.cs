using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Domain.Task;
using SampleApp.Domain.Task.Specifications;

namespace SampleApp.Domain.Tests.Task.Specifications
{
    [TestClass]
    public class TaskNameRequiredTests
    {
        [TestMethod]
        public void GetBrokenRulesValidInputTest()
        {
            var task = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now
            };

            var spec = new TaskNameRequired();
            var broken = spec.GetBrokenRules(task);

            Assert.IsTrue(!broken.Any());
        }

        [TestMethod]
        public void GetBrokenRulesMissingNameTest()
        {
            var task = new Domain.Task.Task {
                Id = Guid.NewGuid(),
                Name = null,
                Time = DateTime.Now
            };

            var spec = new TaskNameRequired();
            var broken = spec.GetBrokenRules(task);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(TaskBusinessRules.TaskNameRequired.Description, broken.First().Description);
        }
    }
}