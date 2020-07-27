using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Inventory
{
  public  class ProductPackingInventorySummaryRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ProductPackingInventorySummaryRepository, ProductPackingInventorySummaryModel, ProductPackingInventorySummaryDataUtil>
    {
        
        private const string ENTITY = "ProductPackingInventorySummaries";
        public ProductPackingInventorySummaryRepositoryTest() : base(ENTITY)
        {
        }
    }
}
