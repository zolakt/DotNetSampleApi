using SampleApp.Service.User.Messaging;

namespace SampleApp.Service.User
{
    public interface IUserService
    {
        GetUserResponse GetUser(GetUserRequest request);

        GetUsersResponse GetUsers(GetUsersRequest request);

        InsertUserResponse InsertUser(InsertUserRequest request);

        UpdateUserResponse UpdateUser(UpdateUserRequest request);

        DeleteUserResponse DeleteUser(DeleteUserRequest request);
    }
}
