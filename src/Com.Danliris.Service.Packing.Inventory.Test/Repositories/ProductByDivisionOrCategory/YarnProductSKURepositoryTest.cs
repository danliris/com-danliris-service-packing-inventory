using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.ProductByDivisionOrCategory
{
   public class YarnProductSKURepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, YarnProductSKURepository, YarnProductSKUModel, YarnProductSKUDataUtil>
    {
        private const string ENTITY = "YarnProductPackings";
        public YarnProductSKURepositoryTest() : base(ENTITY)
        {
        }
    }
}
