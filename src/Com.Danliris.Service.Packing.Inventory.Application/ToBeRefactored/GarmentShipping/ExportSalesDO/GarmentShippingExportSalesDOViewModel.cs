using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOViewModel : BaseViewModel, IValidatableObject
    {
        public string exportSalesDONo { get;  set; }
        public string invoiceNo { get;  set; }
        public int packingListId { get;  set; }
        public DateTimeOffset date { get;  set; }
        public Buyer buyerAgent { get;  set; }
        public string to { get;  set; }
        public Unit unit { get;  set; }

        public ICollection<GarmentShippingExportSalesDOItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }
            if (buyerAgent == null || buyerAgent.Id == 0)
            {
                yield return new ValidationResult("Buyer Agent tidak boleh kosong", new List<string> { "buyerAgent" });
            }
            if (string.IsNullOrEmpty(invoiceNo))
            {
                yield return new ValidationResult("Invoice No tidak boleh kosong", new List<string> { "invoiceNo" });
            }
            if (string.IsNullOrEmpty(to))
            {
                yield return new ValidationResult("Kepada tidak boleh kosong", new List<string> { "to" });
            }
            if (unit == null || unit.Id == 0)
            {
                yield return new ValidationResult("Bag Gudang tidak boleh kosong", new List<string> { "unit" });
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

                    if (string.IsNullOrWhiteSpace(item.description))
                    {
                        errorItem["description"] = "Description tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.quantity<=0)
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

                    if (item.cartonQuantity <= 0)
                    {
                        errorItem["cartonQuantity"] = "Carton Quantity harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.uom == null || item.uom.Id == 0)
                    {
                        errorItem["uom"] = "Satuan tidak boleh kosong";
                        errorItemsCount++;
                    }
                    if (item.comodity == null || item.comodity.Id == 0)
                    {
                        errorItem["comodity"] = "Komoditi tidak boleh kosong";
                        errorItemsCount++;
                    }
                    if (item.volume <= 0)
                    {
                        errorItem["volume"] = "Volume harus lebih dari 0";
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
