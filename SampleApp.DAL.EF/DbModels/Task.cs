using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApp.DAL.EF.DbModels
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? Time { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}