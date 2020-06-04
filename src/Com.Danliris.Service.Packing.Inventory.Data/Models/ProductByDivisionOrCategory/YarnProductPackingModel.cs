using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory
{
    public class YarnProductPackingModel : StandardEntity
    {
        public YarnProductPackingModel()
        {

        }

        public YarnProductPackingModel(
            string code,
            int yarnProductSKUId,
            int productSKUId,
            int productPackingId,
            string uomUnit,
            double packingSize
            )
        {
            Code = code;
            YarnProductSKUId = yarnProductSKUId;
            ProductSKUId = productSKUId;
            ProductPackingId = productPackingId;
            UOMUnit = uomUnit;
            PackingSize = packingSize;
        }

        [MaxLength(64)]
        public string Code { get; }
        public int YarnProductSKUId { get; }
        public int ProductSKUId { get; }
        public int ProductPackingId { get; }
        [MaxLength(64)]
        public string UOMUnit { get; }
        public double PackingSize { get; }
    }
}
