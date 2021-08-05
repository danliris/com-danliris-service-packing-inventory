using Com.Danliris.Service.Packing.Inventory.Domain.Exceptions;
using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Domain.Product
{
    public class ProductCatalog : StandardEntity
    {
        public ProductCatalog()
        {

        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        //public string PictureFileName { get; set; }

        //public string PictureUri { get; set; }

        public int ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        // Quantity in stock
        public decimal AvailableStock { get; set; }

        // Available stock at which we should reorder
        public decimal RestockThreshold { get; set; }


        // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
        public decimal MaxStockThreshold { get; set; }

        /// <summary>
        /// True if item is on reorder
        /// </summary>
        public bool OnReorder { get; set; }

        public decimal RemoveStock(decimal quantity)
        {
            if (AvailableStock == 0)
            {
                throw new ProductCatalogDomainException($"Empty stock, product item {Name}");
            }

            if (quantity <= 0)
            {
                throw new ProductCatalogDomainException($"Item quantity should be greater than zero");
            }

            AvailableStock -= quantity;

            return AvailableStock;
        }

        public decimal AddStock(decimal quantity)
        {
            if (quantity <= 0)
            {
                throw new ProductCatalogDomainException($"Item quantity should be greater than zero");
            }

            AvailableStock += quantity;

            return AvailableStock;
        }
    }
}
