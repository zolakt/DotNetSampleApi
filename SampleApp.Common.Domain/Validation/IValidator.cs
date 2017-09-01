using System.Collections.Generic;

namespace SampleApp.Common.Domain.Validation
{
    public interface IValidator<in TEntityType>
    {
        bool IsValid(TEntityType entity);

        IEnumerable<BusinessRule> GetBrokenRules(TEntityType entity);

        ValidationRulesMap GetRulesDetails();
    }
}
