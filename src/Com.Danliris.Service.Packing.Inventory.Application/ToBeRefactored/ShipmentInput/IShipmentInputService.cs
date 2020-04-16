using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.ShipmentInput
{
    public interface IShipmentInputService
    {
        Task<int> Create(ShipmentInputViewModel viewModel);
        Task<int> Update(int id, ShipmentInputViewModel viewModel);
        Task<int> Delete(int id);
        Task<ShipmentInputViewModel> ReadById(int id);
        ListResult<ShipmentIndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<PreShipmentIndexViewModel> LoaderPreShipmentData(int page, int size, string filter, string order, string keyword);
    }
}
