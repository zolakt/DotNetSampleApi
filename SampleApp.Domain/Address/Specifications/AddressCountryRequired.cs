using SampleApp.Common.Domain.Validation;
using System.Collections.Generic;

namespace SampleApp.Domain.Address.Specifications
{
    public class AddressCountryRequired : ISpecification<Address>
    {
        public IEnumerable<BusinessRule> Rules
        {
            get
            {
                return new[] { AddressBusinessRules.AddressCountryRequired };
            }
        }

        public IEnumerable<BusinessRule> GetBrokenRules(Address entity)
        {
            if (string.IsNullOrEmpty(entity.Country))
            {
                return Rules;
            }

            return new List<BusinessRule>();
        }
    }
}