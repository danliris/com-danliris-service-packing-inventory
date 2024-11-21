using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalMDSalesNote
{
    public class GarmentMDLocalSalesNoteDetailModel : StandardEntity
    {
        public int LocalSalesNoteItemId { get; set; }
        public string BonNo { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public string BonFrom { get; private set; }

        public GarmentMDLocalSalesNoteDetailModel()
        {
        }

        public GarmentMDLocalSalesNoteDetailModel(string bonNo, double quantity, int uomId, string uomUnit, string bonFrom)
        {
            BonNo = bonNo;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            BonFrom = bonFrom;
        }
    }
}
