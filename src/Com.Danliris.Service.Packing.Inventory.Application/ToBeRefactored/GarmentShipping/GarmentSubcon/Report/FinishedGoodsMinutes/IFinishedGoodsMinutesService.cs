using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.FinishedGoodsMinutes
{
    public interface IFinishedGoodsMinutesService
    {
        Task<List<FinishedGoodsMinutesVM>> GetReportQuery(string invoiceNo);
        Task<MemoryStream> GetExcel(string invoiceNo);
    }
}
