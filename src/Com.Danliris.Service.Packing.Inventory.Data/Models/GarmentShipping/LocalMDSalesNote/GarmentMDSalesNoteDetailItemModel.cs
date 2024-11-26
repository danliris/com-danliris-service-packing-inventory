using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalMDSalesNote
{
    public class GarmentMDSalesNoteDetailItemModel : StandardEntity
    {
        public int LocalSalesNoteDetailId { get; set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public int ComodityId { get; private set; }
        public string ComodityCode { get; private set; }
        public string ComodityName { get; private set; }
        public string RONo { get; private set; }

        public GarmentMDSalesNoteDetailItemModel()
        {
        }

        public GarmentMDSalesNoteDetailItemModel( double quantity, int uomId, string uomUnit,int comodityId,string comodityCode,string comodityName,string roNo)
        {
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            ComodityId = comodityId;
            ComodityCode = comodityCode;
            ComodityName = comodityName;
            RONo = roNo;
        }
    }
}
