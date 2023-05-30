using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN
{
    public interface IDPWarehouseInService
    {
        List<OutputPreWarehouseItemListViewModel> PreInputWarehouse(string packingCode);

    }
}
