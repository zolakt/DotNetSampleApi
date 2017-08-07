namespace SampleApp.Common.Email
{
    public interface IEmailService
    {
        EmailSendingResult SendEmail(EmailArguments emailArguments);
    }
}