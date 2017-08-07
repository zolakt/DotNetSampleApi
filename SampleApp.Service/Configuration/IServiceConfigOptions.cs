namespace SampleApp.Service.Configuration
{
    public interface IServiceConfigOptions
    {
        string SystemEmail { get; set; }

        string AdminEmail { get; set; }

        string SmtpServer { get; set; }

        string ErrorEmailSubject { get; set; }
    }
}