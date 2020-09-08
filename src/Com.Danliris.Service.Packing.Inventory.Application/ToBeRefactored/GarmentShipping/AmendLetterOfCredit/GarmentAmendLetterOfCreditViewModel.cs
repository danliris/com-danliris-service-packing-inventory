using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.AmendLetterOfCredit
{
    public class GarmentAmendLetterOfCreditViewModel : BaseViewModel, IValidatableObject
    {
        public string documentCreditNo { get; set; }
        public int letterOfCreditId { get; set; }
        public int amendNumber { get; set; }
        public DateTimeOffset date { get; set; }
        public string description { get; set; }
        public double amount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(documentCreditNo))
            {
                yield return new ValidationResult("Document Credit No tidak boleh kosong", new List<string> { "documentCreditNo" });
            }

            //if (amount <= 0)
            //{
            //    yield return new ValidationResult("Amount tidak boleh kosong", new List<string> { "amount" });
            //}

            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (string.IsNullOrEmpty(description))
            {
                yield return new ValidationResult("Description tidak boleh kosong", new List<string> { "description" });
            }
        }
    }
}
