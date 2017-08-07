using System;
using System.Collections.Generic;
using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.Task.Specifications
{
    public class TaskUserRequired : ISpecification<Task>
    {
        public IEnumerable<BusinessRule> GetBrokenRules(Task entity)
        {
            if ((entity.User == null) || (entity.User.Id == Guid.Empty))
            {
                yield return TaskBusinessRules.TaskUserRequired;
            }
        }
    }
}