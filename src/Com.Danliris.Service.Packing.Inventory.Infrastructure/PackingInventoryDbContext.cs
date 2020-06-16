using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.Master;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Packing.Inventory.Data.Models;

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
        //public DbSet<DyeingPrintingAreaOutputAvalItemModel> DyeingPrintingAreaOutputAvalItems { get; set; }
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

        public DbSet<GarmentPackingListModel> GarmentPackingLists { get; set; }
        public DbSet<GarmentPackingListItemModel> GarmentPackingListItems { get; set; }
        public DbSet<GarmentPackingListDetailModel> GarmentPackingListDetails { get; set; }
        public DbSet<GarmentPackingListDetailSizeModel> GarmentPackingListDetailSizes { get; set; }
        public DbSet<GarmentPackingListMeasurementModel> GarmentPackingListMeasurements { get; set; }
        public DbSet<GarmentShippingCoverLetterModel> GarmentShippingCoverLetters { get; set; }

        public DbSet<GarmentShippingInstructionModel> GarmentShippingInstructions { get; set; }
        public DbSet<GarmentShippingLetterOfCreditModel> GarmentShippingLetterOfCredits { get; set; }
        public DbSet<GarmentShippingNoteModel> GarmentShippingNotes { get; set; }
        public DbSet<GarmentShippingNoteItemModel> GarmentShippingNoteItems { get; set; }


		public DbSet<GarmentShippingInvoiceModel> GarmentShippingInvoices { get; set; }
		public DbSet<GarmentShippingInvoiceItemModel> GarmentShippingInvoiceItems { get; set; }
		public DbSet<GarmentShippingInvoiceAdjustmentModel> GarmentShippingInvoiceAdjustments { get; set; }

        public DbSet<GarmentShippingAmendLetterOfCreditModel> GarmentShippingAmendLetterOfCredits { get; set; }


        #region master
        public DbSet<WeftTypeModel> IPWeftTypes { get; set; }
        public DbSet<WarpTypeModel> IPWarpTypes { get; set; }
        public DbSet<MaterialConstructionModel> IPMaterialConstructions { get; set; }
        public DbSet<GradeModel> IPGrades { get; set; }
        public DbSet<IPWidthTypeModel> IPWidthType { get; set; }
        public DbSet<IPWarpTypeModel> IPWovenType { get; set; }
        public DbSet<IPYarnTypeModel> IPYarnType { get; set; }
        public DbSet<IPProcessTypeModel> IPProcessType { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FabricQualityControlEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FabricGradeTestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CriteriaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaInputEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaInputProductionOrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaOutputEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaOutputProductionOrderEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new DyeingPrintingAreaOutputAvalItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaMovementEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DyeingPrintingAreaSummaryEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentPackingListEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListDetailSizeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListMeasurementEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentShippingInstructionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentCoverLetterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentLetterOfCreditEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentShippingNoteConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingNoteItemConfig());

			modelBuilder.ApplyConfiguration(new GarmentShippingInvoiceEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new GarmentShippingInvoiceItemEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new GarmentShippingInvoiceAdjustmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentAmendLetterOfCreditEntityTypeConfiguration());

            modelBuilder.Entity<ProductPackingModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<FabricQualityControlModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<FabricGradeTestModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaInputModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaInputProductionOrderModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaOutputModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaOutputProductionOrderModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaMovementModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaSummaryModel>().HasQueryFilter(entity => !entity.IsDeleted);
            //modelBuilder.Entity<DyeingPrintingAreaOutputAvalItemModel>().HasQueryFilter(entity => !entity.IsDeleted);

            modelBuilder.Entity<CategoryModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<UnitOfMeasurementModel>().HasQueryFilter(entity => !entity.IsDeleted);

            #region master
            modelBuilder.ApplyConfiguration(new WeftTypeEntityTypeConfiguration());
            modelBuilder.Entity<WeftTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.ApplyConfiguration(new WarpTypeEntityTypeConfiguration());
            modelBuilder.Entity<WarpTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.ApplyConfiguration(new MaterialConstructionEntityTypeConfiguration());
            modelBuilder.Entity<MaterialConstructionModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.ApplyConfiguration(new GradeEntityTypeConfiguration());
            modelBuilder.Entity<GradeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.ApplyConfiguration(new IPWidthTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IPWarpTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IPYarnTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IPProcessTypeEntityTypeConfiguration());
            modelBuilder.Entity<IPWidthTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<IPWarpTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<IPYarnTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<IPProcessTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}