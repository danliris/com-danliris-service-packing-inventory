using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LetterOfCredit
{
    public class GarmentLetterOfCreditViewModel : BaseViewModel, IValidatableObject
    {
        public string DocumentCreditNo { get;  set; }
        public DateTimeOffset? Date { get;  set; }
        public string IssuedBank { get;  set; }
        public Buyer Applicant { get;  set; }
        public DateTimeOffset? ExpireDate { get;  set; }
        public string ExpirePlace { get;  set; }
        public DateTimeOffset? LatestShipment { get;  set; }
        public string LCCondition { get;  set; }
        public double Quantity { get;  set; }
        public UnitOfMeasurement Uom { get;  set; }
        public double TotalAmount { get;  set; }
        public bool available { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(DocumentCreditNo))
            {
                yield return new ValidationResult("Document Credit No tidak boleh kosong", new List<string> { "DocumentCreditNo" });
            }
            else if (available)
            {
                yield return new ValidationResult("Document Credit No sudah ada", new List<string> { "DocumentCreditNo" });
            }

            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "Date" });
            }

            if (string.IsNullOrEmpty(IssuedBank))
            {
                yield return new ValidationResult("Issued Bank tidak boleh kosong", new List<string> { "IssuedBank" });
            }

            if (Applicant == null || Applicant.Id == 0)
            {
                yield return new ValidationResult("Applicant tidak boleh kosong", new List<string> { "Applicant" });
            }

            if (ExpireDate == null || ExpireDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Expire Date tidak boleh kosong", new List<string> { "ExpireDate" });
            }

            if (string.IsNullOrEmpty(ExpirePlace))
            {
                yield return new ValidationResult("Expire Place tidak boleh kosong", new List<string> { "ExpirePlace" });
            }

            if (LatestShipment == null || LatestShipment == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Latest Shipment tidak boleh kosong", new List<string> { "LatestShipment" });
            }

            if (string.IsNullOrEmpty(LCCondition))
            {
                yield return new ValidationResult("L/C Condition tidak boleh kosong", new List<string> { "LCCondition" });
            }

            if (Uom == null || Uom.Id == 0)
            {
                yield return new ValidationResult("Satuan tidak boleh kosong", new List<string> { "Uom" });
            }

            if (Quantity < 0)
            {
                yield return new ValidationResult("Quantity tidak boleh kosong", new List<string> { "Quantity" });
            }

            if (TotalAmount <= 0)
            {
                yield return new ValidationResult("Total Amount tidak boleh kosong", new List<string> { "TotalAmount" });
            }
        }
    }
}
