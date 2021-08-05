using Microsoft.Azure.ServiceBus;
using System;

namespace Com.Danliris.Service.Packing.Inventory.EventBusServiceBus
{
    public interface IServiceBusPersisterConnection : IDisposable
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }
        ITopicClient CreateModel();
    }
}
