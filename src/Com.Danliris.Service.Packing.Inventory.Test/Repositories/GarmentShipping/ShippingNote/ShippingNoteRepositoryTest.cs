using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCoverLetter;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingNote
{
    public class ShippingNoteRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingNoteRepository, GarmentShippingNoteModel, GarmentShippingNoteDataUtil>
    {
        private const string ENTITY = "GarmentShippingNote";

        public ShippingNoteRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingNoteRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerId(data.BuyerId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerCode(data.BuyerCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerName(data.BuyerName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTotalAmount(data.TotalAmount + 1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetDescription(item.Description + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetCurrencyId(item.CurrencyId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetCurrencyCode(item.CurrencyCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount(item.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
