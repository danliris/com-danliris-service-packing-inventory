using Com.Moonlay.Models;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote
{
    public class GarmentShippingNoteModel : StandardEntity
    {
        public GarmentShippingNoteTypeEnum? NoteType { get; private set; }
        public string NoteNo { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string Description { get; private set; }
        public string ReceiptNo { get; private set; }
        public DateTimeOffset ReceiptDate { get; private set; }

        public int BankId { get; private set; }
        public string BankName { get; private set; }
        public string BankCurrencyCode { get; private set; }
        public string BankAccountNo { get; private set; }

        public double TotalAmount { get; private set; }

        public ICollection<GarmentShippingNoteItemModel> Items { get; private set; }

        public GarmentShippingNoteModel()
        {
        }

        public GarmentShippingNoteModel(GarmentShippingNoteTypeEnum noteType, string noteNo, DateTimeOffset date, int buyerId, string buyerCode, string buyerName, string description, string receiptNo, DateTimeOffset receiptDate, int bankId, string bankName, string bankCurrencyCode, string bankAccountNo, double totalAmount, ICollection<GarmentShippingNoteItemModel> items)
        {
            NoteType = noteType;
            NoteNo = noteNo;
            Date = date;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            Description = description;
            ReceiptNo = receiptNo;
            ReceiptDate = receiptDate;
            BankId = bankId;
            BankName = bankName;
            BankCurrencyCode = bankCurrencyCode;
            BankAccountNo = bankAccountNo;
            TotalAmount = totalAmount;
            Items = items;
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

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description;
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

        public void SetReceiptDate(DateTimeOffset receiptDate, string userName, string userAgent)
        {
            if (ReceiptDate != receiptDate)
            {
                ReceiptDate = receiptDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTotalAmount(double totalAmount, string userName, string userAgent)
        {
            if (TotalAmount != totalAmount)
            {
                TotalAmount = totalAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBankId(int bankId, string userName, string userAgent)
        {
            if (BankId != bankId)
            {
                BankId = bankId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBankName(string bankName, string userName, string userAgent)
        {
            if (BankName != bankName)
            {
                BankName = bankName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBankCurrencyCode(string bankCurrencyCode, string userName, string userAgent)
        {
            if (BankCurrencyCode != bankCurrencyCode)
            {
                BankCurrencyCode = bankCurrencyCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBankAccountNo(string bankAccountNo, string userName, string userAgent)
        {
            if (BankAccountNo != bankAccountNo)
            {
                BankAccountNo = bankAccountNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
