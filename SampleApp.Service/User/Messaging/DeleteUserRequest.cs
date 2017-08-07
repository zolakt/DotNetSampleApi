using System;
using SampleApp.Common.Service;

namespace SampleApp.Service.User.Messaging
{
    public class DeleteUserRequest : GuidIdRequest
    {
        public DeleteUserRequest(Guid userId) : base(userId) {}
    }
}