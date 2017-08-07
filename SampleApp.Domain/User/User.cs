using System;
using SampleApp.Common.Domain;

namespace SampleApp.Domain.User
{
    public class User : EntityBase<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address.Address Address { get; set; }
    }
}