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
		public int UomId { get; set; }
		public string UomUnit { get; set; }
		public decimal Price { get; set; }
		public decimal PriceRO { get; set; }
		public decimal Amount { get; set; }
		public string CurrencyCode { get; set; }
		public int UnitId { get; set; }
		public string UnitCode { get; set; }
		public decimal CMTPrice { get; set; }
	
		public GarmentShippingInvoiceItemModel(string RONo, string SCNo, int BuyerBrandId, string BuyerBrandName, double Quantity, int ComodityId,string ComodityCode, string ComodityName, string ComodityDesc, int UomId, string UomUnit, decimal Price, decimal PriceRO, decimal Amount, string CurrencyCode, int UnitId, string UnitCode,decimal CMTPrice)
		{
			this.Id = Id;
			this.RONo = RONo;
			this.SCNo = SCNo;
			this.BuyerBrandId = BuyerBrandId;
			this.BuyerBrandName = BuyerBrandName;
			this.Quantity = Quantity;
			this.ComodityId = ComodityId;
			this.ComodityCode = ComodityCode;
			this.ComodityName = ComodityName;
			this.ComodityDesc = ComodityDesc;
			this.UomId = UomId;
			this.UomUnit = UomUnit;
			this.Price = Price;
			this.PriceRO = PriceRO;
			this.Amount = Amount;
			this.CurrencyCode = CurrencyCode;
			this.UnitId = UnitId;
			this.UnitCode = UnitCode;
			this.CMTPrice = CMTPrice;
			
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
