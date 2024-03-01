using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListViewModel : BaseViewModel, IValidatableObject
    {
        public int LocalSalesNoteId { get;  set; }
        public string LocalSalesNoteNo { get;  set; }
        public DateTimeOffset LocalSalesNoteDate { get;  set; }

        public int LocalSalesContractId { get;  set; }
        public string LocalSalesContractNo { get;  set; }


        public TransactionType TransactionType { get; set; }
        public Buyer Buyer { get; set; }

        public string PaymentTerm { get;  set; }

        public bool Omzet { get;  set; }
        public bool Accounting { get;  set; }

        public double GrossWeight { get;  set; }
        public double NettWeight { get;  set; }
        public double NetNetWeight { get;  set; }
        public double TotalCartons { get;  set; }

        public bool IsApproved { get;  set; }
        public bool IsUsed { get;  set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }

        //Approval
        public bool IsValidatedMD { get;  set; }
        public string ValidatedMDBy { get;  set; }
        public DateTimeOffset? ValidatedMDDate { get;  set; }
        public double Kurs { get;  set; }
        public string ValidatedMDRemark { get;  set; }

        public bool IsValidatedShipping { get;  set; }
        public string ValidatedShippingBy { get;  set; }
        public DateTimeOffset? ValidatedShippingDate { get;  set; }
        public string RejectReason { get;  set; }
        public string RejectTo { get;  set; }
        public ICollection<GarmentReceiptSubconPackingListItemViewModel> Items { get; set; }


        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            //if (string.IsNullOrEmpty(LocalSalesNoteNo))
            //{
            //    yield return new ValidationResult("Nomer Nota Penjualan Lokal tidak boleh kosong", new List<string> { "LocalSalesNoteNo" });
            //}

            if (InvoiceDate == null || InvoiceDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tgl Invoice tidak boleh kosong", new List<string> { "InvoiceDate" });
            }
            //else if (Date > DateTimeOffset.Now)
            //{
            //    yield return new ValidationResult("Tanggal tidak boleh lebih dari hari ini", new List<string> { "Date" });
            //}

            //if (string.IsNullOrWhiteSpace(PaymentTerm))
            //{
            //    yield return new ValidationResult("Payment Term tidak boleh kosong", new List<string> { "PaymentTerm" });
            //}
            //else if (PaymentTerm.ToUpper() == "LC")
            //{
            //    if (string.IsNullOrEmpty(LCNo))
            //    {
            //        yield return new ValidationResult("LC No tidak boleh kosong", new List<string> { "LCNo" });
            //    }

            //    if (LCDate == null || LCDate == DateTimeOffset.MinValue)
            //    {
            //        yield return new ValidationResult("Tgl. LC harus diisi", new List<string> { "LCDate" });
            //    }

            //    if (string.IsNullOrEmpty(IssuedBy))
            //    {
            //        yield return new ValidationResult("Issued By tidak boleh kosong", new List<string> { "IssuedBy" });
            //    }
            //}

            if (Buyer == null || Buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "Buyer" });
            }

         

            if (Items == null || Items.Count < 1)
            {
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "ItemsCount" });
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

                    if (string.IsNullOrWhiteSpace(item.OrderNo))
                    {
                        errorItem["OrderNo"] = "Order No tidak boleh kosong";
                        errorItemsCount++;
                    }

                    //if (string.IsNullOrWhiteSpace(item.Description))
                    //{
                    //    errorItem["Description"] = "Description tidak boleh kosong";
                    //    errorItemsCount++;
                    //}

                    if (item.TotalQuantityPackingOut != item.TotalQuantitySize )
                    {
                        errorItem["TotalQuantityPackingOut"] = "Jumlah Packing Out dengan Jumlah Detail Size Harus Sama";
                        errorItemsCount++;
                    }

                    if (item.TotalQuantityPackingOut != item.TotalQuantityCarton)
                    {
                        errorItem["TotalQuantityPackingOut"] = "Jumlah Packing Out dengan Jumlah Total Qty Carton Harus Sama";
                        errorItemsCount++;
                    }

                    if (item.Details == null || item.Details.Count < 1)
                    {
                        errorItem["DetailsCount"] = "Details tidak boleh kosong";
                        errorItemsCount++;
                    }
                    else
                    {
                        int errorDetailsCount = 0;
                        List<Dictionary<string, object>> errorDetails = new List<Dictionary<string, object>>();

                        foreach (var detail in item.Details)
                        {
                            Dictionary<string, object> errorDetail = new Dictionary<string, object>();

                            if (detail.Sizes == null || detail.Sizes.Count < 1)
                            {
                                errorDetail["SizesCount"] = "Sizes tidak boleh kosong";
                                errorDetailsCount++;
                            }
                            else
                            {
                                int errorSizesCount = 0;
                                List<Dictionary<string, object>> errorSizes = new List<Dictionary<string, object>>();

                                foreach (var size in detail.Sizes)
                                {
                                    Dictionary<string, object> errorSize = new Dictionary<string, object>();

                                    if (size.Size == null || size.Size.Id == 0)
                                    {
                                        errorSize["Size"] = "Size tidak boleh kosong";
                                        errorSizesCount++;
                                    }

                                    errorSizes.Add(errorSize);
                                }

                                if (errorSizesCount > 0)
                                {
                                    errorDetail["Sizes"] = errorSizes;
                                    errorDetailsCount++;
                                }
                                //
                                // if (detail.Sizes.Sum(s => s.Quantity) != (detail.CartonQuantity * detail.QuantityPCS))
                                // {
                                //     errorDetail["TotalQtySize"] = "Harus sama dengan Total Qty";
                                //     errorDetailsCount++;
                                // }
                            }

                            errorDetails.Add(errorDetail);
                        }

                        if (errorDetailsCount > 0)
                        {
                            errorItem["Details"] = errorDetails;
                            errorItemsCount++;
                        }

                        //if (item.Quantity != item.Details.Sum(d => d.CartonQuantity * d.QuantityPCS))
                        //{
                        //    errorItem["totalQty"] = "Harus sama dengan Qty";
                        //    errorItemsCount++;
                        //}
                    }

                    errorItems.Add(errorItem);
                }

                if (errorItemsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "Items" });
                }
            }


        }
    }
}
