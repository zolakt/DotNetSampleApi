using System.Collections.Generic;
using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.Task.Specifications
{
    public class TaskTimeRequired : ISpecification<Task>
    {
        public IEnumerable<BusinessRule> GetBrokenRules(Task entity)
        {
            if (!entity.Time.HasValue)
            {
                yield return TaskBusinessRules.TaskTimeRequired;
            }
        }
    }
}