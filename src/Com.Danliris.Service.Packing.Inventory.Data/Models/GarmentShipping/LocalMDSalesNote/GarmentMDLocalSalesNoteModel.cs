using Com.Moonlay.Models;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalMDSalesNote
{
    public class GarmentMDLocalSalesNoteModel : StandardEntity
    {
        public string SalesContractNo { get; private set; }
        public int LocalSalesContractId { get; set; }
        public string NoteNo { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeName { get; set; }

        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerNPWP { get; private set; }
        public string KaberType { get; private set; }
        public string ExpenditureNo { get; private set; }
        public string PaymentType { get; private set; }
        public int Tempo { get; set; }
        public string DispositionNo { get; set; }
        public bool UseVat { get; set; }
        public int VatId { get; private set; }
        public int VatRate { get; private set; }
        public string Remark { get; set; }
        public bool IsUsed { get; set; }
        public bool IsCL { get; set; }
        public bool IsDetail { get; set; }
        public bool IsApproveShipping { get; set; }
        public bool IsApproveFinance { get; set; }
        public string ApproveShippingBy { get; set; }
        public string ApproveFinanceBy { get; set; }
        public DateTimeOffset ApproveShippingDate { get; set; }
        public DateTimeOffset ApproveFinanceDate { get; set; }
        public bool IsRejectedFinance{ get; set; }
        public bool IsRejectedShipping { get; set; }
        public string RejectedReason { get; set; }
        public int BankId { get; private set; }
        public string BankName { get; private set; }
        public string AccountNumber { get; private set; }
        public bool IsSubconPackingList { get; set; }
        public string BCNo { get; set; }
        public string BCType { get; set; }
        public DateTimeOffset? BCDate { get; set; }
        public ICollection<GarmentMDLocalSalesNoteItemModel> Items { get; private set; }

        public GarmentMDLocalSalesNoteModel()
        {
        }

        public GarmentMDLocalSalesNoteModel(string salesContractNo, int localSalesContractId, string paymentType, string noteNo, DateTimeOffset date, int transactionTypeId, string transactionTypeCode, string transactionTypeName, int buyerId, string buyerCode, string buyerName, string buyerNPWP, string kaberType, int tempo, string expenditureNo, string dispositionNo, bool useVat, int vatId, int vatRate, string remark, bool isUsed, bool isCL, bool isDetail, bool isApproveShipping, bool isApproveFinance, string approveShippingBy, string approveFinanceBy, DateTimeOffset approveShippingDate, DateTimeOffset approveFinanceDate, bool isRejectedShipping, bool isRejectedFinance, string rejectedReason, int bankId, string bankName, string accountNumber,string bcNo,string bcType,DateTimeOffset? bcDate, ICollection<GarmentMDLocalSalesNoteItemModel> items)
        {
            SalesContractNo = salesContractNo;
            LocalSalesContractId = localSalesContractId;
            NoteNo = noteNo;
            Date = date;
            TransactionTypeId = transactionTypeId;
            TransactionTypeCode = transactionTypeCode;
            TransactionTypeName = transactionTypeName;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            BuyerNPWP = buyerNPWP;
            KaberType = kaberType;
            PaymentType = paymentType;
            Tempo = tempo;
            ExpenditureNo = expenditureNo;
            DispositionNo = dispositionNo;
            UseVat = useVat;
            VatId = vatId;
            VatRate = vatRate;
            Remark = remark;
            IsUsed = isUsed;
            IsCL = isCL;
            IsDetail = isDetail; 
            IsApproveShipping = isApproveShipping;
            IsApproveFinance = isApproveFinance;
            ApproveShippingBy = approveShippingBy;
            ApproveFinanceBy = approveFinanceBy;
            ApproveShippingDate = approveShippingDate;
            ApproveFinanceDate = approveFinanceDate;
            IsRejectedFinance = isRejectedFinance;
            IsRejectedShipping = isRejectedShipping;
            RejectedReason = rejectedReason;
            BankId = bankId;
            BankName = bankName;
            AccountNumber = accountNumber;
            BCNo = bcNo;
            BCType = bcType;
            BCDate = bcDate;
            Items = items;
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTempo(int tempo, string userName, string userAgent)
        {
            if (Tempo != tempo)
            {
                Tempo = tempo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaymentType(string paymentType, string userName, string userAgent)
        {
            if (PaymentType != paymentType)
            {
                PaymentType = paymentType;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetExpenditureNo(string expenditureNo, string userName, string userAgent)
        {
            if (ExpenditureNo != expenditureNo)
            {
                ExpenditureNo = expenditureNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDispositionNo(string dispositionNo, string userName, string userAgent)
        {
            if (DispositionNo != dispositionNo)
            {
                DispositionNo = dispositionNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUseVat(bool useVat, string userName, string userAgent)
        {
            if (UseVat != useVat)
            {
                UseVat = useVat;
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

        public void SetIsUsed(bool isUsed, string userName, string userAgent)
        {
            if (IsUsed != isUsed)
            {
                IsUsed = isUsed;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetIsCL(bool isCL, string userName, string userAgent)
        {
            if (IsCL != isCL)
            {
                IsCL = isCL;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsDetail(bool isDetail, string userName, string userAgent)
        {
            if (IsDetail != isDetail)
            {
                IsDetail = isDetail;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsApproveShipping(bool isApproveShipping, string userName, string userAgent)
        {
            if (IsApproveShipping != isApproveShipping)
            {
                IsApproveShipping = isApproveShipping;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsApproveFinance(bool isApproveFinance, string userName, string userAgent)
        {
            if (IsApproveFinance != isApproveFinance)
            {
                IsApproveFinance = isApproveFinance;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetApproveFinanceBy(string approveFinanceBy, string userName, string userAgent)
        {
            if (ApproveFinanceBy != approveFinanceBy)
            {
                ApproveFinanceBy = approveFinanceBy;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetApproveShippingBy(string approveShippingBy, string userName, string userAgent)
        {
            if (ApproveShippingBy != approveShippingBy)
            {
                ApproveShippingBy = approveShippingBy;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetApproveFinanceDate(DateTimeOffset approveFinanceDate, string userName, string userAgent)
        {
            if (ApproveFinanceDate != approveFinanceDate)
            {
                ApproveFinanceDate = approveFinanceDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetApproveShippingDate(DateTimeOffset approveShippingDate, string userName, string userAgent)
        {
            if (ApproveShippingDate != approveShippingDate)
            {
                ApproveShippingDate = approveShippingDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsRejectedFinance(bool isRejectedFinance, string userName, string userAgent)
        {
            if (IsRejectedFinance != isRejectedFinance)
            {
                IsRejectedFinance = isRejectedFinance;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsRejectedShipping(bool isRejectedShipping, string userName, string userAgent)
        {
            if (IsRejectedShipping != isRejectedShipping)
            {
                IsRejectedShipping = isRejectedShipping;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRejectedReason(string rejectedReason, string userName, string userAgent)
        {
            if (RejectedReason != rejectedReason)
            {
                RejectedReason = rejectedReason;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsSubconPackingList (bool isSubconPackingList, string userName, string userAgent)
        {
            if (IsSubconPackingList != isSubconPackingList)
            {
                IsSubconPackingList = isSubconPackingList;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBCNo(string bcNo, string userName, string userAgent)
        {
            if (BCNo != bcNo)
            {
                BCNo = bcNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBCType(string bcType, string userName, string userAgent)
        {
            if (BCType != bcType)
            {
                BCType = bcType;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBCDate(DateTimeOffset? bcDate, string userName, string userAgent)
        {
            if (BCDate != bcDate)
            {
                BCDate = bcDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
