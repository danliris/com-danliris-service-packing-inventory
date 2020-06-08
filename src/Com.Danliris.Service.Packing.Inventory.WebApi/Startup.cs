﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse;
using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Product;
using Com.Danliris.Service.Packing.Inventory.Application.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Application.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.ReceivingDispatchDocument;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
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
using Swashbuckle.AspNetCore.Swagger;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Transit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Shipping;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Shipping;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Create;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.StockWarehouse;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Newtonsoft.Json.Serialization;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            // Register Middleware
            services.AddTransient<IProductSKURepository, ProductSKURepository>();
            services.AddTransient<IProductPackingRepository, ProductPackingRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductPackingService, ProductPackingService>();
            services.AddTransient<IProductSKUService, ProductSKUService>();

            services.AddTransient<IInventoryDocumentSKURepository, InventoryDocumentSKURepository>();
            services.AddTransient<IInventoryDocumentPackingRepository, InventoryDocumentPackingRepository>();
            services.AddTransient<IReceivingDispatchService, ReceivingDispatchService>();
            services.AddTransient<IInventoryDocumentSKUService, InventoryDocumentSKUService>();
            services.AddTransient<IInventoryDocumentPackingService, InventoryDocumentPackingService>();
            services.AddTransient<IDyeingPrintingAreaMovementRepository, DyeingPrintingAreaMovementRepository>();
            
            services.AddTransient<IFabricQualityControlRepository, FabricQualityControlRepository>();
            services.AddTransient<IFabricGradeTestRepository, FabricGradeTestRepository>();
            services.AddTransient<ICriteriaRepository, CriteriaRepository>();
            services.AddTransient<IFabricQualityControlService, FabricQualityControlService>();
            services.AddTransient<IGoodsWarehouseDocumentsService, GoodsWarehouseDocumentsService>();

            services.AddTransient<IMaterialDeliveryNoteRepository, MaterialDeliveryNoteRepository>();
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IMaterialDeliveryNoteService, MaterialDeliveryNoteService>();
            
            services.AddTransient<IDyeingPrintingAreaInputRepository, DyeingPrintingAreaInputRepository>();
            services.AddTransient<IDyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderRepository>();
            services.AddTransient<IDyeingPrintingAreaOutputRepository, DyeingPrintingAreaOutputRepository>();
            services.AddTransient<IDyeingPrintingAreaOutputProductionOrderRepository, DyeingPrintingAreaOutputProductionOrderRepository>();
            services.AddTransient<IDyeingPrintingAreaMovementRepository, DyeingPrintingAreaMovementRepository>();
            services.AddTransient<IDyeingPrintingAreaSummaryRepository, DyeingPrintingAreaSummaryRepository>();

            services.AddTransient<IGarmentPackingListRepository, GarmentPackingListRepository>();

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

            services.AddTransient<IStockWarehouseService, StockWarehouseService>();

            services.AddTransient<IGarmentPackingListService, GarmentPackingListService>();

            // Register Provider
            services.AddScoped<IIdentityProvider, IdentityProvider>();
            services.AddScoped<IValidateService, ValidateService>();

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
            services.AddSingleton<IValidator<OutputAvalViewModel>, OutputAvalValidator>();
            services.AddSingleton<IValidator<OutputAvalItemViewModel>, OutputAvalItemValidator>();
            //services.AddSingleton<IValidator<InputShippingViewModel>, InputShippingValidator>();
            //services.AddSingleton<IValidator<OutputShippingViewModel>, OutputShippingValidator>();
            services.AddSingleton<IValidator<InputWarehouseCreateViewModel>, InputWarehouseCreateValidator>();
            services.AddSingleton<IValidator<OutputWarehouseViewModel>, OutputWarehouseValidator>();

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

            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<PackingInventoryDbContext>();
            //    context.Database.Migrate();
            //}

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
