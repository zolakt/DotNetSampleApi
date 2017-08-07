using System;
using SampleApp.Common.OutputWriter;

namespace SampleApp.Common.Email
{
    public class FakeEmailService : IEmailService
    {
        private readonly IOutputWriter _outputWriter;

        public FakeEmailService(IOutputWriter outputWriter)
        {
            if (outputWriter == null)
            {
                throw new ArgumentNullException("outputWriter");
            }

            _outputWriter = outputWriter;
        }

        public EmailSendingResult SendEmail(EmailArguments emailArguments)
        {
            if (emailArguments == null)
            {
                throw new ArgumentNullException("emailArguments");
            }

            var message = string.Format("From: {0}, to: {1}, message: {2}, server: {3}, subject: {4}",
                emailArguments.From, emailArguments.To, emailArguments.Message, emailArguments.SmtpServer,
                emailArguments.Subject);

            _outputWriter.WriteLine(message);

            return new EmailSendingResult {
                EmailSendingFailureMessage = null,
                EmailSentSuccessfully = true
            };
        }
    }
}