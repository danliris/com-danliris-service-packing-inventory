using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class CreateNewGarmentPackkingList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PLType",
                table: "GarmentShippingInvoices",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpenditureGoodNo",
                table: "GarmentShippingInvoiceItems",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GarmentShippingPackingLists",
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
                    PaymentTerm = table.Column<string>(maxLength: 25, nullable: true),
                    LCNo = table.Column<string>(maxLength: 100, nullable: true),
                    LCDate = table.Column<DateTimeOffset>(nullable: false),
                    IssuedBy = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    Destination = table.Column<string>(maxLength: 50, nullable: true),
                    FinalDestination = table.Column<string>(maxLength: 50, nullable: true),
                    ShipmentMode = table.Column<string>(nullable: true),
                    TruckingDate = table.Column<DateTimeOffset>(nullable: false),
                    TruckingEstimationDate = table.Column<DateTimeOffset>(nullable: false),
                    ExportEstimationDate = table.Column<DateTimeOffset>(nullable: false),
                    Omzet = table.Column<bool>(nullable: false),
                    Accounting = table.Column<bool>(nullable: false),
                    FabricCountryOrigin = table.Column<string>(maxLength: 255, nullable: true),
                    FabricComposition = table.Column<string>(maxLength: 255, nullable: true),
                    RemarkMd = table.Column<string>(maxLength: 2000, nullable: true),
                    SampleRemarkMd = table.Column<string>(maxLength: 2000, nullable: true),
                    ShippingStaffId = table.Column<int>(nullable: false),
                    ShippingStaffName = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    GrossWeight = table.Column<double>(nullable: false),
                    NettWeight = table.Column<double>(nullable: false),
                    NetNetWeight = table.Column<double>(nullable: false),
                    TotalCartons = table.Column<double>(nullable: false),
                    SayUnit = table.Column<string>(maxLength: 50, nullable: true),
                    OtherCommodity = table.Column<string>(maxLength: 2000, nullable: true),
                    ShippingMark = table.Column<string>(maxLength: 2000, nullable: true),
                    SideMark = table.Column<string>(maxLength: 2000, nullable: true),
                    Remark = table.Column<string>(maxLength: 2000, nullable: true),
                    ShippingMarkImagePath = table.Column<string>(maxLength: 500, nullable: true),
                    SideMarkImagePath = table.Column<string>(maxLength: 500, nullable: true),
                    RemarkImagePath = table.Column<string>(maxLength: 500, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    IsPosted = table.Column<bool>(nullable: false),
                    IsCostStructured = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    IsShipping = table.Column<bool>(nullable: false),
                    IsSampleDelivered = table.Column<bool>(nullable: false),
                    IsSampleExpenditureGood = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPackingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPackingListItems",
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
                    ExpenditureGoodNo = table.Column<string>(maxLength: 50, nullable: true),
                    RONo = table.Column<string>(maxLength: 50, nullable: true),
                    SCNo = table.Column<string>(maxLength: 50, nullable: true),
                    BuyerBrandId = table.Column<int>(nullable: false),
                    BuyerBrandName = table.Column<string>(maxLength: 50, nullable: true),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 50, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    MarketingName = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 50, nullable: true),
                    PriceRO = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PriceFOB = table.Column<double>(nullable: false),
                    PriceCMT = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Valas = table.Column<string>(maxLength: 50, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 50, nullable: true),
                    Article = table.Column<string>(maxLength: 1000, nullable: true),
                    OrderNo = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    DescriptionMd = table.Column<string>(maxLength: 1000, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    RoType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPackingListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPackingListItems_GarmentShippingPackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "GarmentShippingPackingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPackingListMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
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
                    CartonsQuantity = table.Column<double>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPackingListMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPackingListMeasurements_GarmentShippingPackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "GarmentShippingPackingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPackingListStatusActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackingListId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPackingListStatusActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPackingListStatusActivities_GarmentShippingPackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "GarmentShippingPackingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPackingListDetails",
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
                    PackingListItemId = table.Column<int>(nullable: false),
                    Carton1 = table.Column<double>(nullable: false),
                    Carton2 = table.Column<double>(nullable: false),
                    Style = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    CartonQuantity = table.Column<double>(nullable: false),
                    QuantityPCS = table.Column<double>(nullable: false),
                    TotalQuantity = table.Column<double>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    GrossWeight = table.Column<double>(nullable: false),
                    NetWeight = table.Column<double>(nullable: false),
                    NetNetWeight = table.Column<double>(nullable: false),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPackingListDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPackingListDetails_GarmentShippingPackingListItems_PackingListItemId",
                        column: x => x.PackingListItemId,
                        principalTable: "GarmentShippingPackingListItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPackingListDetailSizes",
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
                    SizeIdx = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    GarmentShippingPackingListDetailModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPackingListDetailSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPackingListDetailSizes_GarmentShippingPackingListDetails_GarmentShippingPackingListDetailModelId",
                        column: x => x.GarmentShippingPackingListDetailModelId,
                        principalTable: "GarmentShippingPackingListDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPackingListDetails_PackingListItemId",
                table: "GarmentShippingPackingListDetails",
                column: "PackingListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPackingListDetailSizes_GarmentShippingPackingListDetailModelId",
                table: "GarmentShippingPackingListDetailSizes",
                column: "GarmentShippingPackingListDetailModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPackingListItems_PackingListId",
                table: "GarmentShippingPackingListItems",
                column: "PackingListId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPackingListMeasurements_PackingListId",
                table: "GarmentShippingPackingListMeasurements",
                column: "PackingListId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPackingLists_InvoiceNo",
                table: "GarmentShippingPackingLists",
                column: "InvoiceNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPackingListStatusActivities_PackingListId",
                table: "GarmentShippingPackingListStatusActivities",
                column: "PackingListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingPackingListDetailSizes");

            migrationBuilder.DropTable(
                name: "GarmentShippingPackingListMeasurements");

            migrationBuilder.DropTable(
                name: "GarmentShippingPackingListStatusActivities");

            migrationBuilder.DropTable(
                name: "GarmentShippingPackingListDetails");

            migrationBuilder.DropTable(
                name: "GarmentShippingPackingListItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingPackingLists");

            migrationBuilder.DropColumn(
                name: "PLType",
                table: "GarmentShippingInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "ExpenditureGoodNo",
                table: "GarmentShippingInvoiceItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
