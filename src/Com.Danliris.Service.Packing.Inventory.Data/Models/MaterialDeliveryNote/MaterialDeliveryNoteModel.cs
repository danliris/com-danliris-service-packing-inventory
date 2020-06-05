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
            DateTimeOffset? datesj,
            string boncode,
            DateTimeOffset datefrom,
            DateTimeOffset dateto,
            string donumber,
            string fonumber,
            string receiver,
            string remark,
            string scnumber,
            string sender,
            string storageNumber,
            ICollection<ItemsModel> items
            )
        {
            Code = code;
            DateSJ = datesj;
            BonCode = boncode;
            DateFrom = datefrom;
            DateTo = dateto;
            DONumber = donumber;
            FONumber = fonumber;
            Receiver = receiver;
            Remark = remark;
            SCNumber = scnumber;
            Sender = sender;
            StorageNumber = storageNumber;
            Items = items;

        }

        public void SetCode(string newCode)
        {
            if (newCode != Code)
            {
                Code = newCode;
            }
        }

        public void SetDateSJ(DateTimeOffset? newDateSJ)
        {
            if (newDateSJ != DateSJ)
            {
                DateSJ = newDateSJ;
            }
        }

        public void SetBonCode(string newboncode)
        {
            if (newboncode != BonCode)
            {
                BonCode = newboncode;
            }
        }

        public void SetDateFrom(DateTimeOffset? newDateFrom)
        {
            if (newDateFrom != DateFrom)
            {
                DateFrom = newDateFrom;
            }
        }

        public void SetDateTo(DateTimeOffset? newDateTo)
        {
            if (newDateTo != DateTo)
            {
                DateTo = newDateTo;
            }
        }

        public void SetDONumber(string newDONumber)
        {
            if (newDONumber != DONumber)
            {
                DONumber = newDONumber;
            }
        }

        public void SetFONumber(string newFONumber)
        {
            if (newFONumber != FONumber)
            {
                FONumber = newFONumber;
            }
        }

        public void SetReceiver(string newReceiver)
        {
            if (newReceiver != Receiver)
            {
                Receiver = newReceiver;
            }
        }

        public void SetRemark(string newRemark)
        {
            if (newRemark != Remark)
            {
                Remark = newRemark;
            }
        }

        public void SetSCNumber(string newSCNumber)
        {
            if (newSCNumber != SCNumber)
            {
                SCNumber = newSCNumber;
            }
        }

        public void SetSender(string newSender)
        {
            if (newSender != Sender)
            {
                Sender = newSender;
            }
        }

        public void SetStorageNumber(string newStorageNumber)
        {
            if (newStorageNumber != StorageNumber)
            {
                StorageNumber = newStorageNumber;
            }
        }

    }
}