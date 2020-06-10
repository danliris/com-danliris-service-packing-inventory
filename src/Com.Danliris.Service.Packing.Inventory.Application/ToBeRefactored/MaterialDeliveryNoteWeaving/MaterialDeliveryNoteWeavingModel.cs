using Com.Danliris.Service.Packing.Inventory.Application;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingModel
    {
        public MaterialDeliveryNoteWeavingModel()
        {
            ItemsMaterialDeliveryNoteWeaving = new HashSet<ItemsMaterialDeliveryNoteWeavingViewModel>();
        }

        public string Code { get; set; }
        public DateTimeOffset? DateSJ { get; set; }
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
        public string UnitLength { get; set; }
        public string UnitPacking { get; set; }
        public string Remark { get; set; }


        public ICollection<ItemsMaterialDeliveryNoteWeavingViewModel> ItemsMaterialDeliveryNoteWeaving { get; set; }
    }
}