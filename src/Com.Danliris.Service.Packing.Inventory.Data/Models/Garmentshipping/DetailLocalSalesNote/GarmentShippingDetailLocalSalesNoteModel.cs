using Com.Moonlay.Models;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteModel : StandardEntity
    {
        public string SalesContractNo { get; private set; }
        public int LocalSalesContractId { get; set; }
        public int LocalSalesNoteId { get; set; }
        public string NoteNo { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeName { get; set; }
        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }     
        public double Amount { get; set; }   

        public ICollection<GarmentShippingDetailLocalSalesNoteItemModel> Items { get; private set; }

        public GarmentShippingDetailLocalSalesNoteModel()
        {
        }

        public GarmentShippingDetailLocalSalesNoteModel(string salesContractNo, int localSalesContractId, int localSalesNoteId, string noteNo, DateTimeOffset date, int transactionTypeId, string transactionTypeCode, string transactionTypeName, int buyerId, string buyerCode, string buyerName, double amount, ICollection<GarmentShippingDetailLocalSalesNoteItemModel> items)
        {
            SalesContractNo = salesContractNo;
            LocalSalesContractId = localSalesContractId;
            LocalSalesNoteId = localSalesNoteId;
            NoteNo = noteNo;
            Date = date;
            TransactionTypeId = transactionTypeId;
            TransactionTypeCode = transactionTypeCode;
            TransactionTypeName = transactionTypeName;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            Amount = amount;
            Items = items;
        }
        public void SetLocalSalesContractId(int localSalesContractId, string userName, string userAgent)
        {
            if (LocalSalesContractId != localSalesContractId)
            {
                LocalSalesContractId = localSalesContractId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSalesContractNo(string salesContractNo, string userName, string userAgent)
        {
            if (SalesContractNo != salesContractNo)
            {
                SalesContractNo = salesContractNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetLocalSalesNoteId(int localSalesNoteId, string userName, string userAgent)
        {
            if (LocalSalesNoteId != localSalesNoteId)
            {
                LocalSalesNoteId = localSalesNoteId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNoteNo(string noteNo, string userName, string userAgent)
        {
            if (NoteNo != noteNo)
            {
                NoteNo = noteNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerId(int buyerId, string userName, string userAgent)
        {
            if (BuyerId != buyerId)
            {
                BuyerId = buyerId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerCode(string buyerCode, string userName, string userAgent)
        {
            if (BuyerCode != buyerCode)
            {
                BuyerCode = buyerCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerName(string buyerName, string userName, string userAgent)
        {
            if (BuyerName != buyerName)
            {
                BuyerName = buyerName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTransactionTypeId(int localSalesContractId, string userName, string userAgent)
        {
            if (TransactionTypeId != localSalesContractId)
            {
                TransactionTypeId = localSalesContractId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTransactionTypeCode(string transactionTypeCode, string userName, string userAgent)
        {
            if (TransactionTypeCode != transactionTypeCode)
            {
                TransactionTypeCode = transactionTypeCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTransactionTypeName(string transactionTypeName, string userName, string userAgent)
        {
            if (TransactionTypeName != transactionTypeName)
            {
                TransactionTypeName = transactionTypeName;
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

    }
}
