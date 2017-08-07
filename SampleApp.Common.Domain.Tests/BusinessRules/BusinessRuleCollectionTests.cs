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
            var rule = new BusinessRule("test");
            var rules = new BusinessRuleCollection();

            rules.AddRule(rule);

            Assert.IsTrue(rules.HasRules());
        }

        [TestMethod()]
        public void ClearRulesTest()
        {
            var rule = new BusinessRule("test");
            var rules = new BusinessRuleCollection();

            rules.AddRule(rule);

            rules.ClearRules();

            Assert.IsFalse(rules.HasRules());
        }

        [TestMethod()]
        public void HasRulesTest()
        {
            var rule = new BusinessRule("test");
            var rules = new BusinessRuleCollection();

            Assert.IsFalse(rules.HasRules());

            rules.AddRule(rule);

            Assert.IsTrue(rules.HasRules());
        }

        [TestMethod()]
        public void GetRulesSummaryTest()
        {
            var rule1 = new BusinessRule("test");
            var rule2 = new BusinessRule("test2");
            var rules = new BusinessRuleCollection();

            rules.AddRule(rule1);
            rules.AddRule(rule2);

            var expected = "test" + Environment.NewLine + "test2" + Environment.NewLine;

            Assert.AreEqual(expected, rules.GetRulesSummary());
        }
    }
}