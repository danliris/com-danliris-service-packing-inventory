using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class IPProcessTypeEntityTypeConfiguration : IEntityTypeConfiguration<IPProcessTypeModel>
    {
        public void Configure(EntityTypeBuilder<IPProcessTypeModel> builder)
        {
            builder
                .HasKey(s => s.Id);
            builder
                .Property(s => s.Code)
                .HasMaxLength(128);
            builder
                .Property(s => s.ProcessType)
                .HasMaxLength(1024);
        }
    }
}
