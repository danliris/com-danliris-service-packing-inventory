using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    class SKUInventoryDocumentItemEntityTypeConfiguration : IEntityTypeConfiguration<SKUInventoryDocumentItemModel>
    {
        public void Configure(EntityTypeBuilder<SKUInventoryDocumentItemModel> skuInventoryDocumentItemConfiguration)
        {
            skuInventoryDocumentItemConfiguration
                .Property(skuInventoryDocumentItem => skuInventoryDocumentItem.Remark)
                .HasMaxLength(1024);

            skuInventoryDocumentItemConfiguration
                .Property(skuInventoryDocumentItem => skuInventoryDocumentItem.SKU)
                .HasMaxLength(1024);
        }
    }
}