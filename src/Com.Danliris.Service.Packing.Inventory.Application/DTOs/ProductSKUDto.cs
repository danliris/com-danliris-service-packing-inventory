using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.DTOs
{
    public class ProductSKUDto
    {
        public ProductSKUDto(ProductSKUModel product, UnitOfMeasurementModel uom, CategoryModel category)
        {
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            Description = product.Description;
            LasModifiedUtc = product.LastModifiedUtc;
            UOMId = uom.Id;
            UOMUnit = uom.Unit;
            UOM = new UnitOfMeasurementDto(uom);
            Category = new CategoryDto(category);
        }

        public int Id { get; }
        public string Code { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime LasModifiedUtc { get; }
        public int UOMId { get; }
        public string UOMUnit { get; }
        public UnitOfMeasurementDto UOM { get; }
        public CategoryDto Category { get; set; }
    }
}