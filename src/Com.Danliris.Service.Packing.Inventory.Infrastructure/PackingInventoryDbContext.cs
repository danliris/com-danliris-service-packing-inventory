using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure
{
    public class PackingInventoryDbContext : StandardDbContext
    {
        public PackingInventoryDbContext(DbContextOptions<PackingInventoryDbContext> options) : base(options)
        {
        }

        public DbSet<ProductSKUModel> ProductSKUs { get; set; }
        public DbSet<ProductPackingModel> ProductPackings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductSKUEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPackingEntityTypeConfiguration());

            modelBuilder.Entity<ProductSKUModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<ProductPackingModel>().HasQueryFilter(entity => !entity.IsDeleted);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}