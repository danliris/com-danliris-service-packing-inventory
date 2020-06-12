using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceViewModel :BaseViewModel, IValidatableObject
	{
		public int PackingListId { get; set; }
		public string InvoiceNo { get; set; }
		public DateTimeOffset InvoiceDate { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public BuyerAgent BuyerAgent { get; set; }
		public string Consignee { get; set; }
		public string LCNo { get;  set; }
		public string IssuedBy { get; set; }
		public Section Section { get; set; }
		public string ShippingPer { get; set; }
		public DateTimeOffset SailingDate { get; set; }
		public string ConfirmationOfOrderNo { get; set; }
		public int ShippingStaffId { get; set; }
		public string ShippingStaff { get; set; }
		public int FabricTypeId { get; set; }
		public string FabricType { get; set; }
		public int BankAccountId { get; set; }
		public string BankAccount { get; set; }
		public int PaymentDue { get; set; }
		public string PEBNo { get; set; }
		public DateTimeOffset? PEBDate { get; set; }
		public string NPENo { get; set; }
		public DateTimeOffset? NPEDate { get; set; }
		public string Description { get; set; }
		public decimal AmountToBePaid { get; set; }
		public decimal CPrice { get; set; }
		public string Say { get; set; }
		public string Memo { get; set; }
		public bool IsUsed { get; set; }
		public string BL { get; set; }
		public DateTimeOffset? BLDate { get; set; }
		public string CO { get; set; }
		public DateTimeOffset? CODate { get; set; }
		public string COTP { get; set; }
		public DateTimeOffset? COTPDate { get; set; }
		public decimal TotalAmount { get; set; }
		public ICollection<GarmentShippingInvoiceItemViewModel> Items { get;  set; }
		public ICollection<GarmentShippingInvoiceAdjustmentViewModel> GarmentShippingInvoiceAdjustments { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrEmpty(InvoiceNo))
				yield return new ValidationResult("InvoiceNo harus diisi", new List<string> { "InvoiceNo" });
			if (string.IsNullOrEmpty(From))
				yield return new ValidationResult("From harus diisi", new List<string> { "From" });

			if (string.IsNullOrEmpty(To))
				yield return new ValidationResult("To harus diisi", new List<string> { "To" });

			if (SailingDate == default(DateTimeOffset))
			{
				yield return new ValidationResult("SailingDate harus diisi", new List<string> { "SailingDate" });
			}
			 

			if (string.IsNullOrEmpty(Consignee))
				yield return new ValidationResult("Consignee harus diisi", new List<string> { "Consignee" });

			if (string.IsNullOrEmpty(ConfirmationOfOrderNo))
				yield return new ValidationResult("ConfirmationOfOrderNo harus diisi", new List<string> { "ConfirmationOfOrderNo" });

			if (string.IsNullOrEmpty(ShippingStaff))
				yield return new ValidationResult("ShippingStaff harus diisi", new List<string> { "ShippingStaff" });

			if (string.IsNullOrEmpty(FabricType))
				yield return new ValidationResult("FabricType harus diisi", new List<string> { "FabricType" });

			if (string.IsNullOrEmpty(BankAccount))
				yield return new ValidationResult("BankDetail harus diisi", new List<string> { "BankAccount" });

			if (PaymentDue.Equals(0))
				yield return new ValidationResult("PaymentDue harus diisi", new List<string> { "PaymentDue" });

			if (CPrice.Equals(0))
				yield return new ValidationResult("CPrice harus diisi", new List<string> { "CPrice" });

			int Count = 0;
			string DetailErrors = "[";

			if (Items.Count == 0)
			{
				yield return new ValidationResult("Detail  harus Diisi", new List<string> { "Items" });
			}
			else
			{
				foreach (var item in Items)
				{
					DetailErrors += "{";

					if (string.IsNullOrEmpty(item.RONo))
					{
						Count++;
						DetailErrors += "RONo: 'RONo Harus Diisi!',";
					}
					if (string.IsNullOrEmpty(item.ComodityDesc))
					{
						Count++;
						DetailErrors += "ComodityDesc: 'ComodityDesc Harus Diisi!',";
					}

					if (item.Quantity == 0)
					{
						Count++;
						DetailErrors += "Quantity: 'Quantity Harus Diisi!',";
					}

					if (item.Price == 0)
					{
						Count++;
						DetailErrors += "Price: 'Price Harus Diisi!',";
					}

					DetailErrors += "}, ";
				}
			}

			DetailErrors += "]";

		}
	}
}
