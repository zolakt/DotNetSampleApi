using System.Collections.Generic;
using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.User.Specifications
{
    public class UserNameRequired : ISpecification<User>
    {
        public IEnumerable<BusinessRule> GetBrokenRules(User entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName))
            {
                yield return UserBusinessRules.UserNameRequired;
            }
        }
    }
}