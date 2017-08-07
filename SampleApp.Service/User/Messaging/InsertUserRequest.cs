using SampleApp.Common.Service;
using SampleApp.Service.User.DTO;

namespace SampleApp.Service.User.Messaging
{
    public class InsertUserRequest : ServiceRequestBase
    {
        public UserEditDTO UserProperties { get; set; }
    }
}
