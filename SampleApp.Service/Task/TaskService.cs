using System;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Service;
using SampleApp.Common.Service.Exceptions;
using SampleApp.Domain.Task;
using SampleApp.Service.Task.Mapper;
using SampleApp.Service.Task.Messaging;

namespace SampleApp.Service.Task
{
    public class TaskService : ServiceBase, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskDtoMapper _taskMapper;
        private readonly IValidator<Domain.Task.Task> _taskValidator;

        public TaskService(IUnitOfWork uow, ITaskRepository taskRepository, ITaskDtoMapper taskMapper,
            IValidator<Domain.Task.Task> taskValidator) : base(uow)
        {
            if (taskRepository == null)
            {
                throw new ArgumentNullException("taskRepository");
            }
            if (taskMapper == null)
            {
                throw new ArgumentNullException("taskMapper");
            }
            if (taskValidator == null)
            {
                throw new ArgumentNullException("taskValidator");
            }

            _taskRepository = taskRepository;
            _taskMapper = taskMapper;
            _taskValidator = taskValidator;
        }


        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            var response = new GetTaskResponse();

            try
            {
                var task = _taskRepository.FindBy(request.Id);

                if (task == null)
                {
                    response.Exception = GetStandardTaskNotFoundException();
                }
                else
                {
                    response.Result = _taskMapper.ConvertToDTO(task);
                }
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public GetTasksResponse GetTasks(GetTasksRequest request)
        {
            var response = new GetTasksResponse();

            try
            {
                var tasks = _taskRepository.Find(request.QueryParams);
                response.Result = _taskMapper.ConvertToDTO(tasks);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public InsertTaskResponse InsertTask(InsertTaskRequest request)
        {
            var response = new InsertTaskResponse();

            try
            {
                var newTask = _taskMapper.ConvertToDomainObject(request.TaskProperties);

                ThrowExceptionIfTaskIsInvalid(newTask);

                _taskRepository.Insert(newTask);
                _uow.Commit();

                response.Result = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                return response;
            }
        }

        public UpdateTaskResponse UpdateTask(UpdateTaskRequest request)
        {
            var response = new UpdateTaskResponse();

            try
            {
                var existingTask = _taskRepository.FindBy(request.Id);

                if (existingTask != null)
                {
                    _taskMapper.PopulateDomainObject(existingTask, request.TaskProperties);

                    ThrowExceptionIfTaskIsInvalid(existingTask);

                    _taskRepository.Update(existingTask);
                    _uow.Commit();

                    response.Result = true;

                    return response;
                }

                response.Exception = GetStandardTaskNotFoundException();
                return response;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                return response;
            }
        }

        public DeleteTaskResponse DeleteTask(DeleteTaskRequest request)
        {
            var response = new DeleteTaskResponse();

            try
            {
                var task = _taskRepository.FindBy(request.Id);

                if (task != null)
                {
                    _taskRepository.Delete(task);
                    _uow.Commit();

                    response.Result = true;

                    return response;
                }

                response.Exception = GetStandardTaskNotFoundException();
                return response;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                return response;
            }
        }


        private ResourceNotFoundException GetStandardTaskNotFoundException()
        {
            return new ResourceNotFoundException("The requested task was not found.");
        }

        private void ThrowExceptionIfTaskIsInvalid(Domain.Task.Task newTask)
        {
            var brokenRules = new BusinessRuleCollection(_taskValidator.GetBrokenRules(newTask));

            if (brokenRules.HasRules())
            {
                throw new ValidationException(brokenRules.GetRulesSummary());
            }
        }
    }
}