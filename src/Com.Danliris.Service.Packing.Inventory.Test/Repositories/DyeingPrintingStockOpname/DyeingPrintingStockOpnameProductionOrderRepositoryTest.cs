using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.DyeingPrintingStockOpname.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.DyeingPrintingStockOpname
{
 public   class DyeingPrintingStockOpnameProductionOrderRepositoryTest
   : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingStockOpnameProductionOrderRepository, DyeingPrintingStockOpnameProductionOrderModel, DyeingPrintingStockOpnameProductionOrderDataUtil>
    {
        private const string ENTITY = "DyeingPrintingStockOpnameProductionOrder";
        public DyeingPrintingStockOpnameProductionOrderRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingStockOpnameProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingStockOpnameProductionOrderRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAllIgnoreQueryFilter();

            Assert.NotEmpty(result);
        }

      
    }
}
