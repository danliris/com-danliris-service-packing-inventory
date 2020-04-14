using Com.Danliris.Service.Packing.Inventory.Data.Models;
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
    public class DyeingPrintingAreaMovementHistoryRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingAreaMovementHistoryRepository, DyeingPrintingAreaMovementHistoryModel, DyeingPrintingAreaMovementHistoryDataUtil>
    {
        private const string ENTITY = "DyeingPrintintAreaMovementHistory";
        public DyeingPrintingAreaMovementHistoryRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async Task Should_Success_UpdateFromParent()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            DyeingPrintingAreaMovementHistoryRepository repo = new DyeingPrintingAreaMovementHistoryRepository(dbContext, serviceProvider);
            DyeingPrintingAreaMovementHistoryRepository repo2 = new DyeingPrintingAreaMovementHistoryRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();
            var result = await repo2.UpdateAsyncFromParent(model.DyeingPrintingAreaMovementId, AreaEnum.IM, model.Date, model.Shift);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_ReadByDyeingPrintingAreaMovementId()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            DyeingPrintingAreaMovementHistoryRepository repo = new DyeingPrintingAreaMovementHistoryRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadByDyeingPrintingAreaMovement(data.DyeingPrintingAreaMovementId);

            Assert.NotEmpty(result);
        }
    }
}
