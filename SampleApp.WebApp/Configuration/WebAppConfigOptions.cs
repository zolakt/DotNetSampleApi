using SampleApp.Service.Configuration;

namespace SampleApp.WebApp.Configuration
{
    public class WebAppConfigOptions : IServiceConfigOptions
    {
        public string SystemEmail { get; set; }
        public string AdminEmail { get; set; }
        public string SmtpServer { get; set; }
        public string ErrorEmailSubject { get; set; }
    }
}