using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.Product
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder
                .Property(s => s.Code)
                .HasMaxLength(32);

            builder
                .Property(s => s.Name)
                .HasMaxLength(128);
        }
    }
}
