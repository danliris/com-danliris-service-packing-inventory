using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    class ProductSKUEntityTypeConfiguration : IEntityTypeConfiguration<ProductSKUModel>
    {
        public void Configure(EntityTypeBuilder<ProductSKUModel> productSKUConfiguration)
        {
            productSKUConfiguration
                .Property(productSKU => productSKU.Code)
                .HasMaxLength(32);

            productSKUConfiguration
                .Property(productSKU => productSKU.Composition)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.Construction)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.Currency)
                .HasMaxLength(1024);

            productSKUConfiguration
                .Property(productSKU => productSKU.Description)
                .HasMaxLength(1024);

            productSKUConfiguration
                .Property(productSKU => productSKU.Design)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.Grade)
                .HasMaxLength(32);

            productSKUConfiguration
                .Property(productSKU => productSKU.Lot)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.Name)
                .HasMaxLength(256);

            productSKUConfiguration
                .Property(productSKU => productSKU.ProductType)
                .HasMaxLength(32);

            productSKUConfiguration
                .Property(productSKU => productSKU.SKUCode)
                .HasMaxLength(32);

            productSKUConfiguration
                .Property(productSKU => productSKU.SKUId)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.Tags)
                .HasMaxLength(512);

            productSKUConfiguration
                .Property(productSKU => productSKU.UOM)
                .HasMaxLength(1024);

            productSKUConfiguration
                .Property(productSKU => productSKU.Width)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.WovenType)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.YarnType1)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.YarnType2)
                .HasMaxLength(128);
        }
    }
}