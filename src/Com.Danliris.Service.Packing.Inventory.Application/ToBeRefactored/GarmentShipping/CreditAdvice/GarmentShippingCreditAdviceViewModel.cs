using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice
{
    public class GarmentShippingCreditAdviceViewModel : BaseViewModel, IValidatableObject
    {
        public int packingListId { get; set; }
        public int invoiceId { get; set; }
        public string invoiceNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public double amount { get; set; }
        public double amountToBePaid { get; set; }
        public string paymentTerm { get; set; }

        public string lcNo { get; set; }
        public bool valas { get; set; }
        public string lcType { get; set; }
        public double inkaso { get; set; }
        public double disconto { get; set; }
        public string srNo { get; set; }
        public DateTimeOffset? negoDate { get; set; }
        public DateTimeOffset? paymentDate { get; set; }
        public string condition { get; set; }
        public double bankComission { get; set; }
        public double discrepancyFee { get; set; }
        public double nettNego { get; set; }

        public DateTimeOffset? btbCADate { get; set; }
        public double btbAmount { get; set; }
        public double btbRatio { get; set; }
        public double btbRate { get; set; }
        public double btbTransfer { get; set; }
        public double btbMaterial { get; set; }

        public double billDays { get; set; }
        public double billAmount { get; set; }
        public string billCA { get; set; }

        public Buyer buyer { get; set; }

        public BankAccount bank { get; set; }

        public double creditInterest { get; set; }
        public double bankCharges { get; set; }
        public double otherCharge { get; set; }
        public DateTimeOffset? documentPresente { get; set; }

        public string cargoPolicyNo { get; set; }
        public DateTimeOffset? cargoPolicyDate { get; set; }
        public double cargoPolicyValue { get; set; }
        public string accountsReceivablePolicyNo { get; set; }
        public DateTimeOffset? accountsReceivablePolicyDate { get; set; }
        public double accountsReceivablePolicyValue { get; set; }
        public DateTimeOffset? documentSendDate { get; set; }

        public string remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(invoiceNo) || packingListId == 0 || invoiceId == 0)
            {
                yield return new ValidationResult("Invoice No tidak boleh kosong", new List<string> { "invoiceNo" });
            }

            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (string.IsNullOrEmpty(paymentTerm))
            {
                yield return new ValidationResult("Payment Term tidak boleh kosong", new List<string> { "paymentTerm" });
            }
            else if (paymentTerm.ToUpper() == "LC")
            {
                if (string.IsNullOrEmpty(lcNo))
                {
                    yield return new ValidationResult("No LC tidak boleh kosong", new List<string> { "lcNo" });
                }

                if (string.IsNullOrEmpty(lcType))
                {
                    yield return new ValidationResult("Jenis L/C tidak boleh kosong", new List<string> { "lcType" });
                }

                if (string.IsNullOrEmpty(srNo))
                {
                    yield return new ValidationResult("SR. No tidak boleh kosong", new List<string> { "srNo" });
                }

                if (negoDate == null || negoDate == DateTimeOffset.MinValue)
                {
                    yield return new ValidationResult("Tanggal SR tidak boleh kosong", new List<string> { "negoDate" });
                }

                if (paymentDate == null || paymentDate == DateTimeOffset.MinValue)
                {
                    yield return new ValidationResult("Tanggal Pembayaran tidak boleh kosong", new List<string> { "paymentDate" });
                }

                if (string.IsNullOrEmpty(condition))
                {
                    yield return new ValidationResult("Condition tidak boleh kosong", new List<string> { "condition" });
                }

                if (bankComission <= 0)
                {
                    yield return new ValidationResult("Bank Comission harus lebih dari 0", new List<string> { "bankComission" });
                }

                if (discrepancyFee <= 0)
                {
                    yield return new ValidationResult("Discrepancy Fee / Postage harus lebih dari 0", new List<string> { "discrepancyFee" });
                }

                if (btbCADate == null || btbCADate == DateTimeOffset.MinValue)
                {
                    yield return new ValidationResult("Tanggal CA tidak boleh kosong", new List<string> { "btbCADate" });
                }

                if (btbAmount <= 0)
                {
                    yield return new ValidationResult("Amount (US$) harus lebih dari 0", new List<string> { "btbAmount" });
                }

                if (btbRatio <= 0)
                {
                    yield return new ValidationResult("Ratio harus lebih dari 0", new List<string> { "btbRatio" });
                }

                if (btbRate <= 0)
                {
                    yield return new ValidationResult("Rate harus lebih dari 0", new List<string> { "btbRate" });
                }

                if (btbTransfer <= 0)
                {
                    yield return new ValidationResult("Transfer harus lebih dari 0", new List<string> { "btbTransfer" });
                }

                if (btbMaterial <= 0)
                {
                    yield return new ValidationResult("Material harus lebih dari 0", new List<string> { "btbMaterial" });
                }

                if (billDays <= 0)
                {
                    yield return new ValidationResult("Jumlah Hari harus lebih dari 0", new List<string> { "billDays" });
                }

                if (billAmount <= 0)
                {
                    yield return new ValidationResult("Amount harus lebih dari 0", new List<string> { "billAmount" });
                }

                if (string.IsNullOrEmpty(billCA))
                {
                    yield return new ValidationResult("CA tidak boleh kosong", new List<string> { "billCA" });
                }

                if (creditInterest <= 0)
                {
                    yield return new ValidationResult("Credit Interest tidak boleh kosong", new List<string> { "creditInterest" });
                }

                if (documentPresente == null || documentPresente == DateTimeOffset.MinValue)
                {
                    yield return new ValidationResult("Document Presente tidak boleh kosong", new List<string> { "documentPresente" });
                }
            }
            else if (paymentTerm.ToUpper() == "TT/OA")
            {
                if (otherCharge < 0)
                {
                    yield return new ValidationResult("Other Charge tidak boleh < 0", new List<string> { "otherCharge" });
                }
            }

            if (nettNego <= 0)
            {
                yield return new ValidationResult("Nett Nego harus lebih dari 0", new List<string> { "nettNego" });
            }

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }

            if (bank == null || bank.id == 0)
            {
                yield return new ValidationResult("Bank tidak boleh kosong", new List<string> { "bank" });
            }

            //if (bankCharges < 0)
            //{
            //    yield return new ValidationResult("Bank Charges tidak boleh kosong", new List<string> { "bankCharges" });
            //}
        }
    }
}
