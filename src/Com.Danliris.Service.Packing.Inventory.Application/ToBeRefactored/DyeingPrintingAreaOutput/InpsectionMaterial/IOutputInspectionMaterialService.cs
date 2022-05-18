using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public interface IOutputInspectionMaterialService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, OutputInspectionMaterialViewModel viewModel);
        Task<int> Create(OutputInspectionMaterialViewModel viewModel);
        Task<OutputInspectionMaterialViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        MemoryStream GenerateExcel(OutputInspectionMaterialViewModel viewModel);
        
        
        
        
        
        MemoryStream GenerateExcel(OutputInspectionMaterialViewModel viewModel,int timeZoneOffset);
        List<InputInspectionMaterialProductionOrderViewModel> GetInputInspectionMaterialProductionOrders(long productionOrderId);
        ListResult<InputInspectionMaterialProductionOrderViewModel> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword);
        MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
        ListResult<AdjInspectionMaterialProductionOrderViewModel> GetDistinctAllProductionOrder(int page, int size, string filter, string order, string keyword);
    }
}
