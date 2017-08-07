using System;

namespace SampleApp.Service.Task.DTO
{
    public class TaskDTO : TaskEditDTO
    {
        public Guid Id { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
    }
}
