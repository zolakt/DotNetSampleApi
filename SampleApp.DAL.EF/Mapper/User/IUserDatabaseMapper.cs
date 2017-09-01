using SampleApp.Common.DAL.DataContext;

namespace SampleApp.DAL.EF.Mapper.User
{
    public interface IUserDatabaseMapper
    {
        DbModels.User ConvertToDatabaseType(Domain.User.User domainType);

        Domain.User.User ConvertToDomainType(DbModels.User databaseType, IDataContext context);
    }
}