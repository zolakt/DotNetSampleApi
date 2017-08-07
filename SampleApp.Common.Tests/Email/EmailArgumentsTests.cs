using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Email;

namespace SampleApp.Common.Tests.Email
{
    [TestClass()]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class EmailArgumentsTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmailArgumentsEmptySubjectTest()
        {
            var emailArguments = new EmailArguments(null, "a", "a", "a", "a");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmailArgumentsEmptyMessagetTest()
        {
            var emailArguments = new EmailArguments("a", null, "a", "a", "a");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmailArgumentsEmptyToTest()
        {
            var emailArguments = new EmailArguments("a", "a", null, "a", "a");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmailArgumentsEmptyFromTest()
        {
            var emailArguments = new EmailArguments("a", "a", "a", null, "a");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmailArgumentsEmptySmtpServerTest()
        {
            var emailArguments = new EmailArguments("a", "a", "a", "a", null);
        }
    }
}