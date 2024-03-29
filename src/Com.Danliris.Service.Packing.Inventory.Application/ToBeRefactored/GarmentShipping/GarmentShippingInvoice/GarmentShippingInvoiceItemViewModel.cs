﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceItemViewModel : BaseViewModel
	{
		public string RONo { get;  set; }
		public string SCNo { get;  set; }
		public BuyerBrand BuyerBrand { get;  set; }
		public double Quantity { get;  set; }
		public Comodity Comodity { get;  set; }
		public string ComodityDesc { get;  set; }
		public string MarketingName { get; set; }
		public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public string Desc4 { get; set; }
        public UnitOfMeasurement Uom { get;  set; }
		public decimal Price { get;  set; }
		public decimal PriceRO { get;  set; }
		public decimal Amount { get;  set; }
		public string CurrencyCode { get;  set; }
		public Unit Unit { get; set; }
		public decimal CMTPrice { get; set; }
		public int PackingListItemId { get; set; }
	}
}
