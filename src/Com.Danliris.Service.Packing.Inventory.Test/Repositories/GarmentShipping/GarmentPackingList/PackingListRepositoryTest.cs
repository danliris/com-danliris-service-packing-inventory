using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentPackingList
{
    public class PackingListRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentPackingListRepository, GarmentPackingListModel, GarmentPackingListDataUtil>
    {
        private const string ENTITY = "GarmentPackingList";

        public PackingListRepositoryTest() : base(ENTITY)
        {
        }
    }
}
