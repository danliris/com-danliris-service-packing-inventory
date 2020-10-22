using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.PaymentDisposition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingPaymentDispositionRepository, GarmentShippingPaymentDispositionModel, GarmentShippingPaymentDispositionDataUtil>
    {
        private const string ENTITY = "GarmentShippingPaymentDisposition";

        public GarmentShippingPaymentDispositionRepositoryTest() : base(ENTITY)
        {
        }

        
    }
}