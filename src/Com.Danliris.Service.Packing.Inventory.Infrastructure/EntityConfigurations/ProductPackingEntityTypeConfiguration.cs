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
                .Property(productPacking => productPacking.Code)
                .HasMaxLength(32);

            productPackingConfiguration
                .Property(productPacking => productPacking.PackType)
                .HasMaxLength(32);
        }
    }
}
