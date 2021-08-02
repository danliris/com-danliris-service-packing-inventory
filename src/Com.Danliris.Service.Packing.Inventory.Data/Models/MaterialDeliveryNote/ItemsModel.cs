using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote
{
    public class ItemsModel : StandardEntity
    {

        public ItemsModel()
        {

        }

        public ItemsModel(
            int? idsop,
            string noSOP,
            string materialName,
            string inputLot,
            double? weightBruto,
            string weightDOS,
            string weightCone,
            double? weightBale,
            double? getTotal
            )
        {
            IdSOP = idsop;
            NoSOP = noSOP;
            MaterialName = materialName;
            InputLot = inputLot;
            WeightBruto = weightBruto;
            WeightDOS = weightDOS;
            WeightCone = weightCone;
            WeightBale = weightBale;
            GetTotal = getTotal;

        }
        
        public int? IdSOP { get; set; }
        public string NoSOP { get; set; }
        public string MaterialName { get; set; }
        public string InputLot { get; set; }
        public double? WeightBruto { get; set; }
        public string WeightDOS { get; set; }
        public string WeightCone { get; set; }
        public double? WeightBale { get; set; }
        public double? GetTotal { get; set; }

        public void Setidsop(int? newIdSOP)
        {
            if (newIdSOP != IdSOP)
            {
                IdSOP = newIdSOP;
            }
        }
        public void SetNoSPP(string newNoSPP)
        {
            if (newNoSPP != NoSOP)
            {
                NoSOP = newNoSPP;
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

        public void SetWeightDOS(string newWeightDOS)
        {
            if (newWeightDOS != WeightDOS)
            {
                WeightDOS = newWeightDOS;
            }
        }

        public void SetWeightCone(string newWeightCone)
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
