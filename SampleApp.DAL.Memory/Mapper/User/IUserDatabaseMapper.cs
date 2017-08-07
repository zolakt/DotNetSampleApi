using SampleApp.Common.DAL.DataContext;

namespace SampleApp.DAL.Memory.Mapper.User
{
    public interface IUserDatabaseMapper
    {
        DbModels.User ConvertToDatabaseType(Domain.User.User domainType);

        Domain.User.User ConvertToDomainType(DbModels.User databaseType, IDataContext context);
    }
}