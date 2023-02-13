using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.SalesExport
{
    public class GarmentShippingExportSalesContractConfig : IEntityTypeConfiguration<GarmentShippingExportSalesContractModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingExportSalesContractModel> builder)
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
                .Property(s => s.SalesContractNo)
                .HasMaxLength(50);

            builder
                .HasIndex(i => i.SalesContractNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");

            builder
                .Property(s => s.TransactionTypeCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.TransactionTypeName)
                .HasMaxLength(250);

            builder
                .Property(s => s.BuyerCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerName)
                .HasMaxLength(250);

            builder
                .Property(s => s.BuyerNPWP)
                .HasMaxLength(50);

            builder
                .Property(s => s.BuyerAddress)
                .HasMaxLength(4000);

            builder
               .Property(s => s.SellerName)
               .HasMaxLength(100);

            builder
               .Property(s => s.SellerNPWP)
               .HasMaxLength(50);

            builder
               .Property(s => s.SellerPosition)
               .HasMaxLength(100);

            builder
                .Property(s => s.SellerAddress)
                .HasMaxLength(4000);
            
            builder
                 .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.ExportSalesContractId);
        }
    }
}
