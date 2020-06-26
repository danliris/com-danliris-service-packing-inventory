using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
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
        public DeliveryOrderMaterialDeliveryNoteWeaving DONumber { get; set; }
        public string FONumber { get; set; }
        public BuyerMaterialDeliveryNoteWeaving buyer { get; set; }
        public string Remark { get; set; }
        public SalesContract salesContract { get; set; }
        public UnitMaterialDeliveryNoteWeaving unit { get; set; }
        public StorageMaterialDeliveryNoteWeaving storage { get; set; }

        public ICollection<ItemsViewModel> Items { get; set; }
    }
}
