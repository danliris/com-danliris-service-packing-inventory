using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.QueueService
{
    public interface IAzureServiceBusSender<T>
    {
        Task SendMessage(T payload);
    }
}
