using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.ShipmentLocalSalesNote
{
    public interface IShipmentLocalSalesNoteService
    {
        Task<List<ShipmentLocalSalesNoteVM>> GetReportQuery(DateTime dateFrom, DateTime DateTo);
        Task<MemoryStream> GetExcel(DateTime dateFrom, DateTime DateTo);
    }
}
