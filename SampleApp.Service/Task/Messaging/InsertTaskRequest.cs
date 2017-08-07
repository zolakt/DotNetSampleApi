using SampleApp.Common.Service;
using SampleApp.Service.Task.DTO;

namespace SampleApp.Service.Task.Messaging
{
    public class InsertTaskRequest : ServiceRequestBase
    {
        public TaskEditDTO TaskProperties { get; set; }
    }
}
