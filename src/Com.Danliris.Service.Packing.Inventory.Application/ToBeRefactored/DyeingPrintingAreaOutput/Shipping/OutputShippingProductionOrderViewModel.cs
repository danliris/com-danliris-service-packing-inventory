using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping
{
    public class OutputShippingProductionOrderViewModel : BaseViewModel
    {
        public DeliveryOrderSales DeliveryOrder { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
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
        public string PackingType { get; set; }
        public string Remark { get; set; }
        public string DeliveryNote { get; set; }
        public bool HasNextAreaDocument { get; set; }

        public int InputId { get; set; }

        public int DyeingPrintingAreaInputProductionOrderId { get; set; }

        public bool IsSave { get; set; }

        public bool HasSalesInvoice { get; set; }

        public string ShippingGrade { get; set; }
        public string ShippingRemark { get; set; }
        public double Weight { get; set; }
    }
}
