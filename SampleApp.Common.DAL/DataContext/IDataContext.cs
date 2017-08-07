using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleApp.Common.DAL.DataContext
{
    public interface IDataContext
    {
        IEnumerable<TEntityType> GetAllEntities<TEntityType>() where TEntityType : class;

        TEntityType GetEntity<TEntityType>(object id) where TEntityType : class;

        void AddEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class;

        void UpdateEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class;

        void DeleteEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class;

        bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, TProperty>> navigationProperty)
            where TEntityType : class
            where TProperty : class;

        bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, ICollection<TProperty>>> navigationProperty)
            where TEntityType : class
            where TProperty : class;
    }
}