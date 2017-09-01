using System.Collections.Generic;
using SampleApp.Common.DAL.DataContext;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext
{
    public interface IDomainContext : IDataContext
    {
        IEnumerable<Task> Tasks { get; }

        IEnumerable<User> Users { get; }
    }
}