using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LetterOfCredit
{
    public class GarmentLetterOfCreditEntityTypeConfiguration : IEntityTypeConfiguration<GarmentShippingLetterOfCreditModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingLetterOfCreditModel> builder)
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

            builder.Property(s => s.DocumentCreditNo).HasMaxLength(50);
            builder.Property(s => s.IssuedBank).HasMaxLength(200);
            builder.Property(s => s.ApplicantCode).HasMaxLength(100);
            builder.Property(s => s.ApplicantName).HasMaxLength(255);
            builder.Property(s => s.ExpirePlace).HasMaxLength(255);
            builder.Property(s => s.LCCondition).HasMaxLength(20);
            builder.Property(s => s.UomUnit).HasMaxLength(50);

            builder
                .HasIndex(i => i.DocumentCreditNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");
        }
    }
}
