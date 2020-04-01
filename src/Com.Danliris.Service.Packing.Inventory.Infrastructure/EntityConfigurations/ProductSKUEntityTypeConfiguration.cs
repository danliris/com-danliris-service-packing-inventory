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
                .HasKey(inventoryDocument => inventoryDocument.Id);

            productSKUConfiguration
                .Property(inventoryDocument => inventoryDocument.CreatedAgent)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(inventoryDocument => inventoryDocument.CreatedBy)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(inventoryDocument => inventoryDocument.LastModifiedAgent)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(inventoryDocument => inventoryDocument.LastModifiedBy)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(inventoryDocument => inventoryDocument.DeletedAgent)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(inventoryDocument => inventoryDocument.DeletedBy)
                .HasMaxLength(128);

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
                .Property(productSKU => productSKU.Design)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.Grade)
                .HasMaxLength(32);

            productSKUConfiguration
                .Property(productSKU => productSKU.LotNo)
                .HasMaxLength(128);

            productSKUConfiguration
                .Property(productSKU => productSKU.Name)
                .HasMaxLength(256);

            productSKUConfiguration
                .Property(productSKU => productSKU.ProductType)
                .HasMaxLength(32);

            productSKUConfiguration
                .Property(productSKU => productSKU.UOMUnit)
                .HasMaxLength(64);

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