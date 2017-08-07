using System;
using System.Collections.Generic;
using System.Linq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.Repositories;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.DAL.Memory.DataContext;
using SampleApp.DAL.Memory.Mapper.Task;
using SampleApp.Domain.Task;
using SampleApp.Domain.Task.QueryParams;

namespace SampleApp.DAL.Memory.Repositories
{
    public class TaskRepository : RepositoryBase<IDomainContext, Task, DbModels.Task>, ITaskRepository
    {
        private readonly ITaskDatabaseMapper _mapper;

        public TaskRepository(IUnitOfWork unitOfWork, IDataContextFactory<IDomainContext> contextFactory,
            ITaskDatabaseMapper mapper)
            : base(unitOfWork, contextFactory)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            _mapper = mapper;
        }

        public IEnumerable<Task> Find(TaskQueryParam options)
        {
            var results = Context.Tasks.AsEnumerable();

            if (options?.Filter?.UserId != null)
            {
                results = results.Where(x => x.UserId == options.Filter.UserId);
            }

            return ConvertToDomainType(results);
        }


        protected override DbModels.Task ConvertToDatabaseType(Task domainType)
        {
            return _mapper.ConvertToDatabaseType(domainType);
        }

        protected override Task ConvertToDomainType(DbModels.Task databaseType)
        {
            return _mapper.ConvertToDomainType(databaseType, Context);
        }
    }
}