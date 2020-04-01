using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
