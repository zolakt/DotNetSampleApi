using SampleApp.Common.Domain.Validation;
using SampleApp.Domain.Task;
using SampleApp.Service.Task;
using SampleApp.Service.Task.DTO;
using SampleApp.Service.Task.Messaging;
using SampleApp.Service.User;
using SampleApp.Service.User.Messaging;
using SampleApp.WebApp.JsonValidationMapper;
using SampleApp.WebApp.ViewModels.Shared;
using SampleApp.WebApp.ViewModels.Tasks;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SampleApp.WebApp.Controllers
{
    public class TasksController : MvcController
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IValidator<Task> _taskValidator;
        private readonly IJsonValidationMapper _jsValidationMapper;

        public TasksController(ITaskService taskService, IUserService userService, 
            IValidator<Task> taskValidator, IJsonValidationMapper jsValidationMapper) : base()
        {
            if (taskService == null)
            {
                throw new ArgumentNullException("taskService");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (taskValidator == null)
            {
                throw new ArgumentNullException("taskValidator");
            }
            if (jsValidationMapper == null)
            {
                throw new ArgumentNullException("jsValidationMapper");
            }

            _taskService = taskService;
            _userService = userService;
            _taskValidator = taskValidator;
            _jsValidationMapper = jsValidationMapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var serviceRequest = new GetTasksRequest
            {
                QueryParams = new Domain.Task.QueryParams.TaskQueryParam
                {
                    Include = new Domain.Task.QueryParams.TaskQueryParam.IncludeOptions
                    {
                        User = true
                    }
                }
            };

            var serviceResponse = _taskService.GetTasks(serviceRequest);

            HandleServiceException(serviceResponse);

            return View(new IndexViewModel
            {
                Tasks = serviceResponse.Result
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return GetEditView(new TaskDTO(), SaveMode.Create);
        }

        [HttpPost]
        public ActionResult Create(TaskDTO task)
        {
            var serviceRequest = new InsertTaskRequest
            {
                TaskProperties = task
            };

            var serviceResponse = _taskService.InsertTask(serviceRequest);

            if (!HandleServiceException(serviceResponse))
            {
                return GetEditView(task, SaveMode.Create);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            var serviceRequest = new GetTaskRequest(id);
            var serviceResponse = _taskService.GetTask(serviceRequest);

            if (serviceResponse.Exception != null)
            {
                throw serviceResponse.Exception;
            };

            return GetEditView(serviceResponse.Result, SaveMode.Update);
        }

        [HttpPost]
        public ActionResult Update(TaskDTO task)
        {
            var serviceRequest = new UpdateTaskRequest(task.Id)
            {
                TaskProperties = task
            };

            var serviceResponse = _taskService.UpdateTask(serviceRequest);

            if (!HandleServiceException(serviceResponse))
            {
                return GetEditView(task, SaveMode.Update);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var serviceRequest = new DeleteTaskRequest(id);
            var serviceResponse = _taskService.DeleteTask(serviceRequest);

            HandleServiceException(serviceResponse);

            return RedirectToAction("Index");
        }


        private SelectList GetUsersDropdown(Guid? selected = null)
        {
            var usersRequest = new GetUsersRequest();
            var usersResponse = _userService.GetUsers(usersRequest);

            var users = usersResponse.Result.Select(x => new SelectListItem
            {
                Text = x.LastName + " " + x.FirstName,
                Value = x.Id.ToString()
            });

            return new SelectList(users, "Value", "Text", selected);
        }

        private ViewResult GetEditView(TaskDTO task, SaveMode mode)
        {
            return View("Edit", new EditViewModel
            {
                SaveMode = mode,
                Task = task,
                Users = GetUsersDropdown(task.UserId),
                JsonValidation = _jsValidationMapper.Format(_taskValidator.GetRulesDetails(), "Task.")
            });
        }
    }
}