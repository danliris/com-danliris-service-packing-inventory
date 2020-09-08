using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class DyeingPrintingAreaOutputProductionOrderRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingAreaOutputProductionOrderRepository, DyeingPrintingAreaOutputProductionOrderModel, DyeingPrintingAreaOutputProductionOrderDataUtil>
    {
        private const string ENTITY = "DyeingPrintingAreaOutputProductionOrder";
        public DyeingPrintingAreaOutputProductionOrderRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAllIgnoreQueryFilter();

            Assert.NotEmpty(result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInput()
        {
            string testName = GetCurrentMethod() + "UpdateFromInput";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateFromInputAsync(new List<int>() { data.Id }, true);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInputNextAreaFlag()
        {
            string testName = GetCurrentMethod() + "UpdateFromInputNextAreaFlag";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateFromInputNextAreaFlagAsync(data.Id, true);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateHasSalesInvoice()
        {
            string testName = GetCurrentMethod() + "UpdateHasSalesInvoice";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateHasSalesInvoice(data.Id, true);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateHasProductPacking()
        {
            string testName = GetCurrentMethod() + "UpdateHasProductPacking";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateHasPrintingProductPacking(data.Id, true);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInputNextArea()
        {
            string testName = GetCurrentMethod() + "UpdateFromInputNextArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateFromInputAsync(new List<int>() { data.Id }, true, "TERIMA");
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInputNextAreaFlagNextArea()
        {
            string testName = GetCurrentMethod() + "UpdateFromInputNextAreaFlagNextArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateFromInputNextAreaFlagAsync(data.Id, true, "TERIMA");
            Assert.NotEqual(0, result);
        }

    }
}
