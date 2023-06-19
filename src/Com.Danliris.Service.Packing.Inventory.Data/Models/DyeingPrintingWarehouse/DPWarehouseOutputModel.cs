using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse
{
    public class DPWarehouseOutputModel : StandardEntity
    {
        public DateTimeOffset Date { get; private set; }
        public string Area { get; private set; }
        public string Shift { get; private set; }
        public string BonNo { get; private set; }
        public string DestinationArea { get; private set; }
        public string Group { get; private set; }
        public long DeliveryOrderSalesId { get; private set; }
        public string DeliveryOrderSalesNo { get; private set; }
        public string Type { get; private set; }
        public string ShippingCode { get; private set; }
        public string PackingListNo { get; private set; }
        public string PackingType { get; private set; }
        public string PackingListRemark { get; private set; }
        public string PackingListAuthorized { get; private set; }
        public string PackingListLCNumber { get; private set; }
        public string PackingListIssuedBy { get; private set; }
        public string PackingListDescription { get; private set; }
        public bool UpdateBySales { get; private set; }
        public ICollection<DPWarehouseOutputItemModel> DPWarehouseOutputItems { get; set; }
        public DPWarehouseOutputModel()
        {
            DPWarehouseOutputItems = new HashSet<DPWarehouseOutputItemModel>();
        }


    }
}
