using System.Collections.Generic;

namespace SampleApp.Common.Domain.Validation
{
    public class BusinessRule
    {
        public string Description { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}