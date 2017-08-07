using System;
using SampleApp.Common.Configuration;
using SampleApp.Common.Email;
using SampleApp.Common.Logging;
using SampleApp.Common.Service;
using SampleApp.Service.Configuration;
using SampleApp.Service.Email;
using SampleApp.Service.User.Messaging;

namespace SampleApp.Service.User
{
    public class EnrichedUserService : IUserService
    {
        private readonly IAppConfig<IServiceConfigOptions> _appConfig;
        private readonly IEmailService _emailService;
        private readonly IUserService _innerService;
        private readonly ILoggingService _logger;

        public EnrichedUserService(IUserService innerService, ILoggingService logger, IEmailService emailService,
            IAppConfig<IServiceConfigOptions> appConfig)
        {
            if (innerService == null)
            {
                throw new ArgumentNullException("innerService");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if (emailService == null)
            {
                throw new ArgumentNullException("emailService");
            }
            if (appConfig == null)
            {
                throw new ArgumentNullException("appConfig");
            }

            _innerService = innerService;
            _logger = logger;
            _emailService = emailService;
            _appConfig = appConfig;
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            return _innerService.GetUser(request);
        }

        public GetUsersResponse GetUsers(GetUsersRequest request)
        {
            return _innerService.GetUsers(request);
        }

        public InsertUserResponse InsertUser(InsertUserRequest request)
        {
            var response = _innerService.InsertUser(request);

            NotifyIfException(response);

            return response;
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            var response = _innerService.UpdateUser(request);

            NotifyIfException(response);

            return response;
        }

        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            var response = _innerService.DeleteUser(request);

            NotifyIfException(response);

            return response;
        }


        private void NotifyIfException<T>(ServiceResponseBase<T> response)
        {
            if (response.Exception != null)
            {
                _logger.LogError(this, response.Exception.Message);
                _emailService.SendEmail(new ExceptionEmailArguments(_appConfig, response.Exception.Message));
            }
        }
    }
}