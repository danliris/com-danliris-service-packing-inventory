using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping
{
    public class DPShippingInputModel : StandardEntity
    {
        public DateTimeOffset Date { get; set; }
        public string ShippingType { get; set; }
        public string Shift { get; set; }
        public string BonNo { get; set; }
        public ICollection<DPShippingInputItemModel> DPShippingInputItems { get; set; }

        public DPShippingInputModel()
        {
            DPShippingInputItems = new HashSet<DPShippingInputItemModel>();
        }
    }
}
