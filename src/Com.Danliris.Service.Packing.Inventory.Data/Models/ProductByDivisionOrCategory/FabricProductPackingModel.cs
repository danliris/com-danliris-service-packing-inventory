using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory
{
    public class FabricProductPackingModel : StandardEntity
    {
        public FabricProductPackingModel()
        {

        }

        public FabricProductPackingModel(
            string code,
            int fabricProductSKUId,
            int productSKUId,
            int productPackingId,
            string uomUnit,
            double packingSize
            )
        {
            Code = code;
            FabricProductSKUId = fabricProductSKUId;
            ProductSKUId = productSKUId;
            ProductPackingId = productPackingId;
            UOMUnit = uomUnit;
            PackingSize = packingSize;
        }

        [MaxLength(64)]
        public string Code { get; private set; }
        public int FabricProductSKUId { get; private set; }
        public int ProductSKUId { get; private set; }
        public int ProductPackingId { get; private set; }
        [MaxLength(64)]
        public string UOMUnit { get; private set; }
        public double PackingSize { get; private set; }
    }
}
