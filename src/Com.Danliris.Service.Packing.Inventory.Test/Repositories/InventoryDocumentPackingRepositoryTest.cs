using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class InventoryDocumentPackingRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, InventoryDocumentPackingRepository, InventoryDocumentPackingModel, InventoryDocumentPackingDataUtil>
    {
        private const string Entity = "InventoryDocumentPacking";
        public InventoryDocumentPackingRepositoryTest() : base(Entity)
        {
        }

        public override async Task Should_Success_Update()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            InventoryDocumentPackingRepository repo = new InventoryDocumentPackingRepository(dbContext, serviceProvider);
            InventoryDocumentPackingRepository repo2 = new InventoryDocumentPackingRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            await Assert.ThrowsAsync<NotImplementedException>(() => repo2.UpdateAsync(data.Id, model));
        }

        public override async Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            InventoryDocumentPackingRepository repo = new InventoryDocumentPackingRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();

            await Assert.ThrowsAsync<NotImplementedException>(() => repo.DeleteAsync(data.Id));
        }

    }
}
