using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.QueueService
{
    public class QueueManager : IQueueManager
    {
        private readonly string _serviceBusConnectionString;

        public QueueManager(IConfiguration configuration)
        {
            _serviceBusConnectionString = configuration["ServiceBusConnectionString"];
        }

        public async Task<IQueueClient> GetOrCreateQueueClient(string queueName)
        {
            var managementClient = new ManagementClient(_serviceBusConnectionString);
            if (!(await managementClient.QueueExistsAsync(queueName)))
            {
                await managementClient.CreateQueueAsync(new QueueDescription(queueName));
            }

            var queueClient = new QueueClient(_serviceBusConnectionString, queueName);
            return queueClient;
        }

        public IQueueClient GetQueueClient(string queueName)
        {
            var queueClient = new QueueClient(_serviceBusConnectionString, queueName);
            return queueClient;
        }
    }
}
