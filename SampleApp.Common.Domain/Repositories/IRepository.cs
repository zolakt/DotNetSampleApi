namespace SampleApp.Common.Domain.Repositories
{
    public interface IRepository<TAggregateType> : IReadOnlyRepository<TAggregateType>
        where TAggregateType : IAggregateRoot
    {
        void Update(TAggregateType aggregate);

        void Insert(TAggregateType aggregate);

        void Delete(TAggregateType aggregate);
    }
}