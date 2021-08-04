using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create
{
    public class InputWarehouseCreateViewModel : BaseViewModel, IValidatableObject
    {
        public InputWarehouseCreateViewModel()
        {
            MappedWarehousesProductionOrders = new HashSet<InputWarehouseProductionOrderCreateViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public int OutputId { get; set; }
        public string Group { get; set; }
        public ICollection<InputWarehouseProductionOrderCreateViewModel> MappedWarehousesProductionOrders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Area))
                yield return new ValidationResult("Area harus diisi", new List<string> { "Area" });

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

            int Count = 0;
            string DetailErrors = "[";

            if (MappedWarehousesProductionOrders.Count == 0)
            {
                yield return new ValidationResult("SPP harus Diisi", new List<string> { "MappedWarehousesProductionOrder" });
            }
            else
            {
                var Items = MappedWarehousesProductionOrders.GroupBy(s => s.ProductionOrder.Id);
                foreach (var item in Items)
                {
                    DetailErrors += "{";
                    DetailErrors += "WarehouseList : [ ";
                    foreach (var detail in item)
                    {
                        DetailErrors += "{";
                        //if (detail.PreviousOutputPackagingQty != detail.InputPackagingQty)
                        //{
                        //    Count++;
                        //    DetailErrors += $"InputPackagingQty: 'Qty Packing harus berjumlah {detail.PreviousOutputPackagingQty}!',";
                        //}

                        DetailErrors += "}, ";
                    }
                    DetailErrors += "], ";
                    DetailErrors += "}, ";
                }
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "MappedWarehousesProductionOrders" });
        }
    }
}
