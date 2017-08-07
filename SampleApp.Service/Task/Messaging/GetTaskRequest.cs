using System;
using SampleApp.Common.Service;

namespace SampleApp.Service.Task.Messaging
{
    public class GetTaskRequest : GuidIdRequest
    {
        public GetTaskRequest(Guid taskId): base(taskId)
        {
        }
    }
}
