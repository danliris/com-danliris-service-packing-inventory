using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory
{
    public class ProductPackingInventoryDocumentModel : StandardEntity
    {
        public ProductPackingInventoryDocumentModel()
        {

        }

        public ProductPackingInventoryDocumentModel(
            string documentNo,
            DateTimeOffset date,
            string referenceNo,
            string referenceType,
            int storageId,
            string storageName,
            string storageCode,
            string inventoryType,
            string remark
            )
        {
            DocumentNo = documentNo;
            Date = date;
            ReferenceNo = referenceNo;
            ReferenceType = referenceType;
            StorageId = storageId;
            StorageName = storageName;
            StorageCode = storageCode;
            InventoryType = inventoryType;
            Remark = remark;
        }

        [MaxLength(64)]
        public string DocumentNo { get; }
        public DateTimeOffset Date { get; }
        [MaxLength(64)]
        public string ReferenceNo { get; }
        [MaxLength(256)]
        public string ReferenceType { get; }
        public int StorageId { get; }
        [MaxLength(512)]
        public string StorageName { get; }
        [MaxLength(64)]
        public string StorageCode { get; }
        [MaxLength(32)]
        public string InventoryType { get; }
        public string Remark { get; }
    }
}
