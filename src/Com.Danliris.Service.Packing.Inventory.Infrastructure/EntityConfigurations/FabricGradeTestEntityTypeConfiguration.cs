using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class FabricGradeTestEntityTypeConfiguration : IEntityTypeConfiguration<FabricGradeTestModel>
    {
        public void Configure(EntityTypeBuilder<FabricGradeTestModel> builder)
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
                .Property(s => s.Grade)
                .HasMaxLength(512);

            builder
                .Property(s => s.PcsNo)
                .HasMaxLength(4096);

            builder
                .Property(s => s.Type)
                .HasMaxLength(1024);

        }
    }
}
