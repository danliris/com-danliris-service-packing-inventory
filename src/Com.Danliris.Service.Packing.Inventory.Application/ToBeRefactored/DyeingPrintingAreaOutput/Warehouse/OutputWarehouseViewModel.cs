using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse
{
    public class OutputWarehouseViewModel : BaseViewModel, IValidatableObject
    {
        public OutputWarehouseViewModel()
        {
            WarehousesProductionOrders = new HashSet<OutputWarehouseProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public IndexViewModel Bon { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputWarehouseId { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public ICollection<OutputWarehouseProductionOrderViewModel> WarehousesProductionOrders { get; set; }

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

            if (Type == "OUT" && string.IsNullOrEmpty(DestinationArea))
                yield return new ValidationResult("Tujuan Area Harus Diisi!", new List<string> { "DestinationArea" });

            int Count = 0;
            string DetailErrors = "[";

            if (Type == "OUT")
            {
                if (WarehousesProductionOrders.Count() == 0)
                {
                    yield return new ValidationResult("SPP harus Diisi", new List<string> { "WarehousesProductionOrder" });
                }
                else
                {
                    var Items = WarehousesProductionOrders.GroupBy(s => s.ProductionOrder.Id);

                    //if (Items.Any(s => s.Count() > 1))
                    //{
                    //    yield return new ValidationResult("Tidak boleh duplikat SPP dalam satu Tabel!", new List<string> { "PackagingProductionOrder" });
                    //}
                    //else
                    //{
                    foreach (var item in Items)
                    {
                        DetailErrors += "{";
                        DetailErrors += "WarehouseList : [ ";

                        foreach (var detail in item)
                        {
                            DetailErrors += "{";

                            if (detail.IsSave)
                            {
                                if (DestinationArea == "SHIPPING" && (detail.DeliveryOrderSalesId == 0 || string.IsNullOrEmpty(detail.DeliveryOrderSalesNo)))
                                {
                                    Count++;
                                    DetailErrors += "DeliveryOrderSales: 'Nomor DO harus diisi!',";
                                }

                                if (detail.PackagingQty <= 0)
                                {
                                    Count++;
                                    DetailErrors += "PackagingQty: 'Qty Packing Terima Harus Lebih dari 0!',";
                                }

                                if (string.IsNullOrEmpty(detail.PackagingUnit))
                                {
                                    Count++;
                                    DetailErrors += "PackagingUnit: 'Unit Packaging Tidak Boleh Kosong!',";
                                }

                                if (detail.Balance <= 0)
                                {
                                    Count++;
                                    DetailErrors += "Balance: 'Qty Terima Harus Lebih dari 0!',";
                                }
                                else
                                {
                                    if (detail.Balance > detail.PreviousBalance)
                                    {
                                        Count++;
                                        DetailErrors += string.Format("Balance: 'Qty Keluar Tidak boleh Lebih dari sisa saldo {0}!',", detail.PreviousBalance);
                                    }
                                }

                            }


                            DetailErrors += "}, ";
                        }
                        DetailErrors += "], ";
                        DetailErrors += "}, ";
                    }

                    //foreach (var item in WarehousesProductionOrders)
                    //{
                    //    DetailErrors += "{";

                    //    if (item.Balance <= 0)
                    //    {
                    //        Count++;
                    //        DetailErrors += "Balance: 'Qty Terima Harus Lebih dari 0!',";
                    //    }
                    //    else
                    //    {
                    //        if (item.Balance > item.PreviousBalance)
                    //        {
                    //            Count++;
                    //            DetailErrors += string.Format("Balance: 'Qty Keluar Tidak boleh Lebih dari sisa saldo {0}!',", item.PreviousBalance);
                    //        }
                    //    }
                    //    DetailErrors += "}, ";
                    //}
                }
            }
            else
            {
                if (WarehousesProductionOrders.Count == 0)
                {
                    yield return new ValidationResult("SPP harus Diisi", new List<string> { "WarehousesProductionOrder" });
                }
                else
                {
                    var items = WarehousesProductionOrders.Where(e => e.Balance != 0).Select(d => d.Balance);

                    if (!(items.All(d => d > 0) || items.All(d => d < 0)))
                    {
                        yield return new ValidationResult("Quantity SPP harus Positif semua atau Negatif Semua", new List<string> { "WarehousesProductionOrder" });
                    }

                    foreach (var item in WarehousesProductionOrders)
                    {
                        DetailErrors += "{";

                        if (item.ProductionOrder == null || item.ProductionOrder.Id == 0)
                        {
                            Count++;
                            DetailErrors += "ProductionOrder: 'SPP Harus Diisi!',";
                        }

                        if (item.Balance == 0)
                        {
                            Count++;
                            DetailErrors += "Balance: 'Qty Tidak Boleh sama dengan 0!',";
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
                yield return new ValidationResult(DetailErrors, new List<string> { "WarehousesProductionOrders" });
        }
    }
}
