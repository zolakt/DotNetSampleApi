using Newtonsoft.Json;
using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Domain.Validation.Common;
using System.Collections.Generic;

namespace SampleApp.WebApp.JsonValidationMapper
{
    public class JQueryValidationMapper : IJsonValidationMapper
    {
        public string Format(ValidationRulesMap validationMap, string prefix = null)
        {
            var rules = new Dictionary<string, JQueryValidationRule>();
            var messages = new Dictionary<string, JQueryValidationMessage>();

            if (validationMap != null && validationMap.ContainsKey(ValidationRuleType.Required))
            {
                foreach (var row in validationMap[ValidationRuleType.Required])
                {
                    var key = prefix + row.Key;

                    rules[key] = new JQueryValidationRule
                    {
                        Required = true
                    };

                    messages[key] = new JQueryValidationMessage
                    {
                        Required = row.Value.ToString()
                    };
                }
            }

            return JsonConvert.SerializeObject(new JQueryValidationSummary
            {
                Rules = rules,
                Messages = messages
            });
        }

        private class JQueryValidationSummary
        {
            [JsonProperty("rules")]
            public Dictionary<string, JQueryValidationRule> Rules { get; set; }

            [JsonProperty("messages")]
            public Dictionary<string, JQueryValidationMessage> Messages { get; set; }
        }

        private class JQueryValidationRule
        {
            [JsonProperty("required")]
            public bool Required { get; set; }

            [JsonProperty("email")]
            public bool Email { get; set; }
        }

        private class JQueryValidationMessage
        {
            [JsonProperty("required")]
            public string Required { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }
        }
    }
}