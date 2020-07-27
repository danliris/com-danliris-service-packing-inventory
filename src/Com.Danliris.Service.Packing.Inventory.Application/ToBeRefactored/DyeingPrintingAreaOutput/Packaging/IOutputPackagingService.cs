using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public interface IOutputPackagingService
    {
        Task<int> Create(OutputPackagingViewModel viewModel);
        Task<int> CreateV2(OutputPackagingViewModel viewModel);
        Task<OutputPackagingViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<MemoryStream> GenerateExcel(int id);
        ListResult<IndexViewModel> ReadBonOutFromPack(int page, int size, string filter, string order, string keyword);
        ListResult<InputPackagingProductionOrdersViewModel> ReadSppInFromPack(int page, int size, string filter, string order, string keyword);
        ListResult<InputPackagingProductionOrdersViewModel> ReadSppInFromPackSumBySPPNo(int page, int size, string filter, string order, string keyword);

        ListResult<OutputPackagingProductionOrderGroupedViewModel> ReadSppInFromPackGroup(int page, int size, string filter, string order, string keyword);
        ListResult<PlainAdjPackagingProductionOrder> GetDistinctProductionOrder(int page, int size, string filter, string order, string keyword);
        Task<int> CreateAdj(OutputPackagingViewModel viewModel);

        MemoryStream GenerateExcelAll(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
        Task<int> Delete(int bonId);
        Task<int> DeleteV2(int bonId);

    }
}
