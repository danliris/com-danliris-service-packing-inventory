using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data
{
    public class MaterialDeliveryNoteWeavingModel: StandardEntity
    {

        public string Code { get; set; }
        public DateTimeOffset DateSJ { get; set; }
        public long DoSalesNumberId { get; set; }
        public string DoSalesNumber { get; set; }
        public string SendTo { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int BuyerId { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string NumberOut { get; set; }
        public int? StorageId { get; set; }
        public string StorageCode { get; set; }
        public string StorageName { get; set; }
        public string Remark { get; set; }

        public ICollection<ItemsMaterialDeliveryNoteWeavingModel> ItemsMaterialDeliveryNoteWeaving { get; private set; }

        public MaterialDeliveryNoteWeavingModel()
        {
            ItemsMaterialDeliveryNoteWeaving = new HashSet<ItemsMaterialDeliveryNoteWeavingModel>();
        }

        public MaterialDeliveryNoteWeavingModel(
            string code,
            DateTimeOffset datesj,
            long dosalesnumberid,
            string dosalesnumber,
            string sendto,
            int unitid,
            string unitname,
            int buyerid,
            string buyercode,
            string buyername,
            string numberout,
            int? storageid,
            string storagecode,
            string storagename,
            string remark,
            ICollection<ItemsMaterialDeliveryNoteWeavingModel> items
            )
        {
            Code = code;
            DateSJ = datesj;
            DoSalesNumberId = dosalesnumberid;
            DoSalesNumber = dosalesnumber;
            SendTo = sendto;
            UnitId = unitid;
            UnitName = unitname;
            BuyerId = buyerid;
            BuyerCode= buyercode;
            BuyerName = buyername;
            NumberOut = numberout;
            StorageId = storageid;
            StorageCode = storagecode;
            StorageName = storagename;
            Remark = remark;
            ItemsMaterialDeliveryNoteWeaving = items;

        }

        public void SetCode(string newCode)
        {
            if (newCode != Code)
            {
                Code = newCode;
            }
        }

        public void SetDateSJ(DateTimeOffset newDateSJ)
        {
            if (newDateSJ != DateSJ)
            {
                DateSJ = newDateSJ;
            }
        }

        public void SetDoSalesNumberId(long newdosalesnumberid)
        {
            if (newdosalesnumberid != DoSalesNumberId)
            {
                DoSalesNumberId = newdosalesnumberid;
            }
        }

        public void SetDoSalesNumber(string newDoSalesNumber)
        {
            if (newDoSalesNumber != DoSalesNumber)
            {
                DoSalesNumber = newDoSalesNumber;
            }
        }

        public void SetSendTo(string newSendTo)
        {
            if (newSendTo != SendTo)
            {
                SendTo = newSendTo;
            }
        }

        public void SetUnitId(int newUnitId)
        {
            if (newUnitId != UnitId)
            {
                UnitId = newUnitId;
            }
        }

        public void SetUnitName(string newUnitName)
        {
            if (newUnitName != UnitName)
            {
                UnitName = newUnitName;
            }
        }

        public void SetBuyerId(int newBuyerId)
        {
            if (newBuyerId != BuyerId)
            {
                BuyerId = newBuyerId;
            }
        }

        public void SetBuyerCode(string newBuyerCode)
        {
            if (newBuyerCode != BuyerCode)
            {
                BuyerCode = newBuyerCode;
            }
        }

        public void SetBuyerName(string newBuyerName)
        {
            if (newBuyerName != BuyerName)
            {
                BuyerName = newBuyerName;
            }
        }

        public void SetNumberOut(string newNumberOut)
        {
            if (newNumberOut != NumberOut)
            {
                NumberOut = newNumberOut;
            }
        }

        public void SetStorageId(int? newStorageId)
        {
            if (newStorageId != StorageId)
            {
                StorageId = newStorageId;
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

        public void SetRemark(string newRemark)
        {
            if (newRemark != Remark)
            {
                Remark = newRemark;
            }
        }
    }
}
