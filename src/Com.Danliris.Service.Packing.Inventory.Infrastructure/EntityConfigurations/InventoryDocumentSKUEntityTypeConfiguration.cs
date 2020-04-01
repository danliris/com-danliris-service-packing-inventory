using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class InventoryDocumentSKUEntityTypeConfiguration : IEntityTypeConfiguration<InventoryDocumentSKUModel>
    {
        public void Configure(EntityTypeBuilder<InventoryDocumentSKUModel> configuration)
        {
            configuration
                .HasKey(inventoryDocument => inventoryDocument.Id);

            configuration
                .Property(inventoryDocument => inventoryDocument.CreatedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocument => inventoryDocument.CreatedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocument => inventoryDocument.LastModifiedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocument => inventoryDocument.LastModifiedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocument => inventoryDocument.DeletedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocument => inventoryDocument.DeletedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocument => inventoryDocument.Code)
                .HasMaxLength(32);

            configuration
                .Property(inventoryDocument => inventoryDocument.ReferenceNo)
                .HasMaxLength(256);

            configuration
                .Property(inventoryDocument => inventoryDocument.ReferenceType)
                .HasMaxLength(256);

            configuration
                .Property(inventoryDocument => inventoryDocument.Remark)
                .HasMaxLength(1024);

            configuration
                .Property(inventoryDocument => inventoryDocument.Type)
                .HasMaxLength(32);

            configuration
                .HasMany(inventoryDocument => inventoryDocument.Items)
                .WithOne()
                .HasForeignKey("InventoryDocumentSKUId");
        }
    }
}
