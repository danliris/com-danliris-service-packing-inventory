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
        public long? DoNumberId { get; set; }
        public string DONumber { get; set; }
        public string FONumber { get; set; }
        public int? ReceiverId { get; set; }
        public string ReceiverCode { get; set; }
        public string ReceiverName { get; set; }
        public string Remark { get; set; }
        public int? SCNumberId { get; set; }
        public string SCNumber { get; set; }
        public int? SenderId { get; set; }
        public string SenderCode { get; set; }
        public string SenderName { get; set; }
        public int? StorageId { get; set; }
        public string StorageCode { get; set; }
        public string StorageName { get; set; }
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
            long? donumberid,
            string donumber,
            string fonumber,
            int? receiverid,
            string receivercode,
            string receivername,
            string remark,
            int? scnumberid,
            string scnumber,
            int? senderid,
            string sendercode,
            string sendername,
            int? storageid,
            string storagecode,
            string storagename,
            ICollection<ItemsModel> items
            )
        {
            Code = code;
            DateSJ = datesj;
            BonCode = boncode;
            DateFrom = datefrom;
            DateTo = dateto;
            DoNumberId = donumberid;
            DONumber = donumber;
            FONumber = fonumber;
            ReceiverId = receiverid;
            ReceiverCode = receivercode;
            ReceiverName = receivername;
            Remark = remark;
            SCNumberId = scnumberid;
            SCNumber = scnumber;
            SenderId = senderid;
            SenderCode = sendercode;
            SenderName = sendername;
            StorageId = storageid;
            StorageCode = storagecode;
            StorageName = storagename;
            
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

        public void SetDONumberId(long? newDONumberid)
        {
            if (newDONumberid != DoNumberId)
            {
                DoNumberId = newDONumberid;
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

        public void SetReceiverId(int? newReceiverid)
        {
            if (newReceiverid != ReceiverId)
            {
                ReceiverId = newReceiverid;
            }
        }

        public void SetReceiverCode(string newReceivercode)
        {
            if (newReceivercode != ReceiverCode)
            {
                ReceiverCode = newReceivercode;
            }
        }

        public void SetReceiverName(string newReceivername)
        {
            if (newReceivername != ReceiverName)
            {
                ReceiverName = newReceivername;
            }
        }

        public void SetRemark(string newRemark)
        {
            if (newRemark != Remark)
            {
                Remark = newRemark;
            }
        }

        public void SetSCNumberId(int? newSCNumberid)
        {
            if (newSCNumberid != SCNumberId)
            {
                SCNumberId = newSCNumberid;
            }
        }

        public void SetSCNumber(string newSCNumber)
        {
            if (newSCNumber != SCNumber)
            {
                SCNumber = newSCNumber;
            }
        }

        public void SetSenderId(int? newSenderid)
        {
            if (newSenderid != SenderId)
            {
                SenderId = newSenderid;
            }
        }

        public void SetSenderCode(string newSendercode)
        {
            if (newSendercode != SenderCode)
            {
                SenderCode = newSendercode;
            }
        }

        public void SetSenderName(string newSendername)
        {
            if (newSendername != SenderName)
            {
                SenderName = newSendername;
            }
        }

        public void SetStorageid(int? newStorageid)
        {
            if (newStorageid != StorageId)
            {
                StorageId = newStorageid;
            }
        }

        public void SetStorageCode(string newStorageCode)
        {
            if (newStorageCode != StorageCode)
            {
                StorageCode = newStorageCode;
            }
        }

        public void SetStorageName(string newStorageName)
        {
            if (newStorageName != StorageName)
            {
                StorageName = newStorageName;
            }
        }

    }
}