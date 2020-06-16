using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice
{
    public class GarmentShippingCreditAdviceModel : StandardEntity
    {

        public int PackingListId { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Amount { get; set; }
        public double AmountToBePaid { get; set; }

        public bool Valas { get; set; }
        public string LCType { get; set; }
        public double Inkaso { get; set; }
        public double Disconto { get; set; }
        public string SRNo { get; set; }
        public DateTimeOffset NegoDate { get; set; }
        public string Condition { get; set; }
        public double BankComission { get; set; }
        public double DiscrepancyFee { get; set; }
        public double NettNego { get; set; }

        public DateTimeOffset BTBCADate { get; set; }
        public double BTBAmount { get; set; }
        public double BTBRatio { get; set; }
        public double BTBRate { get; set; }
        public double BTBTransfer { get; set; }
        public double BTBMaterial { get; set; }

        public double BillDays { get; set; }
        public double BillAmount { get; set; }
        public string BillCA { get; set; }

        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }

        public int BankAccountId { get; set; }
        public string BankAccountName { get; set; }
        public string BankAddress { get; set; }

        public double CreditInterest { get; set; }
        public double BankCharges { get; set; }
        public DateTimeOffset DocumentPresente { get; set; }

        public GarmentShippingCreditAdviceModel()
        {
        }

        public GarmentShippingCreditAdviceModel(int packingListId, int invoiceId, string invoiceNo, DateTimeOffset date, double amount, double amountToBePaid, bool valas, string lCType, double inkaso, double disconto, string sRNo, DateTimeOffset negoDate, string condition, double bankComission, double discrepancyFee, double nettNego, DateTimeOffset bTBCADate, double bTBAmount, double bTBRatio, double bTBRate, double bTBTransfer, double bTBMaterial, double billDays, double billAmount, string billCA, int buyerId, string buyerName, string buyerAddress, int bankAccountId, string bankAccountName, string bankAddress, double creditInterest, double bankCharges, DateTimeOffset documentPresente)
        {
            PackingListId = packingListId;
            InvoiceId = invoiceId;
            InvoiceNo = invoiceNo;
            Date = date;
            Amount = amount;
            AmountToBePaid = amountToBePaid;
            Valas = valas;
            LCType = lCType;
            Inkaso = inkaso;
            Disconto = disconto;
            SRNo = sRNo;
            NegoDate = negoDate;
            Condition = condition;
            BankComission = bankComission;
            DiscrepancyFee = discrepancyFee;
            NettNego = nettNego;
            BTBCADate = bTBCADate;
            BTBAmount = bTBAmount;
            BTBRatio = bTBRatio;
            BTBRate = bTBRate;
            BTBTransfer = bTBTransfer;
            BTBMaterial = bTBMaterial;
            BillDays = billDays;
            BillAmount = billAmount;
            BillCA = billCA;
            BuyerId = buyerId;
            BuyerName = buyerName;
            BuyerAddress = buyerAddress;
            BankAccountId = bankAccountId;
            BankAccountName = bankAccountName;
            BankAddress = bankAddress;
            CreditInterest = creditInterest;
            BankCharges = bankCharges;
            DocumentPresente = documentPresente;
        }

        public void SetValas(bool valas, string userName, string userAgent)
        {
            if (Valas != valas)
            {
                Valas = valas;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetLCType(string lCType, string userName, string userAgent)
        {
            if (LCType != lCType)
            {
                LCType = lCType;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetInkaso(double inkaso, string userName, string userAgent)
        {
            if (Inkaso != inkaso)
            {
                Inkaso = inkaso;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetDisconto(double disconto, string userName, string userAgent)
        {
            if (Disconto != disconto)
            {
                Disconto = disconto;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetSRNo(string sRNo, string userName, string userAgent)
        {
            if (SRNo != sRNo)
            {
                SRNo = sRNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetNegoDate(DateTimeOffset negoDate, string userName, string userAgent)
        {
            if (NegoDate != negoDate)
            {
                NegoDate = negoDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetCondition(string condition, string userName, string userAgent)
        {
            if (Condition != condition)
            {
                Condition = condition;
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
        public void SetDiscrepancyFee(double discrepancyFee, string userName, string userAgent)
        {
            if (DiscrepancyFee != discrepancyFee)
            {
                DiscrepancyFee = discrepancyFee;
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
        public void SetBTBCADate(DateTimeOffset bTBCADate, string userName, string userAgent)
        {
            if (BTBCADate != bTBCADate)
            {
                BTBCADate = bTBCADate;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBTBAmount(double bTBAmount, string userName, string userAgent)
        {
            if (BTBAmount != bTBAmount)
            {
                BTBAmount = bTBAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBTBRatio(double bTBRatio, string userName, string userAgent)
        {
            if (BTBRatio != bTBRatio)
            {
                BTBRatio = bTBRatio;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBTBRate(double bTBRate, string userName, string userAgent)
        {
            if (BTBRate != bTBRate)
            {
                BTBRate = bTBRate;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBTBTransfer(double bTBTransfer, string userName, string userAgent)
        {
            if (BTBTransfer != bTBTransfer)
            {
                BTBTransfer = bTBTransfer;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBTBMaterial(double bTBMaterial, string userName, string userAgent)
        {
            if (BTBMaterial != bTBMaterial)
            {
                BTBMaterial = bTBMaterial;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBillDays(double billDays, string userName, string userAgent)
        {
            if (BillDays != billDays)
            {
                BillDays = billDays;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBillAmount(double billAmount, string userName, string userAgent)
        {
            if (BillAmount != billAmount)
            {
                BillAmount = billAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBillCA(string billCA, string userName, string userAgent)
        {
            if (BillCA != billCA)
            {
                BillCA = billCA;
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
        public void SetDocumentPresente(DateTimeOffset documentPresente, string userName, string userAgent)
        {
            if (DocumentPresente != documentPresente)
            {
                DocumentPresente = documentPresente;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
