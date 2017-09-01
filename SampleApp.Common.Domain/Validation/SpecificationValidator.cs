using SampleApp.Common.Domain.Validation.Common;
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

        public override ValidationRulesMap GetRulesDetails()
        {
            var result = new ValidationRulesMap();

            foreach (var spec in _specifications)
            {
                if (spec is IRequiredSpecification)
                {
                    if (!result.ContainsKey(ValidationRuleType.Required))
                    {
                        result.Add(ValidationRuleType.Required, new Dictionary<string, object>());
                    }

                    foreach (var rule in spec.Rules)
                    {
                        if (rule.Tags.Any())
                        {
                            foreach(var tag in rule.Tags)
                            {
                                result[ValidationRuleType.Required].Add(tag, rule.Description);
                            }
                        }
                        else
                        {
                            result[ValidationRuleType.Required].Add(string.Empty, rule.Description);
                        }
                    }

                }
            }

            return result;
        }
    }
}