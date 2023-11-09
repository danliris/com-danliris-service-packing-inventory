using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingPackingList;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDetailRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingPackingListDetailRepository, GarmentShippingPackingListDetailModel, GarmentShippingPackingListDetailDataUtil>
    {
        private const string ENTITY = "GarmentPackingListDetailRepositoryTest";

        public GarmentShippingPackingListDetailRepositoryTest() : base(ENTITY)
        {
        }
        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingPackingListDetailRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetLength(data.Length + 1, data.LastModifiedBy, data.LastModifiedAgent);


            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }

    }
}
