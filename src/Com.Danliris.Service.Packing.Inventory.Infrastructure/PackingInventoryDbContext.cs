using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentPackingList;
//using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.Master;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
//using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport.ExportCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.DetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.ShippingNoteCreditAdvice;
//using Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.GarmentDraftPackingListItem;

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
        public DbSet<DyeingPrintingAreaReferenceModel> DyeingPrintingAreaReferences { get; set; }
        public DbSet<DyeingPrintingAreaMovementModel> DyeingPrintingAreaMovements { get; set; }
        public DbSet<DyeingPrintingAreaSummaryModel> DyeingPrintingAreaSummaries { get; set; }

        public DbSet<DyeingPrintingStockOpnameProductionOrderModel> DyeingPrintingStockOpnameProductionOrders { get; set; }
        public DbSet<DyeingPrintingStockOpnameModel> DyeingPrintingStockOpnames { get; set; }

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

        public DbSet<MaterialDeliveryNoteModel> MaterialDeliveryNote { get; set; }
        public DbSet<ItemsModel> Items { get; set; }

        public DbSet<MaterialDeliveryNoteWeavingModel> MaterialDeliveryNoteWeaving { get; set; }
        public DbSet<ItemsMaterialDeliveryNoteWeavingModel> ItemsMaterialDeliveryNoteWeaving { get; set; }

        public DbSet<GarmentPackingListModel> GarmentPackingLists { get; set; }
        public DbSet<GarmentPackingListItemModel> GarmentPackingListItems { get; set; }
        public DbSet<GarmentPackingListDetailModel> GarmentPackingListDetails { get; set; }
        public DbSet<GarmentPackingListDetailSizeModel> GarmentPackingListDetailSizes { get; set; }
        public DbSet<GarmentPackingListMeasurementModel> GarmentPackingListMeasurements { get; set; }
        public DbSet<GarmentPackingListStatusActivityModel> GarmentPackingListStatusActivities { get; set; }


        public DbSet<GarmentDraftPackingListItemModel> GarmentDraftPackingListItems { get; set; }
        public DbSet<GarmentDraftPackingListDetailModel> GarmentDraftPackingListDetails { get; set; }
        public DbSet<GarmentDraftPackingListDetailSizeModel> GarmentDraftPackingListDetailSizes { get; set; }


        public DbSet<GarmentShippingCoverLetterModel> GarmentShippingCoverLetters { get; set; }

        public DbSet<GarmentShippingInstructionModel> GarmentShippingInstructions { get; set; }
        public DbSet<GarmentShippingLetterOfCreditModel> GarmentShippingLetterOfCredits { get; set; }
        public DbSet<GarmentShippingNoteModel> GarmentShippingNotes { get; set; }
        public DbSet<GarmentShippingNoteItemModel> GarmentShippingNoteItems { get; set; }


        public DbSet<GarmentShippingInvoiceModel> GarmentShippingInvoices { get; set; }
        public DbSet<GarmentShippingInvoiceItemModel> GarmentShippingInvoiceItems { get; set; }
        public DbSet<GarmentShippingInvoiceAdjustmentModel> GarmentShippingInvoiceAdjustments { get; set; }
        public DbSet<GarmentShippingInvoiceUnitModel> GarmentShippingInvoiceUnitPercentages { get; set; }

        public DbSet<GarmentShippingAmendLetterOfCreditModel> GarmentShippingAmendLetterOfCredits { get; set; }

        public DbSet<GarmentShippingExportSalesDOModel> GarmentShippingExportSalesDOs { get; set; }
        public DbSet<GarmentShippingExportSalesDOItemModel> GarmentShippingExportSalesDOItems { get; set; }
        public DbSet<GarmentShippingLocalSalesNoteModel> GarmentShippingLocalSalesNotes { get; set; }
        public DbSet<GarmentShippingLocalSalesNoteItemModel> GarmentShippingLocalSalesNoteItems { get; set; }

        public DbSet<GarmentShippingLocalCoverLetterModel> GarmentShippingLocalCoverLetters { get; set; }

        public DbSet<GarmentShippingLocalSalesDOModel> GarmentShippingLocalSalesDOs { get; set; }
        public DbSet<GarmentShippingLocalSalesDOItemModel> GarmentShippingLocalSalesDOItems { get; set; }

        public DbSet<GarmentShippingLocalPriceCorrectionNoteModel> GarmentShippingLocalPriceCorrectionNotes { get; set; }
        public DbSet<GarmentShippingLocalPriceCorrectionNoteItemModel> GarmentShippingLocalPriceCorrectionNoteItems { get; set; }

        public DbSet<GarmentShippingLocalReturnNoteModel> GarmentShippingLocalReturnNotes { get; set; }
        public DbSet<GarmentShippingLocalReturnNoteItemModel> GarmentShippingLocalReturnNoteItems { get; set; }

        public DbSet<GarmentShippingLocalPriceCuttingNoteModel> GarmentShippingLocalPriceCuttingNotes { get; set; }
        public DbSet<GarmentShippingLocalPriceCuttingNoteItemModel> GarmentShippingLocalPriceCuttingNoteItems { get; set; }
        public DbSet<GarmentDebiturBalanceModel> GarmentDebiturBalances { get; set; }

        #region master
        public DbSet<WeftTypeModel> IPWeftTypes { get; set; }
        public DbSet<WarpTypeModel> IPWarpTypes { get; set; }
        public DbSet<MaterialConstructionModel> IPMaterialConstructions { get; set; }
        public DbSet<GradeModel> IPGrades { get; set; }
        public DbSet<IPWidthTypeModel> IPWidthType { get; set; }
        public DbSet<IPWovenTypeModel> IPWovenType { get; set; }
        public DbSet<IPYarnTypeModel> IPYarnType { get; set; }
        public DbSet<IPProcessTypeModel> IPProcessType { get; set; }
        #endregion

        public DbSet<GarmentShippingCreditAdviceModel> GarmentShippingCreditAdvices { get; set; }

        public DbSet<GarmentShippingVBPaymentModel> GarmentShippingVBPayments { get; set; }
        public DbSet<GarmentShippingVBPaymentInvoiceModel> GarmentShippingVBPaymentInvoices { get; set; }
        public DbSet<GarmentShippingVBPaymentUnitModel> GarmentShippingVBPaymentUnits { get; set; }

        public DbSet<GarmentShippingLocalSalesContractModel> GarmentShippingLocalSalesContracts { get; set; }
        public DbSet<GarmentShippingLocalSalesContractItemModel> GarmentShippingLocalSalesContractItems { get; set; }

        public DbSet<GarmentShippingInsuranceDispositionModel> GarmentShippingInsuranceDispositions { get; set; }
        public DbSet<GarmentShippingInsuranceDispositionItemModel> GarmentShippingInsuranceDispositionItems { get; set; }

        public DbSet<GarmentShippingPaymentDispositionModel> GarmentShippingPaymentDispositions { get; set; }
        public DbSet<GarmentShippingPaymentDispositionBillDetailModel> GarmentShippingPaymentDispositionBillDetails { get; set; }
        public DbSet<GarmentShippingPaymentDispositionInvoiceDetailModel> GarmentShippingPaymentDispositionInvoiceDetails { get; set; }
        public DbSet<GarmentShippingPaymentDispositionUnitChargeModel> GarmentShippingPaymentDispositionUnitCharges { get; set; }
        public DbSet<GarmentShippingPaymentDispositionPaymentDetailModel> GarmentShippingPaymentDispositionPaymentDetails { get; set; }

        public DbSet<GarmentShippingPaymentDispositionRecapModel> GarmentShippingPaymentDispositionRecaps { get; set; }
        public DbSet<GarmentShippingPaymentDispositionRecapItemModel> GarmentShippingPaymentDispositionRecapItems { get; set; }

        public DbSet<GarmentShippingCostStructureModel> GarmentShippingCostStructures { get; set; }
        public DbSet<GarmentShippingCostStructureItemModel> GarmentShippingCostStructureItems { get; set; }
        public DbSet<GarmentShippingCostStructureDetailModel> GarmentShippingCostStructureDetails { get; set; }
        public DbSet<DyeingPrintingStockOpnameMutationModel> DyeingPrintingStockOpnameMutations { get; set; }
        public DbSet<DyeingPrintingStockOpnameMutationItemModel> DyeingPrintingStockOpnameMutationItems { get; set; }
        public DbSet<DyeingPrintingStockOpnameSummaryModel> DyeingPrintingStockOpnameSummaries { get; set; }

        public DbSet<GarmentShippingExportCoverLetterModel> GarmentShippingExportCoverLetters { get; set; }
        public DbSet<GarmentShippingExportSalesContractModel> GarmentShippingExportSalesContracts { get; set; }
        public DbSet<GarmentShippingExportSalesContractItemModel> GarmentShippingExportSalesContractItems { get; set; }
        public DbSet<GarmentShippingLeftOverExportSalesDOModel> GarmentShippingLeftOverExportSalesDOs { get; set; }
        public DbSet<GarmentShippingLeftOverExportSalesDOItemModel> GarmentShippingLeftOverExportSalesDOItems { get; set; }
        public DbSet<GarmentShippingExportSalesNoteModel> GarmentShippingExportSalesNotes { get; set; }
        public DbSet<GarmentShippingExportSalesNoteItemModel> GarmentShippingExportSalesNoteItems { get; set; }
        public DbSet<GarmentShippingDetailLocalSalesNoteModel> GarmentShippingDetailLocalSalesNotes { get; set; }
        public DbSet<GarmentShippingDetailLocalSalesNoteItemModel> GarmentShippingDetailLocalSalesNoteItems { get; set; }

        public DbSet<GarmentShippingNoteCreditAdviceModel> GarmentShippingNoteCreditAdvices { get; set; }

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
            modelBuilder.ApplyConfiguration(new DyeingPrintingStockOpnameEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new DyeingPrintingStockOpnameProductionOrderEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentPackingListEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListDetailSizeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentPackingListMeasurementEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new GarmentPackingListStatusActivityEntityTypeConfiguration());

            //modelBuilder.ApplyConfiguration(new GarmentDraftPackingListItemEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new GarmentDraftPackingListDetailEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new GarmentDraftPackingListDetailSizeEntityConfiguration());

            //modelBuilder.ApplyConfiguration(new GarmentShippingInstructionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentCoverLetterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentLetterOfCreditEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentShippingNoteConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingNoteItemConfig());

            modelBuilder.ApplyConfiguration(new MaterialDeliveryNoteWeavingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemsMaterialDeliveryNoteWeavingEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new MaterialDeliveryNoteEntitiyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemsEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentShippingInvoiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentShippingInvoiceItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentShippingInvoiceAdjustmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentShippingInvoiceUnitEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentAmendLetterOfCreditEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesDOEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesDOItemEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentCreditAdviceEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentShippingLocalSalesNoteConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingLocalSalesNoteItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentLocalCoverLetterConfig());
            modelBuilder.ApplyConfiguration(new GarmentDebiturBalanceEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentShippingLocalSalesDOConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingLocalSalesDOItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingLocalPriceCorrectionNoteConfig());
            //modelBuilder.ApplyConfiguration(new GarmentShippingLocalPriceCorrectionNoteItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingLocalReturnNoteConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingLocalReturnNoteItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingLocalPriceCuttingNoteConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingLocalPriceCuttingNoteItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingVBPaymentConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingVBPaymentInvoiceConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingVBPaymentUnitConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingLocalSalesContractConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingLocalSalesContractItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingInsuranceDispositionConfiguration());
            //modelBuilder.ApplyConfiguration(new GarmentShippingInsuranceDispositionItemConfiguration());

            modelBuilder.ApplyConfiguration(new GarmentShippingPaymentDispositionConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingPaymentDispositionBillDetailConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingPaymentDispositionInvoiceDetailConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingPaymentDispositionUnitChargeConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingPaymentDispositionPaymentDetailConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingPaymentDispositionRecapConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingPaymentDispositionRecapItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentExportCoverLetterConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesContractConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesContractItemConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesDOConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesDOItemConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesNoteConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingExportSalesNoteItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingDetailLocalSalesNoteConfig());
            modelBuilder.ApplyConfiguration(new GarmentShippingDetailLocalSalesNoteItemConfig());

            modelBuilder.ApplyConfiguration(new GarmentShippingNoteCreditAdviceEntityTypeConfiguration());

            //modelBuilder.ApplyConfiguration(new GarmentShippingCostStructureEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new GarmentShippingCostStructureItemEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new GarmentShippingCostStructureDetailEntityTypeConfiguration());

            //modelBuilder.Entity<InventoryDocumentPackingItemModel>().HasQueryFilter(entity => !entity.IsDeleted);
            //modelBuilder.Entity<InventoryDocumentPackingModel>().HasQueryFilter(entity => !entity.IsDeleted);
            //modelBuilder.Entity<InventoryDocumentSKUItemModel>().HasQueryFilter(entity => !entity.IsDeleted);
            //modelBuilder.Entity<InventoryDocumentSKUModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<ProductSKUModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<ProductPackingModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<FabricQualityControlModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<FabricGradeTestModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaInputModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaInputProductionOrderModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaOutputModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaOutputProductionOrderModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaMovementModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingAreaSummaryModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingStockOpnameModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingStockOpnameProductionOrderModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingStockOpnameMutationModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingStockOpnameMutationItemModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<DyeingPrintingStockOpnameSummaryModel>().HasQueryFilter(entity => !entity.IsDeleted);
            //modelBuilder.Entity<DyeingPrintingAreaOutputAvalItemModel>().HasQueryFilter(entity => !entity.IsDeleted);

            modelBuilder.Entity<CategoryModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<UnitOfMeasurementModel>().HasQueryFilter(entity => !entity.IsDeleted);

            //modelBuilder.Entity<PackagingStockModel>().HasQueryFilter(entity => !entity.IsDeleted);

            modelBuilder.Entity<MaterialDeliveryNoteModel>().HasQueryFilter(entity => !entity.IsDeleted);

            modelBuilder.Entity<ItemsModel>().HasQueryFilter(entity => !entity.IsDeleted);

            modelBuilder.Entity<MaterialDeliveryNoteWeavingModel>().HasQueryFilter(entity => !entity.IsDeleted);

            modelBuilder.Entity<ItemsMaterialDeliveryNoteWeavingModel>().HasQueryFilter(entity => !entity.IsDeleted);
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
            modelBuilder.Entity<IPWovenTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<IPYarnTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            modelBuilder.Entity<IPProcessTypeModel>().HasQueryFilter(entity => !entity.IsDeleted);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}