using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.UOM
{
    public class UOMIndexInfo
    {
        public UOMIndexInfo()
        {
        }

        public UOMIndexInfo(UnitOfMeasurementModel entity)
        {
            LastModifiedUtc = entity.LastModifiedUtc;
            Unit = entity.Unit;
            Id = entity.Id;
        }

        public DateTime LastModifiedUtc { get; set; }
        public string Unit { get; set; }
        public int Id { get; set; }
    }
}