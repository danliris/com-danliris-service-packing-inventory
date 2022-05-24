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
        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentPackingListDetailSizeRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetQuantity(data.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSizeIdx(data.SizeIdx + 1, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }


    }
}
