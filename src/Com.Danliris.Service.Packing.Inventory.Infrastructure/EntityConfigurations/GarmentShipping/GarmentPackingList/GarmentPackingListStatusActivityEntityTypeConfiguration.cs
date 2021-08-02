using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentPackingList
{
    class GarmentPackingListStatusActivityEntityTypeConfiguration : IEntityTypeConfiguration<GarmentPackingListStatusActivityModel>
    {
        public void Configure(EntityTypeBuilder<GarmentPackingListStatusActivityModel> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CreatedBy).HasMaxLength(128);
            builder.Property(s => s.CreatedAgent).HasMaxLength(128);

            builder
                .Property(s => s.Status)
                .HasMaxLength(50)
                .HasConversion<string>();

            builder
                .Property(s => s.Remark)
                .HasMaxLength(2000);
        }
    }
}
