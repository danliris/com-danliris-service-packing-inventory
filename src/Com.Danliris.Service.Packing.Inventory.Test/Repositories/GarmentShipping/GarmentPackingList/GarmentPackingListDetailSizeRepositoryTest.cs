using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDetailSizeRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentPackingListDetailSizeRepository, GarmentPackingListDetailSizeModel, GarmentPackingListDetailSizeDataUtil>
    {
        private const string ENTITY = "GarmentPackingListDetailSizeRepositoryTest";

        public GarmentPackingListDetailSizeRepositoryTest() : base(ENTITY)
        {
        }

    }
}
