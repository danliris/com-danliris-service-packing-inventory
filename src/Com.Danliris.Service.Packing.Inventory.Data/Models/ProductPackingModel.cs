using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class ProductPackingModel : StandardEntity
    {
        public ProductPackingModel()
        {

        }

        public ProductPackingModel(
            string code,
            string packingType,
            decimal quantity,
            int skuId
        )
        {
            Code = code;
            Quantity = quantity;
            SKUId = skuId;
            PackingType = packingType;
        }

        public string Code { get; set; }
        public string PackingType { get; private set; }
        public decimal Quantity { get; set; }
        public int SKUId { get; private set; }
    }
}