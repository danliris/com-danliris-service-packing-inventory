using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InventoryDocumentPackingModel : StandardEntity
    {
        public InventoryDocumentPackingModel()
        {
            Items = new List<InventoryDocumentPackingItemModel>();
        }

        public InventoryDocumentPackingModel(
            string code,
            DateTimeOffset date,
            ICollection<InventoryDocumentPackingItemModel> items,
            string referenceNo,
            string referenceType,
            string remark,
            string storage,
            int storageId,
            string type
            )
        {
            Code = code;
            Date = date;
            Items = items;
            ReferenceNo = referenceNo;
            ReferenceType = referenceType;
            Remark = remark;
            Storage = storage;
            StorageId = storageId;
            Type = type;
        }

        public string Code { get; set; }
        public DateTimeOffset Date { get; set; }
        public ICollection<InventoryDocumentPackingItemModel> Items { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public string Remark { get; set; }
        public string Storage { get; set; }
        public int StorageId { get; set; }
        public string Type { get; set; }
    }
}
