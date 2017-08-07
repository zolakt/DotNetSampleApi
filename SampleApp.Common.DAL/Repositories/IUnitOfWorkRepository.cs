using SampleApp.Common.Domain;

namespace SampleApp.Common.DAL.Repositories
{
    public interface IUnitOfWorkRepository
    {
        void PersistInsertion(IAggregateRoot aggregateRoot);

        void PersistUpdate(IAggregateRoot aggregateRoot);

        void PersistDeletion(IAggregateRoot aggregateRoot);
    }
}