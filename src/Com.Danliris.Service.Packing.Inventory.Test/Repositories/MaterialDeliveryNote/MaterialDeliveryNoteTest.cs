using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;


namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteTest : BaseRepositoryTest<PackingInventoryDbContext, MaterialDeliveryNoteRepository, Data.MaterialDeliveryNoteModel, MaterialDeliveryNoteDataUtil>
    {

        private const string ENTITY = "MaterialDeliveryNote";

        public MaterialDeliveryNoteTest() : base(ENTITY)
        {

        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new MaterialDeliveryNoteRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAll();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_GetMaterialOrder()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new MaterialDeliveryNoteRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadByIdAsync(data.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_InsertMaterial()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            MaterialDeliveryNoteRepository repo = new MaterialDeliveryNoteRepository(dbContext, serviceProvider);
            var data = DataUtil(repo, dbContext).GetModel();
            var result = await repo.InsertAsync(data);
            Assert.NotEqual(0, result);
        }

        public override async Task Should_Success_Update()
        {
            string testName = GetCurrentMethod() + "Update2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new MaterialDeliveryNoteRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new MaterialDeliveryNoteRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();
            
            
            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        public override async Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            MaterialDeliveryNoteRepository repo = new MaterialDeliveryNoteRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteAsync(data.Id);

            Assert.NotEqual(0, result);
        }
    }
}
