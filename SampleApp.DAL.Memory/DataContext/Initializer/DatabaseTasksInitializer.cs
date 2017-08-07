using System;
using System.Collections.Generic;
using System.Linq;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext.Initializer
{
    public class DatabaseTasksInitializer : IDatabaseTasksInitializer
    {
        public ICollection<Task> Initialize(ICollection<User> users)
        {
            var user1 = users.FirstOrDefault();
            var user2 = users.LastOrDefault();

            return new List<Task>() {
                new Task() {
                    Id = Guid.NewGuid(),
                    Name = "Task 1",
                    Time = new DateTime(2017, 2, 20, 22, 00, 00),
                    User = user1,
                    UserId = user1?.Id ?? Guid.Empty
                },
                new Task() {
                    Id = Guid.NewGuid(),
                    Time = new DateTime(2017, 2, 20, 21, 00, 00),
                    Name = "Task 2",
                    User = user1,
                    UserId = user1?.Id ?? Guid.Empty
                },
                new Task() {
                    Id = Guid.NewGuid(),
                    Name = "Task 3",
                    Time = new DateTime(2017, 2, 22, 10, 30, 00),
                    User = user2,
                    UserId = user2?.Id ?? Guid.Empty
                }
            };
        }
    }
}