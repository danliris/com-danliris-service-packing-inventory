using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.AvalTransformation
{
    public class InputAvalTransformationViewModel : BaseViewModel, IValidatableObject
    {
        public InputAvalTransformationViewModel()
        {
            AvalTransformationProductionOrders = new HashSet<InputAvalTransformationProductionOrderViewModel>();
        }

        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string Area { get; set; }
        public string AvalType { get; set; }
        public string BonNo { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalWeight { get; set; }
        public bool IsTransformedAval { get; set; }

        public ICollection<InputAvalTransformationProductionOrderViewModel> AvalTransformationProductionOrders { get; set; }

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

            if (string.IsNullOrEmpty(AvalType))
                yield return new ValidationResult("Macam Barang harus diisi", new List<string> { "AvalType" });

            if ((Id == 0 && AvalTransformationProductionOrders.Where(s => s.IsSave).Count() == 0) || (Id != 0 && AvalTransformationProductionOrders.Count() == 0))
            {
                yield return new ValidationResult("SPP harus Diisi", new List<string> { "AvalTransformationProductionOrder" });
            }

        }
    }
}
