using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Aval
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Activity { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Group { get; set; }
        //Same as NoteNo?
        public string BonNo { get; set; }
        //Unit Name or Unit Code
        public string Unit { get; set; }
        public string Mutation { get; set; }
        public string ProductionOrderNo { get; set; }
        public string CartNo { get; set; }
        //Same as Type?
        public string ProductionOrderType { get; set; }
        //Same as Quantity?
        public double ProductionOrderQuantity  { get; set; }
        public string UomUnit { get; set; }
        //New Property
        //public double MassKg { get; set; }

        //public string Grade { get; set; }
        //public string Motif { get; set; }
        //public string Color { get; set; }
        //public double QtyPackaging { get; set; }
        //public string Packaging { get; set; }
        //public double ProductionOrderQuantity { get; set; }
        //public decimal Balance { get; set; }
    }
}
