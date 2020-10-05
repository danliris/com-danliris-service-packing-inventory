using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionConfiguration : IEntityTypeConfiguration<GarmentShippingInsuranceDispositionModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingInsuranceDispositionModel> configuration)
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
            .Property(insurance => insurance.BankName)
            .HasMaxLength(255);

            configuration
            .Property(insurance => insurance.DispositionNo)
            .HasMaxLength(50);

            configuration
            .HasIndex(i => i.DispositionNo)
            .IsUnique()
            .HasFilter("[IsDeleted]=(0)");

            configuration
            .Property(insurance => insurance.InsuranceCode)
            .HasMaxLength(50);

            configuration
            .Property(insurance => insurance.InsuranceName)
            .HasMaxLength(255);

            configuration
            .Property(insurance => insurance.PolicyType)
            .HasMaxLength(25);

            configuration
            .Property(insurance => insurance.Remark)
            .HasMaxLength(4000);

            configuration
            .HasMany(h => h.Items)
            .WithOne()
            .HasForeignKey(f => f.InsuranceDispositionId);

            configuration
            .HasMany(h => h.UnitCharge)
            .WithOne()
            .HasForeignKey(f => f.InsuranceDispositionId);
        }
    }
}
