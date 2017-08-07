using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.Task
{
    public static class TaskBusinessRules
    {
        public static readonly BusinessRule TaskNameRequired = new BusinessRule(Resources.BusinessRules.TaskNameRequired);

        public static readonly BusinessRule TaskTimeRequired = new BusinessRule(Resources.BusinessRules.TaskTimeRequired);

        public static readonly BusinessRule TaskUserRequired = new BusinessRule(Resources.BusinessRules.TaskUserRequired);
    }
}