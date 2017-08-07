using System.Collections.Generic;
using System.Linq;
using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.User.Validator
{
    public class UserValidator : SpecificationValidator<User>
    {
        private readonly IValidator<Address.Address> _addressValidator;

        public UserValidator(IEnumerable<ISpecification<User>> specifications,
            IValidator<Address.Address> addressValidator) : base(specifications)
        {
            _addressValidator = addressValidator;
        }

        public override IEnumerable<BusinessRule> GetBrokenRules(User entity)
        {
            var results = _specifications.SelectMany(x => x.GetBrokenRules(entity));

            if (!results.Any())
            {
                results = _addressValidator.GetBrokenRules(entity.Address);
            }

            return results;
        }
    }
}