using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public interface IInventorySKUMovementService
    {
        void AddMovement(ProductSKUInventoryMovementModel model);
    }
}
