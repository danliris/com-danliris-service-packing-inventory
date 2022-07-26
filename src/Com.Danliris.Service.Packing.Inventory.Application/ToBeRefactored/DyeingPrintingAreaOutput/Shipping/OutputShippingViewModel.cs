using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingViewModel : BaseViewModel, IValidatableObject
    {
        public OutputShippingViewModel()
        {
            ShippingProductionOrders = new HashSet<OutputShippingProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string Type { get; set; }
        public string BonNo { get; set; }
        public string ShippingCode { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public string AdjType { get; set; }
        public int InputShippingId { get; set; }
        public string Group { get; set; }
        public bool HasSalesInvoice { get; set; }
        public DeliveryOrderSales DeliveryOrder { get; set; }
        public string PackingListNo { get; set; }
        public string PackingType { get;  set; }
        public string PackingListRemark { get;  set; }
        public string PackingListAuthorized { get;  set; }
        public string PackingListLCNumber { get; set; }
        public string PackingListIssuedBy { get;  set; }
        public string PackingListDescription { get;  set; }
        public bool UpdateBySales { get;  set; }
        public ICollection<OutputShippingProductionOrderViewModel> ShippingProductionOrders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Area))
                yield return new ValidationResult("Area harus diisi", new List<string> { "Area" });

            if (string.IsNullOrEmpty(Type))
                yield return new ValidationResult("Jenis harus diisi", new List<string> { "Type" });

            if (Date == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            }
            else
            {
                if (Id == 0 && !(Date >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - Date).TotalDays <= 1 && (DateTimeOffset.UtcNow - Date).TotalDays >= 0)))
                {
                    yield return new ValidationResult("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini", new List<string> { "Date" });
                }
            }

            if (string.IsNullOrEmpty(Shift))
                yield return new ValidationResult("Shift harus diisi", new List<string> { "Shift" });

            if (string.IsNullOrEmpty(Group))
                yield return new ValidationResult("Group harus diisi", new List<string> { "Group" });

            if (Type == DyeingPrintingArea.OUT && string.IsNullOrEmpty(DestinationArea))
                yield return new ValidationResult("Tujuan Area Harus Diisi!", new List<string> { "DestinationArea" });

            if(Type == DyeingPrintingArea.OUT && DestinationArea == DyeingPrintingArea.PENJUALAN && string.IsNullOrEmpty(ShippingCode))
                yield return new ValidationResult("Kode Harus Diisi!", new List<string> { "ShippingCode" });

            int Count = 0;
            string DetailErrors = "[";

            if(Type == DyeingPrintingArea.OUT)
            {
                if ((Id == 0 && ShippingProductionOrders.Where(s => s.IsSave).Count() == 0) || (Id != 0 && ShippingProductionOrders.Count() == 0))
                {
                    yield return new ValidationResult("SPP harus Diisi", new List<string> { "ShippingProductionOrder" });
                }
                else
                {
                    foreach (var item in ShippingProductionOrders)
                    {
                        DetailErrors += "{";

                        if (Id != 0 || item.IsSave)
                        {
                            if (item.Qty == 0)
                            {
                                Count++;
                                DetailErrors += "Qty: 'Qty Keluar Harus Lebih dari 0!',";
                            }
                            else
                            {
                                if (item.Qty > item.BalanceRemains)
                                {
                                    Count++;
                                    DetailErrors += string.Format("Qty: 'Qty Keluar Tidak boleh Lebih dari sisa saldo {0}!',", item.BalanceRemains);
                                }
                            }

                        }


                        DetailErrors += "}, ";
                    }
                }
            }
            else
            {
                if (ShippingProductionOrders.Count == 0)
                {
                    yield return new ValidationResult("SPP harus Diisi", new List<string> { "ShippingProductionOrder" });
                }
                else
                {
                    var items = ShippingProductionOrders.Where(e => e.Balance != 0).Select(d => d.Balance);

                    if (!(items.All(d => d > 0) || items.All(d => d < 0)))
                    {
                        yield return new ValidationResult("Quantity SPP harus Positif semua atau Negatif Semua", new List<string> { "ShippingProductionOrder" });
                    }
                    else
                    {
                        if (Id != 0 && !string.IsNullOrEmpty(AdjType))
                        {
                            if (items.All(d => d > 0))
                            {
                                if (AdjType != DyeingPrintingArea.ADJ_IN)
                                {
                                    yield return new ValidationResult("Quantity SPP harus Negatif semua", new List<string> { "TransitProductionOrder" });
                                }
                            }
                            else
                            {
                                if (AdjType != DyeingPrintingArea.ADJ_OUT)
                                {
                                    yield return new ValidationResult("Quantity SPP harus Positif Semua", new List<string> { "TransitProductionOrder" });
                                }
                            }
                        }
                    }

                    foreach (var item in ShippingProductionOrders)
                    {
                        DetailErrors += "{";

                        if (item.ProductionOrder == null || item.ProductionOrder.Id == 0)
                        {
                            Count++;
                            DetailErrors += "ProductionOrder: 'SPP Harus Diisi!',";
                        }

                        if (item.Qty == 0)
                        {
                            Count++;
                            DetailErrors += "Qty: 'Qty Harus Lebih dari 0!',";
                        }

                        if (item.QtyPacking == 0)
                        {
                            Count++;
                            DetailErrors += "QtyPacking: 'QtyPacking Harus Lebih dari 0!',";
                        }

                        if (string.IsNullOrEmpty(item.AdjDocumentNo))
                        {
                            Count++;
                            DetailErrors += "AdjDocumentNo: 'No Dokumen Harus Diisi!',";
                        }

                        DetailErrors += "}, ";
                    }
                }
            }

            

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "ShippingProductionOrders" });
        }
    }
}
