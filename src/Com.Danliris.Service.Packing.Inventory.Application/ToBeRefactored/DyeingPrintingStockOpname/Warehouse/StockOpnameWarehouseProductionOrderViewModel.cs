using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
   public class StockOpnameWarehouseProductionOrderViewModel : BaseViewModel
    {

        public double BalanceRemains { get; set; }
        public double Balance { get; set; }
        public int BuyerId { get; set; }
        public string Buyer { get; set; }
        public string Color { get; set; }
        public long DeliveryOrderSalesId { get; set; }
        //public string DeliveryOrderSalesNo { get; set; }
        public string DocumentNo { get; set; }
        public string Grade { get; set; }
        public Material Material { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
        public string PackagingType { get; set; }
        public string PackagingUnit { get; set; }
        public decimal PackagingQty { get; set; }
        public double PackagingLength { get;  set; }
        public string PackingInstruction { get; set; }
        public string MaterialWidth { get; set; }
        public string Motif { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string UomUnit { get; set; }
        public string Remark { get; set; }
        public double PreviousBalance { get; set; }
        public decimal PreviousQtyPacking { get; set; }
        public string ProductionOrderNo { get; set; }
        public bool IsSave { get; set; }
        public int InputId { get; set; }
       // public string MaterialString { get; set; }
        public decimal MtrLength { get; set; }
        public decimal YdsLength { get; set; }
        public double Quantity { get; set; }
       
        public double QtyOrder { get; set; }
      
        public string Status { get; set; }
        // public int DyeingPrintingAreaInputProductionOrderId { get; set; }


        public int ProcessTypeId { get; private set; }
        public string ProcessTypeName { get; private set; }

        public int YarnMaterialId { get; private set; }
        public string YarnMaterialName { get; private set; }

        public ProcessType ProcessType { get; set; }
        public YarnMaterial YarnMaterial { get; set; }






    }
}
