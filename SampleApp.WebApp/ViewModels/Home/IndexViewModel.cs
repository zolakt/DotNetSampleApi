using SampleApp.Service.Task.DTO;
using SampleApp.Service.User.DTO;
using System.Collections.Generic;

namespace SampleApp.WebApp.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<TaskDTO> RecentTasks { get; set; }

        public int TasksLimit { get; set; }

        public IEnumerable<UserDTO> RecentUsers { get; set; }

        public int UsersLimit { get; set; }

    }
}