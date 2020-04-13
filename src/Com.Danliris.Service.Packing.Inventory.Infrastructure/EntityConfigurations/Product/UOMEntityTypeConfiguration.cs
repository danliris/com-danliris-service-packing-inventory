using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.Product
{
    public class UOMEntityTypeConfiguration : IEntityTypeConfiguration<UnitOfMeasurementModel>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasurementModel> builder)
        {
            builder
                .Property(s => s.Unit)
                .HasMaxLength(32);
        }
    }
}
