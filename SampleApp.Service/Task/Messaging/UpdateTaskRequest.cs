using System;
using SampleApp.Common.Service;
using SampleApp.Service.Task.DTO;

namespace SampleApp.Service.Task.Messaging
{
    public class UpdateTaskRequest : GuidIdRequest
    {
        public UpdateTaskRequest(Guid taskId) : base(taskId)
        {
        }

        public TaskEditDTO TaskProperties { get; set; }
    }
}
