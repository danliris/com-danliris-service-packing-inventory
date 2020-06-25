using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteConfig : IEntityTypeConfiguration<GarmentShippingLocalReturnNoteModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingLocalReturnNoteModel> builder)
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
                .Property(s => s.ReturnNoteNo)
                .HasMaxLength(50);

            builder
                .HasIndex(i => i.ReturnNoteNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");

            builder
                .Property(s => s.Description)
                .HasMaxLength(4000);

            builder
                .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.ReturnNoteId);

            builder
                .HasOne(h => h.SalesNote)
                .WithMany()
                .HasForeignKey(f => f.SalesNoteId);
        }
    }
}
