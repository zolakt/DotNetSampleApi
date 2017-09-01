using SampleApp.Common.Configuration;

namespace SampleApp.WebApp.Configuration
{
    public class WebAppConfig : AppConfig<WebAppConfigOptions>
    {
        public override WebAppConfigOptions Options
        {
            get
            {
                return new WebAppConfigOptions
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