using System;
using SampleApp.Common.Configuration;
using SampleApp.Common.Email;
using SampleApp.Service.Configuration;

namespace SampleApp.Service.Email
{
    public class ExceptionEmailArguments : EmailArguments
    {
        public ExceptionEmailArguments(IAppConfig<IServiceConfigOptions> appConfig, string message)
            : base(
                appConfig.Options.ErrorEmailSubject, message, appConfig.Options.AdminEmail,
                appConfig.Options.SystemEmail, appConfig.Options.SmtpServer)
        {
            if (appConfig == null)
            {
                throw new ArgumentNullException("appConfig");
            }
        }
    }
}