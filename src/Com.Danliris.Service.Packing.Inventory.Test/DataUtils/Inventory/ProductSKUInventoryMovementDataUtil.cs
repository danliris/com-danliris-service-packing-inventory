using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory
{
    public class ProductSKUInventoryMovementDataUtil : BaseDataUtil<ProductSKUInventoryMovementRepository, ProductSKUInventoryMovementModel>
    {
        public ProductSKUInventoryMovementDataUtil(ProductSKUInventoryMovementRepository repository) : base(repository)
        {
        }

        public override ProductSKUInventoryMovementModel GetModel()
        {
            return new ProductSKUInventoryMovementModel(1, 1, 1, 1, "storageCode", "storageName", 1, "in", "remark");
        }

    }
}
