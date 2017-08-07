using System;
using System.Collections.Generic;
using System.Linq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.Common.Domain;
using SampleApp.Common.Domain.Repositories;

namespace SampleApp.Common.DAL.Repositories
{
    public abstract class RepositoryBase<TContextType, TDomainType, TDatabaseType> : 
        IUnitOfWorkRepository, IRepository<TDomainType>
        where TContextType : IDataContext
        where TDomainType : class, IAggregateRoot
        where TDatabaseType : class
    {
        private readonly IDataContextFactory<TContextType> _contextFactory;
        private readonly IUnitOfWork _unitOfWork;

        protected RepositoryBase(IUnitOfWork unitOfWork, IDataContextFactory<TContextType> contextFactory)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            _unitOfWork = unitOfWork;
            _contextFactory = contextFactory;
        }

        protected TContextType Context
        {
            get { return _contextFactory.Create(); }
        }


        public void PersistInsertion(IAggregateRoot aggregateRoot)
        {
            if (aggregateRoot != null)
            {
                var databaseType = ConvertToDatabaseType(aggregateRoot as TDomainType);
                Context.AddEntity(databaseType);
            }
        }

        public void PersistUpdate(IAggregateRoot aggregateRoot)
        {
            if (aggregateRoot != null)
            {
                var databaseType = ConvertToDatabaseType(aggregateRoot as TDomainType);
                Context.UpdateEntity(databaseType);
            }
        }

        public void PersistDeletion(IAggregateRoot aggregateRoot)
        {
            if (aggregateRoot != null)
            {
                var databaseType = ConvertToDatabaseType(aggregateRoot as TDomainType);
                Context.DeleteEntity(databaseType);
            }
        }


        public virtual TDomainType FindBy(object id)
        {
            var result = Context.GetEntity<TDatabaseType>(id);
            return (result != null) ? ConvertToDomainType(result) : null;
        }

        public virtual IEnumerable<TDomainType> FindAll()
        {
            var result = Context.GetAllEntities<TDatabaseType>().ToList();
            return ConvertToDomainType(result);
        }

        public void Update(TDomainType aggregate)
        {
            if (aggregate != null)
            {
                _unitOfWork.RegisterUpdate(aggregate, this);
            }
        }

        public void Insert(TDomainType aggregate)
        {
            if (aggregate != null)
            {
                _unitOfWork.RegisterInsertion(aggregate, this);
            }
        }

        public void Delete(TDomainType aggregate)
        {
            if (aggregate != null)
            {
                _unitOfWork.RegisterDeletion(aggregate, this);
            }
        }


        protected abstract TDatabaseType ConvertToDatabaseType(TDomainType domainType);

        protected abstract TDomainType ConvertToDomainType(TDatabaseType databaseType);


        protected virtual IEnumerable<TDatabaseType> ConvertToDatabaseType(IEnumerable<TDomainType> domainType)
        {
            foreach (var x in domainType)
            {
                yield return ConvertToDatabaseType(x);
            }
        }

        protected virtual IEnumerable<TDomainType> ConvertToDomainType(IEnumerable<TDatabaseType> databaseType)
        {
            foreach (var x in databaseType)
            {
                yield return ConvertToDomainType(x);
            }
        }
    }
}