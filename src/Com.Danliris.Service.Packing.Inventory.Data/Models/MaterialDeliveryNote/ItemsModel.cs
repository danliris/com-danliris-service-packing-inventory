using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote
{
    public class ItemsModel : StandardEntity
    {

        public ItemsModel(
            string noSPP,
            string materialName,
            string inputLot,
            double? weightBruto,
            double? weightDOS,
            double? weightCone,
            double? weightBale,
            double? getTotal
            )
        {
            NoSPP = noSPP;
            MaterialName = materialName;
            InputLot = inputLot;
            WeightBruto = weightBruto;
            WeightDOS = weightDOS;
            WeightCone = weightCone;
            WeightBale = weightBale;
            GetTotal = getTotal;

        }
        
        public string NoSPP { get; set; }
        public string MaterialName { get; set; }
        public string InputLot { get; set; }
        public double? WeightBruto { get; set; }
        public double? WeightDOS { get; set; }
        public double? WeightCone { get; set; }
        public double? WeightBale { get; set; }
        public double? GetTotal { get; set; }
    }
}
