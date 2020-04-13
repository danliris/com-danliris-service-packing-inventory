using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class PackingModel : StandardEntity
    {
        public PackingModel()
        {

        }

        public PackingModel(
            string name,
            double size,
            string description
            )
        {
            Name = name;
            Size = size;
            Description = description;
        }

        public string Name { get; set; }
        public double Size { get; set; }
        public string Description { get; set; }
    }
}
