using System.Collections.Generic;
using SampleApp.Common.Domain.Repositories;
using SampleApp.Domain.Task.QueryParams;

namespace SampleApp.Domain.Task
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> Find(TaskQueryParam options);
    }
}