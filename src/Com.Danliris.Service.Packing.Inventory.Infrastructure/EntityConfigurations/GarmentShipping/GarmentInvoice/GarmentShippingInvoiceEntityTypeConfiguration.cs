using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentInvoice
{
	public class GarmentShippingInvoiceEntityTypeConfiguration : IEntityTypeConfiguration<GarmentShippingInvoiceModel>
	{
		public void Configure(EntityTypeBuilder<GarmentShippingInvoiceModel> configuration)
		{
			
			configuration.HasQueryFilter(f => !f.IsDeleted);

			configuration
				.HasKey(shippingInvoice => shippingInvoice.Id);

			configuration
				.Property(shippingInvoice => shippingInvoice.CreatedAgent)
				.HasMaxLength(128);

			configuration
				.Property(shippingInvoice => shippingInvoice.CreatedBy)
				.HasMaxLength(128);

			configuration
				.Property(shippingInvoice => shippingInvoice.LastModifiedAgent)
				.HasMaxLength(128);

			configuration
				.Property(shippingInvoice => shippingInvoice.LastModifiedBy)
				.HasMaxLength(128);

			configuration
				.Property(shippingInvoice => shippingInvoice.DeletedAgent)
				.HasMaxLength(128);

			configuration
				.Property(shippingInvoice => shippingInvoice.DeletedBy)
				.HasMaxLength(128);

			configuration
				.Property(shippingInvoice => shippingInvoice.InvoiceNo)
				.HasMaxLength(50);

			configuration
				.Property(shippingInvoice => shippingInvoice.From)
				.HasMaxLength(255);

			configuration
				.Property(shippingInvoice => shippingInvoice.To)
				.HasMaxLength(255);

			configuration
				.Property(shippingInvoice => shippingInvoice.BuyerAgentCode)
				.HasMaxLength(100);

			configuration
				.Property(shippingInvoice => shippingInvoice.BuyerAgentName)
				.HasMaxLength(255);
			configuration
				.Property(shippingInvoice => shippingInvoice.Consignee)
				.HasMaxLength(255);

			configuration
				.Property(shippingInvoice => shippingInvoice.LCNo)
				.HasMaxLength(100);

			configuration
				.Property(shippingInvoice => shippingInvoice.IssuedBy)
				.HasMaxLength(100);

			configuration
				.Property(shippingInvoice => shippingInvoice.SectionCode)
				.HasMaxLength(100);

			configuration
				.Property(shippingInvoice => shippingInvoice.ShippingPer)
				.HasMaxLength(256);

			configuration
				.Property(shippingInvoice => shippingInvoice.ConfirmationOfOrderNo)
				.HasMaxLength(255);

			configuration
				.Property(shippingInvoice => shippingInvoice.ShippingStaff)
				.HasMaxLength(255);

			configuration
				.Property(shippingInvoice => shippingInvoice.FabricType)
				.HasMaxLength(100);

			configuration
				.Property(shippingInvoice => shippingInvoice.BankAccount)
				.HasMaxLength(50);

			configuration
				.Property(shippingInvoice => shippingInvoice.PaymentDue)
				.HasMaxLength(5);

			configuration
				.Property(shippingInvoice => shippingInvoice.PEBNo)
				.HasMaxLength(50);

			configuration
				.Property(shippingInvoice => shippingInvoice.NPENo)
				.HasMaxLength(50);

			configuration
				.Property(shippingInvoice => shippingInvoice.BL)
				.HasMaxLength(50);

			configuration
				.Property(shippingInvoice => shippingInvoice.CO)
				.HasMaxLength(50);

			configuration
				.Property(shippingInvoice => shippingInvoice.COTP)
				.HasMaxLength(50);

			configuration
				.Property(shippingInvoice => shippingInvoice.Description)
				.HasMaxLength(255);

			configuration
				.HasMany(shippingInvoice => shippingInvoice.Items)
				.WithOne()
				.HasForeignKey("GarmentShippingInvoiceId");
			configuration
			   .HasMany(shippingInvoice => shippingInvoice.GarmentShippingInvoiceAdjustment)
			   .WithOne()
			   .HasForeignKey(s =>s.GarmentShippingInvoiceId);
		}
	}
}
