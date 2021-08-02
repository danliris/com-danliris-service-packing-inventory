using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;

namespace Com.Danliris.Service.Packing.Inventory.Application.DTOs
{
    public class CategoryDto
    {
        public CategoryDto(CategoryModel category)
        {
            Id = category.Id;
            Code = category.Code;
            Name = category.Name;
        }

        public int Id { get; }
        public string Code { get; }
        public string Name { get; }
    }
}