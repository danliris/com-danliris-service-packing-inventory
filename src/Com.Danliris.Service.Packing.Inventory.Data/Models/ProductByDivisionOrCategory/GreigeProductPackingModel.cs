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
        public string Code { get; private set; }
        public int GreigeProductSKUId { get; private set; }
        public int ProductSKUId { get; private set; }
        public int ProductPackingId { get; private set; }
        [MaxLength(64)]
        public string UOMUnit { get; private set; }
        public double PackingSize { get; private set; }
    }
}
