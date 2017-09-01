using SampleApp.Common.Domain.Validation;

namespace SampleApp.Domain.Address
{
    public static class AddressBusinessRules
    {
        public static readonly BusinessRule AddressCountryRequired = new BusinessRule
        {
            Description = Resources.BusinessRules.AddressCountryRequired
        };
    }
}