using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.DyeingPrintingAreaMovement
{
    public interface IDyeingPrintingAreaMovementService
    {
        Task<int> Create(DyeingPrintingAreaMovementViewModel viewModel);
        Task<int> Update(int id, DyeingPrintingAreaMovementViewModel viewModel);
        Task<int> Delete(int id);
        Task<DyeingPrintingAreaMovementViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
    }
}
