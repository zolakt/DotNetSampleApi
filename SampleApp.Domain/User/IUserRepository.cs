using System.Collections.Generic;
using SampleApp.Common.Domain.Repositories;
using SampleApp.Domain.User.QueryParams;

namespace SampleApp.Domain.User
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Find(UserQueryParam options);
    }
}