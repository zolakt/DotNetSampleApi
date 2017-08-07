using System.Collections.Generic;
using System.Linq;

namespace SampleApp.Common.Domain.Validation
{
    public class SpecificationValidator<TEntityType> : BaseValidator<TEntityType>
    {
        protected readonly IEnumerable<ISpecification<TEntityType>> _specifications;

        public SpecificationValidator(IEnumerable<ISpecification<TEntityType>> specifications)
        {
            _specifications = specifications;
        }

        public override IEnumerable<BusinessRule> GetBrokenRules(TEntityType entity)
        {
            return _specifications.SelectMany(x => x.GetBrokenRules(entity));
        }
    }
}