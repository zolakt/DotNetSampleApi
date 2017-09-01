using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.Repositories;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.DAL.EF.DataContext;
using SampleApp.DAL.EF.Mapper.Task;
using SampleApp.Domain.Task;
using SampleApp.Domain.Task.QueryParams;

namespace SampleApp.DAL.EF.Repositories
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
            var results = Context.Tasks.AsQueryable();

            if (options?.Include?.User == true)
            {
                results = results.Include(t => t.User);
            }

            if (options?.Filter?.UserId != null)
            {
                results = results.Where(x => x.UserId == options.Filter.UserId);
            }

            if (options?.Sort?.Latest == true)
            {
                results = results.OrderByDescending(x => x.Time);
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