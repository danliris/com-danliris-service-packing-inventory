using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public interface IStockOpnameSummaryService
    {
        List<UpdateTrackViewModel> GetDataUpdateTrack(int productionOrderId, string barcode, int trackId);

        Task<StockOpnameWarehouseSummaryViewModel> ReadById(int id);
        Task<int> Update(int id, StockOpnameTrackViewModel viewModel);
        MemoryStream GenerateExcelMonitoring(int productionOrderId, string barcode, int trackId);
    }
}
