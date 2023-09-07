using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping
{
    public class DPShippingOutputModel : StandardEntity
    {
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public string BonNo { get; set; }
        public bool HasOuputArea { get; set; }
        public int DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public string DestinationBuyerName { get; set; }
        public string ShippingCode { get; set; }
        public string PackingListAuthorized { get; set; }
        public string PackingListNo { get; set; }
        public string PackingListRemark { get; set; }
        public string PackingType { get; set; }
        public string PackingListDescription { get; set; }
        public string PackingListIssuedBy { get; set; }
        public string PackingListLCNumber { get; set; }
        public bool UpdateBySales { get; set; }
        public ICollection<DPShippingOutputItemModel> DPShippingOutputItems { get; set; }
        public DPShippingOutputModel()
        {
            DPShippingOutputItems = new HashSet<DPShippingOutputItemModel>();
        }

    }
}
