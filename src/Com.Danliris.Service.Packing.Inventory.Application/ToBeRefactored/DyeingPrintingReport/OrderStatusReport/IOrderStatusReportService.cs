using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport
{
    public interface IOrderStatusReportService
    {
        Task<List<OrderStatusReportViewModel>> GetReportData(DateTime startdate, DateTime finishdate, int orderTypeId);
        Task<MemoryStream> GenerateExcel(DateTime startdate, DateTime finishdate, int orderTypeId, string orderTypeName);
    }
}
