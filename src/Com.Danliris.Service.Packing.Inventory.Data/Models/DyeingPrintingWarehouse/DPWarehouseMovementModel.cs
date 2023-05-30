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
        public int DPDocumentBonNo{ get; private set; }
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


    }
}
