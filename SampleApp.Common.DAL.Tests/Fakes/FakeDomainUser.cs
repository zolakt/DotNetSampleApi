using System;
using SampleApp.Common.Domain;

namespace SampleApp.Common.DAL.Tests.Fakes
{
    public class FakeDomainUser : EntityBase<Guid>
    {
        public string Name { get; set; }
    }
}