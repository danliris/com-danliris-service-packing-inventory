using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteItemConfig : IEntityTypeConfiguration<GarmentShippingLocalPriceCorrectionNoteItemModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingLocalPriceCorrectionNoteItemModel> builder)
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
                .HasOne(h => h.SalesNoteItem)
                .WithMany()
                .HasForeignKey(f => f.SalesNoteItemId)
                .OnDelete(DeleteBehavior.Restrict);
            // tambahkan pada migration => "onDelete: ReferentialAction.NoAction"
        }
    }
}
