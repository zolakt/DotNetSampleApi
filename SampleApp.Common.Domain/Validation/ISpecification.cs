using System.Collections.Generic;

namespace SampleApp.Common.Domain.Validation
{
    public interface ISpecification<TEntityType>
    {
        IEnumerable<BusinessRule> GetBrokenRules(TEntityType entity);
    }
}