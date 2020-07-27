using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory
{
   public class ProductPackingInventoryMovementDataUtil : BaseDataUtil<ProductPackingInventoryMovementRepository, ProductPackingInventoryMovementModel>
    {
        public ProductPackingInventoryMovementDataUtil(ProductPackingInventoryMovementRepository repository) : base(repository)
        {
        }

        public override ProductPackingInventoryMovementModel GetModel()
        {
            return new ProductPackingInventoryMovementModel(1,1,1,1, "remark");

        }

      
    }
}
