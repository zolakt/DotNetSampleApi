using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.Task
{
    public static class TaskBusinessRules
    {
        public static readonly BusinessRule TaskNameRequired = new BusinessRule
        {
            Description = Resources.BusinessRules.TaskNameRequired,
            Tags = new[] { "Name" }
        };

        public static readonly BusinessRule TaskTimeRequired = new BusinessRule
        {
            Description = Resources.BusinessRules.TaskTimeRequired,
            Tags = new[] { "Time" }
        };

        public static readonly BusinessRule TaskUserRequired = new BusinessRule
        {
            Description = Resources.BusinessRules.TaskUserRequired,
            Tags = new[] { "UserId" }
        };
    }
}