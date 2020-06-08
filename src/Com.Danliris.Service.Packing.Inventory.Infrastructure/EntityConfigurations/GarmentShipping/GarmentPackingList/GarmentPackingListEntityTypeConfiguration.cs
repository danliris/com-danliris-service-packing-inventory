using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListEntityTypeConfiguration : IEntityTypeConfiguration<GarmentPackingListModel>
    {
        public void Configure(EntityTypeBuilder<GarmentPackingListModel> builder)
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
                .Property(s => s.PackingListType)
                .HasMaxLength(25);

            builder
                .Property(s => s.InvoiceType)
                .HasMaxLength(25);

            builder
                .Property(s => s.SectionCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.LCNo)
                .HasMaxLength(100);

            builder
                .Property(s => s.IssuedBy)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerAgentCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerAgentName)
                .HasMaxLength(255);

            builder
                .Property(s => s.Destination)
                .HasMaxLength(50);

            builder
                .Property(s => s.ShippingMark)
                .HasMaxLength(2000);

            builder
                .Property(s => s.SideMark)
                .HasMaxLength(2000);

            builder
                .Property(s => s.Remark)
                .HasMaxLength(2000);

            builder
                .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.PackingListId);

            builder
                .HasMany(h => h.Measurements)
                .WithOne()
                .HasForeignKey(f => f.PackingListId);
        }
    }
}
