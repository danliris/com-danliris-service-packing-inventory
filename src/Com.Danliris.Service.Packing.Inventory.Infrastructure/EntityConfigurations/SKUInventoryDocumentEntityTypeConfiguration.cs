using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    class SKUInventoryDocumentEntityTypeConfiguration : IEntityTypeConfiguration<SKUInventoryDocumentModel>
    {
        public void Configure(EntityTypeBuilder<SKUInventoryDocumentModel> skuInventoryDocumentConfiguration)
        {
            skuInventoryDocumentConfiguration
                .Property(skuInventoryDocument => skuInventoryDocument.DocumentNo)
                .HasMaxLength(32);

            skuInventoryDocumentConfiguration
                .Property(skuInventoryDocument => skuInventoryDocument.ReferenceNo)
                .HasMaxLength(128);

            skuInventoryDocumentConfiguration
                .Property(skuInventoryDocument => skuInventoryDocument.ReferenceType)
                .HasMaxLength(128);

            skuInventoryDocumentConfiguration
                .Property(skuInventoryDocument => skuInventoryDocument.Status)
                .HasMaxLength(32);

            skuInventoryDocumentConfiguration
                .Property(skuInventoryDocument => skuInventoryDocument.Storage)
                .HasMaxLength(1024);

            skuInventoryDocumentConfiguration
                .Property(skuInventoryDocument => skuInventoryDocument.Remark)
                .HasMaxLength(1024);

            skuInventoryDocumentConfiguration
                .HasMany(skuInventoryDocument => skuInventoryDocument.Items)
                .WithOne()
                .HasForeignKey("SkuInventoryDocumentId");
        }
    }
}