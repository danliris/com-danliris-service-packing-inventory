using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingNote
{
    public class GarmentShippingNoteItemConfig : IEntityTypeConfiguration<GarmentShippingNoteItemModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingNoteItemModel> builder)
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
                .Property(s => s.Description)
                .HasMaxLength(500);

            builder
                .Property(s => s.CurrencyCode)
                .HasMaxLength(100);
        }
    }
}
