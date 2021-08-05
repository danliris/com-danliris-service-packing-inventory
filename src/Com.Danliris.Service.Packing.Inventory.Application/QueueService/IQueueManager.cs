using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.QueueService
{
    public interface IQueueManager
    {
        IQueueClient GetQueueClient(string queueName);
        Task<IQueueClient> GetOrCreateQueueClient(string queueName);
    }
}
