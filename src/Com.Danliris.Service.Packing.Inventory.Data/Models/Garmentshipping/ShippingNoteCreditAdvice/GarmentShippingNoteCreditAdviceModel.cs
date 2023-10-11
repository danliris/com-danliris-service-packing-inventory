using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceModel : StandardEntity
    {

        public int ShippingNoteId { get; private set; }
        public string NoteType { get; private set; }
        public string NoteNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string ReceiptNo { get; private set; }
        public string PaymentTerm { get; private set; }

        public double Amount { get; private set; }
        public double PaidAmount { get; private set; }
        public double BalanceAmount { get; private set; }
     
        public DateTimeOffset PaymentDate { get; private set; }
        public double NettNego { get; private set; }

        public double BankComission { get; private set; }
        public double CreditInterest { get; private set; }
        public double BankCharges { get; private set; }
        public double InsuranceCharge { get; private set; }
      
        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerAddress { get; private set; }

        public int BankAccountId { get; private set; }
        public string BankAccountName { get; private set; }
        public string BankAccountNo { get; private set; }
        public string BankAddress { get; private set; }

        public string Remark { get; private set; }
        public DateTimeOffset DocumentSendDate { get; private set; }

        public GarmentShippingNoteCreditAdviceModel()
        {
        }

        public GarmentShippingNoteCreditAdviceModel(int shippingNoteId, string noteType, string noteNo, DateTimeOffset date, string receiptNo, string paymentTerm, double amount, double paidAmount, double balanceAmount, DateTimeOffset paymentDate, double nettNego, int buyerId, string buyerCode, string buyerName, string buyerAddress, int bankAccountId, string bankAccountName, string bankAccountNo, string bankAddress, double bankComission, double creditInterest, double bankCharges, double insuranceCharge, DateTimeOffset documentSendDate, string remark)
        {
            ShippingNoteId = shippingNoteId;
            NoteType = noteType;
            NoteNo = noteNo;
            Date = date;
            ReceiptNo = receiptNo;
            PaymentTerm = paymentTerm;

            Amount = amount;
            PaidAmount = paidAmount;
            BalanceAmount = balanceAmount;
          
            PaymentDate = paymentDate;
            NettNego = nettNego;

            BankComission = bankComission;
            CreditInterest = creditInterest;
            BankCharges = bankCharges;
            InsuranceCharge = insuranceCharge;

            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            BuyerAddress = buyerAddress;

            BankAccountId = bankAccountId;
            BankAccountName = bankAccountName;
            BankAccountNo = bankAccountNo;
            BankAddress = bankAddress;

            DocumentSendDate = documentSendDate;
            Remark = remark;
        }

        public void SetShippingNoteId(int shippingNoteId, string userName, string userAgent)
        {
            if (ShippingNoteId != shippingNoteId)
            {
                ShippingNoteId = shippingNoteId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetReceiptNo(string receiptNo, string userName, string userAgent)
        {
            if (ReceiptNo != receiptNo)
            {
                ReceiptNo = receiptNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaymentTerm(string paymentTerm, string userName, string userAgent)
        {
            if (PaymentTerm != paymentTerm)
            {
                PaymentTerm = paymentTerm;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaymentDate(DateTimeOffset paymentDate, string userName, string userAgent)
        {
            if (PaymentDate != paymentDate)
            {
                PaymentDate = paymentDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBankComission(double bankComission, string userName, string userAgent)
        {
            if (BankComission != bankComission)
            {
                BankComission = bankComission;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCreditInterest(double creditInterest, string userName, string userAgent)
        {
            if (CreditInterest != creditInterest)
            {
                CreditInterest = creditInterest;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBankCharges(double bankCharges, string userName, string userAgent)
        {
            if (BankCharges != bankCharges)
            {
                BankCharges = bankCharges;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetInsuranceCharge(double insuranceCharge, string userName, string userAgent)
        {
            if (InsuranceCharge != insuranceCharge)
            {
                InsuranceCharge = insuranceCharge;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDocumentSendDate(DateTimeOffset documentSendDate, string userName, string userAgent)
        {
            if (DocumentSendDate != documentSendDate)
            {
                DocumentSendDate = documentSendDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRemark(string remark, string userName, string userAgent)
        {
            if (Remark != remark)
            {
                Remark = remark;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAmount(double amount, string userName, string userAgent)
        {
            if (Amount != amount)
            {
                Amount = amount;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNettNego(double nettNego, string userName, string userAgent)
        {
            if (NettNego != nettNego)
            {
                NettNego = nettNego;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaidAmount(double paidAmount, string userName, string userAgent)
        {
            if (PaidAmount != paidAmount)
            {
                PaidAmount = paidAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBalanceAmount(double balanceAmount, string userName, string userAgent)
        {
            if (BalanceAmount != balanceAmount)
            {
                BalanceAmount = balanceAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
