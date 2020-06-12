using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentInvoice
{
	public class GarmentShippingInvoiceItemEntityTypeConfiguration : IEntityTypeConfiguration<GarmentShippingInvoiceItemModel>
	{
		public void Configure(EntityTypeBuilder<GarmentShippingInvoiceItemModel> configuration)
		{
			configuration
				.HasKey(inventoryDocument => inventoryDocument.Id);

			configuration
				.Property(inventoryDocument => inventoryDocument.CreatedAgent)
				.HasMaxLength(128);

			configuration
				.Property(inventoryDocument => inventoryDocument.CreatedBy)
				.HasMaxLength(128);

			configuration
				.Property(inventoryDocument => inventoryDocument.LastModifiedAgent)
				.HasMaxLength(128);

			configuration
				.Property(inventoryDocument => inventoryDocument.LastModifiedBy)
				.HasMaxLength(128);

			configuration
				.Property(inventoryDocument => inventoryDocument.DeletedAgent)
				.HasMaxLength(128);

			configuration
				.Property(inventoryDocument => inventoryDocument.DeletedBy)
				.HasMaxLength(128);

			configuration
				.Property(inventoryDocument => inventoryDocument.RONo)
				.HasMaxLength(10);

			configuration
				.Property(inventoryDocument => inventoryDocument.SCNo)
				.HasMaxLength(256);

			configuration
				.Property(inventoryDocument => inventoryDocument.BuyerBrandName)
				.HasMaxLength(100);

			configuration
				.Property(inventoryDocument => inventoryDocument.ComodityCode)
				.HasMaxLength(5);

			configuration
				.Property(inventoryDocument => inventoryDocument.ComodityName)
				.HasMaxLength(50);

			configuration
				.Property(inventoryDocument => inventoryDocument.ComodityDesc)
				.HasMaxLength(128);

			configuration
			.Property(inventoryDocument => inventoryDocument.UomUnit)
			.HasMaxLength(10);

			configuration
			.Property(inventoryDocument => inventoryDocument.UnitCode)
			.HasMaxLength(10);

			configuration.HasQueryFilter(f => !f.IsDeleted);
		}
	}
}
