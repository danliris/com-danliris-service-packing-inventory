using iTextSharp.text;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricPackingIdCodeDto
    {
        public FabricPackingIdCodeDto()
        {
            ProductPackingCodes = new List<string>();
        }

        public int ProductPackingId { get; set; }
        public int FabricPackingId { get; set; }
        public string ProductPackingCode { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get; set; }
        public int ProductSKUId { get; set; }
        public List<string> ProductPackingCodes { get; set; }
    }
}