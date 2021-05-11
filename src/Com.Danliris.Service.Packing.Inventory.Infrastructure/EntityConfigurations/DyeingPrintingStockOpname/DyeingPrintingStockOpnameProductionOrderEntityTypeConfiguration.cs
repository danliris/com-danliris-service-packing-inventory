using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.DyeingPrintingStockOpname
{
    public class DyeingPrintingStockOpnameProductionOrderEntityTypeConfiguration : IEntityTypeConfiguration<DyeingPrintingStockOpnameProductionOrderModel>
    {
        public void Configure(EntityTypeBuilder<DyeingPrintingStockOpnameProductionOrderModel> builder)
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
              .Property(s => s.Remark)
              .HasMaxLength(128);

            builder
              .Property(s => s.Grade)
              .HasMaxLength(128);

            builder
              .Property(s => s.Status)
              .HasMaxLength(128);

            builder
              .Property(s => s.ProductionOrderType)
              .HasMaxLength(512);

            builder
              .Property(s => s.YarnMaterialId)
              .HasMaxLength(1024);

            builder
              .Property(s => s.PackingInstruction)
              .HasMaxLength(4096);


            builder
                .Property(s => s.PackagingUnit)
                .HasMaxLength(128);

            builder
                .Property(s => s.PackagingQty)
                .HasColumnType("decimal(18,2)");

            builder
                .Property(s => s.PackagingType)
                .HasMaxLength(128);

            builder
              .Property(s => s.MaterialName)
              .HasMaxLength(1024);

            builder
              .Property(s => s.MaterialConstructionName)
              .HasMaxLength(1024);

            builder
              .Property(s => s.MaterialWidth)
              .HasMaxLength(1024);


            builder
             .Property(s => s.DocumentNo)
             .HasMaxLength(128);

           
        }
    }
}
