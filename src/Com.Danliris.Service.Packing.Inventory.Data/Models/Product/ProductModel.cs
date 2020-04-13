using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class ProductModel : StandardEntity
    {
        public ProductModel()
        {

        }

        public ProductModel(
            string code,
            string name,
            int uomId,
            int categoryId,
            int packingId,
            string description
            )
        {
            Code = code;
            Name = name;
            UOMId = uomId;
            CategoryId = categoryId;
            PackingId = packingId;
            Description = description;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public int UOMId { get; set; }
        public int CategoryId { get; set; }
        public int PackingId { get; set; }
        public string Description { get; set; }
    }
}
