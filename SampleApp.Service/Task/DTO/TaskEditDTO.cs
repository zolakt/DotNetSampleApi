using System;
using System.ComponentModel;

namespace SampleApp.Service.Task.DTO
{
    public class TaskEditDTO
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Time")]
        public DateTime? Time { get; set; }

        [DisplayName("User")]
        public Guid UserId { get; set; }
    }
}
