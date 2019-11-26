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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductSKUEntityTypeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}