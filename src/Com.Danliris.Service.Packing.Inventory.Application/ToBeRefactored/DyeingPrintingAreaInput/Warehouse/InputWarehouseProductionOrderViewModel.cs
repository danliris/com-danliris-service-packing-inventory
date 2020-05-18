using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse
{
    public class InputWarehouseProductionOrderViewModel : BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public string ProductionOrderNo { get; set; }
        public string CartNo { get; set; }
        public string PackingInstruction { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public double Balance { get; set; }
        public bool HasOutputDocument { get; set; }
        public bool IsChecked { get; set; }
        public string Grade { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        //public string Material { get; set; }
        //public decimal MtrLength { get; set; }
        //public decimal YdsLength { get; set; }
        //public decimal Quantity { get; set; }
        public string PackagingType { get; set; }
        public string PackagingUnit { get; set; }
        public decimal PackagingQty { get; set; }
        public double QtyOrder { get; set; }
        public string DeliveryOrderSalesNo { get; set; }

        public int OutputId { get; set; }

        public int InputId { get; set; }
    }
}
