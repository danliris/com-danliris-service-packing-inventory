using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addtableGReceiptSubconPackingList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconPackingLists",
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
                    LocalSalesNoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    LocalSalesNoteDate = table.Column<DateTimeOffset>(nullable: false),
                    LocalSalesContractId = table.Column<int>(nullable: false),
                    LocalSalesContractNo = table.Column<string>(maxLength: 50, nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    TransactionTypeCode = table.Column<string>(maxLength: 25, nullable: true),
                    TransactionTypeName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentTerm = table.Column<string>(maxLength: 25, nullable: true),
                    Omzet = table.Column<bool>(nullable: false),
                    Accounting = table.Column<bool>(nullable: false),
                    GrossWeight = table.Column<double>(nullable: false),
                    NettWeight = table.Column<double>(nullable: false),
                    NetNetWeight = table.Column<double>(nullable: false),
                    TotalCartons = table.Column<double>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentReceiptSubconPackingLists", x => x.Id);
                });

         
            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconPackingListItems",
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
                    PackingOutNo = table.Column<string>(maxLength: 50, nullable: true),
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
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentReceiptSubconPackingListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentReceiptSubconPackingListItems_GarmentReceiptSubconPackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "GarmentReceiptSubconPackingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconPackingListDetails",
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
                    table.PrimaryKey("PK_GarmentReceiptSubconPackingListDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentReceiptSubconPackingListDetails_GarmentReceiptSubconPackingListItems_PackingListItemId",
                        column: x => x.PackingListItemId,
                        principalTable: "GarmentReceiptSubconPackingListItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconPackingListDetailSizes",
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
                    PackingOutItemId = table.Column<Guid>(nullable: false),
                    SizeId = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 100, nullable: true),
                    SizeIdx = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Color = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentReceiptSubconPackingListDetailSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentReceiptSubconPackingListDetailSizes_GarmentReceiptSubconPackingListDetails_PackingListDetailId",
                        column: x => x.PackingListDetailId,
                        principalTable: "GarmentReceiptSubconPackingListDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_GarmentReceiptSubconPackingListDetails_PackingListItemId",
                table: "GarmentReceiptSubconPackingListDetails",
                column: "PackingListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentReceiptSubconPackingListDetailSizes_PackingListDetailId",
                table: "GarmentReceiptSubconPackingListDetailSizes",
                column: "PackingListDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentReceiptSubconPackingListItems_PackingListId",
                table: "GarmentReceiptSubconPackingListItems",
                column: "PackingListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconPackingListDetailSizes");

            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconPackingListDetails");

            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconPackingListItems");

            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconPackingLists");

        }
    }
}
