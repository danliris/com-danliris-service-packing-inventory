using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceViewModel :BaseViewModel, IValidatableObject
	{
		public int PackingListId { get; set; }
		public string PLType { get; set; }
		public string InvoiceNo { get; set; }
		public DateTimeOffset InvoiceDate { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public BuyerAgent BuyerAgent { get; set; }
		public string Consignee { get; set; }
        public string ConsigneeAddress { get; set; }
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
        public string Remark { get; set; }
		public decimal LessFabricCost { get; set; }
		public decimal DHLCharges { get; set; }
		public decimal AmountToBePaid { get; set; }
        public decimal AmountCA { get; set; }
        public string CPrice { get; set; }
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
        public string DeliverTo { get; set; }
        public ICollection<GarmentShippingInvoiceItemViewModel> Items { get;  set; }
		public ICollection<GarmentShippingInvoiceAdjustmentViewModel> GarmentShippingInvoiceAdjustments { get; set; }
        public ICollection<GarmentShippingInvoiceUnitViewModel> GarmentShippingInvoiceUnits { get; set; }

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
			if (string.IsNullOrEmpty(ShippingPer))
				yield return new ValidationResult("ShippingPer harus diisi", new List<string> { "ShippingPer" });

			//if (string.IsNullOrEmpty(ConfirmationOfOrderNo))
			//	yield return new ValidationResult("ConfirmationOfOrderNo harus diisi", new List<string> { "ConfirmationOfOrderNo" });

			if (string.IsNullOrEmpty(ShippingStaff))
				yield return new ValidationResult("ShippingStaff harus diisi", new List<string> { "ShippingStaff" });

			if (string.IsNullOrEmpty(FabricType))
				yield return new ValidationResult("FabricType harus diisi", new List<string> { "FabricType" });

			//if (string.IsNullOrEmpty(BankAccount))
			//	yield return new ValidationResult("BankDetail harus diisi", new List<string> { "BankAccount" });

            if(!string.IsNullOrEmpty(InvoiceNo) && !InvoiceNo.Contains("SM/"))
            {
                if (PaymentDue.Equals(0))
                    yield return new ValidationResult("PaymentDue harus diisi", new List<string> { "PaymentDue" });

            }

            if (string.IsNullOrEmpty(CPrice))
				yield return new ValidationResult("CPrice harus diisi", new List<string> { "CPrice" });

            if (string.IsNullOrEmpty(ConsigneeAddress))
                yield return new ValidationResult("ConsigneeAddress harus diisi", new List<string> { "ConsigneeAddress" });


            if (Items.Count == 0)
			{
				yield return new ValidationResult("Detail  harus Diisi", new List<string> { "ItemsCount" });
			}
			else
			{

				int errorItemsCount = 0;
				List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

				foreach (var item in Items)
				{
					Dictionary<string, object> errorItem = new Dictionary<string, object>();

					if (string.IsNullOrWhiteSpace(item.RONo))
					{
						errorItem["RONo"] = "RONo tidak boleh kosong";
						errorItemsCount++;
					}

					if (item.Quantity == 0)
					{
						errorItem["Quantity"] = "Quantity tidak boleh 0";
						errorItemsCount++;
					}

					if (item.Price == 0)
					{
						errorItem["Price"] = "Price tidak boleh 0";
						errorItemsCount++;
					}


					errorItems.Add(errorItem);
				}
				if (errorItemsCount > 0)
				{
					yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "Items" });
				}


			}
			if (GarmentShippingInvoiceAdjustments.Count > 0)
			{
				int errorAdjustmentCount = 0;
				List<Dictionary<string, object>> errorAdjustments = new List<Dictionary<string, object>>();


				foreach (var item in GarmentShippingInvoiceAdjustments)
				{
					Dictionary<string, object> errorItem = new Dictionary<string, object>();


					if (string.IsNullOrEmpty(item.AdjustmentDescription) && item.AdjustmentValue > 0)
					{
						errorItem["AdjustmentDescription"] = "AdjustmentDescription tidak boleh kosong";
						errorAdjustmentCount++;
						
					}
					if (item.AdjustmentValue == 0 && item.AdjustmentDescription != "")
					{
						errorItem["AdjustmentValue"] = "AdjustmentValue tidak boleh 0";
						errorAdjustmentCount++;
					}
					errorAdjustments.Add(errorItem);

				}
				if (errorAdjustmentCount > 0)
				{
				 
					yield return new ValidationResult(JsonConvert.SerializeObject(errorAdjustments), new List<string> { "GarmentShippingInvoiceAdjustments" });
				}
			}
			
		}
	}
}
