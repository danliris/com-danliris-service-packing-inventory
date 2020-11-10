using System.IO;

namespace Com.Danliris.Service.Packing.Inventory.Application.Utilities
{
    public class MemoryStreamResult
    {
        public MemoryStreamResult(MemoryStream data, string fileName)
        {
            Data = data;
            FileName = fileName;
        }

        public MemoryStream Data { get; set; }
        public string FileName { get; set; }
    }
}