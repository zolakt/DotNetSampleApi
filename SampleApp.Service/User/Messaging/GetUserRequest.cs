using System;
using SampleApp.Common.Service;

namespace SampleApp.Service.User.Messaging
{
    public class GetUserRequest : GuidIdRequest
    {
        public GetUserRequest(Guid userId): base(userId)
        {
        }
    }
}
