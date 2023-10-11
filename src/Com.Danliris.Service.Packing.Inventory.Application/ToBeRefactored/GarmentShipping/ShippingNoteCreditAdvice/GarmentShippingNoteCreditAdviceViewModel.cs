using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceViewModel : BaseViewModel, IValidatableObject
    {
        public int shippingnoteId { get; set; }
        public string noteType { get; set; }
        public string noteNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public string receiptNo { get; set; }
        public string paymentTerm { get; set; }

        public double amount { get; set; }
        public double paidAmount { get; set; }
        public double balanceamount { get; set; }

        public DateTimeOffset? paymentDate { get; set; }
        public double nettNego { get; set; }

        public double bankComission { get; set; }
        public double creditInterest { get; set; }
        public double bankCharges { get; set; }
        public double insuranceCharge { get; set; }
      
        public Buyer buyer { get; set; }

        public BankAccount bank { get; set; }

   
        public string remark { get; set; }
        public DateTimeOffset? documentSendDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(noteNo) || shippingnoteId == 0)
            {
                yield return new ValidationResult("NoTa Debit / Note Kredit tidak boleh kosong", new List<string> { "noteNo" });
            }

            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (paidAmount <= 0)
            {
                yield return new ValidationResult("Paid Amount tidak boleh <= 0", new List<string> { "paidAmount" });
            }

            if (string.IsNullOrEmpty(paymentTerm))
            {
                yield return new ValidationResult("Payment Term tidak boleh kosong", new List<string> { "paymentTerm" });
            }

            if (string.IsNullOrEmpty(noteType))
            {
                yield return new ValidationResult("Note Type tidak boleh kosong", new List<string> { "noteType" });
            }
            //if (bankComission < 0)
            //{
            //    yield return new ValidationResult("Bank Comission tidak boleh < 0", new List<string> { "bankComission" });
            //}

            //if (creditInterest < 0)
            //{
            //    yield return new ValidationResult("Credit Interest tidak boleh < 0", new List<string> { "creditInterest" });
            //}

            //if (bankCharges < 0)
            //{
            //    yield return new ValidationResult("Bank Charges tidak boleh < 0", new List<string> { "bankCharges" });
            //}

            //if (insuranceCharge < 0)
            //{
            //    yield return new ValidationResult("Insurance Charges tidak boleh < 0", new List<string> { "insuranceCharge" });
            //}

            if (nettNego <= 0)
            {
                yield return new ValidationResult("Nett Nego harus lebih dari 0", new List<string> { "nettNego" });
            }

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }

            if (noteType == "NOTA DEBIT")
            {
                if (bank == null || bank.id == 0)
                {
                    yield return new ValidationResult("Bank tidak boleh kosong", new List<string> { "bank" });
                }
            }
        }
    }
}