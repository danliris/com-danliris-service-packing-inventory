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
                .Property(s => s.itemNoSOP)
                .HasMaxLength(128);

            builder
                .Property(s => s.itemMaterialName)
                .HasMaxLength(128);

            builder
                .Property(s => s.itemGrade)
                .HasMaxLength(128);

            builder
                .Property(s => s.itemType)
                .HasMaxLength(128);

            builder
                .Property(s => s.itemCode)
                .HasMaxLength(128);

            builder
                .Property(s => s.inputBale)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.inputPiece)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.inputMeter)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.inputKg)
                .HasColumnType("decimal(18,2)");
        }
    }
}
