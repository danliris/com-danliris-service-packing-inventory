using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_GarmentPackingList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Remark = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentPackingLists", x => x.Id);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentPackingListDetailSizes");

            migrationBuilder.DropTable(
                name: "GarmentPackingListMeasurements");

            migrationBuilder.DropTable(
                name: "GarmentPackingListDetails");

            migrationBuilder.DropTable(
                name: "GarmentPackingListItems");

            migrationBuilder.DropTable(
                name: "GarmentPackingLists");
        }
    }
}
