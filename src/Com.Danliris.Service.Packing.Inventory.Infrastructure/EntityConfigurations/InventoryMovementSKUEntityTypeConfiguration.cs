using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class InventoryMovementSKUEntityTypeConfiguration : IEntityTypeConfiguration<InventoryMovementSKUModel>
    {
        public void Configure(EntityTypeBuilder<InventoryMovementSKUModel> configuration)
        {
            configuration
                .HasKey(inventoryMovement => inventoryMovement.Id);

            configuration
                .Property(inventoryMovement => inventoryMovement.CreatedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryMovement => inventoryMovement.CreatedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryMovement => inventoryMovement.LastModifiedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryMovement => inventoryMovement.LastModifiedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryMovement => inventoryMovement.DeletedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventoryMovement => inventoryMovement.DeletedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventoryMovement => inventoryMovement.Storage)
                .HasMaxLength(1024);

            configuration
                .Property(inventoryMovement => inventoryMovement.Type)
                .HasMaxLength(32);

            configuration
                .Property(inventoryMovement => inventoryMovement.UOMUnit)
                .HasMaxLength(32);
        }
    }
}
