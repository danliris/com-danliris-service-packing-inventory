namespace Com.Danliris.Service.Packing.Inventory.Domain.SeedWork
{
    public interface IRepository<T>
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
