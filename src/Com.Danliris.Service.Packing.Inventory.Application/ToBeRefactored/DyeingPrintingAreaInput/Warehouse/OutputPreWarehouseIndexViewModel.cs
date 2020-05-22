using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse
{
    public class OutputPreWarehouseIndexViewModel : BaseViewModel
    {
        public long ProductionOrderId { get; set; }
        public string ProductionOrderCode { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductionOrderType { get; set; }
        public double ProductionOrderOrderQuantity { get; set; }
        public int OutputId { get; set; }
        public ICollection<ProductionOrderItemListViewModel> ProductionOrderItems { get; set; }

        //public ProductionOrder ProductionOrder { get; set; }
        //public string ProductionOrderNo { get; set; }
        //public string CartNo { get; set; }
        //public string PackingInstruction { get; set; }
        //public string Construction { get; set; }
        //public string Unit { get; set; }
        //public string Buyer { get; set; }
        //public string Color { get; set; }
        //public string Motif { get; set; }
        //public string UomUnit { get; set; }
        //public double Balance { get; set; }
        //public bool HasNextAreaDocument { get; set; }
        //public string Grade { get; set; }
        //public string Remark { get; set; }
        //public string Status { get; set; }
        //public string PackagingType { get; set; }
        //public string PackagingUnit { get; set; }
        //public decimal PackagingQty { get; set; }
        //public double QtyOrder { get; set; }
        //public int DyeingPrintingAreaOutputId { get; set; }


    }
}
