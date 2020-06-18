using Com.Moonlay.Models;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteModel : StandardEntity
    {
        public string NoteNo { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeName { get; set; }

        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerNPWP { get; private set; }

        public int Tempo { get; set; }
        public string DispositionNo { get; set; }
        public bool UseVat { get; set; }
        public string Remark { get; set; }

        public ICollection<GarmentShippingLocalSalesNoteItemModel> Items { get; private set; }

        public GarmentShippingLocalSalesNoteModel()
        {
        }

        public GarmentShippingLocalSalesNoteModel(string noteNo, DateTimeOffset date, int transactionTypeId, string transactionTypeCode, string transactionTypeName, int buyerId, string buyerCode, string buyerName, string buyerNPWP, int tempo, string dispositionNo, bool useVat, string remark, ICollection<GarmentShippingLocalSalesNoteItemModel> items)
        {
            NoteNo = noteNo;
            Date = date;
            TransactionTypeId = transactionTypeId;
            TransactionTypeCode = transactionTypeCode;
            TransactionTypeName = transactionTypeName;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            BuyerNPWP = buyerNPWP;
            Tempo = tempo;
            DispositionNo = dispositionNo;
            UseVat = useVat;
            Remark = remark;
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
    }
}
