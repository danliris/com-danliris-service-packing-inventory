﻿using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.AvalTransformation;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WeftType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WarpType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.MaterialConstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWidthType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWidthType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.Grade;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UOM;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingCostStructure;

using Com.Danliris.Service.Packing.Inventory.Application.QueueService;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWovenType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWovenType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDebitNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByUnit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByBuyer;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyBySection;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByCountry;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByComodity;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyer;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearUnit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearSection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesReportByBuyer;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesOmzet;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearCountry;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyerComodity;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoiceHistory;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesBook;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingGenerateData;
//using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInsuranceDispositionReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionReport;
//using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionRecapReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCMTSalesReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentRecapOmzetReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report.GarmentFinanceLocalSalesJournal;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByBrand;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesOmzetUnPaid;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetAnnualByUnitReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByMarketing;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearMarketing;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.ProductionSubmissionReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.FabricQualityControlReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.RegradingResultDocReport;

using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition.PaymentDispositionEMKLs;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailShippingLocalSalesNote;

using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdvice4MII;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditAdviceMII;

using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.ProductionSubmissionReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdviceMII;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice4MII;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInsuranceDispositionReport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetterTS;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetterTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDOTS;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDOTS;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.FinishedGoodsMinutes;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.Report.ShipmentLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CashInBank;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.DownPayment;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.OmzetCorrectionService;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.AR_ReportMutation;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesContract;

