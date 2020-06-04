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
        public string Code { get; }
        public int FabricProductSKUId { get; }
        public int ProductSKUId { get; }
        public int ProductPackingId { get; }
        [MaxLength(64)]
        public string UOMUnit { get; }
        public double PackingSize { get; }
    }
}
