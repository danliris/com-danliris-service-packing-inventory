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

        public int DyeingPrintingProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string PackagingType { get; private set; }
        public string PackagingUnit { get; private set; }
        public decimal PackagingQty { get; private set; }
        public string UomUnit { get; private set; }
        public decimal Length { get; private set; }
        public bool HasNextArea { get; private set; }

        public void SetPackagingType(string newPackagingType, string user, string agent)
        {
            if(newPackagingType != PackagingType)
            {
                PackagingType = newPackagingType;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetPackagingUnit(string newPackagingUnit, string user, string agent)
        {
            if(newPackagingUnit!= PackagingUnit)
            {
                PackagingUnit = newPackagingUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingQty(decimal newPackagingQty,string user, string agent)
        {
            if(newPackagingQty != PackagingQty)
            {
                PackagingQty = newPackagingQty;
                this.FlagForUpdate(user, agent);

            }
        }

        public void SetUomUnit(string newUomUnit, string user, string agent)
        {
            if(newUomUnit != UomUnit)
            {
                UomUnit = newUomUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetLength(decimal newLength, string user, string agent)
        {
            if(newLength != Length)
            {
                Length = newLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetHasNextArea(bool hasNextArea, string user, string agent)
        {
            if(hasNextArea != HasNextArea)
            {
                HasNextArea = hasNextArea;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
