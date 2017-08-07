using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.Logging;
using SampleApp.Common.OutputWriter;

namespace SampleApp.Common.Tests.Logging
{
    [TestClass()]
    public class ConsoleLoggingServiceTests
    {
        [TestMethod()]
        public void LogInfoTest()
        {
            var source = "source";
            var message = "message";

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogInfo(source, message);

            var expectedLog1 = "Info from " + source + ": " + message;
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
        }

        [TestMethod()]
        public void LogInfoWithExceptionTest()
        {
            var source = "source";
            var message = "message";
            var exception = new Exception("exception");

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogInfo(source, message, exception);

            var expectedLog1 = "Info from " + source + ": " + message;
            var expectedLog2 = "Exception logged: " + exception.Message;

            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog2)), Times.Once);
        }

        [TestMethod()]
        public void LogWarningTest()
        {
            var source = "source";
            var message = "message";

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogWarning(source, message);

            var expectedLog1 = "Warning from " + source + ": " + message;
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
        }

        [TestMethod()]
        public void LogWarningWithExceptionTest()
        {
            var source = "source";
            var message = "message";
            var exception = new Exception("exception");

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogWarning(source, message, exception);

            var expectedLog1 = "Warning from " + source + ": " + message;
            var expectedLog2 = "Exception logged: " + exception.Message;

            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog2)), Times.Once);
        }

        [TestMethod()]
        public void LogErrorTest()
        {
            var source = "source";
            var message = "message";

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogError(source, message);

            var expectedLog1 = "Error from " + source + ": " + message;
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
        }

        [TestMethod()]
        public void LogErrorWithExceptionTest()
        {
            var source = "source";
            var message = "message";
            var exception = new Exception("exception");

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogError(source, message, exception);

            var expectedLog1 = "Error from " + source + ": " + message;
            var expectedLog2 = "Exception logged: " + exception.Message;

            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog2)), Times.Once);
        }

        [TestMethod()]
        public void LogFatalTest()
        {
            var source = "source";
            var message = "message";

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogFatal(source, message);

            var expectedLog1 = "Fatal from " + source + ": " + message;
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
        }

        [TestMethod()]
        public void LogFatalWithExceptionTest()
        {
            var source = "source";
            var message = "message";
            var exception = new Exception("exception");

            var writer = new Mock<IOutputWriter>();

            var logger = new ConsoleLoggingService(writer.Object);

            logger.LogFatal(source, message, exception);

            var expectedLog1 = "Fatal from " + source + ": " + message;
            var expectedLog2 = "Exception logged: " + exception.Message;

            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog1)), Times.Once);
            writer.Verify(w => w.WriteLine(It.Is<string>(s => s == expectedLog2)), Times.Once);
        }
    }
}