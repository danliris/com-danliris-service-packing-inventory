using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse.InputSPPWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public interface IOutputWarehouseService
    {
        Task<int> Create(OutputWarehouseViewModel viewModel);
        Task<OutputWarehouseViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<IndexViewModel> Read(string keyword);

        Task<MemoryStream> GenerateExcel(int id);
        List<InputWarehouseProductionOrderCreateViewModel> GetInputWarehouseProductionOrders();
        List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList();
        List<InputSppWarehouseViewModel> GetInputSppWarehouseItemList(int bonId);
        List<InputSppWarehouseViewModel> GetOutputSppWarehouseItemList(int bonId);

    }
}
