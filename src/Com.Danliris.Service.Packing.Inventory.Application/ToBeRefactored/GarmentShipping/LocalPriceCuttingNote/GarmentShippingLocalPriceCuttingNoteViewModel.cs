using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteViewModel : BaseViewModel, IValidatableObject
    {
        public string cuttingPriceNoteNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public Buyer buyer { get; set; }
        public bool useVat { get; set; }
        public string remark { get; set; }

        public ICollection<GarmentShippingLocalPriceCuttingNoteItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
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

                foreach (var item in items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (string.IsNullOrWhiteSpace(item.salesNoteNo) || item.salesNoteId == 0)
                    {
                        errorItem["salesNote"] = "Nota Penjualan tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.salesAmount <= 0)
                    {
                        errorItem["salesAmount"] = "Sales Note Amount harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.cuttingAmount <= 0)
                    {
                        errorItem["cuttingAmount"] = "Potongan harus lebih dari 0";
                        errorItemsCount++;
                    }

                    errorItems.Add(errorItem);
                }

                if (errorItemsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "items" });
                }
            }
        }
    }
}
