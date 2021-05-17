using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.DyeingPrintingStockOpname.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.DyeingPrintingStockOpname
{
   public class DyeingPrintingStockOpnameRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingStockOpnameRepository, DyeingPrintingStockOpnameModel, StockOpnameWarehouseDataUtil>
    {
        private const string ENTITY = "DyeingPrintingStockOpname";
        public DyeingPrintingStockOpnameRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            //Setup
            string testName = GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingStockOpnameRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            //Act
            var result = repo.GetDbSet();

            //Assert
            Assert.NotEmpty(result);
        }


        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            //Setup
            string testName = GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingStockOpnameRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            //Act
            var result = repo.ReadAllIgnoreQueryFilter();

            //Assert
            Assert.NotEmpty(result);
        }



        [Fact]
        public virtual async Task Should_Success_UpdateAsync()
        {
            //Setup
            string testName = GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var repo = new DyeingPrintingStockOpnameRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new DyeingPrintingStockOpnameRepository(dbContext, GetServiceProviderMock(dbContext).Object);

            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);

            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingStockOpnameProductionOrders)
            {
                var spp = data.DyeingPrintingStockOpnameProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingStockOpnameId = data.Id;
                item.Id = spp.Id;
            }

            //Act
            var result = await repo2.UpdateAsync(data.Id, model);

            //Assert
            Assert.NotEqual(0, result);
        }


       
        [Fact]
        public virtual async Task Should_Success_InsertAsync()
        {
            //Setup
            string testName = GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingStockOpnameRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data =  DataUtil(repo, dbContext).GetModel();

            //Act
            var result = await repo.InsertAsync(data);

            //Assert
            Assert.NotEqual(0, result);
        }


     

        [Fact]
        public virtual async Task Should_Success_DeleteAsync()
        {
            //Setup
            string testName = GetCurrentAsyncMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingStockOpnameRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            //Act
            var result = await repo.DeleteAsync(data.Id);

            //Assert
            Assert.NotEqual(0, result);
        }
    }
}
