using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class DyeingPrintingAreaOutputRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingAreaOutputRepository, DyeingPrintingAreaOutputModel, DyeingPrintingAreaOutputDataUtil>
    {
        private const string ENTITY = "DyeingPrintingAreaInput";
        public DyeingPrintingAreaOutputRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAllIgnoreQueryFilter();

            Assert.NotEmpty(result);
        }

        [Fact]
        public virtual async Task Should_Success_Update_2()
        {
            string testName = GetCurrentMethod() + "Update2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaOutputProductionOrders)
            {
                var spp = data.DyeingPrintingAreaOutputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaOutputId = data.Id;
                item.Id = spp.Id;
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromInput()
        {
            string testName = GetCurrentMethod() + "UpdateFromInput";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaOutputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();

            var result = await repo.UpdateFromInputAsync(data.Id, true);
            Assert.NotEqual(0, result);
        }
    }
}
