using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class DocumentItemDto
    {
        public DocumentItemDto(ProductSKUInventoryMovementModel item, ProductSKUModel product, UnitOfMeasurementModel uom, CategoryModel category)
        {
            Product = new ProductSKUDto(product, uom, category);
            Quantity = item.Quantity;
            Remark = item.Remark;
        }

        public ProductSKUDto Product { get; }
        public double Quantity { get; }
        public string Remark { get; }
    }
}