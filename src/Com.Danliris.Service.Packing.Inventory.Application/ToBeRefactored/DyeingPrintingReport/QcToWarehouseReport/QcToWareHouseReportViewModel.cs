using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport
{
    public class QcToWarehouseReportViewModel
    {
      //  public DateTimeOffset date { get; set; }
        //public string bonNo { get; set; }
      //  public long productionOrderId { get; set; }
      //  public string productionOrderNo { get; set; }
        public string orderType { get; set; }
        //public string buyer { get; set; }
        //public string construction { get; set; }
        //public string motif { get; set; }
        //public string color { get; set; }
        //public string cartNo { get; set; }
        public double inputQuantitySolid { get; set; }
        public double inputQuantityDyeing { get; set; }
        public double inputQuantityPrinting { get; set; }
        //Laporan Penyerahan QC ke Gudang
        public DateTime createdUtc { get; set; }
    }
}
