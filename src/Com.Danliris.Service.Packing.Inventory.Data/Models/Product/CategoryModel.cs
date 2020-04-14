using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class CategoryModel : StandardEntity
    {
        public CategoryModel()
        {

        }

        public CategoryModel(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; set; }
        public string Code { get; set; }
    }
}
