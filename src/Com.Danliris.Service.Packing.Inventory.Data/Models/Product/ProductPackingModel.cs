using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class ProductPackingModel : StandardEntity
    {
        public ProductPackingModel()
        {

        }

        // UOM is packing type
        public ProductPackingModel(int productSKUId, int uomId, double packingSize, string code, string name)
        {
            ProductSKUId = productSKUId;
            UOMId = uomId;
            PackingSize = packingSize;
            Code = code;
            Name = name;
        }

        public int ProductSKUId { get; }
        public int UOMId { get; }
        public double PackingSize { get; }
        [MaxLength(64)]
        public string Code { get; }
        [MaxLength(512)]
        public string Name { get; }
    }
}
