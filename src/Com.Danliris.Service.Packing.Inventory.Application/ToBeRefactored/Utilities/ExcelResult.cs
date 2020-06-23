using Com.Danliris.Service.Packing.Inventory.Data;
using System.Collections.Generic;
using System.IO;

namespace Com.Danliris.Service.Packing.Inventory.Application.Utilities
{
    public class ExcelResult
    {
        public ExcelResult(MemoryStream data, string fileName)
        {
            Data = data;
            FileName = fileName;
        }

        public MemoryStream Data { get; set; }
        public string FileName { get; set; }
    }
}