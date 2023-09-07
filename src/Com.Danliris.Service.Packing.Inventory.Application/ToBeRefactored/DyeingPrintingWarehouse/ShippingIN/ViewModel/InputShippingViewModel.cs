using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN.ViewModel
{
    public class InputShippingViewModel : BaseViewModel, IValidatableObject
    {
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string ShippingType { get; set; }
        public string Shift { get; set; }
        public ICollection<InputShippingItemViewModel> ShippingProductionOrders { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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
        }
    }
}
