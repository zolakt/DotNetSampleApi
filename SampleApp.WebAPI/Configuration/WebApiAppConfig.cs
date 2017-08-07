using SampleApp.Common.Configuration;

namespace SampleApp.WebAPI.Configuration
{
    public class WebApiAppConfig : AppConfig<WebApiConfigOptions>
    {
        public override WebApiConfigOptions Options
        {
            get
            {
                return new WebApiConfigOptions
                {
                    AdminEmail = GetConfigurationValue("AdminEmail"),
                    SystemEmail = GetConfigurationValue("SystemEmail"),
                    SmtpServer = GetConfigurationValue("SmtpServer"),
                    ErrorEmailSubject = GetConfigurationValue("ErrorEmailSubject")
                };
            }
        }
    }
}