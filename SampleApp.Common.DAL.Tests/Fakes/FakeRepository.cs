using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.Repositories;
using SampleApp.Common.DAL.UnitOfWork;

namespace SampleApp.Common.DAL.Tests.Fakes
{
    public class FakeRepository : RepositoryBase<IDataContext, FakeDomainUser, FakeDatabaseUser>
    {
        public FakeRepository(IUnitOfWork unitOfWork, IDataContextFactory<IDataContext> contextFactory)
            : base(unitOfWork, contextFactory) {}

        protected override FakeDatabaseUser ConvertToDatabaseType(FakeDomainUser domainType)
        {
            return new FakeDatabaseUser {
                Id = domainType.Id,
                Name = domainType.Name
            };
        }

        protected override FakeDomainUser ConvertToDomainType(FakeDatabaseUser databaseType)
        {
            return new FakeDomainUser {
                Id = databaseType.Id,
                Name = databaseType.Name
            };
        }
    }
}