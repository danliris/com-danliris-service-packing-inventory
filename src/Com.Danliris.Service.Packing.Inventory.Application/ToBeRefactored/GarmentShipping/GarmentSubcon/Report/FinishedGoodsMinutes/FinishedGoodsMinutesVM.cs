using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.FinishedGoodsMinutes
{
    public class FinishedGoodsMinutesVM
    {
        public string InvoiceNo { get; set; }
        public string RONo { get; set; }
        public string BCNo { get; set; }
        public string BCType { get; set; }
        public DateTimeOffset BCDate { get; set; }
        public double SentQty { get; set; }
        public string SentUomUnit { get; set; }
        public string ComodityName { get; set; }
        public string BuyerName { get; set; }
        public string LocalSalesNote { get; set; }
        public List<FinishedGoodsMinutesPurchasingVM> detailRO { get; set; }
    }

    public class FinishedGoodsMinutesPurchasingVM
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double UsedQty { get; set; }
        public string UsedUomUnit { get; set; }
        public string DONo { get; set; }
        public string SupplierName { get; set; }
        public double ReceiptQty { get; set; }
        public string ReceiptUomUnit { get; set; }
        public string ReceiptBCNo { get; set; }
        public string ReceiptBCType { get; set; }
        public DateTimeOffset ReceiptBCDate { get; set; }
        public string RONo { get; set; }
    }

    public class GetBCOUTVM
    {
        public string LocalSalesNote { get; set; }
        public string BCNo { get; set; }
        public string BCType { get; set; }
        public DateTimeOffset BCDate { get; set; }
    }
}
