using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public interface IGarmentShippingLocalPriceCuttingNoteRepository : IRepository<GarmentShippingLocalPriceCuttingNoteModel>
    {
        IQueryable<GarmentShippingLocalPriceCuttingNoteItemModel> ReadItemAll();
    }
}
