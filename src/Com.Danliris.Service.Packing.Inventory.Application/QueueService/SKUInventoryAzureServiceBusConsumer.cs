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
        private readonly QueueClient _queueClient;
        private readonly IInventorySKUService _service;

        public SKUInventoryAzureServiceBusConsumer(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            _queueClient = new QueueClient(configuration["ServiceBusConnectionString"], QUEUE_NAME);
            _service = serviceProvider.GetService<IInventorySKUService>();
        }

        public async Task CloseQueueAsync()
        {
            await _queueClient.CloseAsync();
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var payload = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
            _service.AddMovement(payload);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            return Task.CompletedTask;
        }

    }
}
