using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApp.DAL.EF.DbModels
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(255)]
        public string Street { get; set; }

        [StringLength(255)]
        public string HouseNumber { get; set; }
    }
}