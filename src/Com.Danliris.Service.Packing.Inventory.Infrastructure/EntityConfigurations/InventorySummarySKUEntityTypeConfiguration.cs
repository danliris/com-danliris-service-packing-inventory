using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class InventorySummarySKUEntityTypeConfiguration : IEntityTypeConfiguration<InventorySummarySKUModel>
    {
        public void Configure(EntityTypeBuilder<InventorySummarySKUModel> configuration)
        {
            configuration
                .HasKey(inventorySummary => inventorySummary.Id);

            configuration
                .Property(inventorySummary => inventorySummary.CreatedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventorySummary => inventorySummary.CreatedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventorySummary => inventorySummary.LastModifiedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventorySummary => inventorySummary.LastModifiedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventorySummary => inventorySummary.DeletedAgent)
                .HasMaxLength(128);

            configuration
                .Property(inventorySummary => inventorySummary.DeletedBy)
                .HasMaxLength(128);

            configuration
                .Property(inventorySummary => inventorySummary.Storage)
                .HasMaxLength(1024);

            configuration
                .Property(inventorySummary => inventorySummary.UOMUnit)
                .HasMaxLength(32);
        }
    }
}
