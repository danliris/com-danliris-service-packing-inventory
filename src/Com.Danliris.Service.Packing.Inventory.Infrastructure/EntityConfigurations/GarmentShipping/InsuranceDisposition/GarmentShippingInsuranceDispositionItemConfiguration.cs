using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionItemConfiguration : IEntityTypeConfiguration<GarmentShippingInsuranceDispositionItemModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingInsuranceDispositionItemModel> configuration)
        {
            /* StandardEntity */
            configuration.HasKey(s => s.Id);
            configuration.Property(s => s.CreatedAgent).HasMaxLength(128);
            configuration.Property(s => s.CreatedBy).HasMaxLength(128);
            configuration.Property(s => s.LastModifiedAgent).HasMaxLength(128);
            configuration.Property(s => s.LastModifiedBy).HasMaxLength(128);
            configuration.Property(s => s.DeletedAgent).HasMaxLength(128);
            configuration.Property(s => s.DeletedBy).HasMaxLength(128);
            configuration.HasQueryFilter(f => !f.IsDeleted);
            /* StandardEntity */

            configuration
            .Property(insurance => insurance.BuyerAgentCode)
            .HasMaxLength(10);

            configuration
            .Property(insurance => insurance.BuyerAgentName)
            .HasMaxLength(255);

            configuration
            .Property(insurance => insurance.InvoiceNo)
            .HasMaxLength(50);

            configuration
            .Property(insurance => insurance.PolicyNo)
            .HasMaxLength(255);
        }
    }
}
