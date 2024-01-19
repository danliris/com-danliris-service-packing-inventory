using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;

using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingLocalSalesNoteTS
{
    public class GarmentShippingLocalSalesNoteTSItemRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalSalesNoteItemRepository, GarmentShippingLocalSalesNoteItemModel, GarmentShippingLocalSalesNoteItemDataUtil>
    {
        private const string ENTITY = "GarmentShippingNoteItem";

        public GarmentShippingLocalSalesNoteTSItemRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingLocalSalesNoteItemRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetProductId(data.ProductId +1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetProductCode(data.ProductCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetProductName(data.ProductName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetQuantity(data.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUomId(data.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUomUnit(data.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPrice(data.Price + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPackageQuantity(data.PackageQuantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPackageUomId(data.PackageUomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPackageUomUnit(data.PackageUomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
