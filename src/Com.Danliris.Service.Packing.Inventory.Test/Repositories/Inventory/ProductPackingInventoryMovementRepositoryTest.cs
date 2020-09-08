using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Inventory
{
    public class ProductPackingInventoryMovementRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, ProductPackingInventoryMovementRepository, ProductPackingInventoryMovementModel, ProductPackingInventoryMovementDataUtil>
    {
       
        private const string ENTITY = "ProductPackingInventoryMovements";
        public ProductPackingInventoryMovementRepositoryTest() : base(ENTITY)
        {
        }
    }
}
