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

        public DPShippingOutputModel(DateTimeOffset date, string bonNo,  bool hasOuputArea,
            string destinationArea,  int deliveryOrderId, string deliveryOrderNo, string destinationBuyerName, string shippingCode, bool updateBySales,
             ICollection<DPShippingOutputItemModel> dPShippingOutputItems)
        {
            Date = date;

            BonNo = bonNo;
            HasOuputArea = hasOuputArea;
            DestinationArea = destinationArea;
            DPShippingOutputItems = dPShippingOutputItems;
            DeliveryOrderSalesId = deliveryOrderId;
            DeliveryOrderSalesNo = deliveryOrderNo;
            DestinationBuyerName = destinationBuyerName;
            ShippingCode = shippingCode;
            

            UpdateBySales = updateBySales;
        }

        public DPShippingOutputModel(DateTimeOffset date, string bonNo, bool hasOuputArea,
            string destinationArea, int deliveryOrderId, string deliveryOrderNo, string destinationBuyerName, string shippingCode, bool updateBySales,
            string packingListNo, string packingType, string packingListRemark,
            string packingListAuthorized, string packingListLCNumber, string packingListIssuedBy, string packingListDescription,
             ICollection<DPShippingOutputItemModel> dPShippingOutputItems)
        {
            Date = date;

            BonNo = bonNo;
            HasOuputArea = hasOuputArea;
            DestinationArea = destinationArea;
            DPShippingOutputItems = dPShippingOutputItems;
            DeliveryOrderSalesId = deliveryOrderId;
            DeliveryOrderSalesNo = deliveryOrderNo;
            DestinationBuyerName = destinationBuyerName;
            ShippingCode = shippingCode;
            PackingListNo = packingListNo;
            PackingType = packingType;
            PackingListRemark = packingListRemark;
            PackingListAuthorized = packingListAuthorized;
            PackingListLCNumber = packingListLCNumber;
            PackingListIssuedBy = packingListIssuedBy;
            PackingListDescription = packingListDescription;


            UpdateBySales = updateBySales;
        }

    }
}
