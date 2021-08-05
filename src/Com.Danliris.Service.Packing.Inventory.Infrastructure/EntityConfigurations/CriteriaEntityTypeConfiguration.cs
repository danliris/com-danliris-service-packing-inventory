using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class CriteriaEntityTypeConfiguration : IEntityTypeConfiguration<CriteriaModel>
    {
        public void Configure(EntityTypeBuilder<CriteriaModel> builder)
        {
            builder
               .HasKey(s => s.Id);

            builder
                .Property(s => s.Code)
                .HasMaxLength(32);

            builder
                .Property(s => s.Group)
                .HasMaxLength(4096);

            builder
                .Property(s => s.Name)
                .HasMaxLength(4096);
        }
    }
}
