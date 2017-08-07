using System.Collections.Generic;
using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.Address.Specifications
{
    public class AddressCountryRequired : ISpecification<Address>
    {
        public IEnumerable<BusinessRule> GetBrokenRules(Address entity)
        {
            if (string.IsNullOrEmpty(entity.Country))
            {
                yield return AddressBusinessRules.AddressCountryRequired;
            }
        }
    }
}