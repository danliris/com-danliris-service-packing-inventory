using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddTableGarmentReceiptSubconLocalSalesDOs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDO",
                table: "GarmentReceiptSubconLocalSalesNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconLocalSalesDOs",
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
                    LocalSalesDONo = table.Column<string>(maxLength: 50, nullable: true),
                    LocalSalesNoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    LocalSalesNoteId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    To = table.Column<string>(maxLength: 255, nullable: true),
                    StorageDivision = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentReceiptSubconLocalSalesDOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconLocalSalesDOItems",
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
                    LocalSalesDOId = table.Column<int>(nullable: false),
                    LocalSalesNoteItemId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 100, nullable: true),
                    PackQuantity = table.Column<double>(nullable: false),
                    PackUomId = table.Column<int>(nullable: false),
                    PackUomUnit = table.Column<string>(maxLength: 100, nullable: true),
                    GrossWeight = table.Column<double>(nullable: false),
                    NettWeight = table.Column<double>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentReceiptSubconLocalSalesDOItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentReceiptSubconLocalSalesDOItems_GarmentReceiptSubconLocalSalesDOs_LocalSalesDOId",
                        column: x => x.LocalSalesDOId,
                        principalTable: "GarmentReceiptSubconLocalSalesDOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentReceiptSubconLocalSalesDOItems_LocalSalesDOId",
                table: "GarmentReceiptSubconLocalSalesDOItems",
                column: "LocalSalesDOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconLocalSalesDOItems");

            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconLocalSalesDOs");

            migrationBuilder.DropColumn(
                name: "IsDO",
                table: "GarmentReceiptSubconLocalSalesNotes");
        }
    }
}
