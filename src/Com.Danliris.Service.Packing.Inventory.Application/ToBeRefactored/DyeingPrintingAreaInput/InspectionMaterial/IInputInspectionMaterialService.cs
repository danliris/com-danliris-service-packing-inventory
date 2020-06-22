using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial
{
    public interface IInputInspectionMaterialService
    {
        Task<int> Update(int id, InputInspectionMaterialViewModel viewModel);
        Task<int> Create(InputInspectionMaterialViewModel viewModel);
        Task<InputInspectionMaterialViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<InputInspectionMaterialProductionOrderViewModel> ReadProductionOrders(int page, int size, string filter, string order, string keyword);
        Task<int> Delete(int id);
        MemoryStream GenerateExcel();
    }
}
