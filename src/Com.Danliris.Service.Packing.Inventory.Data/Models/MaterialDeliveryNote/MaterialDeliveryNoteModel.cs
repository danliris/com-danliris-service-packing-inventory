using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data
{
    public class MaterialDeliveryNoteModel : StandardEntity
    {
        public string Code { get; set; }
        public DateTimeOffset? DateSJ { get; set; }
        public string BonCode { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
        public string DONumber { get; set; }
        public string FONumber { get; set; }
        public string Receiver { get; set; }
        public string Remark { get; set; }
        public string SCNumber { get; set; }
        public string Sender { get; set; }
        public string StorageNumber { get; set; }
        public ICollection<ItemsModel> Items { get; private set; }

        public MaterialDeliveryNoteModel()
        {
            Items = new HashSet<ItemsModel>();
        }

        public MaterialDeliveryNoteModel(
            string code,
            DateTimeOffset? dateSJ,
            string bonCode,
            DateTimeOffset dateFrom,
            DateTimeOffset dateTo,
            string doNumber,
            string fONumber,
            string receiver,
            string remark,
            string sCNumber,
            string sender,
            string storageNumber,
            ICollection<ItemsModel> items
            )
        {
            Code = code;
            DateSJ = dateSJ;
            BonCode = bonCode;
            DateFrom = dateFrom;
            DateTo = dateTo;
            DONumber = doNumber;
            FONumber = fONumber;
            Receiver = receiver;
            Remark = remark;
            SCNumber = sCNumber;
            Sender = sender;
            StorageNumber = storageNumber;
            Items = items;

        }
        

    }
}