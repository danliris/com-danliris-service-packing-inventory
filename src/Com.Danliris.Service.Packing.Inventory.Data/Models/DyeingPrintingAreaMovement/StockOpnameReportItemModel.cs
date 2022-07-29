using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class StockOpnameReportItemModel : StandardEntity
    {
        public StockOpnameReportItemModel(string productionOrderNo, string material, string unit, string motif, string buyer, string color, string grade, string jenis, double stockOpnameQuantity, double warehouseQuantity, double difference, int headerId)
        {
            ProductionOrderNo = productionOrderNo;
            Material = material;
            Unit = unit;
            Motif = motif;
            Buyer = buyer;
            Color = color;
            Grade = grade;
            Jenis = jenis;
            StockOpnameQuantity = stockOpnameQuantity;
            WarehouseQuantity = warehouseQuantity;
            Difference = difference;
            HeaderId = headerId;
        }

        [MaxLength(128)]
        public string ProductionOrderNo { get; private set; }
        [MaxLength(128)]
        public string Material { get; private set; }
        [MaxLength(128)]
        public string Unit { get; private set; }
        [MaxLength(128)]
        public string Motif { get; private set; }
        [MaxLength(1024)]
        public string Buyer { get; private set; }
        [MaxLength(128)]
        public string Color { get; private set; }
        [MaxLength(128)]
        public string Grade { get; private set; }
        [MaxLength(128)]
        public string Jenis { get; private set; }
        public double StockOpnameQuantity { get; private set; }
        public double WarehouseQuantity { get; private set; }
        public double Difference { get; private set; }
        public int HeaderId { get; private set; }
    }
}
