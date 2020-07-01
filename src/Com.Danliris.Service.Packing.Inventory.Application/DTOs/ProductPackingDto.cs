using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.DTOs
{
    public class ProductPackingDto
    {
        public ProductPackingDto(ProductPackingModel productPacking, ProductSKUModel product, UnitOfMeasurementModel uom, UnitOfMeasurementModel skuUOM, CategoryModel skuCategory)
        {
            Id = productPacking.Id;
            Code = productPacking.Code;
            Name = productPacking.Name;
            LasModifiedUtc = productPacking.LastModifiedUtc;
            ProductSKU = new ProductSKUDto(product, skuUOM, skuCategory);
            UOM = new UnitOfMeasurementDto(uom);
            PackingSize = productPacking.PackingSize;
        }

        public int Id { get; }
        public string Code { get; }
        public string Name { get; }
        public DateTime LasModifiedUtc { get; }
        public ProductSKUDto ProductSKU { get; }
        public UnitOfMeasurementDto UOM { get; }
        public double PackingSize { get; }
    }
}
