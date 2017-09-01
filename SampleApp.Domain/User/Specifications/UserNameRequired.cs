using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Domain.Validation.Common;
using System.Collections.Generic;

namespace SampleApp.Domain.User.Specifications
{
    public class UserNameRequired : ISpecification<User>, IRequiredSpecification
    {
        public IEnumerable<BusinessRule> Rules
        {
            get
            {
                return new[] { UserBusinessRules.UserNameRequired };
            }
        }

        public IEnumerable<BusinessRule> GetBrokenRules(User entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName))
            {
                return Rules;
            }

            return new List<BusinessRule>();
        }
    }
}