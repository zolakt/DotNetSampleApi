using System;

namespace SampleApp.Common.Domain.Tests.Fakes
{
    public class FakeDomainUser : EntityBase<Guid>
    {
        public string Name { get; set; }
    }
}