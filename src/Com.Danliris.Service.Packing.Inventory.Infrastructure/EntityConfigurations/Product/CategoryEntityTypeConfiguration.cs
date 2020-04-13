using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.Product
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder
                .Property(s => s.Name)
                .HasMaxLength(128);

            builder
                .Property(s => s.Code)
                .HasMaxLength(32);
        }
    }
}
