using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.MaterialDeliveryNoteWeaving
{
    public class ItemsMaterialDeliveryNoteWeavingEntityTypeConfiguration : IEntityTypeConfiguration<ItemsMaterialDeliveryNoteWeavingModel>
    {
        public void Configure(EntityTypeBuilder<ItemsMaterialDeliveryNoteWeavingModel> builder)
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
                .Property(s => s.ItemNoSOP)
                .HasMaxLength(128);

            builder
                .Property(s => s.ItemMaterialName)
                .HasMaxLength(128);

            builder
                .Property(s => s.ItemGrade)
                .HasMaxLength(128);

            builder
                .Property(s => s.ItemType)
                .HasMaxLength(128);

            builder
                .Property(s => s.ItemCode)
                .HasMaxLength(128);

            builder
                .Property(s => s.InputBale)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.InputPiece)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.InputMeter)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.InputKg)
                .HasColumnType("decimal(18,2)");
        }
    }
}
