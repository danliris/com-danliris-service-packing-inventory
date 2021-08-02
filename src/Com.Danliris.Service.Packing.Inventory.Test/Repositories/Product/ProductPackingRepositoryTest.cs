using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Product
{
   public  class ProductPackingRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ProductPackingRepository, ProductPackingModel, ProductPackingDataUtil>
    {
        private const string ENTITY = "ProductPackings";
        public ProductPackingRepositoryTest() : base(ENTITY)
        {
        }
    }
}
