using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameMutationItemViewModel : BaseViewModel
    {
        public double Balance { get;  set; }
        public string Color { get;  set; }
        public string Construction { get;  set; }
        //public int DyeingPrintingStockOpnameMutationId { get; set; }
        public string Grade { get;  set; }
        public string Motif { get;  set; }
        public decimal PackagingQty { get;  set; }
        public double PackagingLength { get;  set; }
        public string PackagingType { get;  set; }
        public string PackagingUnit { get;  set; }
        //public long ProductionOrderId { get;  set; }
        //public string ProductionOrderNo { get;  set; }
        //public string ProductionOrderType { get;  set; }
        //public double ProductionOrderOrderQuantity { get;  set; }
        public ProductionOrder ProductionOrder { get; set; }
        public string Remark { get;  set; }
        public int ProcessTypeId { get; set; }
        public string ProcessTypeName { get; set; }
        public string Unit { get;  set; }
        public string UomUnit { get;  set; }

        public Track Track { get; set; }
        //public int TrackId { get;  set; }
        //public string TrackType { get;  set; }
        //public string TrackName { get;  set; }
        public int ProductSKUId { get;  set; }

        public int FabricSKUId { get;  set; }

        public string ProductSKUCode { get;  set; }

        public int ProductPackingId { get;  set; }

        public int FabricPackingId { get;  set; }

        public string ProductPackingCode { get;  set; }
        public string TypeOut { get;  set; }

        public decimal SendQuantity { get;  set; }
        public string Description { get; set; }
    }
}
