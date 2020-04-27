using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class PackagingStockModel : StandardEntity
    {
        public PackagingStockModel()
        {

        }

        public PackagingStockModel(int dyeingPrintingProductionOrderId, string productionOrderNo, string packagingType, string packagingUnit, decimal packagingQty, string uomUnit, decimal length, bool hasNextArea)
        {
            DyeingPrintingProductionOrderId = dyeingPrintingProductionOrderId;
            ProductionOrderNo = productionOrderNo;
            PackagingType = packagingType;
            PackagingUnit = packagingUnit;
            PackagingQty = packagingQty;
            UomUnit = uomUnit;
            Length = length;
            HasNextArea = hasNextArea;
        }

        public int DyeingPrintingProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public string PackagingType { get; set; }
        public string PackagingUnit { get; set; }
        public decimal PackagingQty { get; set; }
        public string UomUnit { get; set; }
        public decimal Length { get; set; }
        public bool HasNextArea { get; set; }

    }
}
