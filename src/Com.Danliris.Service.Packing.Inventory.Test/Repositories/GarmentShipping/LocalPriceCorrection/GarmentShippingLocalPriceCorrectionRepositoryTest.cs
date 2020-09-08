using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalPriceCorrectionNote;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalPriceCorrectionNoteRepository, GarmentShippingLocalPriceCorrectionNoteModel, GarmentShippingLocalPriceCorrectionNoteDataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalPriceCorrectionNote";

        public GarmentShippingLocalPriceCorrectionNoteRepositoryTest() : base(ENTITY)
        {
        }
    }
}
