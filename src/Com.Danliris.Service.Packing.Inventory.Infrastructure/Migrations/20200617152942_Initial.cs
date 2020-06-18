using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Initial : Migration
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
                    Group = table.Column<string>(maxLength: 16, nullable: true),
                    AvalType = table.Column<string>(maxLength: 128, nullable: true),
                    TotalAvalQuantity = table.Column<double>(nullable: false),
                    TotalAvalWeight = table.Column<double>(nullable: false),
                    IsTransformedAval = table.Column<bool>(nullable: false)
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
                    DyeingPrintingAreaProductionOrderDocumentId = table.Column<int>(nullable: false),
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
                    Balance = table.Column<double>(nullable: false),
                    AvalQuantity = table.Column<double>(nullable: false),
                    AvalWeightQuantity = table.Column<double>(nullable: false),
                    AvalType = table.Column<string>(maxLength: 128, nullable: true)
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
                    DyeingPrintingAreaProductionOrderDocumentId = table.Column<int>(nullable: false),
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
                name: "FabricProductPackings",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    FabricProductSKUId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    ProductPackingId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false),
                    PackingSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FabricProductSKUs",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    ColorWayId = table.Column<int>(nullable: false),
                    ConstructionTypeId = table.Column<int>(nullable: false),
                    GradeId = table.Column<int>(nullable: false),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false),
                    WarpThreadId = table.Column<int>(nullable: false),
                    WeftThreadId = table.Column<int>(nullable: false),
                    WidthId = table.Column<int>(nullable: false),
                    WovenTypeId = table.Column<int>(nullable: false),
                    ProductSKUID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricProductSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentPackingLists",
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
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    PackingListType = table.Column<string>(maxLength: 25, nullable: true),
                    InvoiceType = table.Column<string>(maxLength: 25, nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    SectionCode = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    LCNo = table.Column<string>(maxLength: 100, nullable: true),
                    IssuedBy = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    Destination = table.Column<string>(maxLength: 50, nullable: true),
                    TruckingDate = table.Column<DateTimeOffset>(nullable: false),
                    ExportEstimationDate = table.Column<DateTimeOffset>(nullable: false),
                    Omzet = table.Column<bool>(nullable: false),
                    Accounting = table.Column<bool>(nullable: false),
                    GrossWeight = table.Column<double>(nullable: false),
                    NettWeight = table.Column<double>(nullable: false),
                    TotalCartons = table.Column<double>(nullable: false),
                    ShippingMark = table.Column<string>(maxLength: 2000, nullable: true),
                    SideMark = table.Column<string>(maxLength: 2000, nullable: true),
                    Remark = table.Column<string>(maxLength: 2000, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentPackingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingAmendLetterOfCredits",
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
                    DocumentCreditNo = table.Column<string>(maxLength: 50, nullable: true),
                    LetterOfCreditId = table.Column<int>(nullable: false),
                    AmendNumber = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingAmendLetterOfCredits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingCoverLetters",
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
                    PackingListId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    ATTN = table.Column<string>(maxLength: 250, nullable: true),
                    Phone = table.Column<string>(maxLength: 250, nullable: true),
                    BookingDate = table.Column<DateTimeOffset>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    OrderCode = table.Column<string>(maxLength: 100, nullable: true),
                    OrderName = table.Column<string>(maxLength: 255, nullable: true),
                    PCSQuantity = table.Column<double>(nullable: false),
                    SETSQuantity = table.Column<double>(nullable: false),
                    PACKQuantity = table.Column<double>(nullable: false),
                    CartoonQuantity = table.Column<double>(nullable: false),
                    ForwarderId = table.Column<int>(nullable: false),
                    ForwarderCode = table.Column<string>(maxLength: 50, nullable: true),
                    ForwarderName = table.Column<string>(maxLength: 250, nullable: true),
                    Truck = table.Column<string>(maxLength: 250, nullable: true),
                    PlateNumber = table.Column<string>(maxLength: 250, nullable: true),
                    Driver = table.Column<string>(maxLength: 250, nullable: true),
                    ContainerNo = table.Column<string>(maxLength: 250, nullable: true),
                    Freight = table.Column<string>(maxLength: 250, nullable: true),
                    ShippingSeal = table.Column<string>(maxLength: 250, nullable: true),
                    DLSeal = table.Column<string>(maxLength: 250, nullable: true),
                    EMKLSeal = table.Column<string>(maxLength: 250, nullable: true),
                    ExportEstimationDate = table.Column<DateTimeOffset>(nullable: false),
                    Unit = table.Column<string>(maxLength: 1000, nullable: true),
                    ShippingStaffId = table.Column<int>(nullable: false),
                    ShippingStaffName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingCoverLetters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingCreditAdvices",
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
                    PackingListId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AmountToBePaid = table.Column<double>(nullable: false),
                    Valas = table.Column<bool>(nullable: false),
                    LCType = table.Column<string>(maxLength: 25, nullable: true),
                    Inkaso = table.Column<double>(nullable: false),
                    Disconto = table.Column<double>(nullable: false),
                    SRNo = table.Column<string>(maxLength: 250, nullable: true),
                    NegoDate = table.Column<DateTimeOffset>(nullable: false),
                    Condition = table.Column<string>(maxLength: 25, nullable: true),
                    BankComission = table.Column<double>(nullable: false),
                    DiscrepancyFee = table.Column<double>(nullable: false),
                    NettNego = table.Column<double>(nullable: false),
                    BTBCADate = table.Column<DateTimeOffset>(nullable: false),
                    BTBAmount = table.Column<double>(nullable: false),
                    BTBRatio = table.Column<double>(nullable: false),
                    BTBRate = table.Column<double>(nullable: false),
                    BTBTransfer = table.Column<double>(nullable: false),
                    BTBMaterial = table.Column<double>(nullable: false),
                    BillDays = table.Column<double>(nullable: false),
                    BillAmount = table.Column<double>(nullable: false),
                    BillCA = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    BankAccountId = table.Column<int>(nullable: false),
                    BankAccountName = table.Column<string>(maxLength: 255, nullable: true),
                    BankAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    CreditInterest = table.Column<double>(nullable: false),
                    BankCharges = table.Column<double>(nullable: false),
                    DocumentPresente = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingCreditAdvices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInstructions",
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
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    EMKLId = table.Column<int>(nullable: false),
                    EMKLCode = table.Column<string>(maxLength: 50, nullable: true),
                    EMKLName = table.Column<string>(maxLength: 255, nullable: true),
                    ATTN = table.Column<string>(maxLength: 1000, nullable: true),
                    Fax = table.Column<string>(maxLength: 500, nullable: true),
                    CC = table.Column<string>(maxLength: 500, nullable: true),
                    ShippingStaffId = table.Column<int>(nullable: false),
                    ShippingStaffName = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    ShippedBy = table.Column<string>(maxLength: 20, nullable: true),
                    TruckingDate = table.Column<DateTimeOffset>(nullable: false),
                    CartonNo = table.Column<string>(maxLength: 50, nullable: true),
                    PortOfDischarge = table.Column<string>(maxLength: 255, nullable: true),
                    PlaceOfDelivery = table.Column<string>(maxLength: 255, nullable: true),
                    FeederVessel = table.Column<string>(maxLength: 255, nullable: true),
                    OceanVessel = table.Column<string>(maxLength: 255, nullable: true),
                    Carrier = table.Column<string>(maxLength: 255, nullable: true),
                    Flight = table.Column<string>(maxLength: 255, nullable: true),
                    Transit = table.Column<string>(maxLength: 255, nullable: true),
                    BankAccountId = table.Column<int>(nullable: false),
                    BankAccountName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAgentAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    Notify = table.Column<string>(maxLength: 2000, nullable: true),
                    SpecialInstruction = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInstructions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInvoices",
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
                    PackingListId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceDate = table.Column<DateTimeOffset>(nullable: false),
                    From = table.Column<string>(maxLength: 255, nullable: true),
                    To = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    Consignee = table.Column<string>(maxLength: 255, nullable: true),
                    LCNo = table.Column<string>(maxLength: 100, nullable: true),
                    IssuedBy = table.Column<string>(maxLength: 100, nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    SectionCode = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingPer = table.Column<string>(maxLength: 256, nullable: true),
                    SailingDate = table.Column<DateTimeOffset>(nullable: false),
                    ConfirmationOfOrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    ShippingStaffId = table.Column<int>(nullable: false),
                    ShippingStaff = table.Column<string>(maxLength: 255, nullable: true),
                    FabricTypeId = table.Column<int>(nullable: false),
                    FabricType = table.Column<string>(maxLength: 100, nullable: true),
                    BankAccountId = table.Column<int>(nullable: false),
                    BankAccount = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentDue = table.Column<int>(maxLength: 5, nullable: false),
                    PEBNo = table.Column<string>(maxLength: 50, nullable: true),
                    PEBDate = table.Column<DateTimeOffset>(nullable: false),
                    NPENo = table.Column<string>(maxLength: 50, nullable: true),
                    NPEDate = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    AmountToBePaid = table.Column<decimal>(nullable: false),
                    CPrice = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    BL = table.Column<string>(maxLength: 50, nullable: true),
                    BLDate = table.Column<DateTimeOffset>(nullable: false),
                    CO = table.Column<string>(maxLength: 50, nullable: true),
                    CODate = table.Column<DateTimeOffset>(nullable: false),
                    COTP = table.Column<string>(maxLength: 50, nullable: true),
                    COTPDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLetterOfCredits",
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
                    DocumentCreditNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    IssuedBank = table.Column<string>(maxLength: 200, nullable: true),
                    ApplicantId = table.Column<int>(nullable: false),
                    ApplicantCode = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicantName = table.Column<string>(maxLength: 255, nullable: true),
                    ExpireDate = table.Column<DateTimeOffset>(nullable: false),
                    ExpirePlace = table.Column<string>(maxLength: 255, nullable: true),
                    LatestShipment = table.Column<DateTimeOffset>(nullable: false),
                    LCCondition = table.Column<string>(maxLength: 20, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 50, nullable: true),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLetterOfCredits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalSalesNotes",
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
                    NoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    TransactionTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    TransactionTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    Tempo = table.Column<int>(nullable: false),
                    DispositionNo = table.Column<string>(maxLength: 100, nullable: true),
                    UseVat = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalSalesNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingNotes",
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
                    NoteType = table.Column<int>(nullable: true),
                    NoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreigeProductPackings",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    GreigeProductSKUId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    ProductPackingId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    PackingSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreigeProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreigeProductSKUs",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    WovenType = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 128, nullable: true),
                    Width = table.Column<string>(maxLength: 64, nullable: true),
                    Warp = table.Column<string>(maxLength: 64, nullable: true),
                    Weft = table.Column<string>(maxLength: 64, nullable: true),
                    Grade = table.Column<string>(maxLength: 32, nullable: true),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreigeProductSKUs", x => x.Id);
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
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Code = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPGrades",
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
                    Type = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 16, nullable: true),
                    IsAvalGrade = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPMaterialConstructions",
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
                    Type = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPMaterialConstructions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPProcessType",
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
                    Code = table.Column<string>(maxLength: 128, nullable: true),
                    ProcessType = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPProcessType", x => x.Id);
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
                    Unit = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPUnitOfMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPWarpTypes",
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
                    Type = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPWarpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPWeftTypes",
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
                    Type = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPWeftTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPWidthType",
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
                    Code = table.Column<string>(maxLength: 128, nullable: true),
                    WidthType = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPWidthType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPWovenType",
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
                    Code = table.Column<string>(maxLength: 128, nullable: true),
                    WarpType = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPWovenType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPYarnType",
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
                    Code = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPYarnType", x => x.Id);
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
                name: "ProductPackingInventoryDocuments",
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
                    DocumentNo = table.Column<string>(maxLength: 64, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 64, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    StorageName = table.Column<string>(maxLength: 512, nullable: true),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    InventoryType = table.Column<string>(maxLength: 32, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackingInventoryDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackingInventoryMovements",
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
                    InventoryDocumentId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackingInventoryMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackingInventorySummaries",
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
                    ProductPackingId = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    StorageName = table.Column<string>(maxLength: 512, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackingInventorySummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackings",
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
                    ProductSKUId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false),
                    PackingSize = table.Column<double>(nullable: false),
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 512, nullable: true),
                    CreatedYear = table.Column<int>(nullable: false),
                    CreatedMonth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUInventoryDocuments",
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
                    DocumentNo = table.Column<string>(maxLength: 64, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 64, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    StorageName = table.Column<string>(maxLength: 512, nullable: true),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    InventoryType = table.Column<string>(maxLength: 32, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUInventoryDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUInventoryMovements",
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
                    InventoryDocumentId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUInventoryMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUInventorySummaries",
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
                    ProductSKUId = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    StorageName = table.Column<string>(maxLength: 256, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUInventorySummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUs",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 512, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnProductPackings",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    YarnProductSKUId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    ProductPackingId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    PackingSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnProductSKUs",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    YarnType = table.Column<string>(maxLength: 128, nullable: true),
                    LotNo = table.Column<string>(maxLength: 128, nullable: true),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    ProductSKUId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnProductSKUs", x => x.Id);
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
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(maxLength: 1024, nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 1024, nullable: true),
                    MaterialWidth = table.Column<string>(maxLength: 1024, nullable: true),
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
                    DeliveryOrderSalesId = table.Column<long>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingUnit = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingType = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    BalanceRemains = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InputAvalBonNo = table.Column<string>(maxLength: 64, nullable: true),
                    AvalQuantityKg = table.Column<double>(nullable: false),
                    AvalQuantity = table.Column<double>(nullable: false),
                    DyeingPrintingAreaInputId = table.Column<int>(nullable: false),
                    DyeingPrintingAreaOutputProductionOrderId = table.Column<int>(nullable: false)
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
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(maxLength: 1024, nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 1024, nullable: true),
                    MaterialWidth = table.Column<string>(maxLength: 1024, nullable: true),
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
                name: "GarmentPackingListItems",
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
                    PackingListId = table.Column<int>(nullable: false),
                    RONo = table.Column<string>(maxLength: 50, nullable: true),
                    SCNo = table.Column<string>(maxLength: 50, nullable: true),
                    BuyerBrandId = table.Column<int>(nullable: false),
                    BuyerBrandName = table.Column<string>(maxLength: 50, nullable: true),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 50, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 50, nullable: true),
                    PriceRO = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Valas = table.Column<string>(maxLength: 50, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 50, nullable: true),
                    Article = table.Column<string>(maxLength: 100, nullable: true),
                    OrderNo = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    AVG_GW = table.Column<double>(nullable: false),
                    AVG_NW = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentPackingListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentPackingListItems_GarmentPackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "GarmentPackingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentPackingListMeasurements",
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
                    PackingListId = table.Column<int>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    CartonsQuantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentPackingListMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentPackingListMeasurements_GarmentPackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "GarmentPackingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInvoiceAdjustments",
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
                    GarmentShippingInvoiceId = table.Column<int>(nullable: false),
                    AdjustmentDescription = table.Column<string>(nullable: true),
                    AdjustmentValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInvoiceAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingInvoiceAdjustments_GarmentShippingInvoices_GarmentShippingInvoiceId",
                        column: x => x.GarmentShippingInvoiceId,
                        principalTable: "GarmentShippingInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInvoiceItems",
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
                    RONo = table.Column<string>(maxLength: 10, nullable: true),
                    SCNo = table.Column<string>(maxLength: 256, nullable: true),
                    BuyerBrandId = table.Column<int>(nullable: false),
                    BuyerBrandName = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 5, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 50, nullable: true),
                    ComodityDesc = table.Column<string>(maxLength: 128, nullable: true),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 10, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PriceRO = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 10, nullable: true),
                    CMTPrice = table.Column<decimal>(nullable: false),
                    GarmentShippingInvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingInvoiceItems_GarmentShippingInvoices_GarmentShippingInvoiceId",
                        column: x => x.GarmentShippingInvoiceId,
                        principalTable: "GarmentShippingInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalSalesNoteItems",
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
                    LocalSalesNoteId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalSalesNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalSalesNoteItems_GarmentShippingLocalSalesNotes_LocalSalesNoteId",
                        column: x => x.LocalSalesNoteId,
                        principalTable: "GarmentShippingLocalSalesNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingNoteItems",
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
                    ShippingNoteId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 100, nullable: true),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingNoteItems_GarmentShippingNotes_ShippingNoteId",
                        column: x => x.ShippingNoteId,
                        principalTable: "GarmentShippingNotes",
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
                name: "GarmentPackingListDetails",
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
                    PackingListItemId = table.Column<int>(nullable: false),
                    Carton1 = table.Column<double>(nullable: false),
                    Carton2 = table.Column<double>(nullable: false),
                    Colour = table.Column<string>(maxLength: 100, nullable: true),
                    CartonQuantity = table.Column<double>(nullable: false),
                    QuantityPCS = table.Column<double>(nullable: false),
                    TotalQuantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentPackingListDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentPackingListDetails_GarmentPackingListItems_PackingListItemId",
                        column: x => x.PackingListItemId,
                        principalTable: "GarmentPackingListItems",
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

            migrationBuilder.CreateTable(
                name: "GarmentPackingListDetailSizes",
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
                    PackingListDetailId = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentPackingListDetailSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentPackingListDetailSizes_GarmentPackingListDetails_PackingListDetailId",
                        column: x => x.PackingListDetailId,
                        principalTable: "GarmentPackingListDetails",
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
                name: "IX_GarmentPackingListDetails_PackingListItemId",
                table: "GarmentPackingListDetails",
                column: "PackingListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentPackingListDetailSizes_PackingListDetailId",
                table: "GarmentPackingListDetailSizes",
                column: "PackingListDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentPackingListItems_PackingListId",
                table: "GarmentPackingListItems",
                column: "PackingListId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentPackingListMeasurements_PackingListId",
                table: "GarmentPackingListMeasurements",
                column: "PackingListId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentPackingLists_InvoiceNo",
                table: "GarmentPackingLists",
                column: "InvoiceNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInvoiceAdjustments_GarmentShippingInvoiceId",
                table: "GarmentShippingInvoiceAdjustments",
                column: "GarmentShippingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInvoiceItems_GarmentShippingInvoiceId",
                table: "GarmentShippingInvoiceItems",
                column: "GarmentShippingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLetterOfCredits_DocumentCreditNo",
                table: "GarmentShippingLetterOfCredits",
                column: "DocumentCreditNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalSalesNoteItems_LocalSalesNoteId",
                table: "GarmentShippingLocalSalesNoteItems",
                column: "LocalSalesNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalSalesNotes_NoteNo",
                table: "GarmentShippingLocalSalesNotes",
                column: "NoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingNoteItems_ShippingNoteId",
                table: "GarmentShippingNoteItems",
                column: "ShippingNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingNotes_NoteNo",
                table: "GarmentShippingNotes",
                column: "NoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

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
                name: "FabricProductPackings");

            migrationBuilder.DropTable(
                name: "FabricProductSKUs");

            migrationBuilder.DropTable(
                name: "GarmentPackingListDetailSizes");

            migrationBuilder.DropTable(
                name: "GarmentPackingListMeasurements");

            migrationBuilder.DropTable(
                name: "GarmentShippingAmendLetterOfCredits");

            migrationBuilder.DropTable(
                name: "GarmentShippingCoverLetters");

            migrationBuilder.DropTable(
                name: "GarmentShippingCreditAdvices");

            migrationBuilder.DropTable(
                name: "GarmentShippingInstructions");

            migrationBuilder.DropTable(
                name: "GarmentShippingInvoiceAdjustments");

            migrationBuilder.DropTable(
                name: "GarmentShippingInvoiceItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingLetterOfCredits");

            migrationBuilder.DropTable(
                name: "GarmentShippingLocalSalesNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingNoteItems");

            migrationBuilder.DropTable(
                name: "GreigeProductPackings");

            migrationBuilder.DropTable(
                name: "GreigeProductSKUs");

            migrationBuilder.DropTable(
                name: "IPCategories");

            migrationBuilder.DropTable(
                name: "IPGrades");

            migrationBuilder.DropTable(
                name: "IPMaterialConstructions");

            migrationBuilder.DropTable(
                name: "IPProcessType");

            migrationBuilder.DropTable(
                name: "IPUnitOfMeasurements");

            migrationBuilder.DropTable(
                name: "IPWarpTypes");

            migrationBuilder.DropTable(
                name: "IPWeftTypes");

            migrationBuilder.DropTable(
                name: "IPWidthType");

            migrationBuilder.DropTable(
                name: "IPWovenType");

            migrationBuilder.DropTable(
                name: "IPYarnType");

            migrationBuilder.DropTable(
                name: "NewCriterias");

            migrationBuilder.DropTable(
                name: "ProductPackingInventoryDocuments");

            migrationBuilder.DropTable(
                name: "ProductPackingInventoryMovements");

            migrationBuilder.DropTable(
                name: "ProductPackingInventorySummaries");

            migrationBuilder.DropTable(
                name: "ProductPackings");

            migrationBuilder.DropTable(
                name: "ProductSKUInventoryDocuments");

            migrationBuilder.DropTable(
                name: "ProductSKUInventoryMovements");

            migrationBuilder.DropTable(
                name: "ProductSKUInventorySummaries");

            migrationBuilder.DropTable(
                name: "ProductSKUs");

            migrationBuilder.DropTable(
                name: "YarnProductPackings");

            migrationBuilder.DropTable(
                name: "YarnProductSKUs");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaInputs");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropTable(
                name: "GarmentPackingListDetails");

            migrationBuilder.DropTable(
                name: "GarmentShippingInvoices");

            migrationBuilder.DropTable(
                name: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropTable(
                name: "GarmentShippingNotes");

            migrationBuilder.DropTable(
                name: "NewFabricGradeTests");

            migrationBuilder.DropTable(
                name: "GarmentPackingListItems");

            migrationBuilder.DropTable(
                name: "NewFabricQualityControls");

            migrationBuilder.DropTable(
                name: "GarmentPackingLists");
        }
    }
}
