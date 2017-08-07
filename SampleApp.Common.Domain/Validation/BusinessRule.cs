namespace SampleApp.Common.Domain.Validation
{
    public class BusinessRule
    {
        public BusinessRule(string ruleDescription)
        {
            RuleDescription = ruleDescription;
        }

        public string RuleDescription { get; }
    }
}