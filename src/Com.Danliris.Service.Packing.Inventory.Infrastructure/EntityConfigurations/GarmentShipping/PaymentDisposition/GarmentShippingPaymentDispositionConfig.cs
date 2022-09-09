using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionConfig : IEntityTypeConfiguration<GarmentShippingPaymentDispositionModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingPaymentDispositionModel> builder)
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
                .Property(s => s.BuyerAgentCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerAgentName)
                .HasMaxLength(255);

            builder
                .Property(s => s.Bank)
                .HasMaxLength(255);

            builder
                .Property(s => s.AccNo)
                .HasMaxLength(100);

            builder
                .Property(s => s.ForwarderCode)
                .HasMaxLength(50);

            builder
                .Property(s => s.ForwarderName)
                .HasMaxLength(255);

            builder
                .Property(s => s.CourierCode)
                .HasMaxLength(50);

            builder
                .Property(s => s.CourierName)
                .HasMaxLength(255);

            builder
                .Property(s => s.EMKLCode)
                .HasMaxLength(50);

            builder
                .Property(s => s.EMKLName)
                .HasMaxLength(255);

            builder
                .Property(s => s.NPWP)
                .HasMaxLength(100);

            builder
                .Property(s => s.Address)
                .HasMaxLength(4000);

            builder
                .Property(s => s.InvoiceNumber)
                .HasMaxLength(100);

            builder
                .Property(s => s.InvoiceTaxNumber)
                .HasMaxLength(100);

            builder
                .Property(s => s.IncomeTaxName)
                .HasMaxLength(255);

            builder
                .Property(s => s.FreightBy)
                .HasMaxLength(100);

            builder
                .Property(s => s.FlightVessel)
                .HasMaxLength(4000);

            builder
                .Property(s => s.PaymentMethod)
                .HasMaxLength(50);

            builder
                .Property(s => s.PaymentTerm)
                .HasMaxLength(50);

            builder
                .Property(s => s.PaymentType)
                .HasMaxLength(50);

            builder
                .Property(s => s.PaidAt)
                .HasMaxLength(50);

            builder
                .Property(s => s.FreightBy)
                .HasMaxLength(50);

            builder
                .Property(s => s.FreightNo)
                .HasMaxLength(255);

            builder
                .Property(s => s.Remark)
                .HasMaxLength(4000);

            builder
               .HasMany(s => s.BillDetails)
               .WithOne()
               .HasForeignKey(a => a.PaymentDispositionId);

            builder
               .HasMany(s => s.UnitCharges)
               .WithOne()
               .HasForeignKey(a => a.PaymentDispositionId);

            builder
               .HasMany(s => s.InvoiceDetails)
               .WithOne()
               .HasForeignKey(a => a.PaymentDispositionId);

            builder
              .HasMany(s => s.PaymentDetails)
              .WithOne()
              .HasForeignKey(a => a.PaymentDispositionId);
        }
    }
}
