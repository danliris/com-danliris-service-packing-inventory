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
        public string Code { get; private set; }
        [MaxLength(128)]
        public string Composition { get; private set; }
        [MaxLength(128)]
        public string Construction { get; private set; }
        [MaxLength(64)]
        public string Width { get; private set; }
        [MaxLength(64)]
        public string Design { get; private set; }
        [MaxLength(32)]
        public string Grade { get; private set; }
        [MaxLength(64)]
        public string UOMUnit { get; private set; }
        public int ProductSKUId { get; private set; }
    }
}
