using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.DTOs
{
    public class ProductPackingDto
    {
        public ProductPackingDto(ProductPackingModel productPacking, ProductSKUModel product, UnitOfMeasurementModel uom)
        {
            Id = productPacking.Id;
            Code = productPacking.Code;
            Name = productPacking.Name;
            LasModifiedUtc = productPacking.LastModifiedUtc;
            UOM = new UnitOfMeasurementDto(uom);
            PackingSize = productPacking.PackingSize;
        }

        public int Id { get; }
        public string Code { get; }
        public string Name { get; }
        public DateTime LasModifiedUtc { get; }
        public UnitOfMeasurementDto UOM { get; }
        public double PackingSize { get; }
    }
}
