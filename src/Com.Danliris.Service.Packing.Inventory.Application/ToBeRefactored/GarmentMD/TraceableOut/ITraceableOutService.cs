using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.TraceableOut
{
    public interface ITraceableOutService
    {
        Task<List<TraceableOutVM>> getQuery(string bcno, string bcType, string category);
        Task<MemoryStream> GetExcel(string bcno, string bcType, string category);
    }
}
