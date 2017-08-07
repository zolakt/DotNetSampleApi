using SampleApp.Common.Service;
using SampleApp.Domain.User.QueryParams;

namespace SampleApp.Service.User.Messaging
{
    public class GetUsersRequest : ServiceRequestBase
    {
        public UserQueryParam QueryParams { get; set; }
    }
}