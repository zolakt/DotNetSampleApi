using SampleApp.Common.DAL.DataContext;
using SampleApp.DAL.EF.DbModels;
using System.Data.Entity;

namespace SampleApp.DAL.EF.DataContext
{
    public interface IDomainContext : IDataContext
    {
        DbSet<Task> Tasks { get; }

        DbSet<User> Users { get; }
    }
}