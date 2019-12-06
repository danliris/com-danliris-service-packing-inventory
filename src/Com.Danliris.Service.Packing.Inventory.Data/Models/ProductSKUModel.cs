using System;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class ProductSKUModel : StandardEntity
    {
        public ProductSKUModel()
        {

        }

        public ProductSKUModel(
            string code,
            string composition,
            string construction,
            string design,
            string grade,
            string lotNo,
            string productType,
            string uomUnit,
            string width,
            string wovenType,
            string yarnType1,
            string yarnType2
        )
        {
            Code = code;
            Composition = composition;
            Construction = construction;
            Design = design;
            Grade = grade;
            LotNo = lotNo;
            ProductType = productType;
            UOMUnit = uomUnit;
            Width = width;
            WovenType = wovenType;
            YarnType1 = yarnType1;
            YarnType2 = yarnType2;

            Name = SetName();
        }

        private string SetName()
        {
            var name = "";

            switch (ProductType)
            {
                case "FABRIC":
                    name = Composition + " " + Construction + " " + Width + " " + Design + " " + Grade;
                    break;
                case "GREIGE":
                    name = WovenType + " " + Construction + " " + Width + " " + YarnType1 + " " + YarnType2 + " " + Grade;
                    break;
                case "YARN":
                    name = YarnType1 + " " + LotNo;
                    break;
                default:
                    break;
            }

            return name;
        }

        public string Code { get; set; }
        public string Composition { get; set; }
        public string Construction { get; set; }
        public string Design { get; set; }
        public string Grade { get; set; }
        public string LotNo { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public string UOMUnit { get; set; }
        public string Width { get; set; }
        public string WovenType { get; set; }
        public string YarnType1 { get; set; }
        public string YarnType2 { get; set; }
    }
}