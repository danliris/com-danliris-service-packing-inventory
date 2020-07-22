using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingViewModel : BaseViewModel, IValidatableObject
    {
        public OutputPackagingViewModel()
        {
            PackagingProductionOrders = new HashSet<OutputPackagingProductionOrderViewModel>();
        }
        public string Type { get; set; }
        public string Area { get; set; }
        public string BonNo { get; set; }
        public string BonNoInput { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputPackagingId { get; set; }
        public string Group { get; set; }
        public ICollection<OutputPackagingProductionOrderViewModel> PackagingProductionOrders { get; set; }
        public ICollection<InputPlainAdjPackagingProductionOrder> PackagingProductionOrdersAdj { get; set; }

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

            int CountAdj = 0;
            string DetailErrorsAdj = "[";

            if (Type == "OUT")
            {
                if ((Id == 0 && PackagingProductionOrders.Where(s => s.IsSave).Count() == 0) || (Id != 0 && PackagingProductionOrders.Count() == 0))
                {
                    yield return new ValidationResult("SPP harus Diisi", new List<string> { "PackagingProductionOrder" });
                }
                else
                {
                    var Items = PackagingProductionOrders.GroupBy(s => s.ProductionOrder.Id);

                    //if (Items.Any(s => s.Count() > 1))
                    //{
                    //    yield return new ValidationResult("Tidak boleh duplikat SPP dalam satu Tabel!", new List<string> { "PackagingProductionOrder" });
                    //}
                    //else
                    //{
                    foreach (var item in Items)
                    {
                        DetailErrors += "{";
                        DetailErrors += "PackagingList : [ ";

                        foreach (var detail in item)
                        {
                            DetailErrors += "{";

                            if (detail.IsSave)
                            {
                                if (detail.PackagingQTY <= 0)
                                {
                                    Count++;
                                    DetailErrors += "PackagingQTY: 'Qty Packing Terima Harus Lebih dari 0!',";
                                }

                                if (string.IsNullOrEmpty(detail.PackagingType))
                                {
                                    Count++;
                                    DetailErrors += "PackagingType: 'Jenis Packaging Tidak Boleh Kosong!',";
                                }

                                if (string.IsNullOrEmpty(detail.PackagingUnit))
                                {
                                    Count++;
                                    DetailErrors += "PackagingUnit: 'Unit Packaging Tidak Boleh Kosong!',";
                                }

                                if (detail.QtyOut <= 0)
                                {
                                    Count++;
                                    DetailErrors += "QtyOut: 'Qty Keluar Harus Lebih dari 0!',";
                                }
                                else
                                {
                                    if (detail.QtyOut > detail.Balance)
                                    {
                                        Count++;
                                        DetailErrors += "QtyOut: 'Qty Keluar Tidak Boleh lebih dari saldo!',";
                                    }
                                }
                            }
                            

                            DetailErrors += "}, ";
                        }
                        DetailErrors += "], ";
                        DetailErrors += "}, ";
                    }
                    //}

                    //foreach (var item in PackagingProductionOrders)
                    //{
                    //    DetailErrors += "{";

                    //    if (item.PackagingQTY <= 0)
                    //    {
                    //        Count++;
                    //        DetailErrors += "PackagingQTY: 'Qty Packing Terima Harus Lebih dari 0!',";
                    //    }

                    //    if (string.IsNullOrEmpty(item.PackagingType))
                    //    {
                    //        Count++;
                    //        DetailErrors += "PackagingType: 'Jenis Packaging Tidak Boleh Kosong!',";
                    //    }

                    //    if (string.IsNullOrEmpty(item.PackagingUnit))
                    //    {
                    //        Count++;
                    //        DetailErrors += "PackagingUnit: 'Unit Packaging Tidak Boleh Kosong!',";
                    //    }

                    //    if (item.QtyOut <= 0)
                    //    {
                    //        Count++;
                    //        DetailErrors += "QtyOut: 'Qty Keluar Terima Harus Lebih dari 0!',";
                    //    }
                    //    else
                    //    {
                    //        if(item.QtyOut > item.Balance)
                    //        {
                    //            Count++;
                    //            DetailErrors += "QtyOut: 'Qty Keluar Tidak Boleh lebih dari saldo!',";
                    //        }
                    //    }

                    //    DetailErrors += "}, ";
                    //}
                }
            }
            else
            {
                if (PackagingProductionOrdersAdj.Count == 0)
                {
                    yield return new ValidationResult("SPP harus Diisi", new List<string> { "PackagingProductionOrderAdj" });
                }
                else
                {
                    var items = PackagingProductionOrdersAdj.Where(e => e.Balance != 0).Select(d => d.Balance);

                    if (!(items.All(d => d > 0) || items.All(d => d < 0)))
                    {
                        yield return new ValidationResult("Quantity SPP harus Positif semua atau Negatif Semua", new List<string> { "PackagingProductionOrderAdj" });
                    }

                    foreach (var item in PackagingProductionOrdersAdj)
                    {
                        DetailErrorsAdj += "{";

                        if (item.ProductionOrder == null || item.ProductionOrder.Id == 0)
                        {
                            CountAdj++;
                            DetailErrorsAdj += "ProductionOrder: 'SPP Harus Diisi!',";
                        }

                        if (item.Balance == 0)
                        {
                            CountAdj++;
                            DetailErrorsAdj += "Balance: 'Qty Tidak Boleh sama dengan 0!',";
                        }


                        if (string.IsNullOrEmpty(item.NoDocument))
                        {
                            CountAdj++;
                            DetailErrorsAdj += "AdjDocumentNo: 'No Dokumen Harus Diisi!',";
                        }

                        DetailErrorsAdj += "}, ";
                    }
                }
            }


            DetailErrorsAdj += "]";
            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "PackagingProductionOrders" });

            if (CountAdj > 0)
                yield return new ValidationResult(DetailErrorsAdj, new List<string> { "PackagingProductionOrdersAdj" });
        }
    }
}
