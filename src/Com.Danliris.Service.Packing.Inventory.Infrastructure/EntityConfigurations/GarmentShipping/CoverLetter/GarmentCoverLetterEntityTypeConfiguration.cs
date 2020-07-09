using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.CoverLetter
{
    public class GarmentCoverLetterEntityTypeConfiguration : IEntityTypeConfiguration<GarmentShippingCoverLetterModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingCoverLetterModel> builder)
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

            builder.Property(s => s.InvoiceNo).HasMaxLength(50);
            builder.Property(s => s.EMKLCode).HasMaxLength(100);
            builder.Property(s => s.Name).HasMaxLength(250);
            builder.Property(s => s.Address).HasMaxLength(1000);
            builder.Property(s => s.ATTN).HasMaxLength(250);
            builder.Property(s => s.Phone).HasMaxLength(250);
            builder.Property(s => s.OrderCode).HasMaxLength(100);
            builder.Property(s => s.OrderName).HasMaxLength(255);

            builder.Property(s => s.ForwarderCode).HasMaxLength(50);
            builder.Property(s => s.ForwarderName).HasMaxLength(250);

            builder.Property(s => s.Truck).HasMaxLength(250);
            builder.Property(s => s.PlateNumber).HasMaxLength(250);
            builder.Property(s => s.Driver).HasMaxLength(250);
            builder.Property(s => s.ContainerNo).HasMaxLength(250);
            builder.Property(s => s.Freight).HasMaxLength(250);
            builder.Property(s => s.ShippingSeal).HasMaxLength(250);
            builder.Property(s => s.DLSeal).HasMaxLength(250);
            builder.Property(s => s.EMKLSeal).HasMaxLength(250);
            builder.Property(s => s.Unit).HasMaxLength(1000);
            builder.Property(s => s.ShippingStaffName).HasMaxLength(250);
        }
    }
}
