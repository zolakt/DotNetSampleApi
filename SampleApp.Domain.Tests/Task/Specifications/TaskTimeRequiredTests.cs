using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Domain.Task;
using SampleApp.Domain.Task.Specifications;

namespace SampleApp.Domain.Tests.Task.Specifications
{
    [TestClass()]
    public class TaskTimeRequiredTests
    {
        [TestMethod]
        public void GetBrokenRulesValidInputTest()
        {
            var task = new Domain.Task.Task
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = DateTime.Now
            };

            var spec = new TaskTimeRequired();
            var broken = spec.GetBrokenRules(task);

            Assert.IsTrue(!broken.Any());
        }

        [TestMethod]
        public void GetBrokenRulesMissingTimeTest()
        {
            var task = new Domain.Task.Task
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Time = null
            };

            var spec = new TaskTimeRequired();
            var broken = spec.GetBrokenRules(task);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(TaskBusinessRules.TaskTimeRequired.RuleDescription, broken.First().RuleDescription);
        }
    }
}