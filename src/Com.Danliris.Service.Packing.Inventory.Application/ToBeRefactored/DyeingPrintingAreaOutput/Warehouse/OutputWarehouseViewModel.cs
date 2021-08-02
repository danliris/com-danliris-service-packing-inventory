using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
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
        public string AdjType { get; set; }
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

            if (Type == DyeingPrintingArea.OUT && string.IsNullOrEmpty(DestinationArea))
                yield return new ValidationResult("Tujuan Area Harus Diisi!", new List<string> { "DestinationArea" });

            int Count = 0;
            string DetailErrors = "[";

            if (Type == DyeingPrintingArea.OUT)
            {
                if ((Id == 0 && WarehousesProductionOrders.Where(s => s.IsSave).Count() == 0) || (Id != 0 && WarehousesProductionOrders.Count() == 0))
                {
                    yield return new ValidationResult("SPP harus Diisi", new List<string> { "WarehousesProductionOrder" });
                }
                else
                {
                   
                    var Items = WarehousesProductionOrders.GroupBy(s => s.ProductionOrder.Id);
                    
                    foreach (var item in Items)
                    {
                        DetailErrors += "{";
                        DetailErrors += "WarehouseList : [ ";
                        var items = item.GroupBy(x => x.Id).Select(s => new OutputWarehouseProductionOrderViewModel
                        {
                            Active = s.FirstOrDefault().Active,
                            AdjDocumentNo = s.FirstOrDefault().AdjDocumentNo,
                            Balance = s.Sum(x => x.Balance),
                            BalanceRemains = s.FirstOrDefault().BalanceRemains,
                            Buyer = s.FirstOrDefault().Buyer,
                            BuyerId = s.FirstOrDefault().BuyerId,
                            CartNo = s.FirstOrDefault().CartNo,
                            Color = s.FirstOrDefault().Color,
                            Construction = s.FirstOrDefault().Construction,
                            CreatedAgent = s.FirstOrDefault().CreatedAgent,
                            CreatedBy = s.FirstOrDefault().CreatedBy,
                            CreatedUtc = s.FirstOrDefault().CreatedUtc,
                            DateIn = s.FirstOrDefault().DateIn,
                            DateOut = s.FirstOrDefault().DateOut,
                            DeletedAgent = s.FirstOrDefault().DeletedAgent,
                            DeletedBy = s.FirstOrDefault().DeletedBy,
                            DeletedUtc = s.FirstOrDefault().DeletedUtc,
                            DeliveryOrderSalesId = s.FirstOrDefault().DeliveryOrderSalesId,
                            DeliveryOrderSalesNo = s.FirstOrDefault().DeliveryOrderSalesNo,
                            DyeingPrintingAreaInputProductionOrderId = s.FirstOrDefault().DyeingPrintingAreaInputProductionOrderId,
                            FabricPackingId = s.FirstOrDefault().FabricPackingId,
                            FabricSKUId = s.FirstOrDefault().FabricSKUId,
                            FinishWidth = s.FirstOrDefault().FinishWidth,
                            Grade = s.FirstOrDefault().Grade,
                            HasNextAreaDocument = s.FirstOrDefault().HasNextAreaDocument,
                            HasPrintingProductPacking = s.FirstOrDefault().HasPrintingProductPacking,
                            HasPrintingProductSKU = s.FirstOrDefault().HasPrintingProductSKU,
                            Id = s.FirstOrDefault().Id,
                            InputId = s.FirstOrDefault().InputId,
                            IsDeleted = s.FirstOrDefault().IsDeleted,
                            IsSave = s.FirstOrDefault().IsSave,
                            LastModifiedAgent = s.FirstOrDefault().LastModifiedAgent,
                            LastModifiedBy = s.FirstOrDefault().LastModifiedBy,
                            LastModifiedUtc = s.FirstOrDefault().LastModifiedUtc,
                            Material = s.FirstOrDefault().Material,
                            MaterialConstruction = s.FirstOrDefault().MaterialConstruction,
                            MaterialProduct = s.FirstOrDefault().MaterialProduct,
                            MaterialWidth = s.FirstOrDefault().MaterialWidth,
                            Motif = s.FirstOrDefault().Motif,
                            MtrLength = s.FirstOrDefault().MtrLength,
                            PackagingQty = s.Sum(x => x.PackagingQty),
                            PackagingType = s.FirstOrDefault().PackagingType,
                            PackagingUnit = s.FirstOrDefault().PackagingUnit,
                            PackingInstruction = s.FirstOrDefault().PackingInstruction,
                            PreviousBalance = s.FirstOrDefault().PreviousBalance,
                            ProcessType = s.FirstOrDefault().ProcessType,
                            ProductionOrder = s.FirstOrDefault().ProductionOrder,
                            ProductionOrderNo = s.FirstOrDefault().ProductionOrderNo,
                            ProductPackingCode = s.FirstOrDefault().ProductPackingCode,
                            ProductPackingId = s.FirstOrDefault().ProductPackingId,
                            ProductSKUCode = s.FirstOrDefault().ProductSKUCode,
                            ProductSKUId = s.FirstOrDefault().ProductSKUId,
                            QtyOrder = s.FirstOrDefault().QtyOrder,
                            Quantity = s.FirstOrDefault().Quantity,
                            Remark = s.FirstOrDefault().Remark,
                            Status = s.FirstOrDefault().Status,
                            Unit = s.FirstOrDefault().Unit,
                            UomUnit = s.FirstOrDefault().UomUnit,
                            YarnMaterial = s.FirstOrDefault().YarnMaterial,
                            YdsLength = s.FirstOrDefault().YdsLength,
                            PreviousQtyPacking = s.FirstOrDefault().PreviousQtyPacking
                        });
                        //foreach(var detail in item.Where(x=>x.Id == ))
                        foreach (var detail in item)
                        {
                            DetailErrors += "{";

                            if (Id != 0 || detail.IsSave)
                            {
                                if (DestinationArea == DyeingPrintingArea.SHIPPING && (detail.DeliveryOrderSalesId == 0 || string.IsNullOrEmpty(detail.DeliveryOrderSalesNo)))
                                {
                                    Count++;
                                    DetailErrors += "DeliveryOrderSales: 'Nomor DO harus diisi!',";
                                }
                                var detail1 = items.FirstOrDefault(x => x.Id == detail.Id);
                                if (detail1.PackagingQty <= 0)
                                {
                                    Count++;
                                    DetailErrors += "PackagingQty: 'Qty Packing Terima Harus Lebih dari 0!',";
                                }
                                else {
                                    if(detail1.PackagingQty > detail.PreviousQtyPacking)
                                    {
                                        Count++;
                                        DetailErrors += string.Format("PackagingQty: 'Qty Packing Tidak Boleh Lebih dari Qty Packing {0}!',",detail.PreviousQtyPacking);
                                    }
                                }

                                if (string.IsNullOrEmpty(detail.PackagingUnit))
                                {
                                    Count++;
                                    DetailErrors += "PackagingUnit: 'Unit Packaging Tidak Boleh Kosong!',";
                                }

                                if (detail1.Balance <= 0)
                                {
                                    Count++;
                                    DetailErrors += "Balance: 'Qty Terima Harus Lebih dari 0!',";
                                }
                                else
                                {
                                    if (detail1.Balance > detail1.PreviousBalance)
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
