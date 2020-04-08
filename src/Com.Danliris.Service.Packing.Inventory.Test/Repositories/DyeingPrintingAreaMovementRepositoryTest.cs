using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Moonlay.Models;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Test.Infrastructure
{
    public class DyeingPrintingAreaMovementRepositoryTest
    {
        private const string ENTITY = "DyeingPrintintAreaMovement";


        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private PackingInventoryDbContext GetDbContext(string testName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PackingInventoryDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            var dbContext = new PackingInventoryDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private DyeingPrintingAreaMovementRepository GetRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            return new DyeingPrintingAreaMovementRepository(dbContext, serviceProvider);
        }

        public DyeingPrintingAreaMovementModel Model
        {
            get
            {
                return new DyeingPrintingAreaMovementModel("area", "no", DateTimeOffset.UtcNow, "shift", 1, "code", "no", 1, "type", "buyer", "inst", "1-2",
                    1, "code", "name", 1, "code", "name", "1", 1, "code", "name", "color", "mtf", "awal", 1, "MTR", 1, "OK", "A", "area");
            }
            set { }
        }

        private Mock<IServiceProvider> GetServiceProvider()
        {
            Mock<IServiceProvider> sp = new Mock<IServiceProvider>();
            sp.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 7, Token = "token", Username = "username" });

            return sp;
        }

        [Fact]
        public async Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = GetDbContext(testName);

            var repo = GetRepository(dbContext, GetServiceProvider().Object);

            var result = await repo.InsertAsync(Model);
            Assert.NotEqual(0, result);
        }

        private async Task<DyeingPrintingAreaMovementModel> CreateHelper(DyeingPrintingAreaMovementRepository repo)
        {
            await repo.InsertAsync(Model);

            var data = repo.ReadAll().FirstOrDefault();
            return data;
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = GetDbContext(testName);

            var repo = GetRepository(dbContext, GetServiceProvider().Object);
            var data = await CreateHelper(repo);
            var result = await repo.DeleteAsync(data.Id);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Exception_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = GetDbContext(testName);

            var repo = GetRepository(dbContext, GetServiceProvider().Object);
            var data = await CreateHelper(repo);

            var fqcModel = new FabricQualityControlModel("code", DateTimeOffset.UtcNow, "area",false,data.Id, data.BonNo, data.ProductionOrderNo, "machine", 
                "op", 1,1,new List<FabricGradeTestModel>());
            fqcModel.FlagForCreate("test", "test");
            var datafqc = dbContext.Add(fqcModel);
            await dbContext.SaveChangesAsync();

            await Assert.ThrowsAnyAsync<Exception>(() => repo.DeleteAsync(data.Id));
        }
    }
}
