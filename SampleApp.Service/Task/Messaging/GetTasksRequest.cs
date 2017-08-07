using SampleApp.Common.Service;
using SampleApp.Domain.Task.QueryParams;

namespace SampleApp.Service.Task.Messaging
{
    public class GetTasksRequest : ServiceRequestBase
    {
        public TaskQueryParam QueryParams { get; set; }
    }
}
