using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.PackagingStock;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class PackagingStockRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, PackagingStockRepository, PackagingStockModel, PackagingStockDataUtil>
    {
        private const string Entity = "PackagingStock";
        public PackagingStockRepositoryTest() : base(Entity)
        {
        }
    }
}
