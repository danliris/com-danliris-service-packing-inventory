using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailShippingLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteViewModel : BaseViewModel, IValidatableObject
    {
        public string salesContractNo { get; set; }
        public int localSalesContractId { get; set; }
        public int localSalesNoteId { get; set; }
        public string noteNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public TransactionType transactionType { get; set; }
        public Buyer buyer { get; set; }           
        public double amount { get; set; }
        
        public ICollection<GarmentShippingDetailLocalSalesNoteItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(localSalesContractId==0 || string.IsNullOrWhiteSpace(salesContractNo))
            {
                yield return new ValidationResult("Sales Contract Lokal tidak boleh kosong", new List<string> { "salesContract" });
            }
            if (localSalesNoteId == 0 || string.IsNullOrWhiteSpace(noteNo))
            {
                yield return new ValidationResult("Sales Note Lokal tidak boleh kosong", new List<string> { "salesNote" });
            }

            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (transactionType == null || transactionType.id == 0)
            {
                yield return new ValidationResult("Jenis Transaksi tidak boleh kosong", new List<string> { "transactionType" });
            }

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }

            if (items == null || items.Count == 0)
            {
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "itemsCount" });
            }
            else
            {
                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();
                double ItemAmount = 0;
                foreach (var item in items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (item.quantity <= 0)
                    {
                        errorItem["quantity"] = "Quantity harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.uom == null || item.uom.Id == 0)
                    {
                        errorItem["uom"] = "Satuan tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.amount <= 0)
                    {
                        errorItem["amount"] = "Amount lebih dari 0";
                        errorItemsCount++;
                    }

                    //
                    ItemAmount += item.amount;
                    //

                    errorItems.Add(errorItem);
                }

                if (amount - ItemAmount > 0.1)
                {
                    yield return new ValidationResult("Total Amount harus sama dengan Total Item Amount", new List<string> { "TotalAmount" });
                }

                if (errorItemsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "items" });
                }
            }
        }
    }
}
