using System;
using SampleApp.Common.Service;

namespace SampleApp.Service.Task.Messaging
{
    public class DeleteTaskRequest : GuidIdRequest
    {
        public DeleteTaskRequest(Guid taskId) : base(taskId) {}
    }
}