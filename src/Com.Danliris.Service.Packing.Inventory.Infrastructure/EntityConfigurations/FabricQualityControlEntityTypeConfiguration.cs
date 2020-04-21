using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class FabricQualityControlEntityTypeConfiguration : IEntityTypeConfiguration<FabricQualityControlModel>
    {
        public void Configure(EntityTypeBuilder<FabricQualityControlModel> builder)
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
                .Property(s => s.UId)
                .HasMaxLength(256);

            builder
                .Property(s => s.Code)
                .HasMaxLength(32);

            builder
                .Property(s => s.Group)
                .HasMaxLength(4096);

            builder
                .Property(s => s.MachineNoIm)
                .HasMaxLength(4096);

            builder
                .Property(s => s.OperatorIm)
                .HasMaxLength(4096);

            builder
               .Property(s => s.DyeingPrintingAreaInputBonNo)
               .HasMaxLength(64);

            builder
               .Property(s => s.ProductionOrderNo)
               .HasMaxLength(128);

        }
    }
}
