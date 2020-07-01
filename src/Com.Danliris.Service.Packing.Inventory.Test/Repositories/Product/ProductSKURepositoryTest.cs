using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Product
{
    public class ProductSKURepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ProductSKURepository, ProductSKUModel, ProductSKUDataUtil>
    {
        private const string ENTITY = "ProductSKUs";
        public ProductSKURepositoryTest() : base(ENTITY)
        {
        }
    }
}
