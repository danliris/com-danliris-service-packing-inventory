using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice;

using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceItemRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingInvoiceItemRepository, GarmentShippingInvoiceItemModel, GarmentShippingInvoiceItemDataUtil>
    {
        private const string ENTITY = "GarmentShippingInvoiceItem";

        public GarmentShippingInvoiceItemRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingInvoiceItemRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetCMTPrice(data.CMTPrice + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetComodityDesc(data.ComodityDesc + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetMarketingName(data.MarketingName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPrice(data.Price +1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUomId(data.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUomUnit(data.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
