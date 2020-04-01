using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaMovementViewModel : BaseViewModel
    {
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
        public string CartNo { get; set; }
        public Material Material { get; set; }
        public MaterialConstruction MaterialConstruction { get; set; }
        public string MaterialWidth { get; set; }
        public Unit Unit { get; set; }
        public string Color { get; set; }
        public string Mutation { get; set; }
        public double Length { get; set; }
        public string UOMUnit { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
    }
}
