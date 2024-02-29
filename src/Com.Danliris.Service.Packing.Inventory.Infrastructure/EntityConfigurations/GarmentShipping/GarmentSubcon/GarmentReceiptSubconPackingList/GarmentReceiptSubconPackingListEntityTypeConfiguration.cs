
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListEntityTypeConfiguration : IEntityTypeConfiguration<GarmentReceiptSubconPackingListModel>
    {
        public void Configure(EntityTypeBuilder<GarmentReceiptSubconPackingListModel> builder)
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
                .Property(s => s.LocalSalesNoteNo)
                .HasMaxLength(50);

            builder
                .Property(s => s.LocalSalesContractNo)
                .HasMaxLength(50);
            

            builder
                .Property(s => s.TransactionTypeCode)
                .HasMaxLength(25);

            builder
                .Property(s => s.TransactionTypeName)
                .HasMaxLength(255);

            builder
                .Property(s => s.PaymentTerm)
                .HasMaxLength(25);

            builder
                .Property(s => s.BuyerCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerName)
                .HasMaxLength(255);

            builder
               .Property(s => s.BuyerName)
               .HasMaxLength(255);

            builder
               .Property(s => s.BuyerNPWP)
               .HasMaxLength(50);

            builder
             .Property(s => s.ValidatedMDBy)
             .HasMaxLength(100);

            builder
            .Property(s => s.ValidatedMDRemark)
            .HasMaxLength(1000);

            builder
             .Property(s => s.ValidatedShippingBy)
             .HasMaxLength(100);

            builder
            .Property(s => s.RejectReason)
            .HasMaxLength(1000);

            builder
                .HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(f => f.PackingListId);

           

		}
    }
}
