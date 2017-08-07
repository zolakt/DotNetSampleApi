using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.Domain.Validation;
using SampleApp.Domain.Address;
using SampleApp.Domain.User;
using SampleApp.Domain.User.Specifications;
using SampleApp.Domain.User.Validator;

namespace SampleApp.Domain.Tests.User.Validator
{
    [TestClass()]
    public class UserValidatorTests
    {
        [TestMethod()]
        public void GetBrokenRulesValidAddressTest()
        {
            var user = new Domain.User.User
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Address = new Domain.Address.Address
                {
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var mockAddressValidator = new Mock<IValidator<Domain.Address.Address>>();
            mockAddressValidator.Setup(x => x.GetBrokenRules(user.Address)).Returns(new List<BusinessRule>());

            var validator = new UserValidator(new List<ISpecification<Domain.User.User>>(),
                mockAddressValidator.Object);

            var broken = validator.GetBrokenRules(user);

            Assert.IsTrue(!broken.Any());
        }

        [TestMethod()]
        public void GetBrokenRulesMissingAddressCountryTest()
        {
            var user = new Domain.User.User
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Address = new Domain.Address.Address
                {
                    Country = null,
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var mockAddressValidator = new Mock<IValidator<Domain.Address.Address>>();
            mockAddressValidator.Setup(x => x.GetBrokenRules(user.Address)).Returns(new List<BusinessRule> {
                AddressBusinessRules.AddressCountryRequired
            });

            var validator = new UserValidator(new List<ISpecification<Domain.User.User>>(),
                mockAddressValidator.Object);

            var broken = validator.GetBrokenRules(user);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(AddressBusinessRules.AddressCountryRequired.RuleDescription, broken.First().RuleDescription);
        }

        [TestMethod()]
        public void GetBrokenRulesMissingFirstNameAndAddressCountryTest()
        {
            var user = new Domain.User.User
            {
                Id = Guid.NewGuid(),
                FirstName = null,
                LastName = "test",
                Address = new Domain.Address.Address
                {
                    Country = null,
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221B"
                }
            };

            var mockAddressValidator = new Mock<IValidator<Domain.Address.Address>>();
            mockAddressValidator.Setup(x => x.GetBrokenRules(user.Address)).Returns(new List<BusinessRule> {
                AddressBusinessRules.AddressCountryRequired
            });

            var validator = new UserValidator(new List<ISpecification<Domain.User.User>> {
                new UserNameRequired()
            }, mockAddressValidator.Object);

            var broken = validator.GetBrokenRules(user);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(UserBusinessRules.UserNameRequired.RuleDescription, broken.First().RuleDescription);
        }
    }
}