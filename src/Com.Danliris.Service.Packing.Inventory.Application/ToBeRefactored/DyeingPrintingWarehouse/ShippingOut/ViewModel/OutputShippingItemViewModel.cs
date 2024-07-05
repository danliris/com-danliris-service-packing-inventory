using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingOut.ViewModel
{
    public class OutputShippingItemViewModel : BaseViewModel
    {
        public DeliveryOrderSales DeliveryOrder { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
        public Material Material { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public ProcessType ProcessType { get; set; }
        public YarnMaterial YarnMaterial { get; set; }
        public ProductTextile ProductTextile { get; set; }
        public string MaterialWidth { get; set; }
        public string FinishWidth { get; set; }
        public string CartNo { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Grade { get; set; }
        public string Packing { get; set; }
        public decimal QtyPacking { get; set; }
        public double Qty { get; set; }
        public double Balance { get; set; }
        public string PackingType { get; set; }
        public string Remark { get; set; }
        public int InputId { get; set; }
        public int DPAreaInputProductionOrderId { get; set; }
        public bool IsSave { get; set; }
        public string ShippingGrade { get; set; }
        public string ShippingRemark { get; set; }
        public double Weight { get; set; }
        public double PackingLength { get; set; }
        public double BalanceRemains { get; set; }
        public decimal PackagingQty { get; set; }
        public int ProductSKUId { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get; set; }
        public bool HasPrintingProductSKU { get; set; }
        public int ProductPackingId { get; set; }
        public int FabricPackingId { get; set; }
        public string ProductPackingCode { get; set; }
        public string MaterialOrigin { get; set; }
        public string PackingListBaleNo { get; set; }
        public decimal PackingListNet { get; set; }
        public decimal PackingListGross { get; set; }
        public string DeliveryOrderSalesType { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public string DestinationBuyerName { get; set; }
        public string PackagingUnit { get; set; }
        public string PackingInstruction { get; set; }
        public string DeliveryNote { get; set; }
    }
}
