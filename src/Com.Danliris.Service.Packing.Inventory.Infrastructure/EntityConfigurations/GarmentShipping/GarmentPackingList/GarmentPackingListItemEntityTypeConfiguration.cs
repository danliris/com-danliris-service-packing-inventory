using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListItemEntityTypeConfiguration : IEntityTypeConfiguration<GarmentPackingListItemModel>
    {
        public void Configure(EntityTypeBuilder<GarmentPackingListItemModel> builder)
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
                .Property(s => s.RONo)
                .HasMaxLength(50);

            builder
                .Property(s => s.SCNo)
                .HasMaxLength(50);

            builder
                .Property(s => s.BuyerBrandName)
                .HasMaxLength(50);

            builder
                .Property(s => s.ComodityCode)
                .HasMaxLength(50);

            builder
                .Property(s => s.ComodityName)
                .HasMaxLength(255);

            builder
                .Property(s => s.ComodityDescription)
                .HasMaxLength(1000);

            builder
                .Property(s => s.UomUnit)
                .HasMaxLength(50);

            builder
                .Property(s => s.Valas)
                .HasMaxLength(50);

            builder
                .Property(s => s.UnitCode)
                .HasMaxLength(50);

            builder
                .Property(s => s.Article)
                .HasMaxLength(1000);

            builder
                .Property(s => s.OrderNo)
                .HasMaxLength(100);

            builder
                .Property(s => s.Description)
                .HasMaxLength(1000);

            builder
                .Property(s => s.DescriptionMd)
                .HasMaxLength(1000);

            builder
                .HasMany(h => h.Details)
                .WithOne()
                .HasForeignKey(f => f.PackingListItemId);
        }
    }
}
