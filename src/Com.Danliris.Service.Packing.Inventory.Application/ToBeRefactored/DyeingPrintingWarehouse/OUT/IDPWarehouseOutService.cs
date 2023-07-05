using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT
{
    public interface IDPWarehouseOutService
    {
        List<OutputWarehouseItemListViewModel> ListOutputWarehouse(string packingCode, int trackId);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Create(DPWarehouseOutputCreateViewModel viewModel);
    }
}