namespace Com.Danliris.Service.Packing.Inventory.WebApi
{
    public class Startup
    {
        private const string DEFAULT_CONNECTION = "DefaultConnection";
        private const string PACKING_INVENTORY_POLICY = "Packing Inventory Policy";
        private readonly string[] _exposedHeaders = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void RegisterApplicationSetting()
        {
            ApplicationSetting.CoreEndpoint = Configuration.GetValue<string>(Constant.CORE_ENDPOINT) ?? Configuration[Constant.CORE_ENDPOINT];
            ApplicationSetting.ProductionEndpoint = Configuration.GetValue<string>(Constant.GARMENT_PRODUCTION_ENDPOINT) ?? Configuration[Constant.GARMENT_PRODUCTION_ENDPOINT];
            ApplicationSetting.StorageAccountName = Configuration.GetValue<string>(Constant.STORAGE_ACCOUNT_NAME) ?? Configuration[Constant.STORAGE_ACCOUNT_NAME];
            ApplicationSetting.StorageAccountKey = Configuration.GetValue<string>(Constant.STORAGE_ACCOUNT_KEY) ?? Configuration[Constant.STORAGE_ACCOUNT_KEY];
            ApplicationSetting.SalesEndpoint = Configuration.GetValue<string>(Constant.SALES_ENDPOINT) ?? Configuration[Constant.SALES_ENDPOINT];
            ApplicationSetting.DyeingPrintingEndpoint = Configuration.GetValue<string>(Constant.DYEINGPRINTING_ENDPOINT) ?? Configuration[Constant.DYEINGPRINTING_ENDPOINT];
            ApplicationSetting.PurchasingEndpoint = Configuration.GetValue<string>(Constant.PURCHASING_ENDPOINT) ?? Configuration[Constant.PURCHASING_ENDPOINT];

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString(DEFAULT_CONNECTION) ?? Configuration[DEFAULT_CONNECTION];
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<PackingInventoryDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

            RegisterApplicationSetting();

            // Register Middleware

            #region Repository
            services.AddTransient<IDyeingPrintingAreaMovementRepository, DyeingPrintingAreaMovementRepository>();

            services.AddTransient<IFabricQualityControlRepository, FabricQualityControlRepository>();
            services.AddTransient<IFabricGradeTestRepository, FabricGradeTestRepository>();
            services.AddTransient<ICriteriaRepository, CriteriaRepository>();
            services.AddTransient<IFabricQualityControlService, FabricQualityControlService>();
            services.AddTransient<IGoodsWarehouseDocumentsService, GoodsWarehouseDocumentsService>();

            services.AddTransient<IMaterialDeliveryNoteRepository, MaterialDeliveryNoteRepository>();
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IMaterialDeliveryNoteService, MaterialDeliveryNoteService>();

            services.AddTransient<IMaterialDeliveryNoteWeavingRepository, MaterialDeliveryNoteWeavingRepository>();
            services.AddTransient<IItemsMaterialDeliveryNoteWeavingRepository, ItemsMaterialDeliveryNoteWeavingRepository>();
            services.AddTransient<IMaterialDeliveryNoteWeavingService, MaterialDeliveryNoteWeavingService>();

            services.AddTransient<IDyeingPrintingAreaInputRepository, DyeingPrintingAreaInputRepository>();
            services.AddTransient<IDyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderRepository>();
            services.AddTransient<IDyeingPrintingAreaOutputRepository, DyeingPrintingAreaOutputRepository>();
            services.AddTransient<IDyeingPrintingAreaOutputProductionOrderRepository, DyeingPrintingAreaOutputProductionOrderRepository>();
            services.AddTransient<IDyeingPrintingAreaSummaryRepository, DyeingPrintingAreaSummaryRepository>();
            services.AddTransient<IDyeingPrintingAreaReferenceRepository, DyeingPrintingAreaReferenceRepository>();

            services.AddTransient<IDyeingPrintingStockOpnameRepository, DyeingPrintingStockOpnameRepository>();

            services.AddTransient<IGarmentShippingInvoiceRepository, GarmentShippingInvoiceRepository>();
            services.AddTransient<IGarmentShippingInstructionRepository, GarmentShippingInstructionRepository>();
            services.AddTransient<IGarmentPackingListRepository, GarmentPackingListRepository>();
            services.AddTransient<IGarmentPackingListItemRepository, GarmentPackingListItemRepository>();
            services.AddTransient<IGarmentPackingListDetailRepository, GarmentPackingListDetailRepository>();
            services.AddTransient<IGarmentPackingListDetailSizeRepository, GarmentPackingListDetailSizeRepository>();
            services.AddTransient<IGarmentCoverLetterRepository, GarmentCoverLetterRepository>();
            services.AddTransient<IGarmentShippingNoteRepository, GarmentShippingNoteRepository>();
            services.AddTransient<IGarmentLetterOfCreditRepository, GarmentLetterOfCreditRepository>();
            services.AddTransient<IGarmentAmendLetterOfCreditRepository, GarmentAmendLetterOfCreditRepository>();
            services.AddTransient<IWeftTypeRepository, WeftTypeRepository>();
            services.AddTransient<IWarpTypeRepository, WarpTypeRepository>();
            services.AddTransient<IMaterialConstructionRepository, MaterialConstructionRepository>();
            services.AddTransient<IIPWidthTypeRepository, IPWidthTypeRepository>();
            services.AddTransient<IIPYarnTypeRepository, IPYarnTypeRepository>();
            services.AddTransient<IIPWovenTypeRepository, IPWovenTypeRepository>();
            services.AddTransient<IIPProcessTypeRepository, IPProcessTypeRepository>();
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<IGarmentShippingCreditAdviceRepository, GarmentShippingCreditAdviceRepository>();
            services.AddTransient<IGarmentShippingLocalSalesNoteRepository, GarmentShippingLocalSalesNoteRepository>();

            services.AddTransient<IGarmentShippingExportSalesDORepository, GarmentShippingExportSalesDORepository>();
            services.AddTransient<IGarmentShippingLocalSalesDORepository, GarmentShippingLocalSalesDORepository>();
            services.AddTransient<IGarmentLocalCoverLetterRepository, GarmentLocalCoverLetterRepository>();
            services.AddTransient<IGarmentShippingLocalPriceCorrectionNoteRepository, GarmentShippingLocalPriceCorrectionNoteRepository>();
            services.AddTransient<IGarmentShippingLocalReturnNoteRepository, GarmentShippingLocalReturnNoteRepository>();
            services.AddTransient<IGarmentShippingLocalPriceCuttingNoteRepository, GarmentShippingLocalPriceCuttingNoteRepository>();

            services.AddTransient<IGarmentShippingNoteItemRepository, GarmentShippingNoteItemRepository>();
            services.AddTransient<IGarmentShippingInvoiceItemRepository, GarmentShippingInvoiceItemRepository>();
            services.AddTransient<IGarmentShippingInvoiceAdjustmentRepository, GarmentShippingInvoiceAdjustmentRepository>();
            services.AddTransient<IGarmentShippingLocalSalesNoteItemRepository, GarmentShippingLocalSalesNoteItemRepository>();
            services.AddTransient<IGarmentShippingVBPaymentRepository, GarmentShippingVBPaymentRepository>();

            services.AddTransient<IGarmentShippingPaymentDispositionRepository, GarmentShippingPaymentDispositionRepository>();
            services.AddTransient<IGarmentShippingPaymentDispositionRecapRepository, GarmentShippingPaymentDispositionRecapRepository>();

            services.AddTransient<IGarmentShippingCostStructureRepository, GarmentShippingCostStructureRepository>();

            services.AddTransient<IRepository<CategoryModel>, CategoryRepository>();
            services.AddTransient<IRepository<UnitOfMeasurementModel>, UOMRepository>();
            services.AddTransient<IRepository<ProductSKUModel>, ProductSKURepository>();
            services.AddTransient<IRepository<ProductPackingModel>, ProductPackingRepository>();

            services.AddTransient<IBaseRepository<ProductPackingInventoryDocumentModel>, BaseRepository<ProductPackingInventoryDocumentModel>>();
            services.AddTransient<IBaseRepository<ProductPackingInventoryMovementModel>, BaseRepository<ProductPackingInventoryMovementModel>>();
            services.AddTransient<IBaseRepository<ProductPackingInventorySummaryModel>, BaseRepository<ProductPackingInventorySummaryModel>>();
            services.AddTransient<IBaseRepository<ProductSKUInventoryDocumentModel>, BaseRepository<ProductSKUInventoryDocumentModel>>();
            services.AddTransient<IBaseRepository<ProductSKUInventoryMovementModel>, BaseRepository<ProductSKUInventoryMovementModel>>();
            services.AddTransient<IBaseRepository<ProductSKUInventorySummaryModel>, BaseRepository<ProductSKUInventorySummaryModel>>();
            services.AddTransient<IBaseRepository<FabricProductSKUModel>, BaseRepository<FabricProductSKUModel>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IGarmentShippingLocalSalesContractRepository, GarmentShippingLocalSalesContractRepository>();
            services.AddTransient<IGarmentShippingInsuranceDispositionRepository, GarmentShippingInsuranceDispositionRepository>();
            services.AddTransient<IGarmentDebiturBalanceRepository, GarmentDebiturBalanceRepository>();

            services.AddTransient<IGarmentDraftPackingListItemRepository, GarmentDraftPackingListItemRepository>();
            services.AddTransient<IDyeingPrintingStockOpnameMutationRepository, DyeingPrintingStockOpnameMutationRepository>();
            services.AddTransient<IDyeingPrintingStockOpnameMutationItemRepository, DyeingPrintingStockOpnameMutationItemRepository>();
            


            services.AddTransient<IGarmentShippingExportSalesContractRepository, GarmentShippingExportSalesContractRepository>();
            services.AddTransient<IGarmentExportCoverLetterRepository, GarmentExportCoverLetterRepository>();
            services.AddTransient<IGarmentShippingLeftOverExportSalesDORepository, GarmentShippingLeftOverExportSalesDORepository>();
            services.AddTransient<IGarmentShippingExportSalesNoteRepository, GarmentShippingExportSalesNoteRepository>();
            services.AddTransient<IGarmentShippingExportSalesNoteItemRepository, GarmentShippingExportSalesNoteItemRepository>();
            services.AddTransient<IGarmentShippingNoteCreditAdviceRepository, GarmentShippingNoteCreditAdviceRepository>();

            //services.AddTransient<IDyeingPrintingStockOpnameSummaryRepository, DyeingPrintingStockOpnameSummaryRepository>();

            services.AddTransient<IGarmentShippingDetailLocalSalesNoteRepository, GarmentShippingDetailLocalSalesNoteRepository>();
            services.AddTransient<IGarmentShippingDetailLocalSalesNoteItemRepository, GarmentShippingDetailLocalSalesNoteItemRepository>();

            services.AddTransient<IDyeingPrintingStockOpnameSummaryRepository, DyeingPrintingStockOpnameSummaryRepository>();


            services.AddTransient<IDyeingPrintingStockOpnameSummaryRepository, DyeingPrintingStockOpnameSummaryRepository>();

            services.AddTransient<ILogHistoryRepository, LogHistoryRepository>();
            services.AddTransient<IGarmentReceiptSubconPackingListRepository, GarmentReceiptSubconPackingListRepository>();
            services.AddTransient<IGarmentReceiptSubconPackingListItemRepository, GarmentReceiptSubconPackingListItemRepository>();
            services.AddTransient<IGarmentReceiptSubconPackingListDetailRepository, GarmentReceiptSubconPackingListDetailRepository>();
            services.AddTransient<IGarmentReceiptSubconPackingListDetailSizeRepository, GarmentReceiptSubconPackingListDetailSizeRepository>();

            services.AddTransient<ILogHistoryRepository, LogHistoryRepository>();

            services.AddTransient<IGarmentShippingLocalSalesNoteTSRepository, GarmentShippingLocalSalesNoteTSRepository>();
            services.AddTransient<IGarmentShippingLocalSalesNoteTSItemRepository, GarmentShippingLocalSalesNoteTSItemRepository>();

            services.AddTransient<IGarmentLocalCoverLetterTSRepository, GarmentLocalCoverLetterTSRepository>();

            services.AddTransient<IGarmentShippingLocalSalesDOTSRepository, GarmentShippingLocalSalesDOTSRepository>();

            services.AddTransient<IGarmentMDLocalSalesContractRepository, GarmentMDLocalSalesContractRepository>();
            #endregion

            #region Service
            services.AddTransient<IInputInspectionMaterialService, InputInspectionMaterialService>();
            services.AddTransient<IOutputInspectionMaterialService, OutputInspectionMaterialService>();
            services.AddTransient<IInputTransitService, InputTransitService>();
            services.AddTransient<IOutputTransitService, OutputTransitService>();
            services.AddTransient<IInputPackagingService, InputPackagingService>();
            services.AddTransient<IOutputPackagingService, OutputPackagingService>();
            services.AddTransient<IInputAvalService, InputAvalService>();
            services.AddTransient<IOutputAvalService, OutputAvalService>();
            services.AddTransient<IInputShippingService, InputShippingService>();
            services.AddTransient<IOutputShippingService, OutputShippingService>();
            services.AddTransient<IInputWarehouseService, InputWarehouseService>();
            services.AddTransient<IOutputWarehouseService, OutputWarehouseService>();
            services.AddTransient<IStockOpnameWarehouseService, StockOpnameWarehouseService>();
            services.AddTransient<IInputAvalTransformationService, InputAvalTransformationService>();
            services.AddTransient<IStockWarehouseService, StockWarehouseService>();
            services.AddTransient<IAvalStockReportService, AvalStockReportService>();
            services.AddTransient<IGarmentShippingInvoiceService, GarmentShippingInvoiceService>();
            services.AddTransient<IIPWidthTypeService, IPWidthService>();
            services.AddTransient<IIPYarnTypeService, IPYarnTypeService>();
            services.AddTransient<IIPWovenTypeService, IPWovenTypeService>();
            services.AddTransient<IIPProcessTypeService, IPProcessTypeService>();
            services.AddTransient<IGarmentPackingListService, GarmentPackingListService>();
            services.AddTransient<IGarmentPackingListDraftService, GarmentPackingListDraftService>();
            services.AddTransient<IGarmentPackingListItemsService, GarmentPackingListItemsService>();
            services.AddTransient<IGarmentCoverLetterService, GarmentCoverLetterService>();
            services.AddTransient<IGarmentShippingCreditNoteService, GarmentShippingCreditNoteService>();
            services.AddTransient<IGarmentShippingDebitNoteService, GarmentShippingDebitNoteService>();
            services.AddTransient<IGarmentLetterOfCreditService, GarmentLetterOfCreditService>();
            services.AddTransient<IGarmentAmendLetterOfCreditService, GarmentAmendLetterOfCreditService>();
            services.AddTransient<IGarmentShippingInstructionService, GarmentShippingInstructionService>();
            services.AddTransient<IWeftTypeService, WeftTypeService>();
            services.AddTransient<IWarpTypeService, WarpTypeService>();
            services.AddTransient<IMaterialConstructionService, MaterialConstructionService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<IGarmentShippingCreditAdviceService, GarmentShippingCreditAdviceService>();
            services.AddTransient<IGarmentShippingLocalSalesNoteService, GarmentShippingLocalSalesNoteService>();

            services.AddTransient<Application.ToBeRefactored.GarmentShipping.ExportSalesDO.IGarmentShippingExportSalesDOService, Application.ToBeRefactored.GarmentShipping.ExportSalesDO.GarmentShippingExportSalesDOService>();
            services.AddTransient<IGarmentShippingLocalSalesDOService, GarmentShippingLocalSalesDOService>();
            services.AddTransient<IGarmentLocalCoverLetterService, GarmentLocalCoverLetterService>();

            services.AddTransient<IGarmentPackingListMonitoringService, GarmentPackingListMonitoringService>();

            services.AddTransient<IGarmentShippingLocalPriceCorrectionNoteService, GarmentShippingLocalPriceCorrectionNoteService>();
            services.AddTransient<IGarmentShippingLocalReturnNoteService, GarmentShippingLocalReturnNoteService>();
            services.AddTransient<IGarmentShippingLocalPriceCuttingNoteService, GarmentShippingLocalPriceCuttingNoteService>();

            services.AddTransient<IGarmentShippingVBPaymentService, GarmentShippingVBPaymentService>();

            services.AddTransient<IGarmentShippingLocalSalesContractService, GarmentShippingLocalSalesContractService>();
            services.AddTransient<IGarmentShippingInsuranceDispositionService, GarmentShippingInsuranceDispositionService>();

            services.AddTransient<IGarmentShippingPaymentDispositionService, GarmentShippingPaymentDispositionService>();
            services.AddTransient<IPaymentDispositionRecapService, PaymentDispositionRecapService>();

            services.AddTransient<IGarmentShippingCostStructureService, GarmentShippingCostStructureService>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUOMService, UOMService>();
            services.AddTransient<IProductSKUService, ProductSKUService>();
            services.AddTransient<IProductPackingService, ProductPackingService>();
            services.AddTransient<IFabricPackingSKUService, FabricPackingSKUService>();

            services.AddTransient<IInventorySKUService, InventorySKUService>();

            //services.AddTransient<IAzureServiceBusSender<ProductSKUInventoryMovementModel>, SKUInventoryAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            //services.AddTransient<IAzureServiceBusConsumer<ProductSKUInventoryMovementModel>, SKUInventoryAzureServiceBusConsumer<ProductSKUInventoryMovementModel>>();
            services.AddTransient<IGarmentInvoiceMonitoringService, GarmentInvoiceMonitoringService>();
            services.AddTransient<IGarmentDebitNoteMonitoringService, GarmentDebitNoteMonitoringService>();
            services.AddTransient<IGarmentCreditNoteMonitoringService, GarmentCreditNoteMonitoringService>();
            services.AddTransient<IGarmentOmzetMonthlyByUnitService, GarmentOmzetMonthlyByUnitService>();
            services.AddTransient<IGarmentOmzetMonthlyByBuyerService, GarmentOmzetMonthlyByBuyerService>();
            services.AddTransient<IGarmentOmzetMonthlyBySectionService, GarmentOmzetMonthlyBySectionService>();
            services.AddTransient<IRecapOmzetPerMonthMonitoringService, RecapOmzetPerMonthMonitoringService>();
            services.AddTransient<IGarmentOmzetMonthlyByCountryService, GarmentOmzetMonthlyByCountryService>();
            services.AddTransient<IGarmentOmzetMonthlyByComodityService, GarmentOmzetMonthlyByComodityService>();
            services.AddTransient<IOmzetYearBuyerService, OmzetYearBuyerService>();
            services.AddTransient<IOmzetYearUnitService, OmzetYearUnitService>();
            services.AddTransient<IOmzetYearSectionService, OmzetYearSectionService>();
            services.AddTransient<IGarmentLocalSalesReportByBuyerService, GarmentLocalSalesReportByBuyerService>();
            services.AddTransient<IGarmentShippingInstructionMonitoringService, GarmentShippingInstructionMonitoringService>();
            services.AddTransient<IGarmentCoverLetterMonitoringService, GarmentCoverLetterMonitoringService>();
            services.AddTransient<IGarmentShipmentMonitoringService, GarmentShipmentMonitoringService>();
            services.AddTransient<IGarmentLetterOfCreditMonitoringService, GarmentLetterOfCreditMonitoringService>();
            services.AddTransient<IGarmentCreditAdviceMonitoringService, GarmentCreditAdviceMonitoringService>();
            services.AddTransient<IGarmentInvoiceHistoryMonitoringService, GarmentInvoiceHistoryMonitoringService>();
            services.AddTransient<IGarmentLocalSalesBookService, GarmentLocalSalesBookService>();
            services.AddTransient<IGarmentShippingGenerateDataService, GarmentShippingGenerateDataService>();
            services.AddTransient<IDyeingPrintingStockOpnameProductionOrderRepository, DyeingPrintingStockOpnameProductionOrderRepository>();
            services.AddTransient<IGarmentInsuranceDispositionReportService, GarmentInsuranceDispositionReportService>();
            //services.AddTransient<IGarmentPaymentDispositionReportService, GarmentPaymentDispositionReportService>();
            services.AddTransient<IGarmentPaymentDispositionReportService, GarmentPaymentDispositionReportService>();
            //services.AddTransient<IGarmentPaymentDispositionRecapReportService, GarmentPaymentDispositionRecapReportService>();

            services.AddTransient<IGarmentLocalSalesOmzetService, GarmentLocalSalesOmzetService>();
            services.AddTransient<IOmzetYearCountryService, OmzetYearCountryService>();
            services.AddTransient<IOmzetYearBuyerComodityService, OmzetYearBuyerComodityService>();
            services.AddTransient<IDyeingPrintingProductService, DyeingPrintingProductService>();
            services.AddTransient<IGarmentCMTSalesService, GarmentCMTSalesService>();
            services.AddTransient<IGarmentDetailOmzetByUnitReportService, GarmentDetailOmzetByUnitReportService>();
            services.AddTransient<IGarmentRecapOmzetReportService, GarmentRecapOmzetReportService>();
            services.AddTransient<IGarmentDebiturBalanceService, GarmentDebiturBalanceService>();
            services.AddTransient<IGarmentFinanceExportSalesJournalService, GarmentFinanceExportSalesJournalService>();
            services.AddTransient<IGarmentFinanceLocalSalesJournalService, GarmentFinanceLocalSalesJournalService>();
            services.AddTransient<IGarmentOmzetMonthlyByBrandService, GarmentOmzetMonthlyByBrandService>();

            services.AddTransient<IGarmentDraftPackingListItemService, GarmentDraftPackingListItemService>();
            services.AddTransient<IGarmentLocalSalesOmzetUnPaidService, GarmentLocalSalesOmzetUnPaidService>();
            services.AddTransient<IGarmentMonitoringDeliveredPackingListSample, GarmentMonitoringDeliveredPackingListSampleService>();
            services.AddTransient<IGarmentOmzetAnnualByUnitReportService, GarmentOmzetAnnualByUnitReportService>();

            services.AddTransient<IGarmentOmzetMonthlyByMarketingService, GarmentOmzetMonthlyByMarketingService>();
            services.AddTransient<IOmzetYearMarketingService, OmzetYearMarketingService>();
            services.AddTransient<IStockOpnameMutationService, StockOpnameMutationService>();
            services.AddTransient<IStockOpnameSummaryService, StockOpnameSummaryService>();

            services.AddTransient<IProductionSubmissionReportService, ProductionSubmissionReportService>();
            services.AddTransient<IOrderStatusReportService, OrderStatusReportService>();

            services.AddTransient<IQcToWarehouseReportService, QcToWarehouseReportService>();


          
            services.AddTransient<IFabricQualityControlReportService, FabricQualityControlReportService>();
            services.AddTransient<IRegradingResultDocReportService, RegradingResultDocReportService>();


            services.AddTransient<IGarmentShippingExportSalesContractService, GarmentShippingExportSalesContractService>();
            services.AddTransient<IGarmentExportCoverLetterService, GarmentExportCoverLetterService>();
            services.AddTransient<Application.ToBeRefactored.GarmentShipping.SalesExport.IGarmentShippingExportSalesDOService, Application.ToBeRefactored.GarmentShipping.SalesExport.GarmentShippingExportSalesDOService>();
            services.AddTransient<IGarmentShippingExportSalesNoteService, GarmentShippingExportSalesNoteService>();
            services.AddTransient<IGarmentBuyerReceivablesReportService, GarmentBuyerReceivablesReportService>();
            services.AddTransient<IGarmentShippingPaymentEMKLDispositionService, GarmentShippingPaymentDispositionEMKLService>();
            services.AddTransient<IGarmentShippingDetailLocalSalesNoteService, GarmentShippingDetailLocalSalesNoteService>();
            services.AddTransient<IProductionSubmissionReportService, ProductionSubmissionReportService>();
            services.AddTransient<IGarmentCreditAdviceMIIMonitoringService, GarmentCreditAdviceMIIMonitoringService>();
            services.AddTransient<IGarmentFinanceDetailLocalSalesJournalService, GarmentFinanceDetailLocalSalesJournalService>();
            services.AddTransient<IGarmentFinanceDetailExportSalesJournalService, GarmentFinanceDetailExportSalesJournalService>();
            services.AddTransient<IGarmentShippingNoteCreditAdviceService, GarmentShippingNoteCreditAdviceService>();
            services.AddTransient<IGarmentShippingNoteCreditAdviceMonitoringService, GarmentShippingNoteCreditAdviceMonitoringService>();
            services.AddTransient<IGarmentShippingNoteCreditAdviceMIIMonitoringService, GarmentShippingNoteCreditAdviceMIIMonitoringService>();

            services.AddTransient<IGarmentReceiptSubconPackingListService, GarmentReceiptSubconPackingListService>();

            services.AddTransient<IGarmentShippingLocalSalesNoteTSService, GarmentShippingLocalSalesNoteTSService>();

            services.AddTransient<IGarmentLocalCoverLetterTSService, GarmentLocalCoverLetterTSService>();

            services.AddTransient<IGarmentShippingLocalSalesDOTSService, GarmentShippingLocalSalesDOTSService>();

            services.AddTransient<IFinishedGoodsMinutesService, FinishedGoodsMinutesService>();
            services.AddTransient<IShipmentLocalSalesNoteService, ShipmentLocalSalesNoteService>();

            //AR
            services.AddTransient<ICashInBankService, CashInBankService>();
            services.AddTransient<ICMTService, CMTService>();
            services.AddTransient<IDownPaymentService, DownPaymentService>();
            services.AddTransient<IOmzetCorrectionService, OmzetCorrectionService>();
            services.AddTransient<IAR_ReportMutationService, AR_ReportMutationService>();

            services.AddTransient<IGarmentMDLocalSalesContractService, GarmentMDLocalSalesContractService>();

            #endregion

            // Register Provider
            services.AddScoped<IIdentityProvider, IdentityProvider>();
            services.AddScoped<IValidateService, ValidateService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IAzureImageService, AzureImageService>();
            services.AddScoped<ILogHistoryService, LogHistoryService>();


            var secret = Configuration.GetValue<string>("Secret") ?? Configuration["Secret"];
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            // Add Authentication
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = key
                    };
                });

            services.AddCors(option => option.AddPolicy(PACKING_INVENTORY_POLICY, builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders(_exposedHeaders);
            }));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo() { Title = "Packing Inventory API", Version = "v1" });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
                //swagger.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
                //{
                //    {
                //        "Bearer",
                //        Enumerable.Empty<string>()
                //    }
                //});
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                swagger.CustomSchemaIds(i => i.FullName);
            });
            services
                //.AddMvcCore(options => options.Filters.Add(typeof(ValidateModelStateAttribute)))
                .AddMvcCore()
                .AddJsonFormatters()
                .AddApiExplorer()
                .AddAuthorization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => { });

            //services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            // Register Validator
            services.AddSingleton<IValidator<FabricQualityControlViewModel>, FabricQualityControlValidator>();
            //services.AddSingleton<IValidator<InputInspectionMaterialViewModel>, InputInspectionMaterialValidator>();
            //services.AddSingleton<IValidator<OutputInspectionMaterialViewModel>, OutputInspectionMaterialValidator>();
            //services.AddSingleton<IValidator<InputTransitViewModel>, InputTransitValidator>();
            //services.AddSingleton<IValidator<OutputTransitViewModel>, OutputTransitValidator>();
            services.AddSingleton<IValidator<InputPackagingViewModel>, InputPackagingValidator>();
            //services.AddSingleton<IValidator<OutputPackagingViewModel>, OutputPackagingValidator>();
            services.AddSingleton<IValidator<InputAvalViewModel>, InputAvalValidator>();
            services.AddSingleton<IValidator<InputAvalItemViewModel>, InputAvalItemValidator>();
            //services.AddSingleton<IValidator<OutputAvalViewModel>, OutputAvalValidator>();
            //services.AddSingleton<IValidator<OutputAvalItemViewModel>, OutputAvalItemValidator>();
            //services.AddSingleton<IValidator<InputShippingViewModel>, InputShippingValidator>();
            //services.AddSingleton<IValidator<OutputShippingViewModel>, OutputShippingValidator>();
            //services.AddSingleton<IValidator<InputWarehouseCreateViewModel>, InputWarehouseCreateValidator>();
            //services.AddSingleton<IValidator<OutputWarehouseViewModel>, OutputWarehouseValidator>();
            services.AddSingleton<IValidator<IPWidthTypeViewModel>, IPWidthTypeViewModelValidator>();
            services.AddSingleton<IValidator<IPYarnTypeViewModel>, IPYarnTypeViewModelValidator>();
            services.AddSingleton<IValidator<IPWovenTypeViewModel>, IPWovenTypeViewModelValidator>();
            services.AddSingleton<IValidator<IPProcessTypeViewModel>, IPProcessTypeViewModelValidator>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetService<PackingInventoryDbContext>();
                //context.Database.SetCommandTimeout(1000);
                //context.Database.Migrate();

                //var bus = serviceScope.ServiceProvider.GetService<IAzureServiceBusConsumer<ProductSKUInventoryMovementModel>>();
                //bus.RegisterOnMessageHandlerAndReceiveMessages();
            }

            app.UseCors(PACKING_INVENTORY_POLICY);

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Packing Inventory API");
            });

            //var bus = app.ApplicationServices.GetService<IAzureServiceBusConsumer<ProductSKUInventoryMovementModel>>();
            //bus.RegisterOnMessageHandlerAndReceiveMessages();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
