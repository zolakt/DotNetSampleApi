using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.Email;
using SampleApp.Common.OutputWriter;

namespace SampleApp.Common.Tests.Email
{
    [TestClass()]
    public class FakeEmailServiceTests
    {
        [TestMethod()]
        public void SendEmailTest()
        {
            var args = new EmailArguments("a", "a", "a", "a", "a");

            var writer = new Mock<IOutputWriter>();
            var service = new FakeEmailService(writer.Object);

            var result = service.SendEmail(args);

            Assert.IsTrue(result.EmailSentSuccessfully);

            var expectedMessage = "From: a, to: a, message: a, server: a, subject: a";
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedMessage)), Times.Once);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendEmailInvalidArgumentsTest()
        {
            var writer = new Mock<IOutputWriter>();
            var service = new FakeEmailService(writer.Object);

            service.SendEmail(null);
        }
    }
}