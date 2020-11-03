using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.ShippingNote;

using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceAdjustmentRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingInvoiceAdjustmentRepository, GarmentShippingInvoiceAdjustmentModel, GarmentShippingInvoiceAdjustmentDataUtil>
    {
        private const string ENTITY = "GarmentShippingInvoiceAdjustment";

        public GarmentShippingInvoiceAdjustmentRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingInvoiceAdjustmentRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetAdjustmentDescription(data.AdjustmentDescription + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetAdjustmentValue(data.AdjustmentValue + 1, data.LastModifiedBy, data.LastModifiedAgent);
    
            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
