using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class DyeingPrintingAreaMovementEntityTypeConfiguration : IEntityTypeConfiguration<DyeingPrintingAreaMovementModel>
    {
        public void Configure(EntityTypeBuilder<DyeingPrintingAreaMovementModel> builder)
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
                .Property(s => s.Area)
                .HasMaxLength(64);

            builder.HasIndex(s => s.BonNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");

            builder
                .Property(s => s.BonNo)
                .HasMaxLength(64);

            builder
                .Property(s => s.Shift)
                .HasMaxLength(64);

            builder
                .Property(s => s.ProductionOrderCode)
                .HasMaxLength(32);

            builder
                .Property(s => s.ProductionOrderNo)
                .HasMaxLength(128);

            builder
                .Property(s => s.Buyer)
                .HasMaxLength(4096);

            builder
                .Property(s => s.PackingInstruction)
                .HasMaxLength(4096);

            builder
                .Property(s => s.ProductionOrderType)
                .HasMaxLength(4096);

            builder
               .Property(s => s.CartNo)
               .HasMaxLength(128);

            builder
               .Property(s => s.Construction)
               .HasMaxLength(1024);

            builder
               .Property(s => s.MaterialCode)
               .HasMaxLength(32);

            builder
               .Property(s => s.MaterialName)
               .HasMaxLength(4096);

            builder
              .Property(s => s.MaterialConstructionCode)
              .HasMaxLength(32);

            builder
              .Property(s => s.MaterialConstructionName)
              .HasMaxLength(4096);

            builder
              .Property(s => s.MaterialWidth)
              .HasMaxLength(1024);

            builder
              .Property(s => s.UnitCode)
              .HasMaxLength(1024);

            builder
              .Property(s => s.UnitName)
              .HasMaxLength(4096);

            builder
              .Property(s => s.Color)
              .HasMaxLength(4096);

            builder
              .Property(s => s.Motif)
              .HasMaxLength(4096);

            builder
              .Property(s => s.Mutation)
              .HasMaxLength(64);

            builder
              .Property(s => s.UOMUnit)
              .HasMaxLength(32);

            builder
              .Property(s => s.Status)
              .HasMaxLength(32);

            builder
                .Property(s => s.Balance)
                .HasColumnType("decimal(18,2)");

            builder
              .Property(s => s.Grade)
              .HasMaxLength(4096);

            builder
              .Property(s => s.SourceArea)
              .HasMaxLength(64);

            builder
              .Property(s => s.Remark)
              .HasMaxLength(1024);
        }
    }
}
