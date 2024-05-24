using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.IN.ViewModel
{
    public class  DPInputWarehouseCreateViewModel : BaseViewModel, IValidatableObject
    {
      
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public int OutputId { get; set; }
        public string Group { get; set; }
        public ICollection<DPInputWarehouseItemCreateViewModel> DyeingPrintingWarehouseInItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (string.IsNullOrEmpty(Area))
            //    yield return new ValidationResult("Area harus diisi", new List<string> { "Area" });

            //if (string.IsNullOrEmpty(Type))
            //    yield return new ValidationResult("Jenis harus diisi", new List<string> { "Type" });

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

            if (DyeingPrintingWarehouseInItems == null)
            {
                yield return new ValidationResult("Item harus dipilih", new List<string> { "DyeingPrintingWarehouseInItems" });
            }

            if (DyeingPrintingWarehouseInItems != null && DyeingPrintingWarehouseInItems.Count() > 0)
            {
                foreach (var item in DyeingPrintingWarehouseInItems)
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


                    if (item.Sendquantity <= 0)
                    {
                        Count++;
                        DetailErrors += "SendQuantity: 'Qty Keluar Harus Lebih dari 0!',";
                    }

                    if (item.Sendquantity > item.PackagingQty)
                    {
                        Count++;
                        DetailErrors += "SendQuantity: 'Qty Keluar Harus lebih Kecil Atau Sama dengan Saldo!',";
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
