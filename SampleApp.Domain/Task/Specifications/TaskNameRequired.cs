using System.Collections.Generic;
using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.Task.Specifications
{
    public class TaskNameRequired : ISpecification<Task>
    {
        public IEnumerable<BusinessRule> GetBrokenRules(Task entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                yield return TaskBusinessRules.TaskNameRequired;
            }
        }
    }
}