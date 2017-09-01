using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Domain.Validation.Common;
using System.Collections.Generic;

namespace SampleApp.Domain.Task.Specifications
{
    public class TaskNameRequired : ISpecification<Task>, IRequiredSpecification
    {
        public IEnumerable<BusinessRule> Rules
        {
            get
            {
                return new[] { TaskBusinessRules.TaskNameRequired };
            }
        }

        public IEnumerable<BusinessRule> GetBrokenRules(Task entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                return Rules;
            }

            return new List<BusinessRule>();
        }
    }
}