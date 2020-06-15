using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.MaterialDeliveryNote
{
   public class ItemsMaterialDeliveryNoteWeavingRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ItemsMaterialDeliveryNoteWeavingRepository, ItemsMaterialDeliveryNoteWeavingModel, ItemsMaterialDeliveryNoteWeavingDataUtil>
    {
        private const string ENTITY = "ItemsMaterialDeliveryNoteWeaving";

        public ItemsMaterialDeliveryNoteWeavingRepositoryTest() : base(ENTITY)
        {


        }

        [Fact]
        public override async Task Should_Success_Update()
        {
            string testName =ENTITY + GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var itemsMaterialDeliveryNoteWeavingRepository = new ItemsMaterialDeliveryNoteWeavingRepository(dbContext, GetServiceProviderMock(dbContext).Object);

            var data = DataUtil(itemsMaterialDeliveryNoteWeavingRepository, dbContext).GetEmptyModel();
            data.Id = 0;
            data.SetinputBale(2);
            data.SetinputKg(2);
            data.SetinputPiece(2);
            data.SetItemCode("NewCode");
            data.SetitemGrade("New Grade");
            data.SetinputMeter(2);
            await itemsMaterialDeliveryNoteWeavingRepository.InsertAsync(data);

            var materialDeliveryNoteWeavingRepository = new MaterialDeliveryNoteWeavingRepository(dbContext, GetServiceProviderMock(dbContext).Object);
           
            var materialDeliveryNoteWeavingData = new MaterialDeliveryNoteWeavingDataUtil(materialDeliveryNoteWeavingRepository).GetEmptyModel();
            await materialDeliveryNoteWeavingRepository.InsertAsync(materialDeliveryNoteWeavingData);

            var model = DataUtil(itemsMaterialDeliveryNoteWeavingRepository, dbContext).GetModel();

            var itemsMaterialDeliveryNoteWeavingRepository2 = new ItemsMaterialDeliveryNoteWeavingRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var result = itemsMaterialDeliveryNoteWeavingRepository2.UpdateAsync(1, model);

            Assert.NotNull(result);
        }




    }
}
