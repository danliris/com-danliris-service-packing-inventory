using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class PackagingStockEntityTypeConfiguration : IEntityTypeConfiguration<PackagingStockModel>
    {
        public void Configure(EntityTypeBuilder<PackagingStockModel> builder)
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
                .Property(s => s.DyeingPrintingProductionOrderId)
                .HasMaxLength(128);

            builder
              .Property(s => s.UomUnit)
              .HasMaxLength(32);

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
                .Property(s => s.Length)
                .HasColumnType("decimal(18,2)");
            //builder
            //    .Property(s => s.PackagingCode)
            //    .HasMaxLength(128);
        }
    }
}
