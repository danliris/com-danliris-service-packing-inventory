using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionEntityTypeConfiguration : IEntityTypeConfiguration<GarmentShippingInstructionModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingInstructionModel> builder)
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
                .Property(s => s.InvoiceNo)
                .HasMaxLength(50);

            builder
                .Property(s => s.BuyerAgentCode)
                .HasMaxLength(100);

            builder
                .Property(s => s.BuyerAgentName)
                .HasMaxLength(255);

            builder
                .Property(s => s.BuyerAgentAddress)
                .HasMaxLength(4000);

            builder
               .Property(s => s.SpecialInstruction)
               .HasMaxLength(2000);

            builder
               .Property(s => s.ForwarderCode)
               .HasMaxLength(50);

            builder
               .Property(s => s.ForwarderName)
               .HasMaxLength(255);

            builder
               .Property(s => s.ForwarderPhone)
               .HasMaxLength(255);

            builder
               .Property(s => s.ForwarderAddress)
               .HasMaxLength(4000);

            builder
               .Property(s => s.ATTN)
               .HasMaxLength(1000);

            builder
               .Property(s => s.CC)
               .HasMaxLength(500);

            builder
               .Property(s => s.Fax)
               .HasMaxLength(500);

            builder
               .Property(s => s.ShippingStaffName)
               .HasMaxLength(500);

            builder
               .Property(s => s.Phone)
               .HasMaxLength(50);

            builder
               .Property(s => s.ShippedBy)
               .HasMaxLength(20);

            builder
               .Property(s => s.CartonNo)
               .HasMaxLength(50);

            builder
               .Property(s => s.PortOfDischarge)
               .HasMaxLength(255);

            builder
               .Property(s => s.PlaceOfDelivery)
               .HasMaxLength(255);

            builder
               .Property(s => s.FeederVessel)
               .HasMaxLength(255);

            builder
               .Property(s => s.OceanVessel)
               .HasMaxLength(255);

            builder
               .Property(s => s.Carrier)
               .HasMaxLength(255);

            builder
               .Property(s => s.Flight)
               .HasMaxLength(255);

            builder
               .Property(s => s.Transit)
               .HasMaxLength(255);

            builder
               .Property(s => s.BankAccountName)
               .HasMaxLength(255);

            builder
               .Property(s => s.Notify)
               .HasMaxLength(2000);

            builder
               .Property(s => s.LadingBill)
               .HasMaxLength(4000);

            builder
               .Property(s => s.Freight)
               .HasMaxLength(1000);

            builder
               .Property(s => s.Marks)
               .HasMaxLength(4000);
        }
    }
}
