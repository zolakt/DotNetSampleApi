using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Domain.Validation.Common;
using System.Collections.Generic;

namespace SampleApp.Domain.Task.Specifications
{
    public class TaskTimeRequired : ISpecification<Task>, IRequiredSpecification
    {
        public IEnumerable<BusinessRule> Rules
        {
            get
            {
                return new[] { TaskBusinessRules.TaskTimeRequired };
            }
        }

        public IEnumerable<BusinessRule> GetBrokenRules(Task entity)
        {
            if (!entity.Time.HasValue)
            {
                return Rules;
            }

            return new List<BusinessRule>();
        }
    }
}