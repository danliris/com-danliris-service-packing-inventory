using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.PaymentDispositionRecap;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.ShippingPaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingPaymentDispositionRecapRepository, GarmentShippingPaymentDispositionRecapModel, PaymentDispositionRecapDataUtil>
    {
        private const string ENTITY = "GarmentShippingPaymentDispositionRecap";

        public GarmentShippingPaymentDispositionRecapRepositoryTest() : base(ENTITY)
        {
        }
    }
}