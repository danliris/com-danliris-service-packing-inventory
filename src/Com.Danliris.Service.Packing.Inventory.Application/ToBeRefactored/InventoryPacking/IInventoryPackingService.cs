using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryPacking
{
    public interface IInventoryPackingService
    {
        IQueryable<InventoryMovementViewModel> GetMovements(string keyword, string order, int page, int size);
    }
}
