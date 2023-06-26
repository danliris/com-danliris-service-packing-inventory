using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.UpdateTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.UpdateTrack
{
    public interface IDPWarehouseSummaryService
    {
        List<DPUpdateTrackViewModel> GetDataUpdateTrack(int productionOrderId, string barcode, int trackId);
        Task<DPWarehouseSummaryViewModel> ReadById(int id);
        Task<int> UpdateTrack(int id, DPTrackViewModel viewModel);
    }
}
