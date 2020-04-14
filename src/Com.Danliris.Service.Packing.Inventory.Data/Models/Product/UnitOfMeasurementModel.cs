using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

        public string Unit { get; set; }
    }
}
