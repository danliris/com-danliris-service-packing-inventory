using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory
{
    public class GreigeProductPackingModel : StandardEntity
    {
        public GreigeProductPackingModel()
        {

        }

        public GreigeProductPackingModel(
            string code,
            int greigeProductSKUId,
            int productSKUId,
            int productPackingId,
            string uomUnit,
            double packingSize
            )
        {
            Code = code;
            GreigeProductSKUId = greigeProductSKUId;
            ProductSKUId = productSKUId;
            ProductPackingId = productPackingId;
            UOMUnit = uomUnit;
            PackingSize = packingSize;
        }

        [MaxLength(64)]
        public string Code { get; }
        public int GreigeProductSKUId { get; }
        public int ProductSKUId { get; }
        public int ProductPackingId { get; }
        [MaxLength(64)]
        public string UOMUnit { get; }
        public double PackingSize { get; }
    }
}
