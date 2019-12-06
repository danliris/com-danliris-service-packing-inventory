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

        public DbSet<InventoryDocumentPackingItemModel> InventoryDocumentPackingItems { get; set; }
        public DbSet<InventoryDocumentPackingModel> InventoryDocumentPackings { get; set; }
        public DbSet<InventoryDocumentSKUItemModel> InventoryDocumentSKUItems { get; set; }
        public DbSet<InventoryDocumentSKUModel> InventoryDocumentSKUs { get; set; }
        public DbSet<ProductSKUModel> ProductSKUs { get; set; }
        public DbSet<ProductPackingModel> ProductPackings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InventoryDocumentPackingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryDocumentPackingItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryDocumentSKUEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryDocumentSKUItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPackingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPackingEntityTypeConfiguration());

            modelBuilder.Entity<InventoryDocumentPackingItemModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<InventoryDocumentPackingModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<InventoryDocumentSKUItemModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<InventoryDocumentSKUModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<ProductSKUModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<ProductPackingModel>().HasQueryFilter(entity => !entity.IsDeleted);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}