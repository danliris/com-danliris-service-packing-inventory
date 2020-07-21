using Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Product;
using Com.Danliris.Service.Packing.Inventory.Application.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Application.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.ReceivingDispatchDocument;
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
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentSKU;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
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
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.AvalTransformation;
using System.Collections.Generic;
using System.Text;
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
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WarpType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.MaterialConstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWidthType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWidthType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWarpType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWarpType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.Grade;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDebitNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCreditNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByUnit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByBuyer;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyBySection;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByCountry;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByComodity;

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

        private void RegisterEndpoints()
        {
            APIEndpoint.Core = Configuration.GetValue<string>(Constant.CORE_ENDPOINT) ?? Configuration[Constant.CORE_ENDPOINT];
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterEndpoints();

            // Register Middleware

            #region Repository
            services.AddTransient<IProductSKURepository, ProductSKURepository>();
            services.AddTransient<IProductPackingRepository, ProductPackingRepository>();
            services.AddTransient<IInventoryDocumentSKURepository, InventoryDocumentSKURepository>();
            services.AddTransient<IInventoryDocumentPackingRepository, InventoryDocumentPackingRepository>();
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
            services.AddTransient<IDyeingPrintingAreaMovementRepository, DyeingPrintingAreaMovementRepository>();
            services.AddTransient<IDyeingPrintingAreaSummaryRepository, DyeingPrintingAreaSummaryRepository>();
			services.AddTransient<IGarmentShippingInvoiceRepository, GarmentShippingInvoiceRepository>();
			services.AddTransient<IGarmentShippingInstructionRepository, GarmentShippingInstructionRepository>();
            services.AddTransient<IGarmentPackingListRepository, GarmentPackingListRepository>();
            services.AddTransient<IGarmentCoverLetterRepository, GarmentCoverLetterRepository>();
            services.AddTransient<IGarmentShippingNoteRepository, GarmentShippingNoteRepository>();
            services.AddTransient<IGarmentLetterOfCreditRepository, GarmentLetterOfCreditRepository>();
            services.AddTransient<IGarmentAmendLetterOfCreditRepository, GarmentAmendLetterOfCreditRepository>();
            services.AddTransient<IWeftTypeRepository, WeftTypeRepository>();
            services.AddTransient<IWarpTypeRepository, WarpTypeRepository>();
            services.AddTransient<IMaterialConstructionRepository, MaterialConstructionRepository>();
            services.AddTransient<IIPWidthTypeRepository, IPWidthTypeRepository>();
            services.AddTransient<IIPYarnTypeRepository, IPYarnTypeRepository>();
            services.AddTransient<IIPWarpTypeRepository, IPWarpTypeRepository>();
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
            #endregion

            #region Service
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductPackingService, ProductPackingService>();
            services.AddTransient<IProductSKUService, ProductSKUService>();
            services.AddTransient<IReceivingDispatchService, ReceivingDispatchService>();
            services.AddTransient<IInventoryDocumentSKUService, InventoryDocumentSKUService>();
            services.AddTransient<IInventoryDocumentPackingService, InventoryDocumentPackingService>();
            services.AddTransient<IFabricQualityControlService, FabricQualityControlService>();
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
            services.AddTransient<IInputAvalTransformationService, InputAvalTransformationService>();
            services.AddTransient<IStockWarehouseService, StockWarehouseService>();
            services.AddTransient<IAvalStockReportService, AvalStockReportService>();
            services.AddTransient<IGoodsWarehouseDocumentsService, GoodsWarehouseDocumentsService>();
            services.AddTransient<IGarmentShippingInvoiceService, GarmentShippingInvoiceService>();
            services.AddTransient<IIPWidthTypeService, IPWidthService>();
            services.AddTransient<IIPYarnTypeService, IPYarnTypeService>();
            services.AddTransient<IIPWarpTypeService, IPWarpTypeService>();
            services.AddTransient<IIPProcessTypeService, IPProcessTypeService>();
            services.AddTransient<IGarmentPackingListService, GarmentPackingListService>();
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

            services.AddTransient <IGarmentShippingExportSalesDOService, GarmentShippingExportSalesDOService>();
            services.AddTransient<IGarmentShippingLocalSalesDOService, GarmentShippingLocalSalesDOService>();
            services.AddTransient<IGarmentLocalCoverLetterService, GarmentLocalCoverLetterService>();

            services.AddTransient<IGarmentPackingListMonitoringService, GarmentPackingListMonitoringService>();

            services.AddTransient<IGarmentShippingLocalPriceCorrectionNoteService, GarmentShippingLocalPriceCorrectionNoteService>();
            services.AddTransient<IGarmentShippingLocalReturnNoteService, GarmentShippingLocalReturnNoteService>();
            services.AddTransient<IGarmentShippingLocalPriceCuttingNoteService, GarmentShippingLocalPriceCuttingNoteService>();

            services.AddTransient<IGarmentInvoiceMonitoringService, GarmentInvoiceMonitoringService>();
            services.AddTransient<IGarmentDebitNoteMonitoringService, GarmentDebitNoteMonitoringService>();
            services.AddTransient<IGarmentCreditNoteMonitoringService, GarmentCreditNoteMonitoringService>();
            services.AddTransient<IGarmentOmzetMonthlyByUnitService, GarmentOmzetMonthlyByUnitService>();
            services.AddTransient<IGarmentOmzetMonthlyByBuyerService, GarmentOmzetMonthlyByBuyerService>();
            services.AddTransient<IGarmentOmzetMonthlyBySectionService, GarmentOmzetMonthlyBySectionService>();
            services.AddTransient<IGarmentOmzetMonthlyByCountryService, GarmentOmzetMonthlyByCountryService>();
            services.AddTransient<IGarmentOmzetMonthlyByComodityService, GarmentOmzetMonthlyByComodityService>();

            #endregion

            // Register Provider
            services.AddScoped<IIdentityProvider, IdentityProvider>();
            services.AddScoped<IValidateService, ValidateService>();
            services.AddScoped<IHttpClientService, HttpClientService>();

            var connectionString = Configuration.GetConnectionString(DEFAULT_CONNECTION) ?? Configuration[DEFAULT_CONNECTION];
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<PackingInventoryDbContext>(options =>
                {
                    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                    {
                        // sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    });
                });

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
            services.AddSingleton<IValidator<CreateProductPackAndSKUViewModel>, CreateProductPackAndSKUValidator>();
            services.AddSingleton<IValidator<CreateReceivingDispatchDocumentViewModel>, CreateReceivingDispatchDocumentValidator>();
            services.AddSingleton<IValidator<CreateProductSKUViewModel>, CreateProductSKUValidator>();
            services.AddSingleton<IValidator<UpdateProductSKUViewModel>, UpdateProductSKUValidator>();
            services.AddSingleton<IValidator<ProductPackingFormViewModel>, ProductPackingFormValidator>();
            services.AddSingleton<IValidator<CreateInventoryDocumentSKUViewModel>, CreateInventoryDocumentSKUValidator>();
            services.AddSingleton<IValidator<CreateInventoryDocumentPackingViewModel>, CreateInventoryDocumentPackingValidator>();
            services.AddSingleton<IValidator<FabricQualityControlViewModel>, FabricQualityControlValidator>();
            //services.AddSingleton<IValidator<InputInspectionMaterialViewModel>, InputInspectionMaterialValidator>();
            //services.AddSingleton<IValidator<OutputInspectionMaterialViewModel>, OutputInspectionMaterialValidator>();
            //services.AddSingleton<IValidator<InputTransitViewModel>, InputTransitValidator>();
            //services.AddSingleton<IValidator<OutputTransitViewModel>, OutputTransitValidator>();
            services.AddSingleton<IValidator<InputPackagingViewModel>, InputPackagingValidator>();
            services.AddSingleton<IValidator<OutputPackagingViewModel>, OutputPackagingValidator>();
            services.AddSingleton<IValidator<InputAvalViewModel>, InputAvalValidator>();
            services.AddSingleton<IValidator<InputAvalItemViewModel>, InputAvalItemValidator>();
            //services.AddSingleton<IValidator<OutputAvalViewModel>, OutputAvalValidator>();
            //services.AddSingleton<IValidator<OutputAvalItemViewModel>, OutputAvalItemValidator>();
            //services.AddSingleton<IValidator<InputShippingViewModel>, InputShippingValidator>();
            //services.AddSingleton<IValidator<OutputShippingViewModel>, OutputShippingValidator>();
            services.AddSingleton<IValidator<InputWarehouseCreateViewModel>, InputWarehouseCreateValidator>();
            //services.AddSingleton<IValidator<OutputWarehouseViewModel>, OutputWarehouseValidator>();
            services.AddSingleton<IValidator<IPWidthTypeViewModel>, IPWidthTypeViewModelValidator>();
            services.AddSingleton<IValidator<IPYarnTypeViewModel>, IPYarnTypeViewModelValidator>();
            services.AddSingleton<IValidator<IPWarpTypeViewModel>, IPWarpTypeViewModelValidator>();
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
                var context = serviceScope.ServiceProvider.GetService<PackingInventoryDbContext>();
                context.Database.Migrate();
            }

            app.UseCors(PACKING_INVENTORY_POLICY);

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Packing Inventory API");
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
