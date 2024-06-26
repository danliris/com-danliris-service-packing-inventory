﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.UpdateTrack.ViewModel
{
    public class DPUpdateTrackViewModel
    {
        public long Id { get; set; }
        public long ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductPackingCode { get; set; }
        public string ProcessTypeName { get; set; }
        public string PackagingUnit { get; set; }
        public string PackagingType { get; set; }
        public string Grade { get; set; }
        public string Color { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string TrackBox { get; set; }
        public string Track { get; set; }
        public double Balance { get; set; }
        public decimal PackagingQty { get; set; }
        public double PackagingLength { get; set; }
        public string Construction { get; set; }
        public string Motif { get; set; }
        public string Description { get; set; }
    }
}
