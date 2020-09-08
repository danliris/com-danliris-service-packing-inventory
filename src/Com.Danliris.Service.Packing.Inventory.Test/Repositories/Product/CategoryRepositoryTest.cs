using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Product
{
    public  class CategoryRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, CategoryRepository, CategoryModel, CategoryDataUtil>
    {
        private const string ENTITY = "IPCategories";
        public CategoryRepositoryTest() : base(ENTITY)
        {
        }
    }
}
