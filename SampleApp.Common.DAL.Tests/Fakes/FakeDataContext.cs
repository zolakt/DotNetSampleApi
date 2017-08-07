using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SampleApp.Common.DAL.DataContext;

namespace SampleApp.Common.DAL.Tests.Fakes
{
    public class FakeDataContext : IDataContext
    {
        public ICollection<TEntityType> GetDataSet<TEntityType>() where TEntityType : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntityType> GetAllEntities<TEntityType>() where TEntityType : class
        {
            throw new NotImplementedException();
        }

        public TEntityType GetEntity<TEntityType>(object id) where TEntityType : class
        {
            throw new NotImplementedException();
        }

        public void AddEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            throw new NotImplementedException();
        }

        public bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, TProperty>> navigationProperty) where TEntityType : class
            where TProperty : class
        {
            throw new NotImplementedException();
        }

        public bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, ICollection<TProperty>>> navigationProperty) where TEntityType : class
            where TProperty : class
        {
            throw new NotImplementedException();
        }
    }
}