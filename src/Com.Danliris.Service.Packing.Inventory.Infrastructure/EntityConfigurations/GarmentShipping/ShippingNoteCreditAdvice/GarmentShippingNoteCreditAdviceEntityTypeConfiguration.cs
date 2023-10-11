using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceEntityTypeConfiguration : IEntityTypeConfiguration<GarmentShippingNoteCreditAdviceModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingNoteCreditAdviceModel> builder)
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

            builder.Property(s => s.NoteType).HasMaxLength(50);
            builder.Property(s => s.PaymentTerm).HasMaxLength(25);

            builder.Property(s => s.BuyerName).HasMaxLength(255);
            builder.Property(s => s.BuyerAddress).HasMaxLength(1000);

            builder.Property(s => s.BankAccountName).HasMaxLength(255);
            builder.Property(s => s.BankAddress).HasMaxLength(1000);

            builder.Property(s => s.Remark).HasMaxLength(1000);
        }
    }
}
