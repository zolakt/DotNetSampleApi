using SampleApp.Common.Domain.Validation.Common;
using System.Collections.Generic;

namespace SampleApp.Common.Domain.Validation
{
    public class ValidationRulesMap : Dictionary<ValidationRuleType, Dictionary<string, object>>
    {
    }
}
