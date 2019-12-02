using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    class PackagingInventoryDocumentEntityTypeConfiguration : IEntityTypeConfiguration<PackagingInventoryDocumentModel>
    {
        public void Configure(EntityTypeBuilder<PackagingInventoryDocumentModel> packagingInventoryDocumentConfiguration)
        {
            packagingInventoryDocumentConfiguration
                .Property(packagingInventoryDocument => packagingInventoryDocument.DocumentNo)
                .HasMaxLength(32);

            packagingInventoryDocumentConfiguration
                .Property(packagingInventoryDocument => packagingInventoryDocument.ReferenceNo)
                .HasMaxLength(128);

            packagingInventoryDocumentConfiguration
                .Property(packagingInventoryDocument => packagingInventoryDocument.ReferenceType)
                .HasMaxLength(128);

            packagingInventoryDocumentConfiguration
                .Property(packagingInventoryDocument => packagingInventoryDocument.Status)
                .HasMaxLength(32);

            packagingInventoryDocumentConfiguration
                .Property(packagingInventoryDocument => packagingInventoryDocument.Storage)
                .HasMaxLength(1024);

            packagingInventoryDocumentConfiguration
                .Property(packagingInventoryDocument => packagingInventoryDocument.Remark)
                .HasMaxLength(1024);

            packagingInventoryDocumentConfiguration
                .HasMany(packagingInventoryDocument => packagingInventoryDocument.Items)
                .WithOne()
                .HasForeignKey("PackagingIventoryDocumentId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}