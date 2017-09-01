using System.Collections.Generic;

namespace SampleApp.Common.Domain.Validation
{
    public interface ISpecification<TEntityType>
    {
        IEnumerable<BusinessRule> Rules { get; }

        IEnumerable<BusinessRule> GetBrokenRules(TEntityType entity);
    }
}