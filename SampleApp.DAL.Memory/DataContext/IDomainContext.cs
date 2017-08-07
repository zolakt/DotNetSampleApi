using System.Collections.Generic;
using SampleApp.Common.DAL.DataContext;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext
{
    public interface IDomainContext : IDataContext
    {
        ICollection<Task> Tasks { get; }

        ICollection<User> Users { get; }
    }
}