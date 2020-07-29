using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.QueueService
{
    public class SKUInventoryAzureServiceBusConsumer<T> : IAzureServiceBusConsumer<T>
        where T : ProductSKUInventoryMovementModel
    {
        private const string QUEUE_NAME = "SKUInventory";
        private readonly IQueueManager _queueManager;
        private readonly IInventorySKUMovementService _service;

        public SKUInventoryAzureServiceBusConsumer(IServiceProvider serviceProvider)
        {
            _queueManager = serviceProvider.GetService<IQueueManager>();
            _service = serviceProvider.GetService<IInventorySKUMovementService>();
        }

        public async Task CloseQueueAsync()
        {
            new NotImplementedException();
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var payload = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
            _service.AddMovement(payload);
            var queueClient = _queueManager.GetQueueClient(QUEUE_NAME);
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            var queueClient = _queueManager.GetQueueClient(QUEUE_NAME);
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            return Task.CompletedTask;
        }
    }
}
