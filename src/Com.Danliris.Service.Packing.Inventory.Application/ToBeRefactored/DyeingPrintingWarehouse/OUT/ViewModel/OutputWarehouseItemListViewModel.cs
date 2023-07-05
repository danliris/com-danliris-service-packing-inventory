using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.OUT.ViewModel
{
    public class OutputWarehouseItemListViewModel : BaseViewModel
    {
        public ProductionOrder ProductionOrder { get; set; }
        public Material MaterialProduct { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public ProcessType ProcessType { get; set; }
        public YarnMaterial YarnMaterial { get; set; }
        public ProductTextile ProductTextile { get; set; }
        public string MaterialWidth { get; set; }
        public string FinishWidth { get; set; }
        public string CartNo { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public string Motif { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }
        public double InputQuantity { get; set; }
        public string PackingInstruction { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }
        public decimal InputPackagingQty { get; set; }
        public string PackagingUnit { get; set; }

        public long DeliveryOrderSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }

       
        public string DeliveryNote { get; set; }
        public string Area { get; set; }
        public string DestinationArea { get; set; }
        public double Qty { get; set; }
        public int OutputId { get; set; }
        public int ProductSKUId { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get; set; }
        public bool HasPrintingProductSKU { get; set; }
        public int ProductPackingId { get; set; }
        public int FabricPackingId { get; set; }
        public string ProductPackingCode { get; set; }
        public string InventoryType { get; set; }
        public string MaterialOrigin { get; set; }
        public string Description { get; set; }
        public double PackagingLength { get; set; }
        public Track Track { get; set; }
        public int DPWarehouseSummaryId { get; set; }
    }
}
