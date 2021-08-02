using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.Master
{
    public class MaterialConstructionEntityTypeConfiguration : IEntityTypeConfiguration<MaterialConstructionModel>
    {
        public void Configure(EntityTypeBuilder<MaterialConstructionModel> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.CreatedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.CreatedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.LastModifiedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.LastModifiedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.DeletedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.DeletedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.Type)
                .HasMaxLength(128);

            builder
               .Property(s => s.Code)
               .HasMaxLength(16);
        }
    }
}
