using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class InitialPackingInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaInputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    Shift = table.Column<string>(maxLength: 64, nullable: true),
                    BonNo = table.Column<string>(maxLength: 64, nullable: true),
                    Group = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaMovements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Area = table.Column<string>(maxLength: 128, nullable: true),
                    Type = table.Column<string>(maxLength: 32, nullable: true),
                    DyeingPrintingAreaDocumentId = table.Column<int>(nullable: false),
                    DyeingPrintingAreaDocumentBonNo = table.Column<string>(maxLength: 64, nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    Buyer = table.Column<string>(maxLength: 4096, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    Unit = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Motif = table.Column<string>(maxLength: 4096, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Balance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    Shift = table.Column<string>(maxLength: 64, nullable: true),
                    BonNo = table.Column<string>(maxLength: 64, nullable: true),
                    HasNextAreaDocument = table.Column<bool>(nullable: false),
                    DestinationArea = table.Column<string>(maxLength: 64, nullable: true),
                    Group = table.Column<string>(maxLength: 16, nullable: true),
                    HasSalesInvoice = table.Column<bool>(nullable: false),
                    DeliveryOrderSalesId = table.Column<long>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Area = table.Column<string>(maxLength: 128, nullable: true),
                    Type = table.Column<string>(maxLength: 32, nullable: true),
                    DyeingPrintingAreaDocumentId = table.Column<int>(nullable: false),
                    DyeingPrintingAreaDocumentBonNo = table.Column<string>(maxLength: 64, nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    Buyer = table.Column<string>(maxLength: 4096, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    Unit = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Motif = table.Column<string>(maxLength: 4096, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Balance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocumentPackings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 256, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocumentSKUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 256, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPPackings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Size = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    PackingId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPUnitOfMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPUnitOfMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewFabricQualityControls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    UId = table.Column<string>(maxLength: 256, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    DateIm = table.Column<DateTimeOffset>(nullable: false),
                    Group = table.Column<string>(maxLength: 4096, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    DyeingPrintingAreaInputId = table.Column<int>(nullable: false),
                    DyeingPrintingAreaInputBonNo = table.Column<string>(maxLength: 64, nullable: true),
                    DyeingPrintingAreaInputProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    MachineNoIm = table.Column<string>(maxLength: 4096, nullable: true),
                    OperatorIm = table.Column<string>(maxLength: 4096, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewFabricQualityControls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackagingStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DyeingPrintingProductionOrderId = table.Column<int>(maxLength: 128, nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingType = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingUnit = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasNextArea = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    PackingType = table.Column<string>(maxLength: 32, nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    SKUId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Composition = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 128, nullable: true),
                    Design = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 32, nullable: true),
                    LotNo = table.Column<string>(maxLength: 128, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    ProductType = table.Column<string>(maxLength: 32, nullable: true),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    Width = table.Column<string>(maxLength: 128, nullable: true),
                    WovenType = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType1 = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType2 = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaInputProductionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    Buyer = table.Column<string>(maxLength: 4096, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    Unit = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Motif = table.Column<string>(maxLength: 4096, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    HasOutputDocument = table.Column<bool>(nullable: false),
                    IsChecked = table.Column<bool>(nullable: false),
                    PackingInstruction = table.Column<string>(maxLength: 4096, nullable: true),
                    ProductionOrderType = table.Column<string>(maxLength: 512, nullable: true),
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 128, nullable: true),
                    Status = table.Column<string>(maxLength: 128, nullable: true),
                    InitLength = table.Column<double>(nullable: false),
                    AvalALength = table.Column<double>(nullable: false),
                    AvalBLength = table.Column<double>(nullable: false),
                    AvalConnectionLength = table.Column<double>(nullable: false),
                    AvalType = table.Column<string>(nullable: true),
                    AvalCartNo = table.Column<string>(nullable: true),
                    AvalQuantityKg = table.Column<double>(nullable: false),
                    DeliveryOrderSalesId = table.Column<long>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingUnit = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingType = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    BalanceRemains = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DyeingPrintingAreaInputId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaInputProductionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeingPrintingAreaInputProductionOrders_DyeingPrintingAreaInputs_DyeingPrintingAreaInputId",
                        column: x => x.DyeingPrintingAreaInputId,
                        principalTable: "DyeingPrintingAreaInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaOutputProductionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    Buyer = table.Column<string>(maxLength: 4096, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    Unit = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Motif = table.Column<string>(maxLength: 4096, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Remark = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 128, nullable: true),
                    Status = table.Column<string>(maxLength: 128, nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    PackingInstruction = table.Column<string>(maxLength: 4096, nullable: true),
                    ProductionOrderType = table.Column<string>(maxLength: 512, nullable: true),
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    PackagingType = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackagingUnit = table.Column<string>(maxLength: 128, nullable: true),
                    AvalALength = table.Column<double>(nullable: false),
                    AvalBLength = table.Column<double>(nullable: false),
                    AvalConnectionLength = table.Column<double>(nullable: false),
                    DeliveryOrderSalesId = table.Column<long>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(maxLength: 128, nullable: true),
                    AvalType = table.Column<string>(nullable: true),
                    AvalCartNo = table.Column<string>(nullable: true),
                    AvalQuantityKg = table.Column<double>(nullable: false),
                    HasNextAreaDocument = table.Column<bool>(nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    DestinationArea = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(maxLength: 4096, nullable: true),
                    DeliveryNote = table.Column<string>(maxLength: 128, nullable: true),
                    HasSalesInvoice = table.Column<bool>(nullable: false),
                    ShippingGrade = table.Column<string>(maxLength: 128, nullable: true),
                    ShippingRemark = table.Column<string>(maxLength: 512, nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    DyeingPrintingAreaInputProductionOrderId = table.Column<int>(maxLength: 128, nullable: false),
                    DyeingPrintingAreaOutputId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaOutputProductionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeingPrintingAreaOutputProductionOrders_DyeingPrintingAreaOutputs_DyeingPrintingAreaOutputId",
                        column: x => x.DyeingPrintingAreaOutputId,
                        principalTable: "DyeingPrintingAreaOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocumentPackingItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    InventoryDocumentSKUId = table.Column<int>(nullable: false),
                    PackingId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    SKUId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 32, nullable: true),
                    InventoryDocumentPackingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentPackingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryDocumentPackingItems_InventoryDocumentPackings_InventoryDocumentPackingId",
                        column: x => x.InventoryDocumentPackingId,
                        principalTable: "InventoryDocumentPackings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocumentSKUItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    BeforeQuantity = table.Column<decimal>(nullable: false),
                    CurrentQuantity = table.Column<decimal>(nullable: false),
                    InventoryDocumentSKUId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    SKUId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentSKUItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryDocumentSKUItems_InventoryDocumentSKUs_InventoryDocumentSKUId",
                        column: x => x.InventoryDocumentSKUId,
                        principalTable: "InventoryDocumentSKUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewFabricGradeTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    AvalALength = table.Column<double>(nullable: false),
                    AvalBLength = table.Column<double>(nullable: false),
                    AvalConnectionLength = table.Column<double>(nullable: false),
                    FabricGradeTest = table.Column<double>(nullable: false),
                    FinalArea = table.Column<double>(nullable: false),
                    FinalGradeTest = table.Column<double>(nullable: false),
                    FinalLength = table.Column<double>(nullable: false),
                    FinalScore = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(maxLength: 512, nullable: true),
                    InitLength = table.Column<double>(nullable: false),
                    PcsNo = table.Column<string>(maxLength: 4096, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<double>(nullable: false),
                    SampleLength = table.Column<double>(nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Type = table.Column<string>(maxLength: 1024, nullable: true),
                    Width = table.Column<double>(nullable: false),
                    ItemIndex = table.Column<int>(nullable: false),
                    FabricQualityControlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewFabricGradeTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewFabricGradeTests_NewFabricQualityControls_FabricQualityControlId",
                        column: x => x.FabricQualityControlId,
                        principalTable: "NewFabricQualityControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Group = table.Column<string>(maxLength: 4096, nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 4096, nullable: true),
                    ScoreA = table.Column<double>(nullable: false),
                    ScoreB = table.Column<double>(nullable: false),
                    ScoreC = table.Column<double>(nullable: false),
                    ScoreD = table.Column<double>(nullable: false),
                    FabricGradeTestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewCriterias_NewFabricGradeTests_FabricGradeTestId",
                        column: x => x.FabricGradeTestId,
                        principalTable: "NewFabricGradeTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_DyeingPrintingAreaInputId",
                table: "DyeingPrintingAreaInputProductionOrders",
                column: "DyeingPrintingAreaInputId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_DyeingPrintingAreaOutputId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                column: "DyeingPrintingAreaOutputId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocumentPackingItems_InventoryDocumentPackingId",
                table: "InventoryDocumentPackingItems",
                column: "InventoryDocumentPackingId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocumentSKUItems_InventoryDocumentSKUId",
                table: "InventoryDocumentSKUItems",
                column: "InventoryDocumentSKUId");

            migrationBuilder.CreateIndex(
                name: "IX_NewCriterias_FabricGradeTestId",
                table: "NewCriterias",
                column: "FabricGradeTestId");

            migrationBuilder.CreateIndex(
                name: "IX_NewFabricGradeTests_FabricQualityControlId",
                table: "NewFabricGradeTests",
                column: "FabricQualityControlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaMovements");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaSummaries");

            migrationBuilder.DropTable(
                name: "InventoryDocumentPackingItems");

            migrationBuilder.DropTable(
                name: "InventoryDocumentSKUItems");

            migrationBuilder.DropTable(
                name: "IPCategories");

            migrationBuilder.DropTable(
                name: "IPPackings");

            migrationBuilder.DropTable(
                name: "IPProducts");

            migrationBuilder.DropTable(
                name: "IPUnitOfMeasurements");

            migrationBuilder.DropTable(
                name: "NewCriterias");

            migrationBuilder.DropTable(
                name: "PackagingStock");

            migrationBuilder.DropTable(
                name: "ProductPackings");

            migrationBuilder.DropTable(
                name: "ProductSKUs");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaInputs");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropTable(
                name: "InventoryDocumentPackings");

            migrationBuilder.DropTable(
                name: "InventoryDocumentSKUs");

            migrationBuilder.DropTable(
                name: "NewFabricGradeTests");

            migrationBuilder.DropTable(
                name: "NewFabricQualityControls");
        }
    }
}
