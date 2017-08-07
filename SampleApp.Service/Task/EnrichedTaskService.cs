using System;
using SampleApp.Common.Configuration;
using SampleApp.Common.Email;
using SampleApp.Common.Logging;
using SampleApp.Common.Service;
using SampleApp.Service.Configuration;
using SampleApp.Service.Email;
using SampleApp.Service.Task.Messaging;

namespace SampleApp.Service.Task
{
    public class EnrichedTaskService : ITaskService
    {
        private readonly IAppConfig<IServiceConfigOptions> _appConfig;
        private readonly IEmailService _emailService;
        private readonly ITaskService _innerService;
        private readonly ILoggingService _logger;

        public EnrichedTaskService(ITaskService innerService, ILoggingService logger, IEmailService emailService,
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

        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            return _innerService.GetTask(request);
        }

        public GetTasksResponse GetTasks(GetTasksRequest request)
        {
            return _innerService.GetTasks(request);
        }

        public InsertTaskResponse InsertTask(InsertTaskRequest request)
        {
            var response = _innerService.InsertTask(request);

            NotifyIfException(response);

            return response;
        }

        public UpdateTaskResponse UpdateTask(UpdateTaskRequest request)
        {
            var response = _innerService.UpdateTask(request);

            NotifyIfException(response);

            return response;
        }

        public DeleteTaskResponse DeleteTask(DeleteTaskRequest request)
        {
            var response = _innerService.DeleteTask(request);

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