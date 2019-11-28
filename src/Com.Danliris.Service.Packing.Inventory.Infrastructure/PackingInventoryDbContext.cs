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

        public DbSet<PackagingInventoryDocumentModel> PackagingInventoryDocuments { get; set; }
        public DbSet<PackagingInventoryDocumentItemModel> PackagingInventoryDocumentItems { get; set; }
        public DbSet<ProductPackagingModel> ProductPackagings { get; set; }
        public DbSet<ProductSKUModel> ProductSKUs { get; set; }
        public DbSet<SKUInventoryDocumentModel> SKUInventoryDocuments { get; set; }
        public DbSet<SKUInventoryDocumentItemModel> SKUInventoryDocumentItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PackagingInventoryDocumentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PackagingInventoryDocumentItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPackagingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSKUEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SKUInventoryDocumentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SKUInventoryDocumentItemEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}