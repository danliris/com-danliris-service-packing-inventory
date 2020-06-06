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
    public class DyeingPrintingAreaInputRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingAreaInputRepository, DyeingPrintingAreaInputModel, DyeingPrintingAreaInputDataUtil>
    {
        private const string ENTITY = "DyeingPrintingAreaInput";
        public DyeingPrintingAreaInputRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
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
            var repo = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                var spp = data.DyeingPrintingAreaInputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaInputId = data.Id;
                item.Id = spp.Id;
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteIMArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteIMArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            foreach(var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();
            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea_2()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea_2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            foreach (var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                var spp = dbModel.DyeingPrintingAreaInputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaInputId = data.Id;
                item.Id = spp.Id;
            }

            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }
    }
}
