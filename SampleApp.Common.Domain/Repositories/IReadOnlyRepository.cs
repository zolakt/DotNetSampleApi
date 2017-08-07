using System.Collections.Generic;

namespace SampleApp.Common.Domain.Repositories
{
    public interface IReadOnlyRepository<TAggregateType> where TAggregateType : IAggregateRoot
    {
        TAggregateType FindBy(object id);

        IEnumerable<TAggregateType> FindAll();
    }
}