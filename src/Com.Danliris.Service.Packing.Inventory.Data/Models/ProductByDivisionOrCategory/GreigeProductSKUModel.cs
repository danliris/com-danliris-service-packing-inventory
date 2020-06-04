using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory
{
    public class GreigeProductSKUModel : StandardEntity
    {
        public GreigeProductSKUModel()
        {

        }

        public GreigeProductSKUModel(
            string code,
            string wovenType,
            string construction,
            string width,
            // pakan
            string warp,
            // lusi
            string weft,
            string grade,
            string uomUnit
            )
        {
            Code = code;
            WovenType = wovenType;
            Construction = construction;
            Width = width;
            Warp = warp;
            Weft = weft;
            Grade = grade;
            UOMUnit = uomUnit;
        }

        [MaxLength(64)]
        public string Code { get; }
        [MaxLength(128)]
        public string WovenType { get; }
        [MaxLength(128)]
        public string Construction { get; }
        [MaxLength(64)]
        public string Width { get; }
        [MaxLength(64)]
        public string Warp { get; }
        [MaxLength(64)]
        public string Weft { get; }
        [MaxLength(32)]
        public string Grade { get; }
        [MaxLength(64)]
        public string UOMUnit { get; }
    }
}
