using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteModel
    {

        public MaterialDeliveryNoteModel()
        {
            Items = new HashSet<ItemsViewModel>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTimeOffset? DateSJ { get; set; }
        public string BonCode { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public long DoNumberId { get; set; }
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

        public ICollection<ItemsViewModel> Items { get; set; }
    }
}
