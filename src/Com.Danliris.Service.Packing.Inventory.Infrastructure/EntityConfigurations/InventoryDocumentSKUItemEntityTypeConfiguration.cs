using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class InventoryDocumentSKUItemEntityTypeConfiguration : IEntityTypeConfiguration<InventoryDocumentSKUItemModel>
    {
        public void Configure(EntityTypeBuilder<InventoryDocumentSKUItemModel> configuration)
        {
            configuration
                .Property(inventoryDocument => inventoryDocument.UOMUnit)
                .HasMaxLength(32);
        }
    }
}
