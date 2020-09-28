using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class DyeingPrintingAreaInputProductionOrderRepositoryTest
         : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderModel, DyeingPrintingAreaInputProductionOrderDataUtil>
    {
        private const string ENTITY = "DyeingPrintingAreaInputProductionOrder";
        public DyeingPrintingAreaInputProductionOrderRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_GetProductionOrder()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetInputProductionOrder(data.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAllIgnoreQueryFilter();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromFabricQualityControlAsync()
        {
            string testName = GetCurrentMethod() + "UpdateFromFabricQualityControlAsync";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var result = await repo.UpdateFromFabricQualityControlAsync(data.Id, "sss", true, 100, 100, 100, 100);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromOutputAsync()
        {
            string testName = GetCurrentMethod() + "UpdateFromOutputAsync";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var result = await repo.UpdateFromOutputAsync(data.Id, false);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromOutputIMSimpleAsync()
        {
            string testName = GetCurrentMethod() + "UpdateFromOutputIMSimpleAsync";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var result = await repo.UpdateFromOutputIMAsync(data.Id, 2);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromOutputIMSimpleAsync2()
        {
            string testName = GetCurrentMethod() + "UpdateFromOutputIMSimpleAsync2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var result = await repo.UpdateFromOutputIMAsync(data.Id, 1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromOutputAsyncBalance()
        {
            string testName = GetCurrentMethod() + "UpdateFromOutputAsyncBalance";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateFromOutputAsync(data.Id, 1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateBalanceAndRemains()
        {
            string testName = GetCurrentMethod() + "UpdateBalanceAndRemains";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateBalanceAndRemainsAsync(data.Id, 5);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateBalanceAndRemains2()
        {
            string testName = GetCurrentMethod() + "UpdateBalanceAndRemains2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateBalanceAndRemainsAsync(data.Id, 2);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromOutputAsyncBalance2()
        {
            string testName = GetCurrentMethod() + "UpdateFromOutputAsyncBalance2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateFromOutputAsync(data.Id, 2);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromOutputIMAsyncBalance()
        {
            string testName = GetCurrentMethod() + "UpdateFromOutputIMAsyncBalance";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateFromOutputIMAsync(data.Id, 1, 100, 100, 100);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_UpdateFromOutputIMAsyncBalance2()
        {
            string testName = GetCurrentMethod() + "UpdateFromOutputIMAsyncBalance2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateFromOutputIMAsync(data.Id, 2, 100, 100, 100);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromNextAreaInputAsync()
        {
            string testName = GetCurrentMethod() + "UpdateFromNextAreaInputAsync";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = DataUtil(repo, dbContext).GetModelShipping();
            await repo.InsertAsync(data);
            var result = await repo.UpdateFromNextAreaInputAsync(data.Id, 1, 1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromNextAreaInputAsyncGJ()
        {
            string testName = GetCurrentMethod() + "UpdateFromNextAreaInputAsyncGJ";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = DataUtil(repo, dbContext).GetModelGudangJadi();
            await repo.InsertAsync(data);
            var result = await repo.UpdateFromNextAreaInputAsync(data.Id, 1, 1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateFromNextAreaInputAsyncTR()
        {
            string testName = GetCurrentMethod() + "UpdateFromNextAreaInputAsyncTR";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = DataUtil(repo, dbContext).GetModelTransit();
            await repo.InsertAsync(data);
            var result = await repo.UpdateFromNextAreaInputAsync(data.Id, 1, 1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateBalanceAndRemainsWithFlagAsync2()
        {
            string testName = GetCurrentMethod() + "UpdateBalanceAndRemainsWithFlagAsync2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateBalanceAndRemainsWithFlagAsync(data.Id, 1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateBalanceAndRemainsWithFlagAsync()
        {
            string testName = GetCurrentMethod() + "UpdateBalanceAndRemainsWithFlagAsync";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateBalanceAndRemainsWithFlagAsync(data.Id, 5);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateBalanceAndRemainsWithFlagAsyncQtyPacking()
        {
            string testName = GetCurrentMethod() + "Should_Success_UpdateBalanceAndRemainsWithFlagAsyncQtyPacking";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateBalanceAndRemainsWithFlagAsync(data.Id, 5, 2);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_UpdateBalanceAndRemainsWithFlagAsyncQtyPacking2()
        {
            string testName = GetCurrentMethod() + "Should_Success_UpdateBalanceAndRemainsWithFlagAsyncQtyPacking2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.UpdateBalanceAndRemainsWithFlagAsync(data.Id, 1, 1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdatePackingFromOut()
        {
            string testName = GetCurrentMethod() + "UpdatePackingFromOut";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            emptyData.SetArea("PACKING", "", "");
            emptyData.SetBalanceRemains(4, "", "");
            emptyData.SetBalance(4, "", "");
            await repo.InsertAsync(emptyData);
            var result = await repo2.UpdatePackingFromOut("INSPECTION MATERIAL", emptyData.ProductionOrderNo, emptyData.Grade, 2);

            Assert.NotEqual(0, result.Item1);
        }

        [Fact]
        public virtual async Task Should_Success_UpdatePackingFromOut2()
        {
            string testName = GetCurrentMethod() + "UpdatePackingFromOut2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            emptyData.SetArea("PACKING", "", "");
            emptyData.SetBalanceRemains(4, "", "");
            emptyData.SetBalance(4, "", "");
            await repo.InsertAsync(emptyData);
            var emptyData2 = DataUtil(repo, dbContext).GetEmptyModel();
            emptyData2.SetArea("PACKING", "", "");
            emptyData2.SetBalanceRemains(4, "", "");
            emptyData2.SetBalance(4, "", "");
            await repo.InsertAsync(emptyData2);
            var result = await repo2.UpdatePackingFromOut("INSPECTION MATERIAL", emptyData.ProductionOrderNo, emptyData.Grade, 6);

            Assert.NotEqual(0, result.Item1);
        }

        [Fact]
        public virtual async Task Should_Success_RestorePacking()
        {
            string testName = GetCurrentMethod() + "RestorePacking";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            emptyData.SetArea("PACKING", "", "");
            emptyData.SetBalanceRemains(4, "", "");
            emptyData.SetBalance(4, "", "");
            await repo.InsertAsync(emptyData);
            var packingData = new PackingData()
            {
                Id = emptyData.Id,
                Balance = 4
            };
            var result = await repo2.RestorePacking("INSPECTION MATERIAL", new List<PackingData>() { packingData });

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromNextAreaInputPackingAsync()
        {
            string testName = GetCurrentMethod() + "UpdateFromNextAreaInputPackingAsync";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            var repo = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputProductionOrderRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            emptyData.SetArea("PACKING", "", "");
            emptyData.SetBalanceRemains(4, "", "");
            emptyData.SetBalance(4, "", "");
            await repo.InsertAsync(emptyData);
            var packingData = new PackingData()
            {
                Id = emptyData.Id,
                Balance = 4
            };
            var result = await repo2.UpdateFromNextAreaInputPackingAsync(new List<PackingData>() { packingData });

            Assert.NotEqual(0, result);
        }
    }
}
