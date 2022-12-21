using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceItemModel : StandardEntity
	{
        public int GarmentShippingInvoiceId { get; set; }
        public string RONo { get; set; }
		public string SCNo { get; set; }
		public int BuyerBrandId { get; set; }
		public string BuyerBrandName { get; set; }
		public double Quantity { get; set; }
		public int ComodityId { get; set; }
		public string ComodityCode { get; set; }
		public string ComodityName { get; set; }
		public string ComodityDesc { get; set; }
		public string MarketingName { get; set; }
		public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public string Desc4 { get; set; }
        public int UomId { get; set; }
		public string UomUnit { get; set; }
		public decimal Price { get; set; }
		public decimal PriceRO { get; set; }
		public decimal Amount { get; set; }
		public string CurrencyCode { get; set; }
		public int UnitId { get; set; }
		public string UnitCode { get; set; }
		public decimal CMTPrice { get; set; }
        public int PackingListItemId { get; set; }

        public GarmentShippingInvoiceItemModel(string rONo, string sCNo, int buyerBrandId, string buyerBrandName, double quantity, int comodityId, string comodityCode, string comodityName, string comodityDesc, string marketingName, string desc2, string desc3, string desc4, int uomId, string uomUnit, decimal price, decimal priceRO, decimal amount, string currencyCode, int unitId, string unitCode, decimal cMTPrice, int packingListItemId)
        {
            this.Id = Id;
            RONo = rONo;
            SCNo = sCNo;
            BuyerBrandId = buyerBrandId;
            BuyerBrandName = buyerBrandName;
            Quantity = quantity;
            ComodityId = comodityId;
            ComodityCode = comodityCode;
            ComodityName = comodityName;
            ComodityDesc = comodityDesc;
			MarketingName = marketingName;
            Desc2 = desc2;
            Desc3 = desc3;
            Desc4 = desc4;
            UomId = uomId;
            UomUnit = uomUnit;
            Price = price;
            PriceRO = priceRO;
            Amount = amount;
            CurrencyCode = currencyCode;
            UnitId = unitId;
            UnitCode = unitCode;
            CMTPrice = cMTPrice;
			PackingListItemId = packingListItemId;
        }

        public void SetQuantity(double quantity, string username, string uSER_AGENT)
		{
			if (this.Quantity != quantity)
			{
				this.Quantity = quantity;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetPrice(decimal price, string username, string uSER_AGENT)
		{
			if (this.Price != price)
			{
				this.Price = price;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetAmount(decimal amount, string username, string uSER_AGENT)
		{
			if (this.Amount != amount)
			{
				this.Amount = amount;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetCMTPrice(decimal cMTPrice, string username, string uSER_AGENT)
		{
			if (this.CMTPrice != cMTPrice)
			{
				this.CMTPrice = cMTPrice;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetComodityDesc(string comodityDesc, string username, string uSER_AGENT)
		{
			if (this.ComodityDesc != comodityDesc)
			{
				this.ComodityDesc = comodityDesc;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetMarketingName(string marketingName, string username, string uSER_AGENT)
		{
			if (this.MarketingName != marketingName)
			{
				this.MarketingName = marketingName;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetDesc2(string desc2, string username, string uSER_AGENT)
        {
            if (this.Desc2 != desc2)
            {
                this.Desc2 = desc2;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetDesc3(string desc3, string username, string uSER_AGENT)
        {
            if (this.Desc3 != desc3)
            {
                this.Desc3 = desc3;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetDesc4(string desc4, string username, string uSER_AGENT)
        {
            if (this.Desc4 != desc4)
            {
                this.Desc4 = desc4;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetUomId(int uomId, string username, string uSER_AGENT)
		{
			if (this.UomId != uomId)
			{
				this.UomId = uomId;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetUomUnit(string uomUnit, string username, string uSER_AGENT)
		{
			if (this.UomUnit != uomUnit)
			{
				this.UomUnit = uomUnit;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}
	}
}
