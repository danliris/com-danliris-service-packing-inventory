using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalPriceCuttingNote;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalPriceCuttingNoteRepository, GarmentShippingLocalPriceCuttingNoteModel, GarmentShippingLocalPriceCuttingNoteDataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalPriceCuttingNote";

        public GarmentShippingLocalPriceCuttingNoteRepositoryTest() : base(ENTITY)
        {
        }
    }
}
