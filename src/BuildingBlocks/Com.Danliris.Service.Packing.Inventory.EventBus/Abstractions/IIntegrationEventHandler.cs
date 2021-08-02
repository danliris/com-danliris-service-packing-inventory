using Com.Danliris.Service.Packing.Inventory.EventBus.Events;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
