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
        public ProductPackingModel(int productSKUId, int uomId, double packingSize, string code, string name, int createdYear, int createdMonth)
        {
            ProductSKUId = productSKUId;
            UOMId = uomId;
            PackingSize = packingSize;
            Code = code;
            Name = name;
            CreatedYear = createdYear;
            CreatedMonth = createdMonth;
        }

        public int ProductSKUId { get; private set; }
        public int UOMId { get; private set; }
        public double PackingSize { get; private set; }
        [MaxLength(64)]
        public string Code { get; private set; }
        [MaxLength(512)]
        public string Name { get; private set; }
        public int CreatedYear { get; private set; }
        public int CreatedMonth { get; private set; }
    }
}
