using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.QueueService
{
    public class SKUInventoryAzureServiceBusSender<T> : IAzureServiceBusSender<T>
        where T : ProductSKUInventoryMovementModel
    {
        private const string QUEUE_NAME = "SKUInventory";
        private readonly IQueueManager _queueManager;

        public SKUInventoryAzureServiceBusSender(IQueueManager queueManager)
        {
            _queueManager = queueManager;
        }

        public async Task SendMessage(T payload)
        {
            var queueClient = await _queueManager.GetOrCreateQueueClient(QUEUE_NAME);
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await queueClient.SendAsync(message);
        }
    }
}
