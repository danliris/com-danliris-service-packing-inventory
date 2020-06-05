using Com.Moonlay.Models;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListItemModel : StandardEntity
    {
        public string RONo { get; private set; }
        public string SCNo { get; private set; }
        public int BuyerId { get; private set; }
        public string BuyerName { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double Price { get; private set; }
        public double Amount { get; private set; }
        public string Valas { get; private set; }
        public int UnitId { get; private set; }
        public string UnitCode { get; private set; }
        public string OTL { get; private set; }
        public string Article { get; private set; }
        public string OrderNo { get; private set; }
        public string Description { get; private set; }

        public ICollection<GarmentPackingListDetailModel> Details { get; private set; }

        public GarmentPackingListItemModel()
        {
            Details = new HashSet<GarmentPackingListDetailModel>();
        }

        public GarmentPackingListItemModel(string rONo, string sCNo, int buyerId, string buyerName, double quantity, int uomId, string uomUnit, double price, double amount, string valas, int unitId, string unitCode, string oTL, string article, string orderNo, string description, ICollection<GarmentPackingListDetailModel> details)
        {
            RONo = rONo;
            SCNo = sCNo;
            BuyerId = buyerId;
            BuyerName = buyerName;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            Price = price;
            Amount = amount;
            Valas = valas;
            UnitId = unitId;
            UnitCode = unitCode;
            OTL = oTL;
            Article = article;
            OrderNo = orderNo;
            Description = description;
            Details = details;
        }
    }
}
