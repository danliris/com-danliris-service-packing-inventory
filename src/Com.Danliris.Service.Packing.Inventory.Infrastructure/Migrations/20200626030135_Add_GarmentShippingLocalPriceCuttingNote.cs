using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_GarmentShippingLocalPriceCuttingNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalPriceCuttingNotes",
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
                    CuttingPriceNoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    UseVat = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalPriceCuttingNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalPriceCuttingNoteItems",
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
                    PriceCuttingNoteId = table.Column<int>(nullable: false),
                    SalesNoteId = table.Column<int>(nullable: false),
                    SalesNoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    SalesAmount = table.Column<double>(nullable: false),
                    CuttingAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalPriceCuttingNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalPriceCuttingNoteItems_GarmentShippingLocalPriceCuttingNotes_PriceCuttingNoteId",
                        column: x => x.PriceCuttingNoteId,
                        principalTable: "GarmentShippingLocalPriceCuttingNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalPriceCuttingNoteItems_PriceCuttingNoteId",
                table: "GarmentShippingLocalPriceCuttingNoteItems",
                column: "PriceCuttingNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalPriceCuttingNotes_CuttingPriceNoteNo",
                table: "GarmentShippingLocalPriceCuttingNotes",
                column: "CuttingPriceNoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingLocalPriceCuttingNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingLocalPriceCuttingNotes");
        }
    }
}
