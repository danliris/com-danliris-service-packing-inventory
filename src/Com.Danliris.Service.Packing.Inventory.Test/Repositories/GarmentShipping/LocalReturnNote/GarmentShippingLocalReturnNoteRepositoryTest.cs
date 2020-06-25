using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalReturnNote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalReturnNoteRepository, GarmentShippingLocalReturnNoteModel, GarmentShippingLocalReturnNoteDataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalReturnNote";

        public GarmentShippingLocalReturnNoteRepositoryTest() : base(ENTITY)
        {
        }
    }
}