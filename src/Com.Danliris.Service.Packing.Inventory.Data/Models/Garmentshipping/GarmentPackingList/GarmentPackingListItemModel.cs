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
        public double PriceFOB { get; private set; }
        public double PriceCMT { get; private set; }
        public double Amount { get; private set; }
        public string Valas { get; private set; }

        public int UnitId { get; private set; }
        public string UnitCode { get; private set; }

        public string Article { get; private set; }
        public string OrderNo { get; private set; }
        public string Description { get; private set; }

        public string DescriptionMd { get; private set; }

        public string Remarks { get; private set; }

        public string RoType { get; private set; }

        public ICollection<GarmentPackingListDetailModel> Details { get; private set; }

        public GarmentPackingListItemModel()
        {
            Details = new HashSet<GarmentPackingListDetailModel>();
        }

        public GarmentPackingListItemModel(string rONo, string sCNo, int buyerBrandId, string buyerBrandName, int comodityId, string comodityCode, string comodityName, string comodityDescription, double quantity, int uomId, string uomUnit, double priceRO, double price, double priceFob, double priceCmt, double amount, string valas, int unitId, string unitCode, string article, string orderNo, string description, string descriptionMd, string remarks, string roType, ICollection<GarmentPackingListDetailModel> details)
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
            PriceFOB = priceFob;
            PriceCMT = priceCmt;
            Amount = amount;
            Valas = valas;
            UnitId = unitId;
            UnitCode = unitCode;
            Article = article;
            OrderNo = orderNo;
            Description = description;
            DescriptionMd = descriptionMd;
            Remarks = remarks;
            Details = details;
            RoType = roType;
        }

        public void SetRONo(string rONo, string userName, string userAgent)
        {
            if (RONo != rONo)
            {
                RONo = rONo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSCNo(string sCNo, string userName, string userAgent)
        {
            if (SCNo != sCNo)
            {
                SCNo = sCNo; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerBrandId(int buyerBrandId, string userName, string userAgent)
        {
            if (BuyerBrandId != buyerBrandId)
            {
                BuyerBrandId = buyerBrandId; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerBrandName(string buyerBrandName, string userName, string userAgent)
        {
            if (BuyerBrandName != buyerBrandName)
            {
                BuyerBrandName = buyerBrandName; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityId(int comodityId, string userName, string userAgent)
        {
            if (ComodityId != comodityId)
            {
                ComodityId = comodityId; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityCode(string comodityCode, string userName, string userAgent)
        {
            if (ComodityCode != comodityCode)
            {
                ComodityCode = comodityCode; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityName(string comodityName, string userName, string userAgent)
        {
            if (ComodityName != comodityName)
            {
                ComodityName = comodityName; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityDescription(string comodityDescription, string userName, string userAgent)
        {
            if (ComodityDescription != comodityDescription)
            {
                ComodityDescription = comodityDescription; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetQuantity(double quantity, string userName, string userAgent)
        {
            if (Quantity != quantity)
            {
                Quantity = quantity; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomId(int uomId, string userName, string userAgent)
        {
            if (UomId != uomId)
            {
                UomId = uomId; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomUnit(string uomUnit, string userName, string userAgent)
        {
            if (UomUnit != uomUnit)
            {
                UomUnit = uomUnit; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPriceRO(double priceRO, string userName, string userAgent)
        {
            if (PriceRO != priceRO)
            {
                PriceRO = priceRO; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPrice(double price, string userName, string userAgent)
        {
            if (Price != price)
            {
                Price = price; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPriceFob(double price, string userName, string userAgent)
        {
            if (PriceFOB != price)
            {
                PriceFOB = price; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPriceCmt(double price, string userName, string userAgent)
        {
            if (PriceCMT != price)
            {
                PriceCMT = price; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAmount(double amount, string userName, string userAgent)
        {
            if (Amount != amount)
            {
                Amount = amount; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetValas(string valas, string userName, string userAgent)
        {
            if (Valas != valas)
            {
                Valas = valas; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUnitId(int unitId, string userName, string userAgent)
        {
            if (UnitId != unitId)
            {
                UnitId = unitId; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUnitCode(string unitCode, string userName, string userAgent)
        {
            if (UnitCode != unitCode)
            {
                UnitCode = unitCode; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetArticle(string article, string userName, string userAgent)
        {
            if (Article != article)
            {
                Article = article; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetOrderNo(string orderNo, string userName, string userAgent)
        {
            if (OrderNo != orderNo)
            {
                OrderNo = orderNo; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDescriptionMd(string descriptionMd, string userName, string userAgent)
        {
            if (DescriptionMd != descriptionMd)
            {
                DescriptionMd = descriptionMd; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRemarks(string remarks, string userName, string userAgent)
        {
            if (Remarks != remarks)
            {
                Remarks = remarks; this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRoType(string roType, string userName, string userAgent)
        {
            if (RoType != roType)
            {
                RoType = roType;
                this.FlagForUpdate(userName, userAgent);
            }
        }


    }
}