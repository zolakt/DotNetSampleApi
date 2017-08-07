using SampleApp.Service.Task.Messaging;

namespace SampleApp.Service.Task
{
    public interface ITaskService
    {
        GetTaskResponse GetTask(GetTaskRequest request);

        GetTasksResponse GetTasks(GetTasksRequest request);

        InsertTaskResponse InsertTask(InsertTaskRequest request);

        UpdateTaskResponse UpdateTask(UpdateTaskRequest request);

        DeleteTaskResponse DeleteTask(DeleteTaskRequest request);
    }
}
