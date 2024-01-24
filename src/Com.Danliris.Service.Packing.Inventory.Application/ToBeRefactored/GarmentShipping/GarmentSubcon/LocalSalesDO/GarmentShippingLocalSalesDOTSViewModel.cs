using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDOTS
{
    public class GarmentShippingLocalSalesDOTSViewModel : BaseViewModel, IValidatableObject
    {

        public string localSalesDONo { get; set; }
        public string localSalesNoteNo { get; set; }
        public int localSalesNoteId { get; set; }
        public string storageDivision { get; set; }
        public DateTimeOffset date { get; set; }
        public Buyer buyer { get; set; }
        public string to { get; set; }
        public string remark { get; set; }
        public ICollection<GarmentShippingLocalSalesDOTSItemViewModel> items { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }
            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer Agent tidak boleh kosong", new List<string> { "buyer" });
            }
            if (string.IsNullOrEmpty(localSalesNoteNo))
            {
                yield return new ValidationResult("No Nota Penjualan tidak boleh kosong", new List<string> { "localSalesNoteNo" });
            }
            if (string.IsNullOrEmpty(to))
            {
                yield return new ValidationResult("Kepada tidak boleh kosong", new List<string> { "to" });
            }
            if (string.IsNullOrEmpty(storageDivision))
            {
                yield return new ValidationResult("Bag Gudang tidak boleh kosong", new List<string> { "storageDivision" });
            }
            if (items == null || items.Count < 1)
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

                    if (item.quantity <= 0)
                    {
                        errorItem["quantity"] = "Quantity harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.nettWeight <= 0)
                    {
                        errorItem["nettWeight"] = "Nett Weight harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.grossWeight <= 0)
                    {
                        errorItem["grossWeight"] = "Gross Weight harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.packQuantity <= 0)
                    {
                        errorItem["packQuantity"] = "Jumlah Kemasan harus lebih dari 0";
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
