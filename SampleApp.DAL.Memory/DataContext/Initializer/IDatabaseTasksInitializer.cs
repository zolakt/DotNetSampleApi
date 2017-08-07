using System.Collections.Generic;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext.Initializer
{
    public interface IDatabaseTasksInitializer
    {
        ICollection<Task> Initialize(ICollection<User> users);
    }
}
