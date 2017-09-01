using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SampleApp.DAL.Memory.DataContext.Initializer;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext
{
    public class InMemoryDataContext : IDomainContext
    {
        public InMemoryDataContext(IDatabaseInitializer databaseInitializer)
        {
            Tasks = databaseInitializer.InitializeTasks();
            Users = databaseInitializer.InitializeUsers();
        }

        public IEnumerable<Task> Tasks { get; }

        public IEnumerable<User> Users { get; }


        public IEnumerable<TEntityType> GetAllEntities<TEntityType>() where TEntityType : class
        {
            return GetDataSet<TEntityType>().ToList();
        }

        public TEntityType GetEntity<TEntityType>(object id) where TEntityType : class
        {
            return GetEntityById<TEntityType>(id);
        }

        public void AddEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            GetDataSet<TEntityType>().Add(databaseEntity);
        }

        public void UpdateEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            var entity = GetEntity(databaseEntity);
            var dataset = GetDataSet<TEntityType>();

            if (entity != null)
            {
                dataset.Remove(entity);
            }

            dataset.Add(databaseEntity);
        }

        public void DeleteEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            var entity = GetEntity(databaseEntity);

            if (entity != null)
            {
                GetDataSet<TEntityType>().Remove(entity);
            }
        }


        public bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, ICollection<TProperty>>> navigationProperty)
            where TEntityType : class
            where TProperty : class
        {
            return true;
        }

        public bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, TProperty>> navigationProperty)
            where TEntityType : class
            where TProperty : class
        {
            return true;
        }


        public ICollection<TEntityType> GetDataSet<TEntityType>() where TEntityType : class
        {
            if (typeof(TEntityType) == typeof(Task))
            {
                return (ICollection<TEntityType>) Tasks;
            }

            if (typeof(TEntityType) == typeof(User))
            {
                return (ICollection<TEntityType>) Users;
            }

            throw new Exception("Invalid dataset type");
        }

        private TEntityType GetEntityById<TEntityType>(object id) where TEntityType : class
        {
            if (typeof(TEntityType) == typeof(Task))
            {
                return GetDataSet<Task>().FirstOrDefault(x => x.Id == (Guid) id) as TEntityType;
            }

            if (typeof(TEntityType) == typeof(User))
            {
                return GetDataSet<User>().FirstOrDefault(x => x.Id == (Guid) id) as TEntityType;
            }

            throw new Exception("Invalid dataset type");
        }

        private TEntityType GetEntity<TEntityType>(TEntityType entity) where TEntityType : class
        {
            if (entity is Task)
            {
                return GetEntityById<Task>((entity as Task).Id) as TEntityType;
            }

            if (entity is User)
            {
                return GetEntityById<User>((entity as User).Id) as TEntityType;
            }

            throw new Exception("Invalid dataset type");
        }
    }
}