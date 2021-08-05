using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.MaterialDeliveryNote
{
    public class ItemsRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ItemsRepository, ItemsModel, ItemsDataUtil>
    {
        private const string ENTITY = "Items";
        public ItemsRepositoryTest() : base(ENTITY)
        {


        }

        [Fact]
        public override async Task Should_Success_Update()
        {
            string testName = ENTITY + GetCurrentAsyncMethod();
            await Task.CompletedTask;
            var dbContext = DbContext(testName);

            ItemsRepository itemsRepository = new ItemsRepository(dbContext, GetServiceProviderMock(dbContext).Object);

            var data = DataUtil(itemsRepository, dbContext).GetEmptyModel();


            //var materialDeliveryNoteWeavingRepository = new MaterialDeliveryNoteWeavingRepository(dbContext, GetServiceProviderMock(dbContext).Object);

            //var materialDeliveryNoteWeavingData = new MaterialDeliveryNoteWeavingDataUtil(materialDeliveryNoteWeavingRepository).GetEmptyModel();
            //await materialDeliveryNoteWeavingRepository.InsertAsync(materialDeliveryNoteWeavingData);

            //var model = DataUtil(itemsRepository, dbContext).GetModel();

            var itemsRepository2 = new ItemsRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            //var result = itemsRepository.

            // Assert.NotNull(result);
        }
    }
}
