using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Domain.Validation;

namespace SampleApp.Common.Domain.Tests.BusinessRules
{
    [TestClass()]
    public class BusinessRuleCollectionTests
    {
        [TestMethod()]
        public void AddRulesTest()
        {
            var rule = new BusinessRule
            {
                Description = "test"
            };

            var rules = new BusinessRuleCollection();

            rules.AddRule(rule);

            Assert.IsTrue(rules.HasRules());
        }

        [TestMethod()]
        public void ClearRulesTest()
        {
            var rule = new BusinessRule
            {
                Description = "test"
            };

            var rules = new BusinessRuleCollection();

            rules.AddRule(rule);

            rules.ClearRules();

            Assert.IsFalse(rules.HasRules());
        }

        [TestMethod()]
        public void HasRulesTest()
        {
            var rule = new BusinessRule
            {
                Description = "test"
            };

            var rules = new BusinessRuleCollection();

            Assert.IsFalse(rules.HasRules());

            rules.AddRule(rule);

            Assert.IsTrue(rules.HasRules());
        }

        [TestMethod()]
        public void GetRulesSummaryTest()
        {
            var rule1 = new BusinessRule
            {
                Description = "test1"
            };

            var rule2 = new BusinessRule
            {
                Description = "test2"
            };

            var rules = new BusinessRuleCollection();

            rules.AddRule(rule1);
            rules.AddRule(rule2);

            var expected = "test1" + Environment.NewLine + "test2";

            Assert.AreEqual(expected, string.Join(Environment.NewLine, rules.GetRulesSummary()));
        }
    }
}