using System;

namespace SampleApp.Common.Email
{
    public class EmailArguments
    {
        public EmailArguments(string subject, string message, string to, string from, string smtpServer)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentNullException("subject");
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("recipient");
            }
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentNullException("sender");
            }
            if (string.IsNullOrEmpty(smtpServer))
            {
                throw new ArgumentNullException("server");
            }

            From = from;
            Message = message;
            SmtpServer = smtpServer;
            Subject = subject;
            To = to;
        }

        public string To { get; }

        public string From { get; }

        public string Subject { get; }

        public string SmtpServer { get; }

        public string Message { get; }
    }
}