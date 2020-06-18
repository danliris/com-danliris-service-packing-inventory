using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Inventory
{
  public  class ProductSKUInventorySummaryRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ProductSKUInventorySummaryRepository, ProductSKUInventorySummaryModel, ProductSKUInventorySummaryDataUtil>
    {
        
        private const string ENTITY = "ProductSKUInventorySummaries";
        public ProductSKUInventorySummaryRepositoryTest() : base(ENTITY)
        {
        }
    }
}
