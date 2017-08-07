using System;
using SampleApp.Common.OutputWriter;

namespace SampleApp.Common.Logging
{
    public class ConsoleLoggingService : ILoggingService
    {
        private readonly IOutputWriter _outputWriter;

        public ConsoleLoggingService(IOutputWriter outputWriter)
        {
            if (outputWriter == null)
            {
                throw new ArgumentNullException("outputWriter");
            }

            _outputWriter = outputWriter;
        }


        public void LogInfo(object logSource, string message, Exception exception = null)
        {
            _outputWriter.SetColor(ConsoleColor.Green);
            _outputWriter.WriteLine(string.Concat("Info from ", logSource.ToString(), ": ", message));

            PrintException(exception);
            ResetConsoleColor();
        }

        public void LogWarning(object logSource, string message, Exception exception = null)
        {
            _outputWriter.SetColor(ConsoleColor.Yellow);
            _outputWriter.WriteLine(string.Concat("Warning from ", logSource.ToString(), ": ", message));

            PrintException(exception);
            ResetConsoleColor();
        }

        public void LogError(object logSource, string message, Exception exception = null)
        {
            _outputWriter.SetColor(ConsoleColor.DarkMagenta);
            _outputWriter.WriteLine(string.Concat("Error from ", logSource.ToString(), ": ", message));

            PrintException(exception);
            ResetConsoleColor();
        }

        public void LogFatal(object logSource, string message, Exception exception = null)
        {
            _outputWriter.SetColor(ConsoleColor.Red);
            _outputWriter.WriteLine(string.Concat("Fatal from ", logSource.ToString(), ": ", message));

            PrintException(exception);
            ResetConsoleColor();
        }


        private void ResetConsoleColor()
        {
            _outputWriter.ResetDefaultColor();
        }

        private void PrintException(Exception exception)
        {
            if (exception != null)
            {
                _outputWriter.WriteLine(string.Concat("Exception logged: ", exception.Message));
            }
        }
    }
}