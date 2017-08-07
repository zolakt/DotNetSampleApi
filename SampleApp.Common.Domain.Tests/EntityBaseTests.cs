using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Domain.Tests.Fakes;

namespace SampleApp.Common.Domain.Tests
{
    [TestClass]
    public class EntityBaseTests
    {
        [TestMethod]
        public void EqualsTest()
        {
            var user1 = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            var user2 = user1;

            Assert.IsTrue(user1.Equals(user2));

            var user3 = new FakeDomainUser {
                Id = user1.Id,
                Name = user1.Name + "aaa"
            };

            Assert.IsTrue(user1.Equals(user3));

            var user4 = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = user1.Name
            };

            Assert.IsFalse(user1.Equals(user4));
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var user = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            Assert.AreEqual(user.Id.GetHashCode(), user.GetHashCode());
        }
    }
}