using System.Collections.Generic;
using System.Linq;

namespace SampleApp.Common.Domain.Validation
{
    public abstract class BaseValidator<TEntityType> : IValidator<TEntityType>
    {
        public bool IsValid(TEntityType entity)
        {
            return GetBrokenRules(entity).Any();
        }

        public abstract IEnumerable<BusinessRule> GetBrokenRules(TEntityType entity);
    }
}
