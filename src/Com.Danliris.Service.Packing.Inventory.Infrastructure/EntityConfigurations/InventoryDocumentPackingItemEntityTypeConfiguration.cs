using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class InventoryDocumentPackingItemEntityTypeConfiguration : IEntityTypeConfiguration<InventoryDocumentPackingItemModel>
    {
        public void Configure(EntityTypeBuilder<InventoryDocumentPackingItemModel> configuration)
        {
            configuration
                .HasKey(inventoryDocumentItem => inventoryDocumentItem.Id);

            configuration
                .Property(inventoryDocumentItem => inventoryDocumentItem.CreatedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocumentItem => inventoryDocumentItem.CreatedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocumentItem => inventoryDocumentItem.LastModifiedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocumentItem => inventoryDocumentItem.LastModifiedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocumentItem => inventoryDocumentItem.DeletedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocumentItem => inventoryDocumentItem.DeletedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryDocumentItem => inventoryDocumentItem.UOMUnit)
                .HasMaxLength(32);
        }
    }
}
