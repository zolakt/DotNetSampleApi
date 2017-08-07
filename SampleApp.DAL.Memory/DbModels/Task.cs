using System;

namespace SampleApp.DAL.Memory.DbModels
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? Time { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}