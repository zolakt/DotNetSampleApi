using SampleApp.Common.Domain;

namespace SampleApp.Domain.Address
{
    public class Address : ValueObjectBase
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }
    }
}