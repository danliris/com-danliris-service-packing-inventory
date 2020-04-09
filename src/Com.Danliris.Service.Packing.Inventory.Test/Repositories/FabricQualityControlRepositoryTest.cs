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
    }
}
