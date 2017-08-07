using System;
using System.Net.Http;
using System.Web.Http;
using SampleApp.Domain.Task.QueryParams;
using SampleApp.Service.Task;
using SampleApp.Service.Task.DTO;
using SampleApp.Service.Task.Messaging;
using SampleApp.WebAPI.ResponseFormat;

namespace SampleApp.WebAPI.Controllers
{
    public class TaskController : ApiController
    {
        private readonly IHttpResponseBuilder _responseBuilder;
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService, IHttpResponseBuilder responseBuilder)
        {
            if (taskService == null)
            {
                throw new ArgumentNullException("taskService");
            }

            if (responseBuilder == null)
            {
                throw new ArgumentNullException("responseBuilder");
            }

            _taskService = taskService;
            _responseBuilder = responseBuilder;
        }


        public HttpResponseMessage Get(Guid id)
        {
            var request = new GetTaskRequest(id);
            var response = _taskService.GetTask(request);

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Get([FromUri] TaskQueryParam options)
        {
            var request = new GetTasksRequest {
                QueryParams = options
            };

            var response = _taskService.GetTasks(request);

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Post([FromBody] TaskEditDTO task)
        {
            var request = new InsertTaskRequest {
                TaskProperties = task
            };

            var response = new InsertTaskResponse();

            try
            {
                response = _taskService.InsertTask(request);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Put([FromBody] TaskDTO task)
        {
            var request = new UpdateTaskRequest(task.Id) {
                TaskProperties = task
            };

            var response = new UpdateTaskResponse();

            try
            {
                response = _taskService.UpdateTask(request);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Delete(Guid id)
        {
            var request = new DeleteTaskRequest(id);

            var response = new DeleteTaskResponse();

            try
            {
                response = _taskService.DeleteTask(request);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return _responseBuilder.BuildResponse(Request, response);
        }
    }
}