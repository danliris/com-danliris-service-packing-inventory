using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
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

        public DbSet<DyeingPrintingAreaInputModel> DyeingPrintingAreaInputs { get; set; }
        public DbSet<DyeingPrintingAreaInputProductionOrderModel> DyeingPrintingAreaInputProductionOrders { get; set; }
        public DbSet<DyeingPrintingAreaOutputModel> DyeingPrintingAreaOutputs { get; set; }
        public DbSet<DyeingPrintingAreaOutputProductionOrderModel> DyeingPrintingAreaOutputProductionOrders { get; set; }
        public DbSet<DyeingPrintingAreaMovementModel> DyeingPrintingAreaMovements { get; set; }
        public DbSet<DyeingPrintingAreaSummaryModel> DyeingPrintingAreaSummaries { get; set; }
        public DbSet<FabricQualityControlModel> NewFabricQualityControls { get; set; }
        public DbSet<FabricGradeTestModel> NewFabricGradeTests { get; set; }
        public DbSet<CriteriaModel> NewCriterias { get; set; }

        public DbSet<CategoryModel> IPCategories { get; set; }
        public DbSet<UnitOfMeasurementModel> IPUnitOfMeasurements { get; set; }

        public DbSet<ProductPackingInventoryDocumentModel> ProductPackingInventoryDocuments { get; set; }
        public DbSet<ProductPackingInventoryMovementModel> ProductPackingInventoryMovements { get; set; }
        public DbSet<ProductPackingInventorySummaryModel> ProductPackingInventorySummaries { get; set; }
        public DbSet<ProductSKUInventoryDocumentModel> ProductSKUInventoryDocuments { get; set; }
        public DbSet<ProductSKUInventoryMovementModel> ProductSKUInventoryMovements { get; set; }
        public DbSet<ProductSKUInventorySummaryModel> ProductSKUInventorySummaries { get; set; }
        public DbSet<ProductPackingModel> ProductPackings { get; set; }
        public DbSet<ProductSKUModel> ProductSKUs { get; set; }

        public DbSet<FabricProductPackingModel> FabricProductPackings { get; set; }
        public DbSet<FabricProductSKUModel> FabricProductSKUs { get; set; }
        public DbSet<GreigeProductPackingModel> GreigeProductPackings { get; set; }
        public DbSet<GreigeProductSKUModel> GreigeProductSKUs { get; set; }
        public DbSet<YarnProductPackingModel> YarnProductPackings { get; set; }
        public DbSet<YarnProductSKUModel> YarnProductSKUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FabricQualityControlEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FabricGradeTestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CriteriaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaInputEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaInputProductionOrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaOutputEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaOutputProductionOrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaMovementEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaSummaryEntityTypeConfiguration());

            modelBuilder.Entity<ProductPackingModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<FabricQualityControlModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<FabricGradeTestModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaInputModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaInputProductionOrderModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaOutputModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaOutputProductionOrderModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaMovementModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaSummaryModel>().HasQueryFilter(entity => !entity.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}