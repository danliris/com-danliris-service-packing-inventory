using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentShippingPackingList
{
    public class GarmentShippingPackingListDetailEntityTypeConfiguration : IEntityTypeConfiguration<GarmentPackingListDetailModel>
    {
        public void Configure(EntityTypeBuilder<GarmentPackingListDetailModel> builder)
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
                .Property(s => s.Style)
                .HasMaxLength(100);

            builder
                .Property(s => s.Colour)
                .HasMaxLength(100);

            builder
                .HasMany(h => h.Sizes)
                .WithOne()
                .HasForeignKey(f => f.PackingListDetailId);
        }
    }
}
