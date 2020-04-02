using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class ProductPackingEntityTypeConfiguration : IEntityTypeConfiguration<ProductPackingModel>
    {
        public void Configure(EntityTypeBuilder<ProductPackingModel> productPackingConfiguration)
        {
            productPackingConfiguration
                .HasKey(inventoryDocument => inventoryDocument.Id);

            productPackingConfiguration
                .Property(inventoryDocument => inventoryDocument.CreatedAgent)
                .HasMaxLength(128);

            productPackingConfiguration
                .Property(inventoryDocument => inventoryDocument.CreatedBy)
                .HasMaxLength(128);

            productPackingConfiguration
                .Property(inventoryDocument => inventoryDocument.LastModifiedAgent)
                .HasMaxLength(128);

            productPackingConfiguration
                .Property(inventoryDocument => inventoryDocument.LastModifiedBy)
                .HasMaxLength(128);

            productPackingConfiguration
                .Property(inventoryDocument => inventoryDocument.DeletedAgent)
                .HasMaxLength(128);

            productPackingConfiguration
                .Property(inventoryDocument => inventoryDocument.DeletedBy)
                .HasMaxLength(128);

            productPackingConfiguration
                .Property(productPacking => productPacking.Code)
                .HasMaxLength(32);

            productPackingConfiguration
                .Property(productPacking => productPacking.PackingType)
                .HasMaxLength(32);
        }
    }
}
