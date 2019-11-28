using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    class ProductPackagingEntityTypeConfiguration : IEntityTypeConfiguration<ProductPackagingModel>
    {
        public void Configure(EntityTypeBuilder<ProductPackagingModel> productPackagingConfiguration)
        {
            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Code)
                .HasMaxLength(32);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Composition)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Construction)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Currency)
                .HasMaxLength(1024);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Description)
                .HasMaxLength(1024);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Design)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Grade)
                .HasMaxLength(32);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Lot)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Name)
                .HasMaxLength(256);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.ProductType)
                .HasMaxLength(32);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.SKU)
                .HasMaxLength(1000);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.SKUId)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Tags)
                .HasMaxLength(512);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.UOM)
                .HasMaxLength(1024);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.Width)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.WovenType)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.YarnType1)
                .HasMaxLength(128);

            productPackagingConfiguration
                .Property(productPackaging => productPackaging.YarnType2)
                .HasMaxLength(128);
        }
    }
}