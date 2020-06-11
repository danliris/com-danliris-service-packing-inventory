using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
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
                .HasMaxLength(128);

            builder
                .Property(s => s.Type)
                .HasMaxLength(32);

            builder
                .Property(s => s.DyeingPrintingAreaDocumentBonNo)
                .HasMaxLength(64);

            builder
                .Property(s => s.ProductionOrderNo)
                .HasMaxLength(128);

            builder
                .Property(s => s.Buyer)
                .HasMaxLength(4096);

            builder
               .Property(s => s.CartNo)
               .HasMaxLength(128);

            builder
               .Property(s => s.Construction)
               .HasMaxLength(1024);

            builder
              .Property(s => s.Unit)
              .HasMaxLength(4096);

            builder
              .Property(s => s.Color)
              .HasMaxLength(4096);

            builder
              .Property(s => s.Motif)
              .HasMaxLength(4096);

            builder
              .Property(s => s.UomUnit)
              .HasMaxLength(32);

            builder
              .Property(s => s.AvalType)
              .HasMaxLength(128);
        }
    }
}
