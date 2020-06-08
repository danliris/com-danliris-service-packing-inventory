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
        //public int MaterialDeliveryNoteModelId { get; set; }

        public void SetNoSPP(string newNoSPP)
        {
            if (newNoSPP != NoSPP)
            {
                NoSPP = newNoSPP;
            }
        }

        public void SetMaterialName(string newMaterialName)
        {
            if (newMaterialName != MaterialName)
            {
                MaterialName = newMaterialName;
            }
        }

        public void SetInputLot(string newInputLot)
        {
            if (newInputLot != InputLot)
            {
                InputLot = newInputLot;
            }
        }

        public void SetWeightBruto(double? newWeightBruto)
        {
            if (newWeightBruto != WeightBruto)
            {
                WeightBruto = newWeightBruto;
            }
        }

        public void SetWeightDOS(double? newWeightDOS)
        {
            if (newWeightDOS != WeightDOS)
            {
                WeightDOS = newWeightDOS;
            }
        }

        public void SetWeightCone(double? newWeightCone)
        {
            if (newWeightCone != WeightCone)
            {
                WeightCone = newWeightCone;
            }
        }

        public void SetWeightBale(double? newWeightBale)
        {
            if (newWeightBale != WeightBale)
            {
                WeightBale = newWeightBale;
            }
        }

        public void SetGetTotal(double? newGetTotal)
        {
            if (newGetTotal != GetTotal)
            {
                GetTotal = newGetTotal;
            }
        }
    }
}
