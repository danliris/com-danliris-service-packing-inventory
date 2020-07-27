using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.ShippingNote;

using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingNote
{
    public class ShippingNoteItemRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingNoteItemRepository, GarmentShippingNoteItemModel, GarmentShippingNoteItemDataUtil>
    {
        private const string ENTITY = "GarmentShippingNoteItem";

        public ShippingNoteItemRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingNoteItemRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetDescription(data.Description + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetCurrencyId(data.CurrencyId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetCurrencyCode(data.CurrencyCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetAmount(data.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
