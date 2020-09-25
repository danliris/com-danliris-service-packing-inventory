using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteConfig : IEntityTypeConfiguration<GarmentShippingLocalSalesNoteModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingLocalSalesNoteModel> builder)
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
                .Property(s => s.SalesContractNo)
                .HasMaxLength(50);

            builder
                .Property(s => s.NoteNo)
                .HasMaxLength(50);

            builder
                .HasIndex(i => i.NoteNo)
                .IsUnique()
                .HasFilter("[IsDeleted]=(0)");

            builder
                .Property(s => s.TransactionTypeCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.TransactionTypeName)
                .HasMaxLength(250);

            builder
                .Property(s => s.BuyerCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerName)
                .HasMaxLength(250);

            builder
                .Property(s => s.BuyerNPWP)
                .HasMaxLength(50);

            builder
               .Property(s => s.DispositionNo)
               .HasMaxLength(100);

            builder
                .Property(s => s.Remark)
                .HasMaxLength(1000);

            builder
                 .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.LocalSalesNoteId);

            builder
                .Property(s => s.PaymentType)
                .HasMaxLength(20);
        }
    }
}
