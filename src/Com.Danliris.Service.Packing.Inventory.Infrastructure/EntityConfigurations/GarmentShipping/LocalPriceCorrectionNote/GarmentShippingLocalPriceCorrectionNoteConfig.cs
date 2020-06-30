using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteConfig : IEntityTypeConfiguration<GarmentShippingLocalPriceCorrectionNoteModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingLocalPriceCorrectionNoteModel> builder)
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
                .Property(s => s.CorrectionNoteNo)
                .HasMaxLength(50);

            builder
                .HasIndex(i => i.CorrectionNoteNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");

            builder
                .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.PriceCorrectionNoteId);

            builder
                .HasOne(h => h.SalesNote)
                .WithMany()
                .HasForeignKey(f => f.SalesNoteId);
        }
    }
}
