using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport
{
    public interface IAvalStockReportService
    {
        Task<ListResult<AvalStockReportViewModel>> GetReportDataAsync(DateTimeOffset searchDate);
        Task<MemoryStream> GenerateExcelAsync(DateTimeOffset searchDate);
    }
}
