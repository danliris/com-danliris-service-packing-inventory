using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class ProductPackingRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ProductPackingRepository, ProductPackingModel, ProductPackingDataUtil>
    {
        private const string Entity = "ProductPacking";
        public ProductPackingRepositoryTest() : base(Entity)
        {
        }
    }
}
