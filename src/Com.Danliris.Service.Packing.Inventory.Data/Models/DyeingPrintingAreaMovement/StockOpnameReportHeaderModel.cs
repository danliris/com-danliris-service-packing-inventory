using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class StockOpnameReportHeaderModel  : StandardEntity
    {
        public StockOpnameReportHeaderModel(DateTime date, string zone, string unit, string material, string buyer, long productionOrderId)
        {
            Date = date;
            Zone = zone;
            Unit = unit;
            Material = material;
            Buyer = buyer;
            ProductionOrderId = productionOrderId;
        }

        public DateTime Date { get; private set; }
        [MaxLength(128)]
        public string Zone { get; private set; }
        [MaxLength(128)]
        public string Unit { get; private set; }
        [MaxLength(1024)]
        public string Material { get; private set; }
        [MaxLength(1024)]
        public string Buyer { get; private set; }
        public long ProductionOrderId { get; private set; }

    }
}
