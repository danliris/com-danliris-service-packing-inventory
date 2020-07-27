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
        private readonly QueueClient _queueClient;

        public SKUInventoryAzureServiceBusSender(IConfiguration configuration)
        {
            _queueClient = new QueueClient(configuration["ServiceBusConnectionString"], QUEUE_NAME);
        }

        public async Task SendMessage(T payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }
    }
}
