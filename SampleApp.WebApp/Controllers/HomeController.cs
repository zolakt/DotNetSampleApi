using SampleApp.Common.DAL.Repositories;
using SampleApp.Domain.Task.QueryParams;
using SampleApp.Domain.User.QueryParams;
using SampleApp.Service.Task;
using SampleApp.Service.Task.Messaging;
using SampleApp.Service.User;
using SampleApp.Service.User.Messaging;
using SampleApp.WebApp.ViewModels.Home;
using System;
using System.Web.Mvc;

namespace SampleApp.WebApp.Controllers
{
    public class HomeController : MvcController
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public HomeController(ITaskService taskService, IUserService userService) : base()
        {
            if (taskService == null)
            {
                throw new ArgumentNullException("taskService");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            _taskService = taskService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var tasksRequest = new GetTasksRequest
            {
                QueryParams = new TaskQueryParam
                {
                    Include = new TaskQueryParam.IncludeOptions
                    {
                        User = true
                    },
                    Sort = new TaskQueryParam.SortOptions
                    {
                        Latest = true,
                    },
                    Pagination = new PaginationOptions
                    {
                        Limit = 5
                    }
                }
            };

            var tasksResponse = _taskService.GetTasks(tasksRequest);

            HandleServiceException(tasksResponse);

            var usersRequest = new GetUsersRequest
            {
                QueryParams = new UserQueryParam
                {
                    Sort = new UserQueryParam.SortOptions
                    {
                        Latest = true
                    },
                    Pagination = new PaginationOptions
                    {
                        Limit = 5
                    }
                }
            };

            var usersResponse = _userService.GetUsers(usersRequest);

            HandleServiceException(usersResponse);

            var viewModel = new IndexViewModel
            {
                RecentTasks = tasksResponse.Result,
                RecentUsers = usersResponse.Result,
                TasksLimit = tasksRequest.QueryParams.Pagination.Limit.Value,
                UsersLimit = usersRequest.QueryParams.Pagination.Limit.Value
            };

            return View(viewModel);
        }
    }
}