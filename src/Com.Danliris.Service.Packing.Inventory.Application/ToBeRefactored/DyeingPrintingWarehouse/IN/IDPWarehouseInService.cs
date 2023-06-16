using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.List;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.PreOutputWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN
{
    public interface IDPWarehouseInService
    {
        List<OutputPreWarehouseItemListViewModel> PreInputWarehouse(string packingCode);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Create(DPInputWarehouseCreateViewModel viewModel);

    }
}
