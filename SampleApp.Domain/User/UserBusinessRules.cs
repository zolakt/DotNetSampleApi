using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.User
{
    public static class UserBusinessRules
    {
        public static readonly BusinessRule UserNameRequired = new BusinessRule
        {
            Description = Resources.BusinessRules.UserNameRequired,
            Tags = new string[] { "FirstName", "LastName" }
        };
    }
}