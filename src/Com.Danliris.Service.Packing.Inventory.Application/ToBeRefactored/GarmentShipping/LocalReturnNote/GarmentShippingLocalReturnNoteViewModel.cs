using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteViewModel : BaseViewModel, IValidatableObject
    {
        public string returnNoteNo { get; set; }
        public GarmentShippingLocalSalesNoteViewModel salesNote { get; set; }
        public DateTimeOffset? returnDate { get; set; }
        public string description { get; set; }

        public ICollection<GarmentShippingLocalReturnNoteItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (salesNote == null || salesNote.Id == 0)
            {
                yield return new ValidationResult("No Nota Penjualan tidak boleh kosong", new List<string> { "salesNote" });
            }

            if (returnDate == null || returnDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "returnDate" });
            }

            if (items == null || items.Count(i => i.isChecked) == 0)
            {
                yield return new ValidationResult("Items harus ada yang dipilih", new List<string> { "itemsCount" });
            }
            else
            {
                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                foreach (var item in items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (item.isChecked)
                    {
                        if (item.salesNoteItem == null || item.salesNoteItem.Id == 0)
                        {
                            errorItem["salesNoteItem"] = "salesNoteItemId tidak boleh kosong";
                            errorItemsCount++;
                        }

                        if (item.returnQuantity <= 0)
                        {
                            errorItem["returnQuantity"] = "Harga harus lebih dari 0";
                            errorItemsCount++;
                        }
                        else if(item.returnQuantity > item.salesNoteItem.quantity)
                        {
                            errorItem["returnQuantity"] = $"Harga tidak boleh lebih dari {item.salesNoteItem.quantity}";
                            errorItemsCount++;
                        }
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
