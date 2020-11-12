using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapViewModel : BaseViewModel, IValidatableObject
    {
        public string recapNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public EMKL emkl { get; set; }
        public IEnumerable<PaymentDispositionRecapItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (emkl == null || emkl.Id == 0)
            {
                yield return new ValidationResult("EMKL tidak boleh kosong", new List<string> { "emkl" });
            }

            if (items == null || items.Count() < 1)
            {
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "itemsCount" });
            }
        }
    }
}
