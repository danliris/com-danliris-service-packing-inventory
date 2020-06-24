using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalSalesNoteRepository, GarmentShippingLocalSalesNoteModel, GarmentShippingLocalSalesNoteDataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalSalesNote";

        public GarmentShippingLocalSalesNoteRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTempo(model.Tempo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetDispositionNo(model.DispositionNo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUseVat(!model.UseVat, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRemark(model.Remark + 1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetProductId(item.ProductId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductCode(item.ProductCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductName(item.ProductName + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetQuantity(item.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomId(item.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomUnit(item.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPrice(item.Price + 1, data.LastModifiedBy, data.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
