using SampleApp.DAL.EF.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace SampleApp.DAL.EF.DataContext
{
    public class EfDataContext : DbContext, IDomainContext
    {
        public EfDataContext() : base("SampleApp")
        {
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }

        public IEnumerable<TEntityType> GetAllEntities<TEntityType>() where TEntityType : class
        {
            return GetDataSet<TEntityType>().ToList();
        }

        public TEntityType GetEntity<TEntityType>(object id) where TEntityType : class
        {
            return GetDataSet<TEntityType>().Find(id);
        }

        public void AddEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            GetDataSet<TEntityType>().Add(databaseEntity);
            SaveChanges();
        }

        public void UpdateEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            GetDataSet<TEntityType>().AddOrUpdate(databaseEntity);
            SaveChanges();
        }

        public void DeleteEntity<TEntityType>(TEntityType databaseEntity) where TEntityType : class
        {
            var keyValue = GetKeys(databaseEntity);
            var dataset = GetDataSet<TEntityType>();

            var entity = dataset.Find(keyValue);

            if (entity != null)
            {
                dataset.Remove(entity);
            }

            SaveChanges();
        }


        public bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, ICollection<TProperty>>> navigationProperty)
            where TEntityType : class
            where TProperty : class
        {
            return Entry(databaseEntity).Collection(navigationProperty).IsLoaded;
        }

        public bool IsLoaded<TEntityType, TProperty>(TEntityType databaseEntity,
            Expression<Func<TEntityType, TProperty>> navigationProperty)
            where TEntityType : class
            where TProperty : class
        {
            return Entry(databaseEntity).Reference(navigationProperty).IsLoaded;
        }


        public DbSet<TEntityType> GetDataSet<TEntityType>() where TEntityType : class
        {
            return Set<TEntityType>();
        }

        private string[] GetKeyNames<TEntityType>() where TEntityType : class
        {
            Type t = typeof(TEntityType);

            //retrieve the base type
            while (t.BaseType != typeof(object))
            {
                t = t.BaseType;
            }

            var objectContext = ((IObjectContextAdapter)this).ObjectContext;

            //create method CreateObjectSet with the generic parameter of the base-type
            var method = typeof(ObjectContext)
                                      .GetMethod("CreateObjectSet", Type.EmptyTypes)
                                      .MakeGenericMethod(t);
            dynamic objectSet = method.Invoke(objectContext, null);
            IEnumerable<dynamic> keyMembers = objectSet.EntitySet.ElementType.KeyMembers;
            string[] keyNames = keyMembers.Select(k => (string)k.Name).ToArray();
            return keyNames;
        }

        public object[] GetKeys<TEntityType>(TEntityType entity) where TEntityType : class
        {
            var keyNames = GetKeyNames<TEntityType>();
            Type type = typeof(TEntityType);

            object[] keys = new object[keyNames.Length];
            for (int i = 0; i < keyNames.Length; i++)
            {
                keys[i] = type.GetProperty(keyNames[i]).GetValue(entity, null);
            }
            return keys;
        }
    }
}