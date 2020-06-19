using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOEntityTypeConfiguration : IEntityTypeConfiguration<GarmentShippingExportSalesDOModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingExportSalesDOModel> builder)
        {
            /* StandardEntity */
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CreatedAgent).HasMaxLength(128);
            builder.Property(s => s.CreatedBy).HasMaxLength(128);
            builder.Property(s => s.LastModifiedAgent).HasMaxLength(128);
            builder.Property(s => s.LastModifiedBy).HasMaxLength(128);
            builder.Property(s => s.DeletedAgent).HasMaxLength(128);
            builder.Property(s => s.DeletedBy).HasMaxLength(128);
            builder.HasQueryFilter(f => !f.IsDeleted);
            /* StandardEntity */

            builder
                .Property(s => s.InvoiceNo)
                .HasMaxLength(50);

            builder
                .Property(s => s.ExportSalesDONo)
                .HasMaxLength(50);

            builder
                 .Property(s => s.BuyerAgentCode)
                 .HasMaxLength(100);

            builder
                .Property(s => s.BuyerAgentName)
                .HasMaxLength(255);

            builder
                .Property(s => s.To)
                .HasMaxLength(255);

            builder
                 .Property(s => s.UnitCode)
                 .HasMaxLength(100);

            builder
                .Property(s => s.UnitName)
                .HasMaxLength(255);

            builder
                .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.ExportSalesDOId);
        }
    }
}
