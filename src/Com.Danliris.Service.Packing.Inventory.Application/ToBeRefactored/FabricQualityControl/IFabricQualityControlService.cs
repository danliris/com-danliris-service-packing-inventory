using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl
{
    public interface IFabricQualityControlService
    {
        Task<int> Create(FabricQualityControlViewModel viewModel);
        Task<int> Update(int id, FabricQualityControlViewModel viewModel);
        Task<int> Delete(int id);
        Task<FabricQualityControlViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);

        ListResult<FabricQualityControlViewModel> GetReport(int page, int size, string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet);

        MemoryStream GenerateExcel(string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet);

        List<FabricQCGradeTestsViewModel> GetForSPP(string no);
    }
}
