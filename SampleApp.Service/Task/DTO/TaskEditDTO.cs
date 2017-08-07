using System;

namespace SampleApp.Service.Task.DTO
{
    public class TaskEditDTO
    {
        public string Name { get; set; }

        public DateTime? Time { get; set; }

        public Guid UserId { get; set; }
    }
}
