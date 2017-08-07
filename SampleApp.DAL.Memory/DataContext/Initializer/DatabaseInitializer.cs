using System;
using System.Collections.Generic;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext.Initializer
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IDatabaseTasksInitializer _tasksInitializer;
        private readonly IDatabaseUsersInitializer _usersInitializer;

        public DatabaseInitializer(IDatabaseTasksInitializer tasksInitializer,
            IDatabaseUsersInitializer usersInitializer)
        {
            if (tasksInitializer == null)
            {
                throw new ArgumentNullException("tasksInitializer");
            }

            if (usersInitializer == null)
            {
                throw new ArgumentNullException("usersInitializer");
            }

            _tasksInitializer = tasksInitializer;
            _usersInitializer = usersInitializer;
        }


        public ICollection<Task> InitializeTasks()
        {
            return _tasksInitializer.Initialize(InitializeUsers());
        }

        public ICollection<User> InitializeUsers()
        {
            return _usersInitializer.Initialize();
        }
    }
}