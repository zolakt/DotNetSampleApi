using System;
using System.Collections.Generic;
using System.Linq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.Repositories;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.DAL.EF.DataContext;
using SampleApp.DAL.EF.Mapper.User;
using SampleApp.Domain.User;
using SampleApp.Domain.User.QueryParams;

namespace SampleApp.DAL.EF.Repositories
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

            if (options?.Sort?.Latest == true)
            {
                results = results.OrderByDescending(x => x.Id);
            }

            if (options?.Pagination?.Offset > 0)
            {
                results = results.Skip(options.Pagination.Offset.Value);
            }

            if (options?.Pagination?.Limit > 1)
            {
                results = results.Take(options.Pagination.Limit.Value);
            }

            var tmp = results.ToList();
            return ConvertToDomainType(tmp);
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