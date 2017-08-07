using System.Collections.Generic;
using SampleApp.Common.Service;
using SampleApp.Service.Task.DTO;

namespace SampleApp.Service.Task.Messaging
{
    public class GetTasksResponse : ServiceResponseBase<IEnumerable<TaskDTO>>
    {
    }
}
