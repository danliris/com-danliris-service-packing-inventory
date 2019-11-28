using System;
using System.Collections.Generic;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class PackagingInventoryDocumentModel : StandardEntity
    {
        public PackagingInventoryDocumentModel()
        {

        }

        public PackagingInventoryDocumentModel(
            string documentNo,
            DateTimeOffset date,
            List<PackagingInventoryDocumentItemModel> items,
            string referenceNo,
            string referenceType,
            string remark,
            string status,
            string storage,
            int storageId
        )
        {
            DocumentNo = documentNo;
            Date = date;
            Items = items;
            ReferenceNo = referenceNo;
            ReferenceType = referenceType;
            Remark = Remark;
            Status = status;
            Storage = Storage;
            StorageId = StorageId;
        }

        public string DocumentNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<PackagingInventoryDocumentItemModel> Items { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string Storage { get; set; }
        public int StorageId { get; set; }
    }
}