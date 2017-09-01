using System;
using SampleApp.Common.DAL.DataContext;
using SampleApp.DAL.EF.Mapper.User;

namespace SampleApp.DAL.EF.Mapper.Task
{
    public class TaskDatabaseMapper : ITaskDatabaseMapper
    {
        private readonly IUserDatabaseMapper _userMapper;

        public TaskDatabaseMapper(IUserDatabaseMapper userMapper)
        {
            if (userMapper == null)
            {
                throw new ArgumentNullException("userMapper");
            }

            _userMapper = userMapper;
        }


        public DbModels.Task ConvertToDatabaseType(Domain.Task.Task domainType)
        {
            return new DbModels.Task {
                Id = domainType.Id,
                Name = domainType.Name,
                Time = domainType.Time,
                UserId = domainType.User.Id
            };
        }

        public Domain.Task.Task ConvertToDomainType(DbModels.Task databaseType, IDataContext context)
        {
            var result = new Domain.Task.Task {
                Id = databaseType.Id,
                Name = databaseType.Name,
                Time = databaseType.Time,
                User = new Domain.User.User {
                    Id = databaseType.UserId
                }
            };

            if (context.IsLoaded(databaseType, x => x.User))
            {
                result.User = _userMapper.ConvertToDomainType(databaseType.User, context);
            }

            return result;
        }
    }
}