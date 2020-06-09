﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public interface IOutputPackagingService
    {
        Task<int> Create(OutputPackagingViewModel viewModel);
        Task<OutputPackagingViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<MemoryStream> GenerateExcel(int id);
        ListResult<IndexViewModel> ReadBonOutFromPack(int page, int size, string filter, string order, string keyword);
        ListResult<InputPackagingProductionOrdersViewModel> ReadSppInFromPack(int page, int size, string filter, string order, string keyword);
        Task<int> Delete(int bonId);

    }
}
