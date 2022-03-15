using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice
{
    public class GarmentShippingCreditAdviceModel : StandardEntity
    {

        public int PackingListId { get; private set; }
        public int InvoiceId { get; private set; }
        public string InvoiceNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public double Amount { get; private set; }
        public double AmountToBePaid { get; private set; }
        public double AmountPaid { get; private set; }
        public double BalanceAmount { get; private set; }
        public string PaymentTerm { get; private set; }
        public string ReceiptNo { get; private set; }

        public string LCNo { get; private set; }
        public bool Valas { get; private set; }
        public string LCType { get; private set; }
        public double Inkaso { get; private set; }
        public double Disconto { get; private set; }
        public string SRNo { get; private set; }
        public DateTimeOffset NegoDate { get; private set; }
        public DateTimeOffset PaymentDate { get; private set; }
        public string Condition { get; private set; }
        public double BankComission { get; private set; }
        public double DiscrepancyFee { get; private set; }
        public double NettNego { get; private set; }

        public DateTimeOffset BTBCADate { get; private set; }
        public double BTBAmount { get; private set; }
        public double BTBRatio { get; private set; }
        public double BTBRate { get; private set; }
        public double BTBTransfer { get; private set; }
        public double BTBMaterial { get; private set; }

        public double BillDays { get; private set; }
        public double BillAmount { get; private set; }
        public string BillCA { get; private set; }

        public int BuyerId { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerAddress { get; private set; }

        public int BankAccountId { get; private set; }
        public string BankAccountName { get; private set; }
        public string BankAddress { get; private set; }

        public double CreditInterest { get; private set; }
        public double BankCharges { get; private set; }
        public double OtherCharge { get; private set; }
        public DateTimeOffset DocumentPresente { get; private set; }

        public string CargoPolicyNo { get; private set; }
        public DateTimeOffset CargoPolicyDate { get; private set; }
        public double CargoPolicyValue { get; private set; }
        public string AccountsReceivablePolicyNo { get; private set; }
        public DateTimeOffset AccountsReceivablePolicyDate { get; private set; }
        public double AccountsReceivablePolicyValue { get; private set; }
        public DateTimeOffset DocumentSendDate { get; private set; }

        public string Remark { get; private set; }

        public GarmentShippingCreditAdviceModel()
        {
        }

        public GarmentShippingCreditAdviceModel(int packingListId, int invoiceId, string invoiceNo, DateTimeOffset date, double amount, double amountToBePaid, double amountPaid, double balanceAmount, string paymentTerm, string receiptNo, string lCNo, bool valas, string lCType, double inkaso, double disconto, string sRNo, DateTimeOffset negoDate, DateTimeOffset paymentDate, string condition, double bankComission, double discrepancyFee, double nettNego, DateTimeOffset bTBCADate, double bTBAmount, double bTBRatio, double bTBRate, double bTBTransfer, double bTBMaterial, double billDays, double billAmount, string billCA, int buyerId, string buyerName, string buyerAddress, int bankAccountId, string bankAccountName, string bankAddress, double creditInterest, double bankCharges, double otherCharge, DateTimeOffset documentPresente, string cargoPolicyNo, DateTimeOffset cargoPolicyDate, double cargoPolicyValue, string accountsReceivablePolicyNo, DateTimeOffset accountsReceivablePolicyDate, double accountsReceivablePolicyValue, DateTimeOffset documentSendDate, string remark)
        {
            PackingListId = packingListId;
            InvoiceId = invoiceId;
            InvoiceNo = invoiceNo;
            Date = date;
            Amount = amount;
            AmountToBePaid = amountToBePaid;
            AmountPaid = amountPaid;
            BalanceAmount = balanceAmount;
            PaymentTerm = paymentTerm;
            ReceiptNo = receiptNo;
            LCNo = lCNo;
            Valas = valas;
            LCType = lCType;
            Inkaso = inkaso;
            Disconto = disconto;
            SRNo = sRNo;
            NegoDate = negoDate;
            PaymentDate = paymentDate;
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
            OtherCharge = otherCharge;
            DocumentPresente = documentPresente;
            CargoPolicyNo = cargoPolicyNo;
            CargoPolicyDate = cargoPolicyDate;
            CargoPolicyValue = cargoPolicyValue;
            AccountsReceivablePolicyNo = accountsReceivablePolicyNo;
            AccountsReceivablePolicyDate = accountsReceivablePolicyDate;
            AccountsReceivablePolicyValue = accountsReceivablePolicyValue;
            DocumentSendDate = documentSendDate;
            Remark = remark;
        }

        public void SetInvoiceId(int invoiceId, string userName, string userAgent)
        {
            if (InvoiceId != invoiceId)
            {
                InvoiceId = invoiceId;
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
        public void SetPaymentDate(DateTimeOffset paymentDate, string userName, string userAgent)
        {
            if (PaymentDate != paymentDate)
            {
                PaymentDate = paymentDate;
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
        public void SetOtherCharge(double otherCharge, string userName, string userAgent)
        {
            if (OtherCharge != otherCharge)
            {
                OtherCharge = otherCharge;
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

        public void SetCargoPolicyNo(string cargoPolicyNo, string userName, string userAgent)
        {
            if (CargoPolicyNo != cargoPolicyNo)
            {
                CargoPolicyNo = cargoPolicyNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCargoPolicyDate(DateTimeOffset cargoPolicyDate, string userName, string userAgent)
        {
            if (CargoPolicyDate != cargoPolicyDate)
            {
                CargoPolicyDate = cargoPolicyDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCargoPolicyValue(double cargoPolicyValue, string userName, string userAgent)
        {
            if (CargoPolicyValue != cargoPolicyValue)
            {
                CargoPolicyValue = cargoPolicyValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAccountsReceivablePolicyNo(string accountsReceivablePolicyNo, string userName, string userAgent)
        {
            if (AccountsReceivablePolicyNo != accountsReceivablePolicyNo)
            {
                AccountsReceivablePolicyNo = accountsReceivablePolicyNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAccountsReceivablePolicyDate(DateTimeOffset accountsReceivablePolicyDate, string userName, string userAgent)
        {
            if (AccountsReceivablePolicyDate != accountsReceivablePolicyDate)
            {
                AccountsReceivablePolicyDate = accountsReceivablePolicyDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAccountsReceivablePolicyValue(double accountsReceivablePolicyValue, string userName, string userAgent)
        {
            if (AccountsReceivablePolicyValue != accountsReceivablePolicyValue)
            {
                AccountsReceivablePolicyValue = accountsReceivablePolicyValue;
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

        public void SetAmountPaid(double amountPaid, string userName, string userAgent)
        {
            if (AmountPaid != amountPaid)
            {
                AmountPaid = amountPaid;
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
