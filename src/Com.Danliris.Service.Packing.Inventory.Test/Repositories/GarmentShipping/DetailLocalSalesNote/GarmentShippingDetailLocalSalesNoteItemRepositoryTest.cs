using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;

using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingDetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteItemRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingDetailLocalSalesNoteItemRepository, GarmentShippingDetailLocalSalesNoteItemModel, GarmentShippingDetailLocalSalesNoteItemDataUtil>
    {
        private const string ENTITY = "GarmentShippingDetailLocalSalesNoteItem";

        public GarmentShippingDetailLocalSalesNoteItemRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingDetailLocalSalesNoteItemRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);


            data.SetUnitId(data.UnitId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUnitCode(data.UnitCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUnitName(data.UnitName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetQuantity(data.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUomId(data.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUomUnit(data.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetAmount(data.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);   

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
