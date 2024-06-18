using System;
using System.Collections.Generic;
using System.Text;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class ProductRFIDModel : StandardEntity
    {
        public int ProductPackingId { get; set; }
        public string ProductPackingCode { get; set; }
        public string RFID { get; set; }
        public string CurrentArea { get; set; }
    }
}
