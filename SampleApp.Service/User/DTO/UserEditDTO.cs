using System.ComponentModel;

namespace SampleApp.Service.User.DTO
{
    public class UserEditDTO
    {
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Street")]
        public string Street { get; set; }

        [DisplayName("House number")]
        public string HouseNumber { get; set; }
    }
}