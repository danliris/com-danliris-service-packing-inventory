using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap
{
    public interface IGarmentShippingPaymentDispositionRecapRepository : IRepository<GarmentShippingPaymentDispositionRecapModel>
    {
        IQueryable<GarmentShippingPaymentDispositionRecapItemModel> ReadItemAll();
    }
}

