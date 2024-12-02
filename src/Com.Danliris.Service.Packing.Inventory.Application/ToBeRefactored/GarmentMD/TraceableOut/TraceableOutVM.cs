using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.TraceableOut
{
    public class TraceableOutVM
    {
        public DateTime Date { get; set; }
        public string ComodityName { get; set; }
        public double Qty { get; set; }
        public string NoteNo { get; set; }
        public string BonNo { get; set; }
        public string BuyerName { get; set; }
        public string BCType { get; set; }
        public string BCNo { get; set; }
        public DateTime BCDate { get; set; }
        public string RO { get; set; }
        public string UnitQtyName { get; set; }
        public List<TraceableOutBeacukaiDetailViewModel> rincian { get; set; }
    }

    public class TraceableOutBeacukaiDetailViewModel
    {
        public string DestinationJob { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double SmallestQuantity { get; set; }
        public string UnitQtyName { get; set; }
        public string BCNo { get; set; }
        public string BCType { get; set; }
        public DateTime BCDate { get; set; }
        public string DONo { get; set; }
        public string SupplierName { get; set; }
    }
}
