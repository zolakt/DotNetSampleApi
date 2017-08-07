using System.Collections.Generic;
using SampleApp.Common.Service;
using SampleApp.Service.User.DTO;

namespace SampleApp.Service.User.Messaging
{
    public class GetUsersResponse : ServiceResponseBase<IEnumerable<UserDTO>>
    {
    }
}
