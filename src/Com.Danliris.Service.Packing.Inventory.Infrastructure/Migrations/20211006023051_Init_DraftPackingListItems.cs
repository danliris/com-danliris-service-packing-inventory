using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Init_DraftPackingListItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentDraftPackingListItems",
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
                    PriceFOB = table.Column<double>(nullable: false),
                    PriceCMT = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Valas = table.Column<string>(maxLength: 50, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 50, nullable: true),
                    Article = table.Column<string>(maxLength: 100, nullable: true),
                    OrderNo = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    DescriptionMd = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentDraftPackingListItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentDraftPackingListDetails",
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
                    Style = table.Column<string>(maxLength: 100, nullable: true),
                    Colour = table.Column<string>(maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_GarmentDraftPackingListDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentDraftPackingListDetails_GarmentDraftPackingListItems_PackingListItemId",
                        column: x => x.PackingListItemId,
                        principalTable: "GarmentDraftPackingListItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentDraftPackingListDetailSizes",
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
                    table.PrimaryKey("PK_GarmentDraftPackingListDetailSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentDraftPackingListDetailSizes_GarmentDraftPackingListDetails_PackingListDetailId",
                        column: x => x.PackingListDetailId,
                        principalTable: "GarmentDraftPackingListDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentDraftPackingListDetails_PackingListItemId",
                table: "GarmentDraftPackingListDetails",
                column: "PackingListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentDraftPackingListDetailSizes_PackingListDetailId",
                table: "GarmentDraftPackingListDetailSizes",
                column: "PackingListDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentDraftPackingListDetailSizes");

            migrationBuilder.DropTable(
                name: "GarmentDraftPackingListDetails");

            migrationBuilder.DropTable(
                name: "GarmentDraftPackingListItems");
        }
    }
}
