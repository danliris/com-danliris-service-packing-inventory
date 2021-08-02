using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteViewModel : BaseViewModel, IValidatableObject
    {
        public string correctionNoteNo { get; set; }
        public GarmentShippingLocalSalesNoteViewModel salesNote { get; set; }
        public DateTimeOffset? correctionDate { get; set; }
        public string remark { get; set; }

        public ICollection<GarmentShippingLocalPriceCorrectionNoteItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (salesNote == null || salesNote.Id == 0)
            {
                yield return new ValidationResult("No Nota Penjualan tidak boleh kosong", new List<string> { "salesNote" });
            }

            if (correctionDate == null || correctionDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "correctionDate" });
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

                        if (item.priceCorrection <= 0)
                        {
                            errorItem["priceCorrection"] = "Harga harus lebih dari 0";
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
