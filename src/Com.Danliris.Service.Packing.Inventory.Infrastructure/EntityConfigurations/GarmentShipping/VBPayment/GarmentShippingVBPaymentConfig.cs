using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentConfig : IEntityTypeConfiguration<GarmentShippingVBPaymentModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingVBPaymentModel> builder)
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
                .HasMany(h => h.Invoices)
                .WithOne()
                .HasForeignKey(f => f.VBPaymentId);

            builder
                .HasMany(h => h.Units)
                .WithOne()
                .HasForeignKey(f => f.VBPaymentId);

            builder
               .Property(s => s.BuyerCode)
               .HasMaxLength(100);

            builder
                .Property(s => s.BuyerName)
                .HasMaxLength(250);

            builder
               .Property(s => s.EMKLCode)
               .HasMaxLength(100);

            builder
                .Property(s => s.EMKLName)
                .HasMaxLength(250);

            builder
               .Property(s => s.EMKLInvoiceNo)
               .HasMaxLength(100);

            builder
               .Property(s => s.ForwarderInvoiceNo)
               .HasMaxLength(100);

            builder
               .Property(s => s.VBNo)
               .HasMaxLength(50);

            builder
               .Property(s => s.PaymentType)
               .HasMaxLength(50);


            builder
               .Property(s => s.IncomeTaxName)
               .HasMaxLength(255);
        }
    }
}
