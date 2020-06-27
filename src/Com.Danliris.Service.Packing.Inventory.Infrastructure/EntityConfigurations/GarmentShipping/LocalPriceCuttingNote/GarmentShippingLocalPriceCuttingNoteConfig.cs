using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteConfig : IEntityTypeConfiguration<GarmentShippingLocalPriceCuttingNoteModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingLocalPriceCuttingNoteModel> builder)
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
                .Property(s => s.CuttingPriceNoteNo)
                .HasMaxLength(50);

            builder
                .HasIndex(i => i.CuttingPriceNoteNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");

            builder
                .Property(s => s.BuyerCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerName)
                .HasMaxLength(250);

            builder
                .Property(s => s.Remark)
                .HasMaxLength(1000);

            builder
                .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.PriceCuttingNoteId);
        }
    }
}
