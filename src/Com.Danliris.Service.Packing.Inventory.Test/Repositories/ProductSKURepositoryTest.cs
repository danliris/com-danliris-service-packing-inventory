using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class ProductSKURepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ProductSKURepository, ProductSKUModel, ProductSKUDataUtil>
    {
        private const string Entity = "ProductPacking";
        public ProductSKURepositoryTest() : base(Entity)
        {
        }

        [Fact]
        public async Task Should_Success_InsertGreige()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductSKURepository repo = new ProductSKURepository(dbContext, serviceProvider);
            var data = DataUtil(repo, dbContext).GetModelGreige();
            var result = await repo.InsertAsync(data);
            Assert.NotEqual(0, result);
        }

        

        [Fact]
        public async Task IsExist_Return_true()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductSKURepository repo = new ProductSKURepository(dbContext, serviceProvider);
            var data = DataUtil(repo, dbContext).GetModelYarn();
            await repo.InsertAsync(data);
            var result = await repo.IsExist(data.Name);
            Assert.True(result);
        }

        [Fact]
        public async Task Should_Success_InsertYarn()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductSKURepository repo = new ProductSKURepository(dbContext, serviceProvider);
            var data = DataUtil(repo, dbContext).GetModelYarn();
            var result = await repo.InsertAsync(data);
            Assert.NotEqual(0, result);
        }
    }
}
