using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse
{
    public class ReportStockWarehouseViewModel
    {
        public string NoSpp { get; set; }
        public string Construction { get; set; }
        public string Unit { get; set; }
        public string Motif { get; set; }
        public string Color { get; set; }
        public string Grade { get; set; }
        public string Jenis { get; set; }
        public string Ket { get; set; }
        public double Awal { get; set; }
        public double Masuk { get; set; }
        public double Keluar { get; set; }
        public double Akhir { get; set; }
        public string Satuan { get; set; }
    }
}
