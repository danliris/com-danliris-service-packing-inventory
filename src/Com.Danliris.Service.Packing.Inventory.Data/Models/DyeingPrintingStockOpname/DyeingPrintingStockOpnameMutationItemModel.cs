using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname
{
    public class DyeingPrintingStockOpnameMutationItemModel : StandardEntity
    {
        public double Balance { get; private set; }
        public string Color { get; private set; }
        public string Construction { get; private set; }
        public int DyeingPrintingStockOpnameMutationId { get; set; }
        public string Grade { get; private set; }
        public string Motif { get; private set; }
        public decimal PackagingQty { get; private set; }
        public double PackagingLength { get; private set; }
        public string PackagingType { get; private set; }
        public string PackagingUnit { get; private set; }
        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }
        public string Remark { get; private set; }
        public int ProcessTypeId { get; private set; }
        public string ProcessTypeName { get; private set; }
        public string Unit { get; private set; }
        public string UomUnit { get; private set; }
        public int TrackId { get; private set; }
        public string TrackType { get; private set; }
        public string TrackName { get; private set; }
        public int ProductSKUId { get; private set; }

        public int FabricSKUId { get; private set; }

        public string ProductSKUCode { get; private set; }

        public int ProductPackingId { get; private set; }

        public int FabricPackingId { get; private set; }

        public string ProductPackingCode { get; private set; }
        public string TypeOut { get; private set; }

        public DyeingPrintingStockOpnameMutationModel DyeingPrintingStockOpnameMutation { get; set; }

        public DyeingPrintingStockOpnameMutationItemModel(double balance,  string color, string construction,  string grade,  string motif,  
            decimal packagingQty, double packagingLength, string packagingType, string packagingUnit,long productionOrderId, string productionOrderNo, 
            string productionOrderType, double productionOrderOrderQuantity, int processTypeId, string processTypeName,string remark, string unit, string uomUnit, int trackId, string trackType, string trackName, int productSKUId, int fabricSKUId, string productSKUCode, int productPackingId,
            int fabricPackingId, string productPackingCode, string typeOut)
        {
            Balance = balance;
            Color = color;
            Construction = construction;
            Grade = grade;
            Motif = motif;
            PackagingQty = packagingQty;
            PackagingLength = packagingLength;
            PackagingType = packagingType;
            PackagingUnit = packagingUnit;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderType = productionOrderType;
            ProcessTypeName = processTypeName;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            ProcessTypeId = processTypeId;
            Remark = remark;
            Unit = unit;
            UomUnit = uomUnit;
            TrackId = trackId;
            TrackType = trackType;
            TrackName = trackName;
            ProductSKUId = productSKUId;
            FabricSKUId = fabricSKUId;
            ProductSKUCode = productSKUCode;
            ProductPackingId = productPackingId;
            FabricPackingId = fabricPackingId;
            ProductPackingCode = productPackingCode;
            TypeOut = typeOut;
        }

    }
}
