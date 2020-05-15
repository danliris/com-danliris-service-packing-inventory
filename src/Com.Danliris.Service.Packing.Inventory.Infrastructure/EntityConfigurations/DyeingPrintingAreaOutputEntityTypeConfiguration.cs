using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class DyeingPrintingAreaOutputEntityTypeConfiguration : IEntityTypeConfiguration<DyeingPrintingAreaOutputModel>
    {
        public void Configure(EntityTypeBuilder<DyeingPrintingAreaOutputModel> builder)
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

            //builder.HasIndex(s => s.BonNo)
            //    .IsUnique()
            //    .HasFilter("[IsDeleted]=(0)");

            builder
                .Property(s => s.BonNo)
                .HasMaxLength(64);

            builder
                .Property(s => s.Shift)
                .HasMaxLength(64);

            builder
               .Property(s => s.DestinationArea)
               .HasMaxLength(64);

            builder
                .Property(s => s.Group)
                .HasMaxLength(16);


            builder
                .Property(s => s.DeliveryOrderSalesNo)
                .HasMaxLength(128);
        }
    }
}
