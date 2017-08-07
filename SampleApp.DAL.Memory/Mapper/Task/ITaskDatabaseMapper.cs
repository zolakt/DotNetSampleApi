using SampleApp.Common.DAL.DataContext;

namespace SampleApp.DAL.Memory.Mapper.Task
{
    public interface ITaskDatabaseMapper
    {
        DbModels.Task ConvertToDatabaseType(Domain.Task.Task domainType);

        Domain.Task.Task ConvertToDomainType(DbModels.Task databaseType, IDataContext context);
    }
}