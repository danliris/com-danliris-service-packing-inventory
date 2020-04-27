using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.PackagingStock;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class PackagingStockRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, PackagingStockRepository, PackagingStockModel, PackagingStockDataUtil>
    {
        private const string Entity = "PackagingStock";

        public PackagingStockRepositoryTest() : base(Entity)
        {
        }

        [Fact]
        public async void Should_Success_SetVoid()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new PackagingStockRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            data.SetPackagingQty(10, "unitTest", "xUnit");
            data.SetPackagingType("WHITE", "unitTest", "xUnit");
            data.SetPackagingUnit("ROLLS", "unitTest", "xUnit");
            data.SetLength(10, "unitTest", "xUnit");
            data.SetUomUnit("METER", "unitTest", "xUnit");
            data.SetHasNextArea(false, "unitTest", "xUnit");

            Assert.Equal(10, data.PackagingQty);
            Assert.Equal("WHITE", data.PackagingType);
            Assert.Equal("ROLLS", data.PackagingUnit);
            Assert.Equal(10, data.Length);
            Assert.Equal("METER", data.UomUnit);
            Assert.False(data.HasNextArea);
        }
    }
}
