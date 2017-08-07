using System;
using SampleApp.Common.Domain;

namespace SampleApp.Domain.Task
{
    public class Task : EntityBase<Guid>
    {
        public string Name { get; set; }

        public DateTime? Time { get; set; }

        public User.User User { get; set; }
    }
}