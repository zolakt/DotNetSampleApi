using System.Collections.Generic;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext.Initializer
{
    public interface IDatabaseInitializer
    {
        ICollection<Task> InitializeTasks();

        ICollection<User> InitializeUsers();
    }
}