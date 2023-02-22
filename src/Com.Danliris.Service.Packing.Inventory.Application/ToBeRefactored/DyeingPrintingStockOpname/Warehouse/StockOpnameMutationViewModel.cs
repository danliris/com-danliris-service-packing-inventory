using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameMutationViewModel : BaseViewModel, IValidatableObject
    {
        public StockOpnameMutationViewModel()
        {
            DyeingPrintingStockOpnameMutationItems = new HashSet<StockOpnameMutationItemViewModel>();
        }
        public string Area { get; set; }
        public IndexViewModel Bon { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public string Type { get; set; }
        public ICollection<StockOpnameMutationItemViewModel> DyeingPrintingStockOpnameMutationItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (string.IsNullOrEmpty(Area))
            //    yield return new ValidationResult("Area harus diisi", new List<string> { "Area" });

            if (string.IsNullOrEmpty(Type))
                yield return new ValidationResult("Jenis harus diisi", new List<string> { "Type" });

            //if (Date == default(DateTimeOffset))
            //{
            //    yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            //}
            //else
            //{
            //    if (Id == 0 && !(Date >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - Date).TotalDays <= 1 && (DateTimeOffset.UtcNow - Date).TotalDays >= 0)))
            //    {
            //        yield return new ValidationResult("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini", new List<string> { "Date" });
            //    }
            //}


            int Count = 0;
            string DetailErrors = "[";

            if (DyeingPrintingStockOpnameMutationItems == null)
            {
                yield return new ValidationResult("Item harus dipilih", new List<string> { "DyeingPrintingStockOpnameMutationItems" });
            }

            if (DyeingPrintingStockOpnameMutationItems != null && DyeingPrintingStockOpnameMutationItems.Count() > 0)
            {
                foreach (var item in DyeingPrintingStockOpnameMutationItems)
                {
                    DetailErrors += "{";

                    if (string.IsNullOrWhiteSpace(item.Grade))
                    {
                        Count++;
                        DetailErrors += "Grade: 'Grade harus diisi',";
                    }

                    if (item.ProductionOrder == null || item.ProductionOrder.Id == 0)
                    {
                        Count++;
                        DetailErrors += "ProductionOrder: 'SPP Harus Diisi!',";
                    }


                    if (item.SendQuantity <= 0)
                    {
                        Count++;
                        DetailErrors += "SendQuantity: 'Qty Keluar Harus Lebih dari 0!',";
                    }

                    if (item.SendQuantity > item.PackagingQty)
                    {
                        Count++;
                        DetailErrors += "SendQuantity: 'Qty Keluar Harus lebih Kecil Atau Sama dengan Saldo!',";
                    }
                    if (item.Track == null)
                    {
                        Count++;
                        DetailErrors += "Track: 'Jalur/Rak Tidak Boleh Kosong',";
                    }
                    //else
                    //{
                    //    if (item.Balance > item.BalanceRemains)
                    //    {
                    //        Count++;
                    //        DetailErrors += string.Format("Balance: 'Qty Keluar Tidak boleh Lebih dari sisa saldo {0}!',", item.BalanceRemains);
                    //    }
                    //}

                    //if (item.PackagingQty <= 0)
                    //{
                    //    Count++;
                    //    DetailErrors += "QtyPacking: 'Qty Packing Harus Lebih dari 0!',";
                    //}

                    if (string.IsNullOrEmpty(item.PackagingUnit))
                    {
                        Count++;
                        DetailErrors += "PackagingUnit: 'Hanya Barang yang berupa Packing yang bisa dikeluarkan!',";
                    }

                    DetailErrors += "}, ";
                }
            }


            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "Items" });
        }
    }
}
