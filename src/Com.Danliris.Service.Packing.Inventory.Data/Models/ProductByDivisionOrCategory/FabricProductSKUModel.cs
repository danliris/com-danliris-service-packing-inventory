using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory
{
    public class FabricProductSKUModel : StandardEntity
    {
        public FabricProductSKUModel()
        {

        }

        public FabricProductSKUModel(
            string code,
            string composition,
            string construction,
            string width,
            string design,
            string grade,
            string uomUnit,
            int productSKUId
            )
        {
            Code = code;
            Composition = composition;
            Construction = construction;
            Width = width;
            Design = design;
            Grade = grade;
            UOMUnit = uomUnit;
            ProductSKUId = productSKUId;
        }

        [MaxLength(64)]
        public string Code { get; }
        [MaxLength(128)]
        public string Composition { get; }
        [MaxLength(128)]
        public string Construction { get; }
        [MaxLength(64)]
        public string Width { get; }
        [MaxLength(64)]
        public string Design { get; }
        [MaxLength(32)]
        public string Grade { get; }
        [MaxLength(64)]
        public string UOMUnit { get; }
        public int ProductSKUId { get; }
    }
}
