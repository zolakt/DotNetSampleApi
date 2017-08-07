using SampleApp.Common.DAL.Repositories;
using SampleApp.Common.Domain;

namespace SampleApp.Common.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);

        void RegisterInsertion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);

        void RegisterDeletion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);

        void Commit();
    }
}