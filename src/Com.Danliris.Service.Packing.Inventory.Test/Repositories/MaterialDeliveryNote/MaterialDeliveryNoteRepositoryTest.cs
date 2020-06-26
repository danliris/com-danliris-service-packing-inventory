using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, MaterialDeliveryNoteRepository, MaterialDeliveryNoteModel, MaterialDeliveryNoteDataUtil>
    {
        private const string ENTITY = "MaterialDeliveryNote";
        public MaterialDeliveryNoteRepositoryTest() : base(ENTITY)
        {

        }

        [Fact]
        public async Task Should_Success_Update_When_locitem_Not_Null()
        {
            string testName = ENTITY + GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var repo = new MaterialDeliveryNoteRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new MaterialDeliveryNoteRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            
            var result = await repo2.UpdateAsync(data.Id, emptyData);

            Assert.NotEqual(0, result);

        }

        [Fact]
        public async Task Should_Success_Insert_99()
        {
            string testName = ENTITY + GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var repo = new MaterialDeliveryNoteRepository(dbContext, GetServiceProviderMock(dbContext).Object);           

            for(int i=0;i<=1000; i++)
            {
                var emptyData = DataUtil(repo, dbContext).GetModel();
                var result = await repo.InsertAsync(emptyData);
                Assert.NotEqual(0, result);
            }

        }
    }
}
