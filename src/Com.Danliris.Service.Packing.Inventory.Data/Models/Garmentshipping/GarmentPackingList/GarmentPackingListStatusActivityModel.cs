using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListStatusActivityModel : BaseEntity<int>
    {
        public int PackingListId { get; protected set; }
        public DateTimeOffset CreatedDate { get; protected set; }
        public string CreatedBy { get; protected set; }
        public string CreatedAgent { get; protected set; }
        public GarmentPackingListStatusEnum Status { get; protected set; }
        public string Remark { get; protected set; }

        public GarmentPackingListStatusActivityModel(string createdBy, string createdAgent, GarmentPackingListStatusEnum status, string remark = null)
        {
            CreatedDate = DateTimeOffset.Now;
            CreatedBy = createdBy;
            CreatedAgent = createdAgent;
            Status = status;
            Remark = remark;
        }
    }
}
