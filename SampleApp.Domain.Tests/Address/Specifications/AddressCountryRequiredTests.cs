using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Domain.Address;
using SampleApp.Domain.Address.Specifications;

namespace SampleApp.Domain.Tests.Address.Specifications
{
    [TestClass]
    public class AddressCountryRequiredTests
    {
        [TestMethod]
        public void GetBrokenRulesValidInputTest()
        {
            var address = new Domain.Address.Address {
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var spec = new AddressCountryRequired();
            var broken = spec.GetBrokenRules(address);

            Assert.IsTrue(!broken.Any());
        }

        [TestMethod]
        public void GetBrokenRulesMissingCountryTest()
        {
            var address = new Domain.Address.Address {
                Country = null,
                City = "London",
                Street = "Baker street",
                HouseNumber = "221B"
            };

            var spec = new AddressCountryRequired();
            var broken = spec.GetBrokenRules(address);

            Assert.IsTrue(broken.Any());
            Assert.AreEqual(1, broken.Count());
            Assert.AreEqual(AddressBusinessRules.AddressCountryRequired.Description, broken.First().Description);
        }
    }
}