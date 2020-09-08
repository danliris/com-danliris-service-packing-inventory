using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class UnitOfMeasurementModel : StandardEntity
    {
        public UnitOfMeasurementModel()
        {

        }

        public UnitOfMeasurementModel(string unit)
        {
            Unit = unit;
        }

        [MaxLength(64)]
        public string Unit { get; private set; }

        public void SetUnit(string unit)
        {
            Unit = unit
;        }
    }
}
