using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractViewModel : BaseViewModel, IValidatableObject
    {
        public string salesContractNo { get; set; }
        public DateTimeOffset? salesContractDate { get; set; }
        public TransactionType transactionType { get; set; }
        public Buyer buyer { get; set; }
        public bool isUseVat { get; set; }
        public Vat vat { get; set; }
        public bool isUsed { get; set; }
        public string sellerName { get; set; }
        public string sellerNPWP { get; set; }
        public string sellerPosition { get; set; }
        public string sellerAddress { get; set; }
        public decimal subTotal { get; set; }

        public ICollection<GarmentShippingLocalSalesContractItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (salesContractDate == null || salesContractDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "salesContractDate" });
            }

            if (transactionType == null || transactionType.id == 0)
            {
                yield return new ValidationResult("Jenis Transaksi tidak boleh kosong", new List<string> { "transactionType" });
            }

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }

            if (string.IsNullOrWhiteSpace(sellerName))
            {
                yield return new ValidationResult("Nama Penjual tidak boleh kosong", new List<string> { "sellerName" });
            }

            if (string.IsNullOrWhiteSpace(sellerNPWP))
            {
                yield return new ValidationResult("NPWP Penjual tidak boleh kosong", new List<string> { "sellerNPWP" });
            }

            if (string.IsNullOrWhiteSpace(sellerAddress))
            {
                yield return new ValidationResult("NPWP Penjual tidak boleh kosong", new List<string> { "sellerAddress" });
            }

            if (string.IsNullOrWhiteSpace(sellerPosition))
            {
                yield return new ValidationResult("Jabatan Penjual tidak boleh kosong", new List<string> { "sellerPosition" });
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

                    if (item.product == null || item.product.id == 0)
                    {
                        errorItem["product"] = "Barang tidak boleh kosong";
                        errorItemsCount++;
                    }

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

                    if (item.price <= 0)
                    {
                        errorItem["price"] = "Harga harus lebih dari 0";
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
