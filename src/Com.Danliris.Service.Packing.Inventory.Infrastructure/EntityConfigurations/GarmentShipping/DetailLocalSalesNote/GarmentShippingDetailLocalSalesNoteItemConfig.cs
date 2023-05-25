using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.DetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteItemConfig : IEntityTypeConfiguration<GarmentShippingDetailLocalSalesNoteItemModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingDetailLocalSalesNoteItemModel> builder)
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
                .Property(s => s.UnitCode)
                .HasMaxLength(10);

            builder
                .Property(s => s.UnitName)
                .HasMaxLength(100);

            builder
                .Property(s => s.UomUnit)
                .HasMaxLength(10);

        }
    }
}
