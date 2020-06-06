using Com.Moonlay.Models;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListItemModel : StandardEntity
    {
        public int PackingListId { get; private set; }

        public string RONo { get; private set; }
        public string SCNo { get; private set; }

        public int BuyerBrandId { get; private set; }
        public string BuyerBrandName { get; private set; }

        public int ComodityId { get; private set; }
        public string ComodityCode { get; private set; }
        public string ComodityName { get; private set; }
        public string ComodityDescription { get; private set; }

        public double Quantity { get; private set; }

        public int UomId { get; private set; }
        public string UomUnit { get; private set; }

        public double PriceRO { get; private set; }
        public double Price { get; private set; }
        public double Amount { get; private set; }
        public string Valas { get; private set; }

        public int UnitId { get; private set; }
        public string UnitCode { get; private set; }

        public string Article { get; private set; }
        public string OrderNo { get; private set; }
        public string Description { get; private set; }

        public ICollection<GarmentPackingListDetailModel> Details { get; private set; }

        public double AVG_GW { get; private set; }
        public double AVG_NW { get; private set; }

        public GarmentPackingListItemModel()
        {
            Details = new HashSet<GarmentPackingListDetailModel>();
        }

        public GarmentPackingListItemModel(string rONo, string sCNo, int buyerBrandId, string buyerBrandName, int comodityId, string comodityCode, string comodityName, string comodityDescription, double quantity, int uomId, string uomUnit, double priceRO, double price, double amount, string valas, int unitId, string unitCode, string article, string orderNo, string description, ICollection<GarmentPackingListDetailModel> details, double aVG_GW, double aVG_NW)
        {
            RONo = rONo;
            SCNo = sCNo;
            BuyerBrandId = buyerBrandId;
            BuyerBrandName = buyerBrandName;
            ComodityId = comodityId;
            ComodityCode = comodityCode;
            ComodityName = comodityName;
            ComodityDescription = comodityDescription;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            PriceRO = priceRO;
            Price = price;
            Amount = amount;
            Valas = valas;
            UnitId = unitId;
            UnitCode = unitCode;
            Article = article;
            OrderNo = orderNo;
            Description = description;
            Details = details;
            AVG_GW = aVG_GW;
            AVG_NW = aVG_NW;
        }
    }
}
