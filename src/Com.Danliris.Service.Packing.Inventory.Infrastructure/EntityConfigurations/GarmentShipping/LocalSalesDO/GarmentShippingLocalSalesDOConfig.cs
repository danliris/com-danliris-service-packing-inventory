using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDOConfig : IEntityTypeConfiguration<GarmentShippingLocalSalesDOModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingLocalSalesDOModel> builder)
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
                .Property(s => s.LocalSalesNoteNo)
                .HasMaxLength(50);

            builder
                .Property(s => s.LocalSalesDONo)
                .HasMaxLength(50);

            builder
                 .Property(s => s.BuyerCode)
                 .HasMaxLength(100);

            builder
                .Property(s => s.BuyerName)
                .HasMaxLength(255);

            builder
                .Property(s => s.To)
                .HasMaxLength(255);

            builder
                 .Property(s => s.StorageDivision)
                 .HasMaxLength(255);

            builder
                .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.LocalSalesDOId);
        }
    }
}
