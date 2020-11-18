using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionRecapConfig : IEntityTypeConfiguration<GarmentShippingPaymentDispositionRecapModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingPaymentDispositionRecapModel> builder)
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
                .Property(s => s.RecapNo)
                .HasMaxLength(100);

            builder
                .HasIndex(i => i.RecapNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");

            builder
                .Property(s => s.EMKLCode)
                .HasMaxLength(50);

            builder
                .Property(s => s.EMKLName)
                .HasMaxLength(255);

            builder
                .Property(s => s.EMKLAddress)
                .HasMaxLength(4000);

            builder
                .Property(s => s.EMKLNPWP)
                .HasMaxLength(100);

            builder
               .HasMany(s => s.Items)
               .WithOne()
               .HasForeignKey(a => a.RecapId);
        }
    }
}
