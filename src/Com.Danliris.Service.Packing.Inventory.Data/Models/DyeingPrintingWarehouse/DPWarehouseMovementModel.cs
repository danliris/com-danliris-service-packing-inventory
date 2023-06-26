using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse
{
    public class DPWarehouseMovementModel :  StandardEntity
    {
        public DateTimeOffset Date { get; private set; }
        public string Area { get; private set; }
        public string Type { get; private set; }

        public int DPDocumentId { get; private set; }
        public int DPDocumentItemId { get; private set; }
        public string DPDocumentBonNo{ get; private set; }
        public int DPSummaryId{ get; private set; }
        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string Buyer { get; private set; }
        public string Construction { get; private set; }
        public string Unit { get; private set; }
        public string Color { get; private set; }
        public string Motif { get; private set; }
        public string UomUnit { get; private set; }
        public double Balance { get; private set; }
        public string Grade { get; private set; }
        public string ProductionOrderType { get; private set; }
        public string Remark { get; private set; }
        public string PackingType { get; private set; }

        public decimal PackagingQty { get; private set; }
        public string PackagingUnit { get; private set; }
        public double PackagingLength { get; private set; }
        public string MaterialOrigin { get; set; }
        public int ProductPackingId { get; set; }
        public string ProductPackingCode { get; set; }

        #region
        public int? ProductTextileId { get; set; }
        public string ProductTextileCode { get; set; }
        public string ProductTextileName { get; set; }
        #endregion
        public int TrackFromId { get; private set; }
        public string TrackFromType { get; private set; }
        public string TrackFromName { get; private set; }
        public string TrackFromBox { get; private set; }

        public int TrackToId { get; private set; }
        public string TrackToType { get; private set; }
        public string TrackToName { get; private set; }
        public string TrackToBox { get; private set; }
        public string Description { get; private set; }

        public DPWarehouseMovementModel(DateTimeOffset date,
            string area,
            string type,
            int dPDocumentId,
            int dPDocumentItemId,
            string dPDocumentBonNo,
            int dPSummaryId,
            long productionOrderId,
            string productionOrderNo,
            string buyer,
            string construction,
            string unit,
            string color,
            string motif,
            string uomUnit,
            double balance,
            string grade,
            string productionOrderType,
            string remark,
            string packingType,
            decimal packagingQty,
            string packagingUnit,
            double packagingLength,
            string materialOrigin,
            int? productTextileId,
            string productTextileCode,
            string productTextileName,
            int productPackingId,
            string productPackingCode,
            string description
            )
        {
            Date = date;
            Area = area;
            Type = type;

            DPDocumentId = dPDocumentId;
            DPDocumentItemId = dPDocumentItemId;
            DPDocumentBonNo = dPDocumentBonNo;
            DPSummaryId = dPSummaryId;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            Remark = remark;
            PackingType = packingType;

            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            PackagingLength = packagingLength;
            MaterialOrigin = materialOrigin;
            ProductTextileId = productTextileId;
            ProductTextileCode = productTextileCode;
            ProductTextileName = productTextileName;
            ProductPackingId = productPackingId;
            ProductPackingCode = productPackingCode;
            Description = description;


        }

        //update track
        public DPWarehouseMovementModel(DateTimeOffset date,
            string area,
            string type,
            int dPDocumentId,
            int dPDocumentItemId,
            string dPDocumentBonNo,
            int dPSummaryId,
            long productionOrderId,
            string productionOrderNo,
            string buyer,
            string construction,
            string unit,
            string color,
            string motif,
            string uomUnit,
            double balance,
            string grade,
            string productionOrderType,
            string remark,
            string packingType,
            decimal packagingQty,
            string packagingUnit,
            double packagingLength,
            string materialOrigin,
            int? productTextileId,
            string productTextileCode,
            string productTextileName,
            int productPackingId,
            string productPackingCode,
            int trackFromId,
            string trackFromName,
            string trackFromBox,
            string trackFromType,
            int trackToId,
            string trackToName,
            string trackToBox,
            string trackToType,
            string description

            )
        {
            Date = date;
            Area = area;
            Type = type;

            DPDocumentId = dPDocumentId;
            DPDocumentItemId = dPDocumentItemId;
            DPDocumentBonNo = dPDocumentBonNo;
            DPSummaryId = dPSummaryId;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            Remark = remark;
            PackingType = packingType;

            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            PackagingLength = packagingLength;
            MaterialOrigin = materialOrigin;
            ProductTextileId = productTextileId;
            ProductTextileCode = productTextileCode;
            ProductTextileName = productTextileName;
            ProductPackingId = productPackingId;
            ProductPackingCode = productPackingCode;
            TrackFromId = trackFromId;
            TrackFromName = trackFromName;
            TrackFromBox = trackFromBox;
            TrackFromType = trackFromType;
            TrackToId = trackToId;
            TrackToName = trackToName;
            TrackToBox = trackToBox;
            TrackToType = trackToType;
            Description = description;
              


        }

    }
}
