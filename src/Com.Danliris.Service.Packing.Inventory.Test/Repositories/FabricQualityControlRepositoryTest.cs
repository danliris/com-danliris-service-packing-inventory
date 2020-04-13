using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class FabricQualityControlRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, FabricQualityControlRepository, FabricQualityControlModel, FabricQualityControlDataUtil>
    {
        private const string ENTITY = "FabricQualityControl";


        public FabricQualityControlRepositoryTest() : base(ENTITY)
        {

        }

        protected override FabricQualityControlDataUtil DataUtil(FabricQualityControlRepository repository, PackingInventoryDbContext dbContext = null)
        {
            var serviceProvider = GetServiceProviderMock(dbContext);
            DyeingPrintingAreaMovementRepository dpRepo = new DyeingPrintingAreaMovementRepository(dbContext, serviceProvider.Object);
            DyeingPrintingAreaMovementDataUtil dpDataUtil = new DyeingPrintingAreaMovementDataUtil(dpRepo);
            FabricQualityControlDataUtil dataUtil = new FabricQualityControlDataUtil(repository, dpDataUtil);
            return dataUtil;
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new FabricQualityControlRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new FabricQualityControlRepository(dbContext, GetServiceProviderMock(dbContext).Object);
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
            var repo = new FabricQualityControlRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new FabricQualityControlRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.FabricGradeTests)
            {
                var fgt = data.FabricGradeTests.ElementAtOrDefault(index++);
                item.FabricQualityControlId = data.Id;
                item.Id = fgt.Id;
                int cIndex = 0;
                foreach (var cri in item.Criteria)
                {
                    var crit = fgt.Criteria.ElementAtOrDefault(cIndex);
                    cri.FabricGradeTestId = fgt.Id;
                    cri.Id = crit.Id;
                }
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_Update_3()
        {
            string testName = GetCurrentMethod() + "Update3";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new FabricQualityControlRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new FabricQualityControlRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.FabricGradeTests)
            {
                var fgt = data.FabricGradeTests.ElementAtOrDefault(index++);
                item.FabricQualityControlId = data.Id;
                item.Id = fgt.Id;
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }
    }
}
