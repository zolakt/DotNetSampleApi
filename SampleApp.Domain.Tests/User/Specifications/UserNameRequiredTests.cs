using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Domain.User;
using SampleApp.Domain.User.Specifications;

namespace SampleApp.Domain.Tests.User.Specifications
{
    [TestClass]
    public class UserNameRequiredTests
    {
        [TestMethod]
        public void GetBrokenRulesValidInputTest()
        {
            var user = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Address = new Domain.Address.Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var spec = new UserNameRequired();
            var broken = spec.GetBrokenRules(user);

            Assert.IsTrue(!broken.Any());
        }

        [TestMethod]
        public void GetBrokenRulesMissingFirstNameTest()
        {
            var address = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = null,
                LastName = "test",
                Address = new Domain.Address.Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var spec = new UserNameRequired();
            var broken = spec.GetBrokenRules(address);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(UserBusinessRules.UserNameRequired.Description, broken.First().Description);
        }

        [TestMethod]
        public void GetBrokenRulesMissingLastNameTest()
        {
            var address = new Domain.User.User {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = null,
                Address = new Domain.Address.Address {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var spec = new UserNameRequired();
            var broken = spec.GetBrokenRules(address);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(UserBusinessRules.UserNameRequired.Description, broken.First().Description);
        }
    }
}