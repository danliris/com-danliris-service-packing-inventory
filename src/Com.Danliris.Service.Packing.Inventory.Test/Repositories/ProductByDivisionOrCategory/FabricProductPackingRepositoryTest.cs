using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.ProductByDivisionOrCategory
{
     public  class FabricProductPackingRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, FabricProductPackingRepository, FabricProductPackingModel, FabricProductPackingDataUtil>
    {
        private const string ENTITY = "FabricProductPackings";
        public FabricProductPackingRepositoryTest() : base(ENTITY)
        {
        }
    }
}
