using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleApp.Common.Domain.Validation
{
    public class BusinessRuleCollection
    {
        private readonly ICollection<BusinessRule> _rules;

        public BusinessRuleCollection()
        {
            _rules = new List<BusinessRule>();
        }

        public BusinessRuleCollection(IEnumerable<BusinessRule> rules)
        {
            _rules = rules.ToList();
        }


        public void AddRule(BusinessRule rule)
        {
            _rules.Add(rule);
        }

        public void ClearRules()
        {
            _rules.Clear();
        }

        public bool HasRules()
        {
            return _rules.Any();
        }

        public IEnumerable<string> GetRulesSummary()
        {
            var issues = new StringBuilder();

            if (_rules.Any())
            {
                foreach (var businessRule in _rules)
                {
                    yield return businessRule.Description;
                }

            }
        }
    }
}