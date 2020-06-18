using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.MaterialDeliveryNote
{
    public class ItemsEntityTypeConfiguration : IEntityTypeConfiguration<ItemsModel>
    {
        public void Configure(EntityTypeBuilder<ItemsModel> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.CreatedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.CreatedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.LastModifiedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.LastModifiedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.DeletedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.DeletedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.NoSPP)
                .HasMaxLength(128);

            builder
                .Property(s => s.MaterialName)
                .HasMaxLength(128);

            builder
                .Property(s => s.InputLot)
                .HasMaxLength(128);

            builder
                .Property(s => s.WeightBruto)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.WeightDOS)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.WeightCone)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.WeightBale)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.GetTotal)
                .HasColumnType("decimal(18,2)");

        }
    }
}
