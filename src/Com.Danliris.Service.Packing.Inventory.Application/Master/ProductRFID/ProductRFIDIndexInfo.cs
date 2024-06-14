using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductRFID
{
    public class ProductRFIDIndexInfo
    {
        public ProductRFIDIndexInfo()
        {
        }
        public DateTime LastModifiedUtc { get; set; }
        public string RFID { get; set; }
        public string ProductPackingCode { get; set; }
        public string UOMUnit { get; set; }
        public double PackingSize { get; set; }
        public int Id { get; set; }
        public string ProductSKUName { get; set; }

    }
}
