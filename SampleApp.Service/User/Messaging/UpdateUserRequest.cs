using System;
using SampleApp.Common.Service;
using SampleApp.Service.User.DTO;

namespace SampleApp.Service.User.Messaging
{
    public class UpdateUserRequest : GuidIdRequest
    {
        public UpdateUserRequest(Guid userId) : base(userId)
        {
        }

        public UserEditDTO UserProperties { get; set; }
    }
}
