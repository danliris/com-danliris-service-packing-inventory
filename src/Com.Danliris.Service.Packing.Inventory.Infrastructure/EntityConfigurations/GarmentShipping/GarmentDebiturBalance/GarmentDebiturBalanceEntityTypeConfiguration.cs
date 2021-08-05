using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceEntityTypeConfiguration : IEntityTypeConfiguration<GarmentDebiturBalanceModel>
    {
        public void Configure(EntityTypeBuilder<GarmentDebiturBalanceModel> builder)
        {
            /* StandardEntity */
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CreatedAgent).HasMaxLength(128);
            builder.Property(s => s.CreatedBy).HasMaxLength(128);
            builder.Property(s => s.LastModifiedAgent).HasMaxLength(128);
            builder.Property(s => s.LastModifiedBy).HasMaxLength(128);
            builder.Property(s => s.DeletedAgent).HasMaxLength(128);
            builder.Property(s => s.DeletedBy).HasMaxLength(128);
            builder.HasQueryFilter(f => !f.IsDeleted);
            /* StandardEntity */

            builder.Property(s => s.BuyerAgentCode).HasMaxLength(100);
            builder.Property(s => s.BuyerAgentName).HasMaxLength(250);
          
        }
    }
}
