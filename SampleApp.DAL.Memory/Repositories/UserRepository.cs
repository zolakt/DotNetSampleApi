using System;
using System.Collections.Generic;
using System.Linq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.Repositories;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.DAL.Memory.DataContext;
using SampleApp.DAL.Memory.Mapper.User;
using SampleApp.Domain.User;
using SampleApp.Domain.User.QueryParams;

namespace SampleApp.DAL.Memory.Repositories
{
    public class UserRepository : RepositoryBase<IDomainContext, User, DbModels.User>, IUserRepository
    {
        private readonly IUserDatabaseMapper _mapper;

        public UserRepository(IUnitOfWork unitOfWork, IDataContextFactory<IDomainContext> contextFactory,
            IUserDatabaseMapper mapper)
            : base(unitOfWork, contextFactory)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            _mapper = mapper;
        }

        public IEnumerable<User> Find(UserQueryParam options)
        {
            var results = Context.Users.AsEnumerable();

            if (!string.IsNullOrEmpty(options?.Filter?.Country))
            {
                results = results.Where(x => x.Country == options.Filter.Country);
            }

            return ConvertToDomainType(results);
        }


        protected override DbModels.User ConvertToDatabaseType(User domainType)
        {
            return _mapper.ConvertToDatabaseType(domainType);
        }

        protected override User ConvertToDomainType(DbModels.User databaseType)
        {
            return _mapper.ConvertToDomainType(databaseType, Context);
        }
    }
}