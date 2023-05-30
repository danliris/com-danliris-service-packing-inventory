using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse
{
    public class DPWarehouseInputModel : StandardEntity
    {
        public DateTimeOffset Date { get; private set; }
        public string Area { get; private set; }
        public string Shift { get; private set; }
        public string BonNo { get; private set; }
        public string Group { get; private set; }
        public string ShippingType { get; private set; }
        //public ICollection<DPWarehouseInputItemModel> DPWarehouseInputItems { get; private set; }
    }
}
