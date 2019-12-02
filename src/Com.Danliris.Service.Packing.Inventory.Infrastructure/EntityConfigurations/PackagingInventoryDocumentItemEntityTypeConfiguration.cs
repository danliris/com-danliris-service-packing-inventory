using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    class PackagingInventoryDocumentItemEntityTypeConfiguration : IEntityTypeConfiguration<PackagingInventoryDocumentItemModel>
    {
        public void Configure(EntityTypeBuilder<PackagingInventoryDocumentItemModel> packagingInventoryItemDocumentConfiguration)
        {
            packagingInventoryItemDocumentConfiguration
                .Property(packagingInventoryDocumentItem => packagingInventoryDocumentItem.Packaging)
                .HasMaxLength(1024);

            packagingInventoryItemDocumentConfiguration
                .Property(packagingInventoryDocumentItem => packagingInventoryDocumentItem.Remark)
                .HasMaxLength(1024);

            packagingInventoryItemDocumentConfiguration
                .Property(packagingInventoryDocumentItem => packagingInventoryDocumentItem.SKU)
                .HasMaxLength(1024);
        }
    }
}