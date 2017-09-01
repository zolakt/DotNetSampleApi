using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Domain.Validation.Common;
using System;
using System.Collections.Generic;

namespace SampleApp.Domain.Task.Specifications
{
    public class TaskUserRequired : ISpecification<Task>, IRequiredSpecification
    {
        public IEnumerable<BusinessRule> Rules
        {
            get
            {
                return new[] { TaskBusinessRules.TaskUserRequired };
            }
        }

        public IEnumerable<BusinessRule> GetBrokenRules(Task entity)
        {
            if ((entity.User == null) || (entity.User.Id == Guid.Empty))
            {
                return Rules;
            }

            return new List<BusinessRule>();
        }
    }
}