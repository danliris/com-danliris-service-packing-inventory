using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNoteTS
{
    public class GarmentShippingLocalSalesNoteTSViewModel : BaseViewModel, IValidatableObject
    {
        public string salesContractNo { get; set; }
        public int salesContractId { get; set; }
        public string noteNo { get; set; }
        public DateTimeOffset? date { get; set; }
        //public TransactionType transactionType { get; set; }
        public Buyer buyer { get; set; }
        public BICurrency BICurrency { get; set; }
        public BankAccount bank { get; set; }
        public int tempo { get; set; }
        //public string expenditureNo { get; set; }
        //public string dispositionNo { get; set; }
        public bool useVat { get; set; }
        public Vat vat { get; set; }
        public string remark { get; set; }
        public bool isUsed { get; set; }
        public bool isCL { get; set; }
        public bool isDetail { get; set; }
        public string paymentType { get; set; }
        //public double Amount { get; set; }
        public bool isApproveShipping { get; set; }
        public bool isApproveFinance { get; set; }
        public string approveShippingBy { get; set; }
        public string approveFinanceBy { get; set; }
        public DateTimeOffset approveShippingDate { get; set; }
        public DateTimeOffset approveFinanceDate { get; set; }
        public bool isRejectedShipping { get; set; }
        public bool isRejectedFinance { get; set; }
        public string rejectedReason { get; set; }
        //public bool isSubconPackingList { get; set; }
        public bool isDO { get; set; }
        public ICollection<GarmentShippingLocalSalesNoteTSItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(salesContractId ==0 || string.IsNullOrWhiteSpace(salesContractNo))
            {
                yield return new ValidationResult("Sales Contract tidak boleh kosong", new List<string> { "salesContract" });
            }
            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            //if (transactionType == null || transactionType.id == 0)
            //{
            //    yield return new ValidationResult("Jenis Transaksi tidak boleh kosong", new List<string> { "transactionType" });
            //}

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }

            if (bank == null || bank.id == 0)
            {
                yield return new ValidationResult("Bank tidak boleh kosong", new List<string> { "bank" });
            }

            //if (vat == null || vat.id == 0)
            //{
            //    yield return new ValidationResult("PPN tidak boleh kosong", new List<string> { "vat" });
            //}

            if (paymentType=="TEMPO" && tempo <= 0)
            {
                yield return new ValidationResult("Tempo Pembayaran tidak boleh kosong", new List<string> { "tempo" });
            }

            //if (string.IsNullOrWhiteSpace(expenditureNo))
            //{
            //    yield return new ValidationResult("No Bon Keluar tidak boleh kosong", new List<string> { "expenditureNo" });
            //}

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

                    //if (item.product == null || item.product.id == 0)
                    //{
                    //    errorItem["product"] = "Barang tidak boleh kosong";
                    //    errorItemsCount++;
                    //}

                    if (string.IsNullOrWhiteSpace(item.invoiceNo))
                    {
                        errorItem["invoice"] = "No Invoice tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.quantity <= 0)
                    {
                        errorItem["quantity"] = "Quantity harus lebih dari 0";
                        errorItemsCount++;
                    }

                    //if (item.quantity > item.remQty)
                    //{
                    //    errorItem["quantity"] = $"Quantity tidak boleh lebih dari {item.remQty}";
                    //    errorItemsCount++;
                    //}

                    if (item.uom == null || item.uom.Id == 0)
                    {
                        errorItem["uom"] = "Satuan tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.packageQuantity <= 0)
                    {
                        errorItem["packageQuantity"] = "Jumlah Kemasan harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.packageUom == null || item.packageUom.Id == 0)
                    {
                        errorItem["packageUom"] = "Satuan Kemasan tidak boleh kosong";
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
